using System;
using System.Reflection;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Data;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class FlightDataHubTests
    {
        private const string HubTypeName = "FlightSim.Platform.Data.FlightDataHub, FlightSim.Data";

        [Test]
        public void HubPublishesCanonicalSnapshotsAndAllTelemetryDomains()
        {
            object hub = CreateHub();
            AircraftId aircraft = new AircraftId("VIPER-01");
            PublishAircraftTelemetry(hub, aircraft);
            PublishMissionTelemetry(hub, aircraft);

            object aircraftSnapshot = ReadAircraft(hub, aircraft);
            Assert.That(GetField(aircraftSnapshot, "Identity"), Is.Not.EqualTo(null));
            Assert.That(((AircraftFastState)GetField(aircraftSnapshot, "Fast")).Tick, Is.EqualTo(42UL));
            Assert.That(((AircraftSystemsState)GetField(aircraftSnapshot, "Systems")).Aircraft, Is.EqualTo(aircraft));
            Assert.That(((TacticalPictureState)GetField(aircraftSnapshot, "Tactical")).Aircraft, Is.EqualTo(aircraft));
            Assert.That(((AircraftCombatState)GetField(aircraftSnapshot, "Combat")).Aircraft, Is.EqualTo(aircraft));
            Assert.That((bool)GetField(aircraftSnapshot, "IsValid"), Is.True);

            object mission = ReadMission(hub);
            Assert.That(((MissionState)GetField(mission, "Mission")).MissionId, Is.EqualTo("TEST-MISSION"));
            Assert.That(((MissionActorState[])GetField(mission, "Actors")).Length, Is.EqualTo(1));
            Assert.That(((MissionObjectiveState[])GetField(mission, "Objectives")).Length, Is.EqualTo(1));
            Assert.That(((AutomationRunState)GetField(mission, "Automation")).RunId, Is.EqualTo("run-001"));

            InvokeSink(hub, "OnSimulationEvent", new SimulationEvent
            {
                Aircraft = aircraft,
                SimulationTimeS = 1.0,
                Type = SimulationEventType.EngineStarted,
                Code = 1,
                Message = "engine"
            });
            InvokeSink(hub, "OnMissionEvent", new MissionEvent
            {
                MissionId = "TEST-MISSION",
                Source = aircraft,
                SimulationTimeS = 2.0,
                Type = MissionEventType.Started,
                Code = 2,
                Message = "mission"
            });
            InvokeSink(hub, "OnWeaponEngagementEvent", new WeaponEngagementEvent
            {
                RunId = "run-001",
                Shooter = aircraft,
                SimulationTimeS = 3.0,
                Outcome = WeaponEngagementOutcome.Hit,
                Reason = "engagement"
            });

            ulong sequence = 0UL;
            FlightDataDomain[] expectedDomains =
            {
                FlightDataDomain.Simulation,
                FlightDataDomain.Mission,
                FlightDataDomain.Engagement
            };
            for (int index = 0; index < 3; index++)
            {
                object dataEvent = ReadEvent(hub, sequence);
                ulong next = (ulong)GetField(dataEvent, "Sequence");
                Assert.That(next, Is.GreaterThan(sequence));
                Assert.That(GetField(dataEvent, "Domain"), Is.EqualTo(expectedDomains[index]));
                sequence = next;
            }
        }

        [Test]
        public void HubBoundsAircraftAndEventStorage()
        {
            object hub = CreateHub(2);
            for (int index = 1; index <= 8; index++)
            {
                AircraftId aircraft = new AircraftId($"VIPER-{index:00}");
                InvokeSink(hub, "OnFastState", new AircraftFastState { Aircraft = aircraft, Tick = (ulong)index });
            }
            InvokeSink(hub, "OnFastState", new AircraftFastState { Aircraft = new AircraftId("VIPER-09"), Tick = 9UL });

            Assert.That((int)GetProperty(hub, "AircraftCount"), Is.EqualTo(8));
            object health = Invoke(hub, "GetHealth");
            Assert.That((int)GetField(health, "RejectedAircraftCount"), Is.EqualTo(1));

            for (int index = 1; index <= 3; index++)
            {
                InvokeSink(hub, "OnSimulationEvent", new SimulationEvent
                {
                    Aircraft = new AircraftId("VIPER-01"),
                    Code = index,
                    Message = index.ToString()
                });
            }

            object firstRetained = ReadEvent(hub, 0UL);
            Assert.That((ulong)GetField(firstRetained, "Sequence"), Is.EqualTo(2UL));
            object secondRetained = ReadEvent(hub, 2UL);
            Assert.That((ulong)GetField(secondRetained, "Sequence"), Is.EqualTo(3UL));
            Assert.That(TryReadEvent(hub, 3UL, out _), Is.False);
        }

        [Test]
        public void HubReturnsCopiesOfMissionArrays()
        {
            object hub = CreateHub();
            AircraftId aircraft = new AircraftId("VIPER-01");
            PublishMissionTelemetry(hub, aircraft);

            object first = ReadMission(hub);
            MissionActorState[] firstActors = (MissionActorState[])GetField(first, "Actors");
            MissionObjectiveState[] firstObjectives = (MissionObjectiveState[])GetField(first, "Objectives");
            firstActors[0].Callsign = "MUTATED";
            firstObjectives[0].ObjectiveId = "MUTATED";

            object second = ReadMission(hub);
            MissionActorState[] secondActors = (MissionActorState[])GetField(second, "Actors");
            MissionObjectiveState[] secondObjectives = (MissionObjectiveState[])GetField(second, "Objectives");
            Assert.That(secondActors, Is.Not.SameAs(firstActors));
            Assert.That(secondActors[0].Callsign, Is.EqualTo("VIPER-01"));
            Assert.That(secondObjectives, Is.Not.SameAs(firstObjectives));
            Assert.That(secondObjectives[0].ObjectiveId, Is.EqualTo("objective-1"));
        }

        [Test]
        public void HubExposesOnlyTheSpecifiedReadOnlyContract()
        {
            Type hubType = HubType;
            Type contractType = Type.GetType("FlightSim.Platform.Data.IFlightDataHub, FlightSim.Data");

            Assert.That(contractType, Is.Not.Null);
            Assert.That(contractType.IsAssignableFrom(hubType), Is.True);
            object hub = CreateHub();
            Assert.That(GetProperty(hub, "ContractVersion"), Is.EqualTo(FlightSimulationContract.ContractVersion));
            Assert.That(GetProperty(hub, "MissionSchemaVersion"), Is.EqualTo((ushort)1));
            Assert.That(hubType.GetMethod("TryGetAircraftId"), Is.Not.Null);
            Assert.That(hubType.GetMethod("TryGetAircraft"), Is.Not.Null);
            Assert.That(hubType.GetMethod("TryGetMission"), Is.Not.Null);
            Assert.That(hubType.GetMethod("TryGetCombat"), Is.Not.Null);
            Assert.That(hubType.GetMethod("GetHealth"), Is.Not.Null);
            Assert.That(hubType.GetMethod("TryReadEvent"), Is.Not.Null);
            Assert.That(hubType.GetMethod("Submit"), Is.Null);
        }

        private static Type HubType
        {
            get
            {
                Type type = Type.GetType(HubTypeName);
                Assert.That(type, Is.Not.Null, "FlightSim.Data must expose FlightDataHub.");
                return type;
            }
        }

        private static object CreateHub(params object[] arguments)
        {
            return Activator.CreateInstance(HubType, arguments);
        }

        private static void PublishAircraftTelemetry(object hub, AircraftId aircraft)
        {
            InvokeSink(hub, "OnFastState", new AircraftFastState
            {
                Aircraft = aircraft,
                Tick = 42UL,
                SimulationTimeS = 12.5,
                TrueAirspeedMps = 250.0
            });
            InvokeSink(hub, "OnSystemsState", new AircraftSystemsState
            {
                Aircraft = aircraft,
                SimulationTimeS = 12.5
            });
            InvokeSink(hub, "OnTacticalPictureState", TacticalPictureState.CreateDefault(aircraft));
        }

        private static void PublishMissionTelemetry(object hub, AircraftId aircraft)
        {
            InvokeSink(hub, "OnMissionState", new MissionState
            {
                MissionId = "TEST-MISSION",
                RunId = "run-001",
                SchemaVersion = 1,
                IsValid = true
            });
            InvokeSink(hub, "OnMissionActorState", new MissionActorState
            {
                Aircraft = aircraft,
                Callsign = "VIPER-01",
                Side = AircraftSide.Friendly,
                Role = AircraftRole.Player,
                IsValid = true
            });
            InvokeSink(hub, "OnMissionObjectiveState", new MissionObjectiveState
            {
                ObjectiveId = "objective-1",
                Status = MissionObjectiveStatus.Active
            });
            InvokeSink(hub, "OnAircraftCombatState", new AircraftCombatState
            {
                Aircraft = aircraft,
                IsValid = true
            });
            InvokeSink(hub, "OnAutomationRunState", new AutomationRunState
            {
                RunId = "run-001",
                MissionId = "TEST-MISSION",
                Status = AutomationRunStatus.Running
            });
        }

        private static object ReadAircraft(object hub, AircraftId aircraft)
        {
            MethodInfo method = hub.GetType().GetMethod("TryGetAircraft");
            object[] arguments = { aircraft, null };
            Assert.That((bool)method.Invoke(hub, arguments), Is.True);
            return arguments[1];
        }

        private static object ReadMission(object hub)
        {
            MethodInfo method = hub.GetType().GetMethod("TryGetMission");
            object[] arguments = { null };
            Assert.That((bool)method.Invoke(hub, arguments), Is.True);
            return arguments[0];
        }

        private static object ReadEvent(object hub, ulong afterSequence)
        {
            Assert.That(TryReadEvent(hub, afterSequence, out object dataEvent), Is.True);
            return dataEvent;
        }

        private static bool TryReadEvent(object hub, ulong afterSequence, out object dataEvent)
        {
            MethodInfo method = hub.GetType().GetMethod("TryReadEvent");
            object[] arguments = { afterSequence, null };
            bool found = (bool)method.Invoke(hub, arguments);
            dataEvent = arguments[1];
            return found;
        }

        private static void InvokeSink(object hub, string methodName, object payload)
        {
            MethodInfo method = hub.GetType().GetMethod(methodName);
            Assert.That(method, Is.Not.Null, $"Missing sink callback {methodName}.");
            method.Invoke(hub, new[] { payload });
        }

        private static object Invoke(object target, string methodName)
        {
            MethodInfo method = target.GetType().GetMethod(methodName);
            Assert.That(method, Is.Not.Null, $"Missing method {methodName}.");
            return method.Invoke(target, null);
        }

        private static object GetProperty(object target, string propertyName)
        {
            PropertyInfo property = target.GetType().GetProperty(propertyName);
            Assert.That(property, Is.Not.Null, $"Missing property {propertyName}.");
            return property.GetValue(target);
        }

        private static object GetField(object target, string fieldName)
        {
            FieldInfo field = target.GetType().GetField(fieldName);
            Assert.That(field, Is.Not.Null, $"Missing field {fieldName}.");
            return field.GetValue(target);
        }
    }
}
