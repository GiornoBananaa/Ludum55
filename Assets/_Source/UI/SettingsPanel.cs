using System;
using AudioSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Button _closeButton;

        private AudioPlayer _audioPlayer;

        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }
        
        private void Awake()
        {
            if(_musicSlider!=null) _musicSlider.onValueChanged.AddListener(_audioPlayer.MusicVolumeChange);
            if(_soundSlider!=null) _soundSlider.onValueChanged.AddListener(_audioPlayer.SoundVolumeChange);
            if(_closeButton!=null) _closeButton!.onClick.AddListener(ClosePanel);

            _musicSlider.value = _audioPlayer.MusicVolume;
            _soundSlider.value = _audioPlayer.SoundVolume;
        }
        
        public void OpenPanel()
        {
            _panel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        
        public void ClosePanel()
        {
            Time.timeScale = 1;
            _panel.gameObject.SetActive(false);
        }
    }
}
