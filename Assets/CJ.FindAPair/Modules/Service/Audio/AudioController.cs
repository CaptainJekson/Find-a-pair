using CJ.FindAPair.Modules.Service.Audio.Configs;
using CJ.FindAPair.Utility;
using UnityEngine;

namespace CJ.FindAPair.Modules.Service.Audio
{
    public class AudioController: MonoBehaviour
    {
        [SerializeField] private AudioClipsCollection _audioClipsCollection;
        [SerializeField] private AudioSource _audioSourcePrefab;

        private AudioSource _musicSource;
        private AudioSource _soundSource;

        public AudioClipsCollection AudioClipsCollection => _audioClipsCollection;
        public bool IsMusicMute => !_musicSource.mute;
        public bool IsSoundsMute => !_soundSource.mute;

        private void Awake()
        {
            _musicSource = Instantiate(_audioSourcePrefab, transform);
            _soundSource = Instantiate(_audioSourcePrefab, transform);
        
            _soundSource.mute = PlayerPrefs.GetString(PlayerPrefsKeys.SoundsState) == "Off";
            _musicSource.mute = PlayerPrefs.GetString(PlayerPrefsKeys.MusicState) == "Off";
        }

        public void PlayMusic(AudioClip clip, bool isLoop = true)
        {
            _musicSource.loop = isLoop;
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    
        public void PlaySound(AudioClip clip, bool isSeveral = false)
        {
            if (isSeveral)
            {
                _soundSource.clip = clip;
                _soundSource.Play();
            }
            else
            {
                _soundSource.PlayOneShot(clip);
            }
        }

        public void SetMusicState(bool isMute)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.MusicState, isMute ? "On" : "Off");
        
            _musicSource.mute = !isMute;
            _musicSource.Play();
        }
    
        public void SetSoundsState(bool isMute)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.SoundsState, isMute ? "On" : "Off");
        
            _soundSource.mute = !isMute;
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }
    }
}