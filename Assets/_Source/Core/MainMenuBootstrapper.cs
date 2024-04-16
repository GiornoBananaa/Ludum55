using System;
using AudioSystem;
using UI;
using UnityEngine;

namespace Core
{
    public class MainMenuBootstrapper : MonoBehaviour
    {
        private const string AUDIO_DATA_PATH = "AudioDataSO";
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private SettingsPanel _settingsPanel;
        
        private AudioDataSO _audioDataSo;
        
        private void Awake()
        {
            _audioDataSo = Resources.Load<AudioDataSO>(AUDIO_DATA_PATH);
            _audioPlayer.Construct(_audioDataSo);
            _settingsPanel.Construct(_audioPlayer);
        }
    }
}
