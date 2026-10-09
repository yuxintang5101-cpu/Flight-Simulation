using System;
using System.IO;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Unity.Controls;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    public enum FlightScreen { Home, FreeFlight, Missions, Controls, Flying, Paused, Debrief }

    [DisallowMultipleComponent]
    public sealed class FlightSessionController : MonoBehaviour
    {
        public FlightSimulationHost Host { get; private set; }
        public PilotInputRouter Input { get; private set; }
        public FlightMissionCatalog Catalog { get; private set; }
        public FlightScreen Screen { get; private set; } = FlightScreen.Home;
        public bool IsFreeFlight { get; private set; }
        public int SessionGeneration { get; private set; }
        public string CurrentTitle { get; private set; } = "FLIGHTSIM";
        public string LastError { get; private set; } = "";
        public string ReportPath { get; private set; } = "";
        public FlightSessionReport LastReport { get; private set; }
        public StartupPreset FreePreset { get; set; } = StartupPreset.RunwayReady;
        public event Action ScreenChanged;
        public event Action DisplayRequested;
        public void RequestDisplay() { if(Screen == FlightScreen.Flying) DisplayRequested?.Invoke(); }
        public Func<bool> DisplayEscapeHandler { get; set; }
        private FlightScreen controlsReturn = FlightScreen.Home;
        private string startUtc;
        private bool activeSession, reportSaved;
        private MissionDefinition selectedMission;
        private bool selectedAutomated;

        public void Initialize(FlightSimulationHost host)
        {
            Host = host;
            Input = host.GetComponent<PilotInputRouter>() ?? host.gameObject.AddComponent<PilotInputRouter>();
            host.SetInputRouter(Input);
            Catalog = FlightMissionCatalog.Load(host.ConfiguredMissions);
            host.MissionStateUpdated += OnMissionState;
            host.ResetRequested += Restart;
            Show(FlightScreen.Home);
        }
        private void OnDestroy()
        {
            if (activeSession && !reportSaved && Host != null && Input != null) SaveReport("Ended", "退出运行模式");
            if (Host != null) { Host.MissionStateUpdated -= OnMissionState; Host.ResetRequested -= Restart; }
        }
        private void Update()
        {
            if (Host == null) return;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (Input.IsListening || Input.IsCalibrating) { Input.CancelLearning(); return; }
                if(Screen == FlightScreen.Flying && DisplayEscapeHandler != null && DisplayEscapeHandler()) return;
                if (Screen == FlightScreen.Flying) Show(FlightScreen.Paused);
                else if (Screen == FlightScreen.Paused) Show(FlightScreen.Flying);
                else if (Screen == FlightScreen.Controls) Show(controlsReturn);
                else if (Screen != FlightScreen.Home) Home();
            }
        }
        public void Show(FlightScreen next)
        {
            Input?.CancelLearning();
            Screen = next;
            if (Host != null) Host.SetSessionPaused(next != FlightScreen.Flying);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ScreenChanged?.Invoke();
        }
        public void Home()
        {
            if (activeSession && !reportSaved) SaveReport("Ended", "返回主菜单");
            activeSession = false;
            LastError = "";
            Show(FlightScreen.Home);
        }
        public void OpenControls() { controlsReturn = Screen == FlightScreen.Flying ? FlightScreen.Paused : Screen; Show(FlightScreen.Controls); }
        public void CloseControls() => Show(controlsReturn);
        public void ReloadMissions() { Catalog = FlightMissionCatalog.Load(Host.ConfiguredMissions); ScreenChanged?.Invoke(); }
        public bool BeginFreeFlight()
        {
            var result = Host.StartFreeFlight(FreePreset);
            if (!result.Accepted) return Fail(result.Message);
            IsFreeFlight = true; selectedMission = null; selectedAutomated = false;
            SessionGeneration++; BeginSession("自由飞行 · KTEX");
            return true;
        }
        public bool BeginMission(MissionDefinition mission, bool automated)
        {
            if (mission == null) return Fail("请选择任务。");
            // Always load a fresh instance. Completed missions can be restarted from any screen.
            var loaded = Host.LoadMissionDefinition(mission);
            if (!loaded.Accepted) return Fail(loaded.Message);
            var started = Host.StartMission(automated);
            if (!started.Accepted) return Fail(started.Message);
            IsFreeFlight = false; selectedMission = mission; selectedAutomated = automated;
            SessionGeneration++; BeginSession(mission.DisplayName);
            return true;
        }
        public void Restart()
        {
            if (activeSession && !reportSaved) SaveReport("Restarted", "重新开始");
            if (selectedMission != null) BeginMission(selectedMission, selectedAutomated); else BeginFreeFlight();
        }
        public void EndFlight()
        {
            if (activeSession && !reportSaved) SaveReport("Ended", "用户结束飞行");
            Show(FlightScreen.Debrief);
        }
        private void BeginSession(string title)
        {
            CurrentTitle = title; LastError = ""; ReportPath = ""; LastReport = null;
            startUtc = DateTime.UtcNow.ToString("o"); activeSession = true; reportSaved = false;
            Show(FlightScreen.Flying);
        }
        private bool Fail(string error) { LastError = error; ScreenChanged?.Invoke(); return false; }
        private void OnMissionState(MissionState state)
        {
            if (!activeSession || IsFreeFlight || reportSaved) return;
            if (state.Phase != MissionPhase.Succeeded && state.Phase != MissionPhase.Failed && state.Phase != MissionPhase.Aborted) return;
            SaveReport(state.Phase.ToString(), state.CompletionReason);
            Show(FlightScreen.Debrief);
        }
        private void SaveReport(string result, string reason)
        {
            reportSaved = true;
            var snapshot = Host.LatestSnapshot;
            LastReport = new FlightSessionReport {
                SchemaVersion = 1, MissionId = IsFreeFlight ? "FREE_FLIGHT" : selectedMission?.MissionId ?? "", Title = CurrentTitle,
                StartedUtc = startUtc, EndedUtc = DateTime.UtcNow.ToString("o"), Result = result, Reason = reason,
                SimulationTimeS = snapshot.Fast.SimulationTimeS, AltitudeM = snapshot.Fast.MeanSeaLevelAltitudeM,
                SpeedMps = snapshot.Fast.TrueAirspeedMps, TerrainValid = snapshot.Fast.TerrainSampleValid,
                InputMode = Input.Profile.Mode.ToString(), Automated = Host.IsPlayerAutomationEnabled
            };
            try
            {
                string directory = Path.Combine(Application.persistentDataPath, "FlightSim", "Reports");
                Directory.CreateDirectory(directory);
                ReportPath = Path.Combine(directory, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + "-" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".json");
                File.WriteAllText(ReportPath, JsonUtility.ToJson(LastReport, true));
            }
            catch (Exception e) { LastError = "复盘报告未保存：" + e.Message; }
        }
    }

    [Serializable]
    public sealed class FlightSessionReport
    {
        public int SchemaVersion;
        public string MissionId, Title, StartedUtc, EndedUtc, Result, Reason, InputMode;
        public double SimulationTimeS, AltitudeM, SpeedMps;
        public bool TerrainValid, Automated;
    }
}
