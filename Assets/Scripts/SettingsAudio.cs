using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] SoundsGroup _sounds;

    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundToggle;

    public void Init()
    {
        InitStartSettingAudio("Music", _musicToggle);
        InitStartSettingAudio("Sound", _soundToggle);
    }

    public void OnToggleMusicClick()
    {
        if (_musicToggle.isOn == true)
        {
            _music.enabled = true;
            PlayerPrefs.SetString("Music", "Yes");
        }
        else
        {
            _music.enabled = false;
            PlayerPrefs.SetString("Music", "No");
        }
    }

    public void OnToggleSoundClick()
    {
        if (_soundToggle.isOn == true)
        {
            _sounds.EnableSounds(true);
            PlayerPrefs.SetString("Sound", "Yes");
        }
        else
        {
            _sounds.EnableSounds(false);
            PlayerPrefs.SetString("Sound", "No");
        }
    }

    private void InitStartSettingAudio(string settingName, Toggle toggle)
    {
        if (PlayerPrefs.GetString(settingName) != "No" && PlayerPrefs.GetString(settingName) != "Yes")
        {
            PlayerPrefs.SetString(settingName, "Yes");
        }

        if (PlayerPrefs.GetString(settingName) == "Yes")
            toggle.isOn = true;
        else if (PlayerPrefs.GetString(settingName) == "No")
            toggle.isOn = false;
    }
}
