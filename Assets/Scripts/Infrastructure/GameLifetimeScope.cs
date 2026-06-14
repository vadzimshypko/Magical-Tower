using Scripts.Configs;
using Scripts.Gameplay;
using Scripts.Gameplay.Enemies;
using Scripts.Gameplay.Spells;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Enemies")]
        [SerializeField]
        private WavesConfig wavesConfig;
        [SerializeField]
        private Transform enemiesRoot;
        [SerializeField]
        private Collider floor;
        [Header("Tower")]
        [SerializeField]
        private TowerView towerView;
        [Header("UI")]
        [SerializeField]
        private GameResultView  gameResultView;
        [SerializeField]
        private HealthBarController _healthBarControllerController;
        [Header("Spells")]
        [SerializeField]
        private Transform spellPanel;
        [SerializeField]
        private SpellButton spellButtonPrefab;
        [SerializeField]
        private FireballConfig fireballConfig;
        [SerializeField]
        private BarrageConfig barrageConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GamePresenter>();
            builder.RegisterInstance(towerView);
            builder.RegisterInstance(_healthBarControllerController);
            builder.RegisterInstance(gameResultView);
            RegistryEnemies(builder);
            RegistrySpells(builder);
        }

        private void RegistryEnemies(IContainerBuilder builder)
        {
            builder.RegisterInstance(wavesConfig);
            builder.Register<WavesSpawner>(Lifetime.Scoped);
            builder.Register<EnemyFactory>(Lifetime.Scoped)
                .WithParameter(typeof(Transform), enemiesRoot)
                .WithParameter(typeof(Collider), floor); 
            builder.Register<EnemyRegistry>(Lifetime.Scoped);
        }
        
        private void RegistrySpells(IContainerBuilder builder)
        {
            builder.RegisterInstance(spellButtonPrefab);
            builder.Register<ProjectileFactory>(Lifetime.Scoped);
            
            builder.Register<FireballSpell>(Lifetime.Scoped).As<ISpell>();
            builder.Register<BarrageSpell>(Lifetime.Scoped).As<ISpell>();
            
            builder.RegisterInstance(fireballConfig).As<SpellConfig>().AsSelf();
            builder.RegisterInstance(barrageConfig).As<SpellConfig>().AsSelf();
            
            builder.RegisterEntryPoint<SpellService>(Lifetime.Scoped)
                .WithParameter(typeof(Transform), spellPanel);
        }
    }
}