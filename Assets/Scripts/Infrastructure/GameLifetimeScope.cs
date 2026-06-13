using Scripts.Configs;
using Scripts.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        // enemy stuff
        [SerializeField]
        private WavesConfig wavesConfig;
        [SerializeField]
        private Transform enemiesRoot;
        [SerializeField]
        private Collider floor;
        // tower
        [SerializeField]
        private TowerView towerView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GamePresenter>();
            RegistryEnemy(builder);
        }

        private void RegistryEnemy(IContainerBuilder builder)
        {
            builder.RegisterInstance(wavesConfig);
            builder.Register<WavesSpawner>(Lifetime.Scoped);
            builder.Register<EnemyFactory>(Lifetime.Scoped)
                .WithParameter(typeof(Transform), enemiesRoot)
                .WithParameter(typeof(TowerView), towerView)
                .WithParameter(typeof(Collider), floor); 
            builder.Register<EnemyRegistry>(Lifetime.Scoped);
        }
    }
}