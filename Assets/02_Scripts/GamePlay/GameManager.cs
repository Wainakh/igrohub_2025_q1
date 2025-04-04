using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager - god obj. Реализация результата интеракции, очки, запуск диалогов. Бинды интеракций
// Gameloop - старт, рестарт
public class GameManager : MonoBehaviour
{
    private CameraFollower _camera;
    private IPlayer _player;
    private IInputSystem _input;

    // Для запоминания и сброса при перезапуска уровня
    private List<IInteractable> _interactables = new List<IInteractable>();

    private IUserInterface _ui;
    private IDialogManager _dialog;
    private IPlayerData _data;

    private List<ITickable> _tickables = new List<ITickable>();
    private IPlayerDataController _playerDataController;

    // Для определения конца уровня
    private bool _isGameEnd;

    private IEnumerator Start()
    {
        yield return Initialization();
        yield return new WaitForSeconds(.1f);
        yield return GameLoop();
    }

    private IEnumerator Initialization()
    {
        _input = CreateInput();
        _input.Lock();

        _player = CreatePlayer();
        _player.SetInput(_input);
        _player.OnInteracted += Interact;

        _camera = CreateCamera();
        _camera.SetFollowTarget(_player.transform);

        _playerDataController = CreateDataController();

        _interactables = new List<IInteractable>(transform.GetComponentsInChildren<IInteractable>());

        _dialog = new DialogManager();

        yield break;
    }

    private PlayerDataController CreateDataController()
    {
        var data = new PlayerData();
        var view = CreatePlayerInterface();
        return new PlayerDataController(view, data);
    }

    private void Interact(IPlayer player, IInteractable obj)
    {
        switch (obj)
        {
            case IScoreChanger coin:
                Debug.Log($"Interact with {nameof(IScoreChanger)}. Increase score => {coin.AddScoreAmount}");
                _playerDataController.AddToScore(coin.AddScoreAmount);
                coin.gameObject.SetActive(false);

                if (_playerDataController.GetScore() == 1)
                    ShowDialog(1);
                break;
            case IDialogHandler dialog:
                Debug.Log($"Interact with {nameof(IDialogHandler)}. Start dialog");
                switch (_playerDataController.GetScore())
                {
                    case 0:
                        ShowDialog(4);
                        break;
                    case 5:
                        ShowDialog(2, SetGameEnd);
                        break;
                    default:
                        ShowDialog(3);
                        break;
                }

                break;
        }
    }

    private void SetGameEnd()
    {
        _playerDataController.SetScore(0);
        _isGameEnd = true;
    }

    private void ShowDialog(int id, Action onFinish = null)
    {
        _input.Lock();
        _dialog.StartDialog(id, () =>
        {
            _input.Unlock();
            onFinish?.Invoke();
        });
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            _isGameEnd = false;

            _input.Lock();

            ReturnPlayerToStartPosition();
            TurnOnInteractedObjects();

            yield return ShowStartMessage();

            _input.Unlock();

            yield return LevelFinish();

            Debug.Log($"Restart game");
        }
    }

    private void ReturnPlayerToStartPosition()
    {
        _player.transform.position = Vector3.zero;
    }

    private IEnumerator ShowStartMessage()
    {
        var wait = new WaitForSeconds(1f);
        for (var i = 3; i > 0; i--)
        {
            Debug.Log($"Start in {i}");
            yield return wait;
        }

        Debug.Log($"Start game");
    }

    private void TurnOnInteractedObjects()
    {
        foreach (var interactable in _interactables)
            interactable.gameObject.SetActive(true);
    }

    private IEnumerator LevelFinish()
    {
        // Ждем финального диалога 
        while (!_isGameEnd)
        {
            if (Vector3.Distance(_player.transform.position, Vector3.zero) > 20)
            {
                Debug.Log($"Finish game");
                yield break;
            }

            yield return null;
        }
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        foreach (var tickable in _tickables)
            tickable.Tick(deltaTime);
    }

    private IInputSystem CreateInput()
    {
        var input = new DesktopInputSystem();
        _tickables.Add(input);
        return input;
    }

    private IUserInterface CreatePlayerInterface() => FindFirstObjectByType<PlayerWidget>();
    private CameraFollower CreateCamera() => FindFirstObjectByType<CameraFollower>();
    private IPlayer CreatePlayer() => FindFirstObjectByType<Player>();
}