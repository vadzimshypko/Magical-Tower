using Cysharp.Threading.Tasks;
using Scripts.Gameplay.Enemies;
using Scripts.Gameplay.Spells;
using VContainer.Unity;

namespace Scripts.Gameplay
{
    public class GamePresenter : IStartable
    {
        private readonly WavesSpawner _wavesSpawner;
        private readonly EnemyRegistry _enemyRegistry;
        private readonly GameResultView _gameResultView;
        private readonly TowerView _towerView;
        private readonly SpellService  _spellService;

        public GamePresenter(
            WavesSpawner wavesSpawner,
            EnemyRegistry enemyRegistry,
            GameResultView gameResultView,
            TowerView towerView,
            SpellService spellService)
        {
            _wavesSpawner = wavesSpawner;
            _enemyRegistry = enemyRegistry;
            _gameResultView = gameResultView;
            _towerView = towerView;
            _spellService =  spellService;
        }

        public void Start()
        {
            StartGameLoop().Forget();
            _towerView.Died += EntryLoseState;
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
            _spellService.Stop();
            _gameResultView.ShowWin();
        }
        
        private void EntryLoseState(IHealth _)
        {
            _spellService.Stop();
            _gameResultView.ShowLose();
        }

        private bool IsWinConditionMet()
        { 
            return !_enemyRegistry.HasEnemiesLeft;
        }
    }
}