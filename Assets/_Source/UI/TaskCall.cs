using AudioSystem;
using UnityEngine;

public class TaskCall : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private AudioPlayer _audioPlayer;
    private static readonly int _state = Animator.StringToHash("state");

    public void Construct(AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }

    public void TaskEnable()
    {
        _audioPlayer.Play(Sounds.TaskPad);
        _animator.SetInteger(_state, 2);
    }

    public void TaskDisable()
    {
        _audioPlayer.Play(Sounds.TaskPad);
        _animator.SetInteger(_state, 1);
    }
}
