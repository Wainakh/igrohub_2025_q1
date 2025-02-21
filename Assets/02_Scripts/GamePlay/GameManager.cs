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
    private List<IInteractable> _interactables = new List<IInteractable>(); // Для запоминания и сброса при перезапуска уровня
    private IUserInterface _ui;
    private IDialogManager _dialog;

    private List<ITickable> _tickables = new List<ITickable>();

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

        _interactables = new List<IInteractable>(transform.GetComponentsInChildren<IInteractable>());
        yield break;
    }

    private void Interact(IPlayer player, IInteractable obj)
    {
        switch (obj)
        {
            case IScoreChanger coin:
                // _gameData.Score += coin.AddScoreAmount;
                Debug.Log($"Interact with {nameof(IScoreChanger)}. Increase score => {coin.AddScoreAmount}");
                coin.gameObject.SetActive(false);
                break;
            case IDialogHandler dialog:
                Debug.Log($"Interact with {nameof(IDialogHandler)}. Start dialog");
                break;
        }
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
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
        while (true)
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

    private CameraFollower CreateCamera() => FindFirstObjectByType<CameraFollower>();
    private IPlayer CreatePlayer() => FindFirstObjectByType<Player>();
}