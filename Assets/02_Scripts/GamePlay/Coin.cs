using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour, IScoreChanger
{
    [SerializeField] private int _cost = 1;
    public int AddScoreAmount => +_cost;
    GameObject IScoreChanger.gameObject => base.gameObject;
}

internal interface IScoreChanger : IInteractable
{
    int AddScoreAmount { get; }
    GameObject gameObject { get; }
}