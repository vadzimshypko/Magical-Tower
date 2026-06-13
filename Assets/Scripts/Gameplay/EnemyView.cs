using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Gameplay
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour
    { 
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField] 
        private Renderer renderer;
        
        private Action<EnemyView> _onEnemyDied;
        private TowerView _target;

        public void Init(TowerView towerView, Action<EnemyView> onEnemyDied)
        {
            _target = towerView;
            _onEnemyDied = onEnemyDied;
            agent.destination = _target.transform.position;
        }

        private void OnDestroy()
        {
            _onEnemyDied.Invoke(this);
        }

        public bool IsVisible()
        {
            return renderer.isVisible;
        }
    }
}