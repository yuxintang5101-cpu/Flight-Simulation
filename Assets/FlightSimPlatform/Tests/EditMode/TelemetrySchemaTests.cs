using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FlightSim.Platform.Contracts;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class TelemetrySchemaTests
    {
        [Test]
        public void FastState_ContainsRequiredPublicFields()
        {
            AssertFieldTypes(typeof(AircraftFastState), new Dictionary<string, Type>
            {
                { "Aircraft", typeof(AircraftId) },
                { "Tick", typeof(ulong) },
                { "SimulationTimeS", typeof(double) },
                { "EcefPositionXM", typeof(double) },
                { "EcefPositionYM", typeof(double) },
                { "EcefPositionZM", typeof(double) },
                { "EcefVelocityXMps", typeof(double) },
                { "EcefVelocityYMps", typeof(double) },
                { "EcefVelocityZMps", typeof(double) },
                { "BodyVelocityXMps", typeof(double) },
                { "BodyVelocityYMps", typeof(double) },
                { "BodyVelocityZMps", typeof(double) },
                { "BodyAngularVelocityXRadps", typeof(double) },
                { "BodyAngularVelocityYRadps", typeof(double) },
                { "BodyAngularVelocityZRadps", typeof(double) },
                { "BodyToEcefQuaternionX", typeof(double) },
                { "BodyToEcefQuaternionY", typeof(double) },
                { "BodyToEcefQuaternionZ", typeof(double) },
                { "BodyToEcefQuaternionW", typeof(double) },
                { "LongitudeRad", typeof(double) },
                { "LatitudeRad", typeof(double) },
                { "EllipsoidHeightM", typeof(double) },
                { "HeadingRad", typeof(double) },
                { "PitchRad", typeof(double) },
                { "RollRad", typeof(double) },
                { "TrueAirspeedMps", typeof(double) },
                { "CalibratedAirspeedMps", typeof(double) },
                { "GroundSpeedMps", typeof(double) },
                { "Mach", typeof(double) },
                { "AngleOfAttackRad", typeof(double) },
                { "SideslipRad", typeof(double) },
                { "NormalLoadFactorG", typeof(double) },
                { "MeanSeaLevelAltitudeM", typeof(double) },
                { "AboveGroundLevelAltitudeM", typeof(double) },
                { "ClimbRateMps", typeof(double) },
                { "TerrainSampleValid", typeof(bool) },
                { "TerrainSampleAgeS", typeof(double) }
            });
        }

        [Test]
        public void FlightControls_ContainCommandsDeflectionsAndLimiters()
        {
            AssertFieldNames(typeof(FlightControlState),
                "PitchCommandNormalized", "RollCommandNormalized", "YawCommandNormalized",
                "AileronDeflectionRad", "ElevatorDeflectionRad", "RudderDeflectionRad",
                "LeadingEdgeFlapDeflectionRad", "TrailingEdgeFlapDeflectionRad",
                "AngleOfAttackLimiterActive", "GForceLimiterActive", "RollRateLimiterActive");
        }

        [Test]
        public void Propulsion_ContainsModeSpoolThrustAndFuelFlow()
        {
            AssertFieldNames(typeof(PropulsionState),
                "Mode", "N1Percent", "N2Percent", "ThrustN", "FuelFlowKgps");
            Assert.That(typeof(PropulsionState).GetField("Mode").FieldType.Name, Is.EqualTo("EngineMode"));
        }

        [Test]
        public void Fuel_ContainsQuantitiesTransferThresholdsImbalanceAndCg()
        {
            AssertFieldNames(typeof(FuelState),
                "InternalFuelKg", "ExternalFuelKg", "LeftFuelKg", "RightFuelKg", "TotalFuelKg",
                "TransferMode", "FuelImbalanceKg", "BingoFuelKg", "JokerFuelKg", "CenterOfGravityPercentMac");
            Assert.That(typeof(FuelState).GetField("TransferMode").FieldType.Name, Is.EqualTo("FuelTransferMode"));
        }

        [Test]
        public void ElectricalHydraulicAndGear_ContainRequiredFields()
        {
            AssertFieldNames(typeof(ElectricalState),
                "MainBusVoltageV", "EssentialBusVoltageV", "AvionicsBusVoltageV",
                "MainBusPowered", "EssentialBusPowered", "AvionicsBusPowered");
            AssertFieldNames(typeof(HydraulicState),
                "SystemAPressurePa", "SystemBPressurePa", "SystemAOnline", "SystemBOnline");
            AssertFieldNames(typeof(LandingGearState),
                "NoseGearPositionNormalized", "LeftMainGearPositionNormalized", "RightMainGearPositionNormalized",
                "LeftBrakePressurePa", "RightBrakePressurePa", "SpeedBrakePositionNormalized",
                "CanopyPositionNormalized", "WeightOnWheels");
        }

        [Test]
        public void StoreStation_ContainsRequiredReleaseAndAerodynamicFields()
        {
            AssertFieldNames(typeof(StoreStationState),
                "StationIndex", "StoreType", "Quantity", "StoreMassKg", "DragCoefficient",
                "LongitudinalCgM", "LateralCgM", "IsArmed", "IsSelected", "IsReady", "IsReleased");
        }

        [Test]
        public void AvionicsAndWarnings_ContainRequiredFields()
        {
            AssertFieldNames(typeof(AvionicsState), "InsState", "HudEnabled", "StartupState");
            AssertFieldNames(typeof(WarningState),
                "MasterCaution", "MasterWarning", "FireWarning", "HydraulicWarning", "ElectricalWarning",
                "FuelWarning", "LowAltitudeWarning", "OverspeedWarning", "StallWarning",
                "LandingGearWarning", "CanopyWarning");
        }

        [Test]
        public void Avionics_ExposesExplicitHudModeAndStartupPresetFields()
        {
            FieldInfo hudMode = typeof(AvionicsState).GetField("HudMode", BindingFlags.Public | BindingFlags.Instance);
            FieldInfo activeStartupPreset = typeof(AvionicsState).GetField(
                "ActiveStartupPreset", BindingFlags.Public | BindingFlags.Instance);

            Assert.That(hudMode, Is.Not.Null);
            Assert.That(hudMode.FieldType, Is.EqualTo(typeof(HudMode)));
            Assert.That(Enum.GetNames(typeof(HudMode)), Is.EqualTo(new[] { "Off", "Nav", "Landing", "AirToAir" }));
            Assert.That(activeStartupPreset, Is.Not.Null);
            Assert.That(activeStartupPreset.FieldType, Is.EqualTo(typeof(StartupPreset)));
        }

        [Test]
        public void StoreAndTrackCollections_EnforceFixedCapacitiesWithoutPublicArrays()
        {
            AssertFixedCollection("StoreStationCollection", 9);
            AssertFixedCollection("StoreLoadoutCollection", 9);
            AssertFixedCollection("TacticalTrackCollection", 32);

            Assert.That(typeof(StoresState).GetField("Stations").FieldType.Name, Is.EqualTo("StoreStationCollection"));
            Assert.That(typeof(LoadoutConfigurationCommand).GetField("Stations").FieldType.Name, Is.EqualTo("StoreLoadoutCollection"));
            Assert.That(typeof(TacticalPictureState).GetField("Tracks").FieldType.Name, Is.EqualTo("TacticalTrackCollection"));
        }

        private static void AssertFieldTypes(Type contractType, IDictionary<string, Type> expectedFields)
        {
            foreach (KeyValuePair<string, Type> expected in expectedFields)
            {
                FieldInfo field = contractType.GetField(expected.Key, BindingFlags.Public | BindingFlags.Instance);
                Assert.That(field, Is.Not.Null, contractType.Name + "." + expected.Key);
                Assert.That(field.FieldType, Is.EqualTo(expected.Value), contractType.Name + "." + expected.Key);
            }
        }

        private static void AssertFieldNames(Type contractType, params string[] expectedFields)
        {
            foreach (string fieldName in expectedFields)
            {
                Assert.That(
                    contractType.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance),
                    Is.Not.Null,
                    contractType.Name + "." + fieldName);
            }
        }

        private static void AssertFixedCollection(string typeName, int expectedCapacity)
        {
            Type collectionType = typeof(AircraftSystemsState).Assembly.GetType(
                "FlightSim.Platform.Contracts." + typeName);
            Assert.That(collectionType, Is.Not.Null, typeName);
            Assert.That(
                collectionType.GetFields(BindingFlags.Public | BindingFlags.Instance).Any(field => field.FieldType.IsArray),
                Is.False,
                typeName + " must not expose a public array");

            object collection = Activator.CreateInstance(collectionType);
            PropertyInfo length = collectionType.GetProperty("Length", BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo indexer = collectionType.GetProperty("Item", BindingFlags.Public | BindingFlags.Instance);
            Assert.That(length, Is.Not.Null, typeName + ".Length");
            Assert.That(length.GetValue(collection, null), Is.EqualTo(expectedCapacity));
            Assert.That(indexer, Is.Not.Null, typeName + " indexer");

            TargetInvocationException exception = Assert.Throws<TargetInvocationException>(
                () => indexer.GetValue(collection, new object[] { expectedCapacity }));
            Assert.That(exception.InnerException, Is.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
