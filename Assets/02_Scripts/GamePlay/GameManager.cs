using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        yield return new WaitForSeconds(1f);
        yield return Initialization();
        yield return GameLoop();
    }

    private IEnumerator Initialization()
    {
        _input = CreateInput();

        _player = CreatePlayer();
        _player.SetInput(_input);
        _player.OnInteracted += Interact;

        _camera = CreateCamera();
        _camera.SetFollowTarget(_player.transform);

        _interactables = new List<IInteractable>(transform.GetComponentsInChildren<IInteractable>());
        yield break;
    }

    private CameraFollower CreateCamera() => FindFirstObjectByType<CameraFollower>();

    private void Interact(IPlayer player, IInteractable obj)
    {
        switch (obj)
        {
            case IScoreChanger coin:
                // _gameData.Score += coin.AddScoreAmount;
                coin.gameObject.SetActive(false);
                break;
            case IDialogHandler dialog:
                break;
        }
    }

    private IEnumerator GameLoop()
    {

        while (true)
        {
            TurnOnInteractedObjects();
            
            // Ждем финального диалога 
            yield return LevelFinish();
            
            yield break;
        }
    }

    private void TurnOnInteractedObjects()
    {
        foreach (var interactable in _interactables)
            interactable.gameObject.SetActive(true);
    }

    private IEnumerator LevelFinish()
    {
        while (true)
            yield return null;
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

    private IPlayer CreatePlayer() => FindFirstObjectByType<Player>();
}