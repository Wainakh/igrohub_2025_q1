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
    private List<IInteractable> _interactables;
    private IUserInterface _ui;
    private IDialogManager _dialog;

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

        _camera.SetFollowTarget(_player.transform);
        yield break;
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {

        }
    }

    private IInputSystem CreateInput()
    {
        throw new System.NotImplementedException();
    }

    private IPlayer CreatePlayer()
    {
        throw new System.NotImplementedException();
    }
}

//Персонаж - перемещение (lerp от точки к точке)
public interface IPlayer
{
    Transform transform { get; }
    void SetInput(IInputSystem input);
}

//Инпут - начать с update -> перенести в интерфейс -> реализовать на ПК -> оставить реализацию на мобилку
public interface IInputSystem{}

//Интеракты - начать с базового взаимодействия (на OnTrigger). Получение очков
public interface IInteractable{}

//UI - верстка, MVC, где то тут - GameManager
public interface IUserInterface{}

//Диалоги - DialogManager, верстка префаба диалогового окна, Scriptable object. ЗАгрузка конфигов диалогов из ресурсов. Диалог стартер
public interface IDialogManager{}
