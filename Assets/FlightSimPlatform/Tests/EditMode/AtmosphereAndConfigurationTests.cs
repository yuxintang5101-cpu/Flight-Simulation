using FlightSim.Platform.Core;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class AtmosphereAndConfigurationTests
    {
        [Test]
        public void IsaSeaLevelMatchesStandardDay()
        {
            AtmosphereSample sample = IsaAtmosphere.Sample(0.0);

            Assert.That(sample.TemperatureK, Is.EqualTo(288.15).Within(0.05));
            Assert.That(sample.PressurePa, Is.EqualTo(101325.0).Within(5.0));
            Assert.That(sample.DensityKgpm3, Is.EqualTo(1.225).Within(0.005));
            Assert.That(sample.SpeedOfSoundMps, Is.EqualTo(340.29).Within(0.5));
        }

        [Test]
        public void IsaAtKtexProducesPlausibleThinAir()
        {
            AtmosphereSample sample = IsaAtmosphere.Sample(2765.0);

            Assert.That(sample.DensityKgpm3, Is.EqualTo(0.935).Within(0.025));
            Assert.That(sample.PressurePa, Is.InRange(71000.0, 73500.0));
            Assert.That(sample.SpeedOfSoundMps, Is.InRange(328.0, 331.0));
        }

        [Test]
        public void DefaultF16ConfigurationUsesPublishedMassAndEnvelopeBaselines()
        {
            F16AircraftDefinition definition = F16AircraftDefinition.CreateDefault();

            Assert.That(definition.EmptyMassKg, Is.EqualTo(8936.0).Within(1.0));
            Assert.That(definition.InternalFuelCapacityKg, Is.EqualTo(3175.0).Within(1.0));
            Assert.That(definition.MaximumTakeoffMassKg, Is.EqualTo(16875.0).Within(1.0));
            Assert.That(definition.MaximumAfterburnerThrustN, Is.EqualTo(120000.0).Within(5000.0));
            Assert.That(definition.WingAreaM2, Is.InRange(27.0, 29.0));
            Assert.That(definition.MaximumLoadFactorG, Is.EqualTo(9.0).Within(0.01));
        }
    }
}
