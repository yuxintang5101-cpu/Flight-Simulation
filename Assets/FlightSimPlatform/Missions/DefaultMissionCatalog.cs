using System;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Missions
{
    public static class DefaultMissionCatalog
    {
        private const double DegToRad = Math.PI / 180.0;

        public static MissionDefinition[] CreateAll()
        {
            return new[]
            {
                CreateScramble(),
                CreateCap(),
                CreateEscort(),
                CreateEmergencyReturn()
            };
        }

        public static MissionDefinition Create(string missionId)
        {
            MissionDefinition[] missions = CreateAll();
            for (int index = 0; index < missions.Length; index++)
            {
                if (string.Equals(missions[index].MissionId, missionId, StringComparison.Ordinal))
                    return missions[index];
            }
            return null;
        }

        private static MissionDefinition CreateScramble()
        {
            AircraftInitialCondition player = CreateRunway(0.65);
            AircraftInitialCondition wingman = CreateAirborne(-107.9700, 37.9600, 4400.0, 285.0, 185.0, 0.72);
            AircraftInitialCondition bandit1 = CreateAirborne(-108.7000, 38.0200, 6500.0, 105.0, 225.0, 0.70);
            AircraftInitialCondition bandit2 = CreateAirborne(-108.7300, 37.9800, 6700.0, 105.0, 225.0, 0.70);
            MissionActorDefinition viper1 = CreateActor("VIPER-01", true, AircraftSide.Friendly, AircraftRole.Player, MissionAiMode.GroundStart, in player);
            MissionActorDefinition viper2 = CreateWingman("VIPER-02", in wingman, 1, -90.0, -90.0, 10.0);
            return CreateMission(
                "KTEX_SCRAMBLE_01",
                "KTEX 紧急截击",
                "从跑道27紧急起飞，与僚机集合并在保护区外拦截来袭目标。",
                1001,
                720.0,
                new[]
                {
                    viper1,
                    viper2,
                    CreateActor("BANDIT-01", false, AircraftSide.Hostile, AircraftRole.Interceptor, MissionAiMode.Intercept, in bandit1),
                    CreateActor("BANDIT-02", false, AircraftSide.Hostile, AircraftRole.Interceptor, MissionAiMode.Intercept, in bandit2)
                },
                new[]
                {
                    Objective("TAKEOFF", "限时起飞", MissionObjectiveType.Takeoff, "VIPER-01", string.Empty, 1, 180.0),
                    Objective("INTERCEPT", "解除来袭威胁", MissionObjectiveType.NeutralizeHostiles, "VIPER-01", string.Empty, 2, 650.0)
                });
        }

        private static MissionDefinition CreateCap()
        {
            MissionActorDefinition[] actors = new MissionActorDefinition[7];
            AircraftInitialCondition player = CreateAirborne(-107.9800, 38.0200, 7000.0, 285.0, 210.0, 0.80);
            actors[0] = CreateActor("VIPER-01", true, AircraftSide.Friendly, AircraftRole.Player, MissionAiMode.Patrol, in player);
            AircraftInitialCondition wing2 = CreateAirborne(-107.9790, 38.0192, 7010.0, 285.0, 210.0, 0.80);
            AircraftInitialCondition wing3 = CreateAirborne(-107.9810, 38.0190, 7020.0, 285.0, 210.0, 0.80);
            actors[1] = CreateWingman("VIPER-02", in wing2, 1, -90.0, -90.0, 10.0);
            actors[2] = CreateWingman("VIPER-03", in wing3, 2, -170.0, 0.0, 20.0);
            for (int index = 0; index < 4; index++)
            {
                AircraftInitialCondition bandit = CreateAirborne(
                    -108.72 - index * 0.025,
                    38.06 + index * 0.015,
                    7100.0 + index * 80.0,
                    105.0,
                    220.0,
                    0.72);
                actors[index + 3] = CreateActor(
                    $"BANDIT-{index + 1:00}",
                    false,
                    AircraftSide.Hostile,
                    AircraftRole.Patrol,
                    MissionAiMode.Patrol,
                    in bandit);
            }
            return CreateMission(
                "KTEX_CAP_01",
                "KTEX 空中战斗巡逻",
                "保持CAP区域，识别并截击进入责任区的敌方编队。",
                2001,
                900.0,
                actors,
                new[]
                {
                    Objective("HOLD_CAP", "保持CAP区域", MissionObjectiveType.HoldArea, "VIPER-01", string.Empty, 1, 600.0),
                    Objective("CLEAR_CAP", "清除空中威胁", MissionObjectiveType.NeutralizeHostiles, "VIPER-01", string.Empty, 4, 850.0)
                });
        }

        private static MissionDefinition CreateEscort()
        {
            AircraftInitialCondition player = CreateAirborne(-107.8500, 37.9800, 6500.0, 285.0, 205.0, 0.60);
            AircraftInitialCondition wing2 = CreateAirborne(-107.8490, 37.9792, 6510.0, 285.0, 205.0, 0.62);
            AircraftInitialCondition wing3 = CreateAirborne(-107.8510, 37.9790, 6520.0, 285.0, 205.0, 0.62);
            AircraftInitialCondition escort = CreateAirborne(-107.8460, 37.9810, 6400.0, 285.0, 190.0, 0.75);
            MissionActorDefinition protectedActor = CreateActor("MAGIC-01", false, AircraftSide.Friendly, AircraftRole.Escort, MissionAiMode.Navigate, in escort);
            protectedActor.Route = new[]
            {
                Waypoint("ESCORT-WP1", -108.10, 38.02, 6400.0, 190.0),
                Waypoint("ESCORT-EGRESS", -108.45, 38.08, 6500.0, 190.0)
            };
            MissionActorDefinition[] actors = new MissionActorDefinition[7];
            actors[0] = CreateActor("VIPER-01", true, AircraftSide.Friendly, AircraftRole.Player, MissionAiMode.Escort, in player);
            actors[1] = CreateWingman("VIPER-02", in wing2, 1, -90.0, -90.0, 10.0);
            actors[2] = CreateWingman("VIPER-03", in wing3, 3, -90.0, 90.0, 10.0);
            actors[3] = protectedActor;
            for (int index = 0; index < 3; index++)
            {
                AircraftInitialCondition bandit = CreateAirborne(
                    -108.25 - index * 0.02,
                    38.18 + index * 0.012,
                    6800.0 + index * 100.0,
                    165.0,
                    225.0,
                    0.68);
                actors[index + 4] = CreateActor(
                    $"BANDIT-{index + 1:00}",
                    false,
                    AircraftSide.Hostile,
                    AircraftRole.Interceptor,
                    MissionAiMode.Intercept,
                    in bandit);
            }
            return CreateMission(
                "KTEX_ESCORT_01",
                "KTEX 护航",
                "保护MAGIC-01沿预定航路抵达西侧撤离点。",
                3001,
                900.0,
                actors,
                new[]
                {
                    Objective("PROTECT", "保护MAGIC-01", MissionObjectiveType.ProtectAircraft, "VIPER-01", "MAGIC-01", 1, 850.0),
                    Objective("ESCORT_EGRESS", "护送至撤离点", MissionObjectiveType.ReachWaypoint, "MAGIC-01", string.Empty, 1, 850.0)
                });
        }

        private static MissionDefinition CreateEmergencyReturn()
        {
            AircraftInitialCondition player = CreateAirborne(-107.6330, 37.8956, 3950.0, 285.0, 175.0, 0.22);
            AircraftInitialCondition wingman = CreateAirborne(-107.6315, 37.8946, 3970.0, 285.0, 175.0, 0.35);
            AircraftInitialCondition threat = CreateAirborne(-107.3000, 38.0700, 5800.0, 235.0, 210.0, 0.55);
            MissionActorDefinition playerActor = CreateActor("VIPER-01", true, AircraftSide.Friendly, AircraftRole.Player, MissionAiMode.ReturnToBase, in player);
            playerActor.InitialFailures = new[]
            {
                new MissionFailureDefinition { Type = FailureType.Hydraulic, SeverityNormalized = 0.65, DurationS = 900.0 }
            };
            return CreateMission(
                "KTEX_EMERGENCY_RTB_01",
                "KTEX 低油量故障返航",
                "在液压A降级和低油量条件下规避威胁并安全返回KTEX。",
                4001,
                720.0,
                new[]
                {
                    playerActor,
                    CreateWingman("VIPER-02", in wingman, 1, -90.0, -90.0, 10.0),
                    CreateActor("BANDIT-01", false, AircraftSide.Hostile, AircraftRole.Threat, MissionAiMode.Intercept, in threat)
                },
                new[]
                {
                    Objective("SURVIVE", "保持飞机可控", MissionObjectiveType.Survive, "VIPER-01", string.Empty, 1, 700.0),
                    Objective("LAND", "安全着陆KTEX", MissionObjectiveType.LandAtKtex, "VIPER-01", string.Empty, 1, 700.0)
                });
        }

        private static MissionDefinition CreateMission(
            string id,
            string displayName,
            string briefing,
            int seed,
            double maximumDurationS,
            MissionActorDefinition[] actors,
            MissionObjectiveDefinition[] objectives)
        {
            return new MissionDefinition
            {
                SchemaVersion = 1,
                MissionId = id,
                DisplayName = displayName,
                Briefing = briefing,
                DefaultSeed = seed,
                MaximumDurationS = maximumDurationS,
                Actors = actors,
                Objectives = objectives
            };
        }

        private static MissionActorDefinition CreateActor(
            string id,
            bool isPlayer,
            AircraftSide side,
            AircraftRole role,
            MissionAiMode aiMode,
            in AircraftInitialCondition condition)
        {
            MissionActorDefinition actor = MissionActorDefinition.Create(id, isPlayer, side, role, in condition);
            actor.InitialAiMode = aiMode;
            actor.AiSkillNormalized = side == AircraftSide.Hostile
                ? 0.45
                : role == AircraftRole.Escort ? 0.65 : 0.85;
            actor.InitialCondition.Loadout = side == AircraftSide.Hostile
                ? CreateHostileAirToAirLoadout()
                : CreateAirToAirLoadout();
            return actor;
        }

        private static MissionActorDefinition CreateWingman(
            string id,
            in AircraftInitialCondition condition,
            int slot,
            double forwardM,
            double rightM,
            double upM)
        {
            MissionActorDefinition actor = CreateActor(id, false, AircraftSide.Friendly, AircraftRole.Wingman, MissionAiMode.Formation, in condition);
            actor.LeaderAircraftId = "VIPER-01";
            actor.FormationSlot = slot;
            actor.FormationForwardOffsetM = forwardM;
            actor.FormationRightOffsetM = rightM;
            actor.FormationUpOffsetM = upM;
            return actor;
        }

        private static AircraftInitialCondition CreateRunway(double fuelFraction)
        {
            AircraftInitialCondition condition = AircraftInitialCondition.CreateForPreset(StartupPreset.RunwayReady);
            condition.InternalFuelFraction = fuelFraction;
            condition.LandingGearDown = true;
            return condition;
        }

        private static AircraftInitialCondition CreateAirborne(
            double longitudeDeg,
            double latitudeDeg,
            double heightM,
            double headingDeg,
            double speedMps,
            double fuelFraction)
        {
            AircraftInitialCondition condition = AircraftInitialCondition.CreateAirborneDefault();
            condition.LongitudeRad = longitudeDeg * DegToRad;
            condition.LatitudeRad = latitudeDeg * DegToRad;
            condition.EllipsoidHeightM = heightM;
            condition.HeadingRad = headingDeg * DegToRad;
            condition.TrueAirspeedMps = speedMps;
            condition.InternalFuelFraction = fuelFraction;
            condition.LandingGearDown = false;
            return condition;
        }

        private static StoreLoadoutCollection CreateAirToAirLoadout()
        {
            StoreLoadoutCollection loadout = default(StoreLoadoutCollection);
            loadout[0] = new StoreLoadoutConfiguration { StationIndex = 0, StoreType = "AIM-9X", Quantity = 1 };
            loadout[1] = new StoreLoadoutConfiguration { StationIndex = 1, StoreType = "AIM-120C", Quantity = 1 };
            loadout[2] = new StoreLoadoutConfiguration { StationIndex = 2, StoreType = "AIM-120C", Quantity = 1 };
            loadout[6] = new StoreLoadoutConfiguration { StationIndex = 6, StoreType = "AIM-120C", Quantity = 1 };
            loadout[7] = new StoreLoadoutConfiguration { StationIndex = 7, StoreType = "AIM-120C", Quantity = 1 };
            loadout[8] = new StoreLoadoutConfiguration { StationIndex = 8, StoreType = "AIM-9X", Quantity = 1 };
            return loadout;
        }

        private static StoreLoadoutCollection CreateHostileAirToAirLoadout()
        {
            StoreLoadoutCollection loadout = default(StoreLoadoutCollection);
            loadout[0] = new StoreLoadoutConfiguration { StationIndex = 0, StoreType = "AIM-9X", Quantity = 1 };
            loadout[8] = new StoreLoadoutConfiguration { StationIndex = 8, StoreType = "AIM-9X", Quantity = 1 };
            return loadout;
        }

        private static MissionObjectiveDefinition Objective(
            string id,
            string name,
            MissionObjectiveType type,
            string subject,
            string target,
            int requiredCount,
            double timeLimitS)
        {
            return new MissionObjectiveDefinition
            {
                ObjectiveId = id,
                DisplayName = name,
                Type = type,
                SubjectAircraftId = subject,
                TargetAircraftId = target,
                RequiredCount = requiredCount,
                TimeLimitS = timeLimitS,
                IsRequired = true
            };
        }

        private static MissionWaypointDefinition Waypoint(
            string id,
            double longitudeDeg,
            double latitudeDeg,
            double heightM,
            double speedMps)
        {
            return new MissionWaypointDefinition
            {
                WaypointId = id,
                LongitudeRad = longitudeDeg * DegToRad,
                LatitudeRad = latitudeDeg * DegToRad,
                EllipsoidHeightM = heightM,
                TargetTrueAirspeedMps = speedMps,
                AcceptanceRadiusM = 6000.0
            };
        }
    }
}
