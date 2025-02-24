using UnityEngine;

namespace Igrohub
{
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour, IScoreChanger
    {
        [SerializeField] private int _cost = 1;
        public int AddScoreAmount => +_cost;
        GameObject IInteractable.gameObject => base.gameObject;
    }
}