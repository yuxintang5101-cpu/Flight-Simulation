using UnityEngine;
namespace FlightSim.Platform.Presentation.LowerDisplay
{
    [DefaultExecutionOrder(100)]
    public sealed class LowerDisplayRenderDriver : MonoBehaviour
    {
        public LowerDisplayController Owner;
        private void LateUpdate(){if(Owner!=null&&Owner.isActiveAndEnabled)Owner.RenderLate();}
    }
}
