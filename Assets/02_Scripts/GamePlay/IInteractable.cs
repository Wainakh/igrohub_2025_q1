//Интеракты - начать с базового взаимодействия (на OnTrigger). Получение очков

using UnityEngine;

namespace ReadyGamePlay
{
    public interface IInteractable
    {
        GameObject gameObject { get; }
    }
}