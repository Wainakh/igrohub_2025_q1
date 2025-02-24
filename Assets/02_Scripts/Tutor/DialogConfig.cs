using System.Collections.Generic;
using UnityEngine;

namespace Igrohub
{
    [CreateAssetMenu(menuName = "Configs/Tutor Dialog Config")]
    public class DialogConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private List<SpeechData> _speechData = new List<SpeechData>();
        public int Id => _id;
        public List<SpeechData> GetSpeeches() => _speechData;
    }
}