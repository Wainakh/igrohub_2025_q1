using UnityEngine;
//Change score when interact
internal interface IScoreChanger : IInteractable
{
    int AddScoreAmount { get; }
    GameObject gameObject { get; }
}