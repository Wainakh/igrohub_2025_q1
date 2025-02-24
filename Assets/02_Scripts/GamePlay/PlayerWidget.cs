using UnityEngine;
using UnityEngine.UI;

namespace ReadyGamePlay
{
    public class PlayerWidget : MonoBehaviour, IUserInterface, MVC.IView
    {
        [SerializeField] private Text _scoreText;

        public void UpdateScoreText(int value)
        {
            _scoreText.text = $"Score: {value}";
        }
    }
}