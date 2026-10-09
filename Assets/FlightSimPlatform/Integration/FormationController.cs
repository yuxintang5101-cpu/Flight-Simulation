using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;

namespace FlightSim.Platform.Integration
{
    internal sealed class FormationController
    {
        private const double DefaultCruiseThrottle = 0.72;
        private const double MinimumSeparationM = 35.0;

        private readonly FormationState[] states;
        private readonly int[] leaderIndexes;

        public FormationController(int capacity)
        {
            states = new FormationState[capacity];
            leaderIndexes = new int[capacity];
            for (int index = 0; index < capacity; index++)
            {
                leaderIndexes[index] = -1;
            }
        }

        public void InitializeAircraft(int aircraftIndex)
        {
            states[aircraftIndex] = default(FormationState);
            leaderIndexes[aircraftIndex] = -1;
        }

        public void ConfigureDefaultWingman(
            int aircraftIndex,
            int leaderIndex,
            AircraftId leaderAircraft,
            int formationSlot)
        {
            GetDefaultOffset(
                formationSlot,
                out double forwardOffsetM,
                out double rightOffsetM,
                out double upOffsetM);
            states[aircraftIndex] = new FormationState
            {
                LeaderAircraft = leaderAircraft,
                FormationSlot = formationSlot,
                DesiredForwardOffsetM = forwardOffsetM,
                DesiredRightOffsetM = rightOffsetM,
                DesiredUpOffsetM = upOffsetM,
                IsFormationActive = true,
                Mode = FormationMode.Rejoin
            };
            leaderIndexes[aircraftIndex] = leaderIndex;
        }

        public FormationState GetState(int aircraftIndex)
        {
            return states[aircraftIndex];
        }

        public FormationMode GetMode(int aircraftIndex)
        {
            return states[aircraftIndex].Mode;
        }

        public bool ShouldControl(int aircraftIndex)
        {
            return states[aircraftIndex].Mode != FormationMode.None;
        }

        public bool TryGetLeaderIndex(int aircraftIndex, out int leaderIndex)
        {
            leaderIndex = leaderIndexes[aircraftIndex];
            return leaderIndex >= 0;
        }

        public void ApplyCommand(
            int aircraftIndex,
            int leaderIndex,
            AircraftId leaderAircraft,
            in FormationCommand command)
        {
            FormationState state = states[aircraftIndex];
            switch (command.Type)
            {
                case FormationCommandType.Leave:
                    state = default(FormationState);
                    leaderIndex = -1;
                    break;

                case FormationCommandType.ReturnToBase:
                    state.LeaderAircraft = default(AircraftId);
                    state.IsFormationActive = false;
                    state.Mode = FormationMode.ReturnToBase;
                    leaderIndex = -1;
                    break;

                case FormationCommandType.Join:
                    state.LeaderAircraft = leaderAircraft;
                    state.IsFormationActive = true;
                    state.Mode = FormationMode.Rejoin;
                    if (state.FormationSlot <= 0)
                    {
                        state.FormationSlot = aircraftIndex > 0 ? aircraftIndex : 1;
                    }

                    if (HasExplicitOffset(in command))
                    {
                        SetOffset(ref state, in command);
                    }
                    else
                    {
                        GetDefaultOffset(
                            state.FormationSlot,
                            out state.DesiredForwardOffsetM,
                            out state.DesiredRightOffsetM,
                            out state.DesiredUpOffsetM);
                    }
                    break;

                case FormationCommandType.SetLeader:
                    state.LeaderAircraft = leaderAircraft;
                    state.IsFormationActive = true;
                    if (state.Mode == FormationMode.None || state.Mode == FormationMode.ReturnToBase)
                    {
                        state.Mode = FormationMode.Rejoin;
                    }
                    break;

                case FormationCommandType.SetOffset:
                    SetOffset(ref state, in command);
                    if (leaderIndex >= 0)
                    {
                        state.LeaderAircraft = leaderAircraft;
                        state.IsFormationActive = true;
                        if (state.Mode == FormationMode.None || state.Mode == FormationMode.ReturnToBase)
                        {
                            state.Mode = FormationMode.Rejoin;
                        }
                    }
                    break;

                case FormationCommandType.Rejoin:
                    state.LeaderAircraft = leaderAircraft;
                    state.IsFormationActive = true;
                    state.Mode = FormationMode.Rejoin;
                    break;

                case FormationCommandType.Maintain:
                    state.LeaderAircraft = leaderAircraft;
                    state.IsFormationActive = true;
                    state.Mode = FormationMode.Maintain;
                    break;

                case FormationCommandType.BreakAway:
                    state.LeaderAircraft = leaderAircraft;
                    state.IsFormationActive = true;
                    state.Mode = FormationMode.BreakAway;
                    break;
            }

            states[aircraftIndex] = state;
            leaderIndexes[aircraftIndex] = leaderIndex;
        }

        public PilotControlInput ComputeInput(
            int aircraftIndex,
            in F16AircraftState ownState,
            in F16AircraftState leaderState,
            in PilotControlInput leaderInput,
            in AnalyticRunway runway,
            in DVector3 separationVelocityEcefMps)
        {
            FormationState formation = states[aircraftIndex];
            DVector3 targetPositionEcefM;
            DVector3 baseVelocityEcefMps;
            double positionGain;
            double maximumCorrectionMps;
            double baseThrottle;

            if (formation.Mode == FormationMode.ReturnToBase)
            {
                targetPositionEcefM = runway.ThresholdEcefM -
                                      runway.ForwardEcef * 1200.0 +
                                      runway.UpEcef * 350.0;
                if ((targetPositionEcefM - ownState.EcefPositionM).Length < 450.0)
                {
                    targetPositionEcefM = runway.CenterEcefM + runway.UpEcef * 80.0;
                }

                baseVelocityEcefMps = runway.ForwardEcef * 135.0;
                positionGain = 0.035;
                maximumCorrectionMps = 75.0;
                baseThrottle = 0.62;
            }
            else
            {
                double forwardOffsetM = formation.DesiredForwardOffsetM;
                double rightOffsetM = formation.DesiredRightOffsetM;
                double upOffsetM = formation.DesiredUpOffsetM;
                if (formation.Mode == FormationMode.BreakAway)
                {
                    double side = rightOffsetM < 0.0 ? -1.0 : 1.0;
                    if (Math.Abs(rightOffsetM) < 1.0)
                    {
                        side = formation.FormationSlot % 2 == 0 ? 1.0 : -1.0;
                    }

                    forwardOffsetM = Math.Min(forwardOffsetM, -350.0);
                    rightOffsetM = side * Math.Max(Math.Abs(rightOffsetM), 650.0);
                    upOffsetM = Math.Max(upOffsetM, 180.0);
                }

                DVector3 offsetBodyM = new DVector3(forwardOffsetM, rightOffsetM, -upOffsetM);
                targetPositionEcefM = leaderState.EcefPositionM +
                                      leaderState.BodyToEcefOrientation.Rotate(offsetBodyM);
                baseVelocityEcefMps = leaderState.EcefVelocityMps;
                positionGain = formation.Mode == FormationMode.Maintain ? 0.055 : 0.032;
                maximumCorrectionMps = formation.Mode == FormationMode.Maintain ? 32.0 : 70.0;
                baseThrottle = FiniteOrDefault(leaderInput.ThrottleNormalized, DefaultCruiseThrottle);
                if (baseThrottle < 0.25)
                {
                    baseThrottle = DefaultCruiseThrottle;
                }
            }

            DVector3 positionErrorEcefM = targetPositionEcefM - ownState.EcefPositionM;
            DVector3 correctionVelocityEcefMps = LimitMagnitude(
                positionErrorEcefM * positionGain,
                maximumCorrectionMps);
            DVector3 desiredVelocityEcefMps = baseVelocityEcefMps +
                                              correctionVelocityEcefMps +
                                              separationVelocityEcefMps;
            DVector3 desiredVelocityBodyMps = ownState.BodyToEcefOrientation
                .RotateInverse(desiredVelocityEcefMps);

            double desiredHorizontalSpeedMps = Math.Sqrt(
                desiredVelocityBodyMps.X * desiredVelocityBodyMps.X +
                desiredVelocityBodyMps.Y * desiredVelocityBodyMps.Y);
            double headingErrorRad = Math.Atan2(
                desiredVelocityBodyMps.Y,
                Math.Max(1.0, desiredVelocityBodyMps.X));
            double desiredFlightPathElevationRad = Math.Atan2(
                -desiredVelocityBodyMps.Z,
                Math.Max(1.0, desiredHorizontalSpeedMps));
            double ownHorizontalSpeedMps = Math.Sqrt(
                ownState.BodyVelocityMps.X * ownState.BodyVelocityMps.X +
                ownState.BodyVelocityMps.Y * ownState.BodyVelocityMps.Y);
            double ownFlightPathElevationRad = Math.Atan2(
                -ownState.BodyVelocityMps.Z,
                Math.Max(1.0, ownHorizontalSpeedMps));
            double desiredSpeedMps = desiredVelocityEcefMps.Length;
            double speedErrorMps = desiredSpeedMps - ownState.TrueAirspeedMps;
            if (formation.Mode == FormationMode.Rejoin && positionErrorEcefM.Length < 25.0)
            {
                formation.Mode = FormationMode.Maintain;
                states[aircraftIndex] = formation;
            }

            PilotControlInput input = PilotControlInput.Neutral;
            double attitudeGain = formation.Mode == FormationMode.Rejoin ? 0.65 : 0.22;
            double speedGain = formation.Mode == FormationMode.Rejoin ? 0.004 : 0.0015;
            input.RollNormalized = Clamp(
                leaderInput.RollNormalized + headingErrorRad * attitudeGain,
                -1.0,
                1.0);
            input.PitchNormalized = Clamp(
                leaderInput.PitchNormalized +
                (desiredFlightPathElevationRad - ownFlightPathElevationRad) * attitudeGain,
                -0.8,
                0.8);
            input.YawNormalized = Clamp(
                leaderInput.YawNormalized + headingErrorRad * attitudeGain * 0.12,
                -0.35,
                0.35);
            input.ThrottleNormalized = Clamp(baseThrottle + speedErrorMps * speedGain, 0.2, 1.0);
            input.SpeedBrakeNormalized = speedErrorMps < -45.0
                ? Clamp((-speedErrorMps - 45.0) / 60.0, 0.0, 1.0)
                : 0.0;

            return input;
        }

        public DVector3 ComputeSeparationVelocity(
            int aircraftIndex,
            int otherAircraftIndex,
            in F16AircraftState ownState,
            in F16AircraftState otherState)
        {
            DVector3 awayEcefM = ownState.EcefPositionM - otherState.EcefPositionM;
            double distanceM = awayEcefM.Length;
            if (distanceM >= MinimumSeparationM)
            {
                return DVector3.Zero;
            }

            if (distanceM < 0.01)
            {
                double side = aircraftIndex < otherAircraftIndex ? -1.0 : 1.0;
                awayEcefM = ownState.BodyToEcefOrientation.Rotate(
                    new DVector3(0.0, side, aircraftIndex % 2 == 0 ? -0.2 : 0.2));
                distanceM = 0.01;
            }

            double strengthMps = 45.0 * (1.0 - distanceM / MinimumSeparationM);
            return awayEcefM.Normalized * strengthMps;
        }

        private static bool HasExplicitOffset(in FormationCommand command)
        {
            return command.ForwardOffsetM != 0.0 ||
                   command.RightOffsetM != 0.0 ||
                   command.UpOffsetM != 0.0;
        }

        private static void SetOffset(ref FormationState state, in FormationCommand command)
        {
            state.DesiredForwardOffsetM = command.ForwardOffsetM;
            state.DesiredRightOffsetM = command.RightOffsetM;
            state.DesiredUpOffsetM = command.UpOffsetM;
        }

        private static void GetDefaultOffset(
            int formationSlot,
            out double forwardOffsetM,
            out double rightOffsetM,
            out double upOffsetM)
        {
            switch (formationSlot)
            {
                case 1:
                    forwardOffsetM = -90.0;
                    rightOffsetM = -90.0;
                    upOffsetM = 10.0;
                    break;
                case 2:
                    forwardOffsetM = -170.0;
                    rightOffsetM = 0.0;
                    upOffsetM = 20.0;
                    break;
                default:
                    forwardOffsetM = -90.0;
                    rightOffsetM = 90.0;
                    upOffsetM = 10.0;
                    break;
            }
        }

        private static DVector3 LimitMagnitude(DVector3 value, double maximumMagnitude)
        {
            double length = value.Length;
            return length > maximumMagnitude && length > 0.0
                ? value * (maximumMagnitude / length)
                : value;
        }

        private static double FiniteOrDefault(double value, double fallback)
        {
            return double.IsNaN(value) || double.IsInfinity(value) ? fallback : value;
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }
    }
}
