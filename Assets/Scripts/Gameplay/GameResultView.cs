using UnityEngine;

namespace Scripts.Gameplay
{
    public class GameResultView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject winScreen;
        [SerializeField]
        private GameObject loseScreen;

        private void Awake()
        {
            Hide();
        }

        public void ShowWin()
        {
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
        }

        public void ShowLose()
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(true);
        }
        
        private void Hide()
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
        }

    }
}