using System.Collections;
using UnityEngine;

namespace Igrohub
{
    public class GameManager : MonoBehaviour
    {
        private CameraFollower _camera;
        private IPlayer _player;
        private IInputSystem _input;
        private IPlayerDataController _playerDataController;

        private IEnumerator Start()
        {
            yield return Initialization();
        }
        
        private IEnumerator Initialization()
        {
            _input = CreateInput();

            _player = CreatePlayer();
            _player.SetInput(_input);
            _player.OnInteracted += Interact;

            _camera = CreateCamera();
            _camera.SetFollowTarget(_player.transform);

            _playerDataController = CreateDataController();
            
            yield break;
        }
        
        private void Interact(IPlayer player, IInteractable obj)
        {
            if (obj is IScoreChanger coin)
            {
                Debug.Log($"Interact with {nameof(IScoreChanger)}. Increase score => {coin.AddScoreAmount}");
                _playerDataController.AddToScore(coin.AddScoreAmount);
                coin.gameObject.SetActive(false);
            }
        }

        private IInputSystem CreateInput() => FindFirstObjectByType<DesktopInputSystem>();

        private IPlayer CreatePlayer() => FindFirstObjectByType<Player>();

        private CameraFollower CreateCamera() => FindFirstObjectByType<CameraFollower>();

        private PlayerDataController CreateDataController()
        {
            var data = new PlayerData();
            var view = CreatePlayerInterface();
            return new PlayerDataController(view, data);
        }

        private IUserInterface CreatePlayerInterface() => FindFirstObjectByType<PlayerWidget>();
    }
}