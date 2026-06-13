using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Scripts.Gameplay
{
    public class GamePresenter : IStartable
    {
        private WavesSpawner _wavesSpawner;

        public GamePresenter(WavesSpawner wavesSpawner)
        {
            _wavesSpawner = wavesSpawner;
        }

        public void Start()
        {
            _wavesSpawner.StartWaves().Forget();
        }
    }
}