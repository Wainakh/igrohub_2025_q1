using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Igrohub
{
    public class GameManager : MonoBehaviour
    {
        private CameraFollower _camera;
        private IPlayer _player;
        private IInputSystem _input;
        private IPlayerDataController _playerDataController;
        private List<ITickable> _tickables = new List<ITickable>();


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
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var tickable in _tickables)
                tickable.Tick(deltaTime);
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

        private IInputSystem CreateInput()
        {
            var input = new DesktopInputSystem();
            _tickables.Add(input);
            return input;
        }

        private IPlayer CreatePlayer()
        {
            var player = FindFirstObjectByType<Player>();
            _tickables.Add(player);
            return player;
        }

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