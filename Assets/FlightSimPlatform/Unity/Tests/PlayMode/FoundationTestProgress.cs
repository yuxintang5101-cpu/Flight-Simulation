using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestRunner;
[assembly:TestRunCallback(typeof(FlightSim.Platform.Unity.Tests.PlayMode.FoundationTestProgress))]
namespace FlightSim.Platform.Unity.Tests.PlayMode
{
    public sealed class FoundationTestProgress : ITestRunCallback
    {
        public void RunStarted(ITest test) { Debug.Log("FOUNDATION TEST RUN START"); }
        public void RunFinished(ITestResult result) { Debug.Log("FOUNDATION TEST RUN END " + result.ResultState); }
        public void TestStarted(ITest test) { if(!test.IsSuite) Debug.Log("FOUNDATION TEST START " + test.FullName); }
        public void TestFinished(ITestResult result) { if(!result.Test.IsSuite) Debug.Log("FOUNDATION TEST END " + result.Test.Name + " " + result.ResultState); }
    }
}
