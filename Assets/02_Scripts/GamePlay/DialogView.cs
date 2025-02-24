using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private List<CharacterHandler> _characters = new List<CharacterHandler>();

    private Action _onClick;

    private void Awake()
    {
        _button.onClick.AddListener(FinishSpeech);
    }

    private void FinishSpeech()
    {
        _onClick?.Invoke();
    }

    public void Show(SpeechData data, Action onFinish)
    {
        _onClick = onFinish;
        _text.text = data.SpeechText;

        foreach (var character in _characters)
            character.GameObject.SetActive(character.CharacterKey.Equals(data.CharacterKey));
        
        gameObject.SetActive(true);
    }

    public void HideAll()
    {
        _text.text = string.Empty;
        _onClick = null;
        gameObject.SetActive(false);
        foreach (var character in _characters)
            character.GameObject.SetActive(false);
    }
}