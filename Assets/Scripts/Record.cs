using UnityEngine;
using TMPro;

public class Record : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private TextMeshProUGUI _prizeText;
    [SerializeField] private GameObject _newRecordPanel;

    private void Awake()
    {
        ShowPrize();
        WriteNewRecord();
    }

    public int CalCalculateRecord()
    {
        return _gameState.Level * _gameState.Points * 15;
    }

    private void ShowPrize()
    {
        _prizeText.text = CalCalculateRecord().ToString();
    }

    private void WriteNewRecord()
    {
        if(PlayerPrefs.GetInt("Record") < CalCalculateRecord())
        {
            PlayerPrefs.SetInt("Record", CalCalculateRecord());
            _newRecordPanel.SetActive(true);
        }
    }
}
