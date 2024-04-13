using UnityEngine;
using UnityEngine.UI;

namespace AudioSystem
{
    [RequireComponent(typeof(Button))]
    public class ButtonSound : SoundPlayer
    {
        [SerializeField] private Button _button;
        
        private void PlayClickSound()
        {
            AudioPlayer.Play(Sounds.ButtonClick);
        }
        
        private void Awake()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(PlayClickSound);
            }
        }
    }
}