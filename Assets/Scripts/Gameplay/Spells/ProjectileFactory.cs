using System;
using Scripts.Configs;
using Scripts.Gameplay.Enemies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Gameplay.Spells
{
    public class ProjectileFactory
    {
        private Transform _root;

        public ProjectileFactory(TowerView towerView)
        {
            _root = towerView.RootForSpells;
        }
        
        public void Create(SpellConfig spellConfig, Vector3 destination, Action<EnemyView, Vector3> onCollision)
        {
            var rotation = Quaternion.LookRotation(destination - _root.position);
            var projectileView = Object.Instantiate(spellConfig.projectilePrefab, _root.position, rotation, _root);
            projectileView.Setup(destination, spellConfig.projectileSpeed, onCollision);
        }
    }
}