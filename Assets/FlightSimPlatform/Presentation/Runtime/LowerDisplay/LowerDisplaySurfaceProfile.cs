using UnityEngine;
namespace FlightSim.Platform.Presentation.LowerDisplay
{
    [CreateAssetMenu(menuName="FlightSim/Lower display surface")]
    public sealed class LowerDisplaySurfaceProfile : ScriptableObject
    {
        public string VisualRootName="F35_Visual";
        public string RendererPath="F35_Cockpit/F35_Model/Object232_f35_mfd_0";
        public int MaterialIndex;
        public Vector3 BottomLeft,TopLeft,TopRight,BottomRight;
    }
}
