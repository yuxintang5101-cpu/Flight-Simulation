using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [CreateAssetMenu(menuName="FlightSim/Effect Profile", fileName="DefaultEffects")]
    public sealed class FlightEffectProfile : ScriptableObject
    {
        [Range(1,32)] public int MaximumInstances = 12;
        [Range(20,300)] public int ExplosionParticles = 100;
        [Range(5,100)] public float FireEmission = 32;
        [Range(3,60)] public float SmokeEmission = 14;
        [Range(2,60)] public float DebrisFireSeconds = 18;
        [Range(.2f,5)] public float SizeMultiplier = 1;
        public Color FlameColor = new Color(1,.34f,.035f);
        public Color SmokeColor = new Color(.14f,.16f,.18f,.65f);
    }
}
