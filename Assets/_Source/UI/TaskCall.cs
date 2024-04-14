using UnityEngine;

public class TaskCall : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TaskEnable()
    {
        _animator.SetInteger("state", 2);
    }

    public void TaskDisable()
    {
        _animator.SetInteger("state", 1);
    }
}
