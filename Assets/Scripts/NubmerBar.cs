using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NubmerBar : MonoBehaviour
{
    [SerializeField] private string _text; 
    private TextMeshProUGUI _levelText;

    public int Number { private get; set; }

    private void Awake()
    {
        _levelText = GetComponent<TextMeshProUGUI>();      
    }

    private void Start()
    {
        OnShowText();
    }

    public void OnShowText()
    {
        _levelText.text = $"{_text}: {Number.ToString()}";
    }
}
