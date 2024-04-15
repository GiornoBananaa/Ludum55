using AudioSystem;
using UnityEngine;

namespace UI
{
    public class TaskCall : MonoBehaviour
    {
        private static readonly int _state = Animator.StringToHash("state");
        [SerializeField] private Animator _animator;
    
        private AudioPlayer _audioPlayer;
    
        public bool IsOpened { get; private set; }
    
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            IsOpened = true;
        }

        public void TaskEnable()
        {
            IsOpened = true;
            _audioPlayer.Play(Sounds.TaskPad);
            _animator.SetInteger(_state, 2);
        }

        public void TaskDisable()
        {
            IsOpened = false;
            _audioPlayer.Play(Sounds.TaskPad);
            _animator.SetInteger(_state, 1);
        }
    }
}
