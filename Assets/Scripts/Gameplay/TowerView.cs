using UnityEngine;

namespace Scripts.Gameplay
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField]
        private Transform rootForSpells;
        
        public Transform RootForSpells => rootForSpells;
    }
}