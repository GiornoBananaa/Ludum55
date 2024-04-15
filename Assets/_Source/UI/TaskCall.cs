using AudioSystem;
using UnityEngine;

public class TaskCall : MonoBehaviour
{
    private Animator _animator;
    
    private AudioPlayer _audioPlayer;

    public void Construct( AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TaskEnable()
    {
        _audioPlayer.Play(Sounds.TaskPad);
        _animator.SetInteger("state", 2);
    }

    public void TaskDisable()
    {
        _audioPlayer.Play(Sounds.TaskPad);
        _animator.SetInteger("state", 1);
    }
}
