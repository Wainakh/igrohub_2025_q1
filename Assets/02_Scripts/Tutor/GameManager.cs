using UnityEngine;

namespace Igrohub
{
    public class GameManager : MonoBehaviour
    {
        private IPlayerDataController _playerDataController;

        private void Start()
        {
            _playerDataController = CreateDataController();
        }

        //TEMP FOR TEST
        public void AddToScore(int score)
        {
            _playerDataController.AddToScore(score);
        }
        
        private PlayerDataController CreateDataController()
        {
            var data = new PlayerData();
            var view = CreatePlayerInterface();
            return new PlayerDataController(view, data);
        }

        private IUserInterface CreatePlayerInterface() => FindFirstObjectByType<PlayerWidget>();
    }
}