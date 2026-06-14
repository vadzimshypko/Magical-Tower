using Cysharp.Threading.Tasks;
using Scripts.Gameplay.Enemies;
using VContainer.Unity;

namespace Scripts.Gameplay
{
    public class GamePresenter : IStartable
    {
        private readonly WavesSpawner _wavesSpawner;
        private readonly EnemyRegistry _enemyRegistry;
        private readonly GameResultView _gameResultView;
        private readonly TowerView _towerView;

        public GamePresenter(
            WavesSpawner wavesSpawner,
            EnemyRegistry enemyRegistry,
            GameResultView gameResultView,
            TowerView towerView)
        {
            _wavesSpawner = wavesSpawner;
            _enemyRegistry = enemyRegistry;
            _gameResultView = gameResultView;
            _towerView = towerView;
        }

        public void Start()
        {
            StartGameLoop().Forget();
            _towerView.Death += EntryLoseState;
        }

        private async UniTask StartGameLoop()
        {
            await _wavesSpawner.StartWaves();
            WaitWhenPlayerWon();
        }

        private void WaitWhenPlayerWon()
        {
            if (IsWinConditionMet())
            {
                EntryWinState();
            }
            else
            {
                _enemyRegistry.AllEnemiesDefeated += EntryWinState;
            }
        }

        private void EntryWinState()
        {
            _gameResultView.ShowWin();
        }
        
        private void EntryLoseState(IDamagable _)
        {
            _gameResultView.ShowLose();
        }

        private bool IsWinConditionMet()
        { 
            return !_enemyRegistry.HasEnemiesLeft;
        }
    }
}