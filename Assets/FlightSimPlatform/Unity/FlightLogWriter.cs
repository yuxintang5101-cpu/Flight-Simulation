using System;
using System.Globalization;
using System.IO;
using System.Text;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [Serializable]
    public sealed class FlightLogMetadata
    {
        public int SchemaVersion;
        public int ContractVersion;
        public string UnityVersion;
        public string SceneName;
        public string AircraftId;
        public string UtcStartTime;
        public int FixedStepHz;
    }

    public sealed class FlightLogWriter : IDisposable
    {
        private const double RadiansToDegrees = 180.0 / Math.PI;
        private const string TelemetryHeader =
            "frame,aircraft_id,tick,simulation_time_s,utc_time,systems_time_s," +
            "ecef_position_x_m,ecef_position_y_m,ecef_position_z_m," +
            "ecef_velocity_x_mps,ecef_velocity_y_mps,ecef_velocity_z_mps," +
            "body_velocity_x_mps,body_velocity_y_mps,body_velocity_z_mps," +
            "body_angular_velocity_x_radps,body_angular_velocity_y_radps,body_angular_velocity_z_radps," +
            "body_to_ecef_quaternion_x,body_to_ecef_quaternion_y,body_to_ecef_quaternion_z,body_to_ecef_quaternion_w," +
            "longitude_deg,latitude_deg,ellipsoid_height_m,heading_deg,pitch_deg,roll_deg," +
            "true_airspeed_mps,calibrated_airspeed_mps,ground_speed_mps,mach,angle_of_attack_deg,sideslip_deg," +
            "normal_load_factor_g,mean_sea_level_altitude_m,above_ground_level_altitude_m,climb_rate_mps," +
            "terrain_sample_valid,terrain_sample_age_s," +
            "input_pitch,input_roll,input_yaw,input_throttle,input_wheel_brake,input_speed_brake,input_engine_start," +
            "raw_mouse_x,raw_mouse_y," +
            "applied_pitch_command,applied_roll_command,applied_yaw_command," +
            "aileron_deflection_deg,elevator_deflection_deg,rudder_deflection_deg," +
            "leading_edge_flap_deflection_deg,trailing_edge_flap_deflection_deg," +
            "aoa_limiter_active,g_limiter_active,roll_rate_limiter_active,fcc_enabled,autopilot_engaged," +
            "engine_mode,engine_running,afterburner_active,n1_percent,n2_percent,egt_c,engine_fuel_flow_kgps,nozzle_position,thrust_n," +
            "internal_fuel_kg,external_fuel_kg,left_fuel_kg,right_fuel_kg,total_fuel_kg,fuel_transfer_mode," +
            "fuel_imbalance_kg,bingo_fuel_kg,joker_fuel_kg,cg_percent_mac,fuel_flow_kgps,low_fuel_warning,fuel_pump_enabled," +
            "main_bus_voltage_v,essential_bus_voltage_v,avionics_bus_voltage_v,battery_voltage_v,generator_current_a," +
            "main_bus_powered,essential_bus_powered,avionics_bus_powered,generator_online,battery_online," +
            "hydraulic_a_pressure_pa,hydraulic_b_pressure_pa,brake_pressure_pa,hydraulic_a_online,hydraulic_b_online," +
            "nose_gear_position,left_main_gear_position,right_main_gear_position,left_brake_pressure_pa,right_brake_pressure_pa," +
            "speed_brake_position,canopy_position,nose_wheel_steering_enabled,weight_on_wheels," +
            "ins_state,hud_enabled,hud_mode,startup_state,master_mode_air_to_air,radar_enabled,radar_locked,radar_range_m," +
            "navigation_computer_enabled,data_link_enabled," +
            "master_caution,master_warning,fire_warning,hydraulic_warning,electrical_warning,fuel_warning," +
            "low_altitude_warning,overspeed_warning,stall_warning,landing_gear_warning,canopy_warning";
        private const string EventsHeader =
            "simulation_time_s,source,event_type,code,message";

        private static readonly Encoding Utf8 = new UTF8Encoding(false);
        private static readonly string NewLine = Environment.NewLine;

        private readonly string sessionDirectory;
        private readonly long telemetryFileByteLimit;
        private readonly StringBuilder rowBuilder = new StringBuilder(4096);
        private StreamWriter telemetryWriter;
        private StreamWriter eventWriter;
        private int telemetryFileIndex;
        private long telemetryBytesWritten;
        private bool disposed;

        public FlightLogWriter(
            string sessionDirectory,
            FlightLogMetadata metadata,
            long telemetryFileByteLimit)
        {
            if (string.IsNullOrWhiteSpace(sessionDirectory))
                throw new ArgumentException("A session directory is required.", nameof(sessionDirectory));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (telemetryFileByteLimit <= 0)
                throw new ArgumentOutOfRangeException(nameof(telemetryFileByteLimit));

            this.sessionDirectory = Path.GetFullPath(sessionDirectory);
            this.telemetryFileByteLimit = telemetryFileByteLimit;
            Directory.CreateDirectory(this.sessionDirectory);
            File.WriteAllText(
                Path.Combine(this.sessionDirectory, "metadata.json"),
                JsonUtility.ToJson(metadata, true),
                Utf8);
            try
            {
                OpenTelemetryFile();
                eventWriter = CreateWriter(Path.Combine(this.sessionDirectory, "events.csv"));
                eventWriter.WriteLine(EventsHeader);
            }
            catch
            {
                try
                {
                    eventWriter?.Dispose();
                }
                catch
                {
                }
                finally
                {
                    eventWriter = null;
                }

                try
                {
                    telemetryWriter?.Dispose();
                }
                catch
                {
                }
                finally
                {
                    telemetryWriter = null;
                }
                throw;
            }
        }

        public void WriteTelemetry(
            in AircraftFastState fast,
            in AircraftSystemsState systems,
            in PilotControlInput input,
            float rawMouseX,
            float rawMouseY,
            int frame)
        {
            WriteTelemetry(
                in fast,
                in systems,
                in input,
                rawMouseX,
                rawMouseY,
                frame,
                DateTime.UtcNow.Ticks);
        }

        public void WriteTelemetry(
            in AircraftFastState fast,
            in AircraftSystemsState systems,
            in PilotControlInput input,
            float rawMouseX,
            float rawMouseY,
            int frame,
            long capturedUtcTicks)
        {
            ThrowIfDisposed();
            rowBuilder.Clear();
            Append(frame);
            Append(fast.Aircraft.Value);
            Append(fast.Tick);
            Append(fast.SimulationTimeS);
            Append(new DateTime(capturedUtcTicks, DateTimeKind.Utc).ToString("O", CultureInfo.InvariantCulture));
            Append(systems.SimulationTimeS);
            Append(fast.EcefPositionXM);
            Append(fast.EcefPositionYM);
            Append(fast.EcefPositionZM);
            Append(fast.EcefVelocityXMps);
            Append(fast.EcefVelocityYMps);
            Append(fast.EcefVelocityZMps);
            Append(fast.BodyVelocityXMps);
            Append(fast.BodyVelocityYMps);
            Append(fast.BodyVelocityZMps);
            Append(fast.BodyAngularVelocityXRadps);
            Append(fast.BodyAngularVelocityYRadps);
            Append(fast.BodyAngularVelocityZRadps);
            Append(fast.BodyToEcefQuaternionX);
            Append(fast.BodyToEcefQuaternionY);
            Append(fast.BodyToEcefQuaternionZ);
            Append(fast.BodyToEcefQuaternionW);
            Append(fast.LongitudeRad * RadiansToDegrees);
            Append(fast.LatitudeRad * RadiansToDegrees);
            Append(fast.EllipsoidHeightM);
            Append(fast.HeadingRad * RadiansToDegrees);
            Append(fast.PitchRad * RadiansToDegrees);
            Append(fast.RollRad * RadiansToDegrees);
            Append(fast.TrueAirspeedMps);
            Append(fast.CalibratedAirspeedMps);
            Append(fast.GroundSpeedMps);
            Append(fast.Mach);
            Append(fast.AngleOfAttackRad * RadiansToDegrees);
            Append(fast.SideslipRad * RadiansToDegrees);
            Append(fast.NormalLoadFactorG);
            Append(fast.MeanSeaLevelAltitudeM);
            Append(fast.AboveGroundLevelAltitudeM);
            Append(fast.ClimbRateMps);
            Append(fast.TerrainSampleValid);
            Append(fast.TerrainSampleAgeS);
            Append(input.PitchNormalized);
            Append(input.RollNormalized);
            Append(input.YawNormalized);
            Append(input.ThrottleNormalized);
            Append(input.WheelBrakeNormalized);
            Append(input.SpeedBrakeNormalized);
            Append(input.EngineStartCommand);
            Append(rawMouseX);
            Append(rawMouseY);

            FlightControlState controls = systems.FlightControls;
            Append(controls.PitchCommandNormalized);
            Append(controls.RollCommandNormalized);
            Append(controls.YawCommandNormalized);
            Append(controls.AileronDeflectionRad * RadiansToDegrees);
            Append(controls.ElevatorDeflectionRad * RadiansToDegrees);
            Append(controls.RudderDeflectionRad * RadiansToDegrees);
            Append(controls.LeadingEdgeFlapDeflectionRad * RadiansToDegrees);
            Append(controls.TrailingEdgeFlapDeflectionRad * RadiansToDegrees);
            Append(controls.AngleOfAttackLimiterActive);
            Append(controls.GForceLimiterActive);
            Append(controls.RollRateLimiterActive);
            Append(controls.FlightControlComputerEnabled);
            Append(controls.AutopilotEngaged);

            PropulsionState propulsion = systems.Propulsion;
            Append(propulsion.Mode.ToString());
            Append(propulsion.EngineRunning);
            Append(propulsion.AfterburnerActive);
            Append(propulsion.N1Percent);
            Append(propulsion.N2Percent);
            Append(propulsion.ExhaustGasTemperatureC);
            Append(propulsion.FuelFlowKgps);
            Append(propulsion.NozzlePositionNormalized);
            Append(propulsion.ThrustN);

            FuelState fuel = systems.Fuel;
            Append(fuel.InternalFuelKg);
            Append(fuel.ExternalFuelKg);
            Append(fuel.LeftFuelKg);
            Append(fuel.RightFuelKg);
            Append(fuel.TotalFuelKg);
            Append(fuel.TransferMode.ToString());
            Append(fuel.FuelImbalanceKg);
            Append(fuel.BingoFuelKg);
            Append(fuel.JokerFuelKg);
            Append(fuel.CenterOfGravityPercentMac);
            Append(fuel.FuelFlowKgps);
            Append(fuel.LowFuelWarning);
            Append(fuel.FuelPumpEnabled);

            ElectricalState electrical = systems.Electrical;
            Append(electrical.MainBusVoltageV);
            Append(electrical.EssentialBusVoltageV);
            Append(electrical.AvionicsBusVoltageV);
            Append(electrical.BatteryVoltageV);
            Append(electrical.GeneratorCurrentA);
            Append(electrical.MainBusPowered);
            Append(electrical.EssentialBusPowered);
            Append(electrical.AvionicsBusPowered);
            Append(electrical.GeneratorOnline);
            Append(electrical.BatteryOnline);

            HydraulicState hydraulics = systems.Hydraulics;
            Append(hydraulics.SystemAPressurePa);
            Append(hydraulics.SystemBPressurePa);
            Append(hydraulics.BrakePressurePa);
            Append(hydraulics.SystemAOnline);
            Append(hydraulics.SystemBOnline);

            LandingGearState gear = systems.LandingGear;
            Append(gear.NoseGearPositionNormalized);
            Append(gear.LeftMainGearPositionNormalized);
            Append(gear.RightMainGearPositionNormalized);
            Append(gear.LeftBrakePressurePa);
            Append(gear.RightBrakePressurePa);
            Append(gear.SpeedBrakePositionNormalized);
            Append(gear.CanopyPositionNormalized);
            Append(gear.NoseWheelSteeringEnabled);
            Append(gear.WeightOnWheels);

            AvionicsState avionics = systems.Avionics;
            Append(avionics.InsState.ToString());
            Append(avionics.HudEnabled);
            Append(avionics.HudMode.ToString());
            Append(avionics.StartupState.ToString());
            Append(avionics.MasterModeAirToAir);
            Append(avionics.RadarEnabled);
            Append(avionics.RadarLocked);
            Append(avionics.RadarRangeM);
            Append(avionics.NavigationComputerEnabled);
            Append(avionics.DataLinkEnabled);

            WarningState warnings = systems.Warnings;
            Append(warnings.MasterCaution);
            Append(warnings.MasterWarning);
            Append(warnings.FireWarning);
            Append(warnings.HydraulicWarning);
            Append(warnings.ElectricalWarning);
            Append(warnings.FuelWarning);
            Append(warnings.LowAltitudeWarning);
            Append(warnings.OverspeedWarning);
            Append(warnings.StallWarning);
            Append(warnings.LandingGearWarning);
            Append(warnings.CanopyWarning);

            WriteTelemetryRow(rowBuilder.ToString());
        }

        public void WriteSimulationEvent(in SimulationEvent simulationEvent)
        {
            WriteEvent(
                simulationEvent.SimulationTimeS,
                "SIMULATION",
                simulationEvent.Type.ToString(),
                simulationEvent.Code,
                simulationEvent.Message);
        }

        public void WriteRecorderEvent(double simulationTimeS, string eventType, string message)
        {
            WriteEvent(simulationTimeS, "RECORDER", eventType, 0, message);
        }

        public void Flush()
        {
            ThrowIfDisposed();
            try
            {
                telemetryWriter?.Dispose();
            }
            finally
            {
                telemetryWriter = null;
                eventWriter?.Dispose();
                eventWriter = null;
            }
        }

        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;
            try
            {
                telemetryWriter?.Dispose();
            }
            finally
            {
                telemetryWriter = null;
                eventWriter?.Dispose();
                eventWriter = null;
            }
        }

        private void WriteTelemetryRow(string row)
        {
            long rowBytes = Utf8.GetByteCount(row) + Utf8.GetByteCount(NewLine);
            long headerBytes = Utf8.GetByteCount(TelemetryHeader) + Utf8.GetByteCount(NewLine);
            if (telemetryBytesWritten > headerBytes &&
                telemetryBytesWritten + rowBytes > telemetryFileByteLimit)
            {
                // Oversized rows remain intact in a dedicated segment and force the next row to roll again.
                telemetryWriter?.Dispose();
                telemetryFileIndex++;
                OpenTelemetryFile();
            }
            else if (telemetryWriter == null)
            {
                telemetryWriter = CreateAppendWriter(GetTelemetryPath());
            }

            telemetryWriter.WriteLine(row);
            telemetryBytesWritten += rowBytes;
        }

        private void WriteEvent(
            double simulationTimeS,
            string source,
            string eventType,
            int code,
            string message)
        {
            ThrowIfDisposed();
            if (eventWriter == null)
                eventWriter = CreateAppendWriter(Path.Combine(sessionDirectory, "events.csv"));
            rowBuilder.Clear();
            Append(simulationTimeS);
            Append(source);
            Append(eventType);
            Append(code);
            Append(message);
            eventWriter.WriteLine(rowBuilder.ToString());
        }

        private void OpenTelemetryFile()
        {
            telemetryWriter = CreateWriter(GetTelemetryPath());
            telemetryWriter.WriteLine(TelemetryHeader);
            telemetryBytesWritten = Utf8.GetByteCount(TelemetryHeader) + Utf8.GetByteCount(NewLine);
        }

        private string GetTelemetryPath()
        {
            string fileName = string.Format(
                CultureInfo.InvariantCulture,
                "telemetry_{0:000}.csv",
                telemetryFileIndex);
            return Path.Combine(sessionDirectory, fileName);
        }

        private static StreamWriter CreateWriter(string path)
        {
            var stream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.Read,
                16 * 1024,
                FileOptions.SequentialScan);
            return new StreamWriter(stream, Utf8, 16 * 1024)
            {
                NewLine = NewLine
            };
        }

        private static StreamWriter CreateAppendWriter(string path)
        {
            var stream = new FileStream(
                path,
                FileMode.Append,
                FileAccess.Write,
                FileShare.Read,
                16 * 1024,
                FileOptions.SequentialScan);
            return new StreamWriter(stream, Utf8, 16 * 1024)
            {
                NewLine = NewLine
            };
        }

        private void Append(string value)
        {
            AddSeparator();
            AppendEscaped(value ?? string.Empty);
        }

        private void Append(bool value)
        {
            AddSeparator();
            rowBuilder.Append(value ? '1' : '0');
        }

        private void Append(int value)
        {
            AddSeparator();
            rowBuilder.Append(value.ToString(CultureInfo.InvariantCulture));
        }

        private void Append(ulong value)
        {
            AddSeparator();
            rowBuilder.Append(value.ToString(CultureInfo.InvariantCulture));
        }

        private void Append(float value)
        {
            AddSeparator();
            rowBuilder.Append(value.ToString("R", CultureInfo.InvariantCulture));
        }

        private void Append(double value)
        {
            AddSeparator();
            rowBuilder.Append(value.ToString("R", CultureInfo.InvariantCulture));
        }

        private void AddSeparator()
        {
            if (rowBuilder.Length > 0)
                rowBuilder.Append(',');
        }

        private void AppendEscaped(string value)
        {
            bool quote = value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0;
            if (!quote)
            {
                rowBuilder.Append(value);
                return;
            }

            rowBuilder.Append('"');
            for (int i = 0; i < value.Length; i++)
            {
                char character = value[i];
                if (character == '"')
                    rowBuilder.Append('"');
                rowBuilder.Append(character);
            }
            rowBuilder.Append('"');
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(FlightLogWriter));
        }
    }
}
