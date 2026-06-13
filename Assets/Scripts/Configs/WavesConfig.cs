using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "WavesConfig", menuName = "ScriptableObjects/WavesConfig")]
    public class WavesConfig : ScriptableObject
    {
        public List<WaveConfig> Waves;
    }
}