using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private SettingsAudio _settingsSound;

    private void Start()
    {
        _recordText.text = "High Score: " + PlayerPrefs.GetInt("Record").ToString();
        _settingsSound.Init();
    }

    public void OnStartGameButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void OnSettingButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void OnExitGameButtonClick()
    {
        Application.Quit();
    }
}
