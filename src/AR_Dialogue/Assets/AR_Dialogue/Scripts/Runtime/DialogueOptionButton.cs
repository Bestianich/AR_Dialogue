using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DialogueOptionButton : MonoBehaviour
{
    private Button _button;
    private string _dialogueText;
    private string _outPortField;
    private Actor _actor;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Init(string dialogueText, string outPortField, Actor actor)
    {
        _dialogueText = dialogueText;
        _outPortField = outPortField;
        _actor = actor;
        _button.GetComponentInChildren<TextMeshProUGUI>().text = dialogueText;
    }

    public void OnClick()
    {
        _actor.NextNode(_outPortField);
        _actor.Parse();
    }
    
}
