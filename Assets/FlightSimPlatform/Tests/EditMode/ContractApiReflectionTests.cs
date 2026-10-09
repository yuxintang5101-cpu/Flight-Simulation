using System;
using System.Linq;
using System.Reflection;
using FlightSim.Platform.Contracts;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class ContractApiReflectionTests
    {
        [Test]
        public void ContractVersion_IsPublicUshortConstantAndServiceProperty()
        {
            FieldInfo version = typeof(FlightSimulationContract).GetField(
                nameof(FlightSimulationContract.ContractVersion),
                BindingFlags.Public | BindingFlags.Static);
            PropertyInfo serviceVersion = typeof(IFlightSimulationService).GetProperty("ContractVersion");

            Assert.That(version, Is.Not.Null);
            Assert.That(version.FieldType, Is.EqualTo(typeof(ushort)));
            Assert.That(version.IsLiteral, Is.True);
            Assert.That(version.GetRawConstantValue(), Is.EqualTo((ushort)2));
            Assert.That(serviceVersion, Is.Not.Null);
            Assert.That(serviceVersion.PropertyType, Is.EqualTo(typeof(ushort)));
        }

        [Test]
        public void Submit_UsesReadonlyStructCommandGenericContract()
        {
            MethodInfo submit = typeof(IFlightSimulationService).GetMethods()
                .Single(method => method.Name == "Submit");
            Type genericParameter = submit.GetGenericArguments().Single();
            ParameterInfo commandParameter = submit.GetParameters().Single();
            GenericParameterAttributes attributes = genericParameter.GenericParameterAttributes;

            Assert.That(submit.IsGenericMethodDefinition, Is.True);
            Assert.That(commandParameter.ParameterType.IsByRef, Is.True);
            Assert.That(commandParameter.ParameterType.GetElementType(), Is.EqualTo(genericParameter));
            Assert.That(commandParameter.IsIn, Is.True);
            Assert.That(attributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint), Is.True);
            Assert.That(genericParameter.GetGenericParameterConstraints(), Does.Contain(typeof(ISimulationCommand)));
        }

        [Test]
        public void FlightSimulationService_ExposesExactPublicContract()
        {
            Type serviceType = typeof(IFlightSimulationService);
            PropertyInfo[] properties = serviceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] methods = serviceType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => !method.IsSpecialName)
                .ToArray();

            Assert.That(properties.Select(property => property.Name), Is.EqualTo(new[] { "ContractVersion" }));
            Assert.That(properties[0].PropertyType, Is.EqualTo(typeof(ushort)));
            Assert.That(properties[0].CanRead, Is.True);
            Assert.That(properties[0].CanWrite, Is.False);
            Assert.That(
                methods.Select(method => method.Name),
                Is.EquivalentTo(new[] { "TryGetLatest", "Submit", "RegisterSink", "UnregisterSink" }));

            MethodInfo tryGetLatest = methods.Single(method => method.Name == "TryGetLatest");
            ParameterInfo[] tryGetLatestParameters = tryGetLatest.GetParameters();
            Assert.That(tryGetLatest.ReturnType, Is.EqualTo(typeof(bool)));
            Assert.That(tryGetLatestParameters.Length, Is.EqualTo(2));
            Assert.That(tryGetLatestParameters[0].ParameterType, Is.EqualTo(typeof(AircraftId)));
            Assert.That(tryGetLatestParameters[1].ParameterType.IsByRef, Is.True);
            Assert.That(tryGetLatestParameters[1].ParameterType.GetElementType(), Is.EqualTo(typeof(AircraftSnapshot)));
            Assert.That(tryGetLatestParameters[1].IsOut, Is.True);

            MethodInfo submit = methods.Single(method => method.Name == "Submit");
            Assert.That(submit.ReturnType, Is.EqualTo(typeof(CommandResult)));

            MethodInfo registerSink = methods.Single(method => method.Name == "RegisterSink");
            Assert.That(registerSink.ReturnType, Is.EqualTo(typeof(void)));
            Assert.That(registerSink.GetParameters()
                .Select(parameter => parameter.ParameterType), Is.EqualTo(new[] { typeof(IFlightTelemetrySink) }));

            MethodInfo unregisterSink = methods.Single(method => method.Name == "UnregisterSink");
            Assert.That(unregisterSink.ReturnType, Is.EqualTo(typeof(void)));
            Assert.That(unregisterSink.GetParameters()
                .Select(parameter => parameter.ParameterType), Is.EqualTo(new[] { typeof(IFlightTelemetrySink) }));
        }

        [Test]
        public void TelemetrySinkCallbacks_UseReadonlyReferences()
        {
            AssertReadonlyCallback("OnFastState", typeof(AircraftFastState));
            AssertReadonlyCallback("OnSystemsState", typeof(AircraftSystemsState));
            AssertReadonlyCallback("OnTacticalPictureState", typeof(TacticalPictureState));
            AssertReadonlyCallback("OnSimulationEvent", typeof(SimulationEvent));
        }

        [Test]
        public void StartupPreset_HasExactApprovedNames()
        {
            Assert.That(
                Enum.GetNames(typeof(StartupPreset)),
                Is.EqualTo(new[] { "ColdAndDark", "RunwayReady", "Airborne" }));
        }

        private static void AssertReadonlyCallback(string methodName, Type valueType)
        {
            MethodInfo method = typeof(IFlightTelemetrySink).GetMethod(methodName);
            Assert.That(method, Is.Not.Null, methodName);
            ParameterInfo parameter = method.GetParameters().Single();
            Assert.That(parameter.ParameterType.IsByRef, Is.True, methodName);
            Assert.That(parameter.ParameterType.GetElementType(), Is.EqualTo(valueType), methodName);
            Assert.That(parameter.IsIn, Is.True, methodName);
        }
    }
}
