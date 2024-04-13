using UnityEngine;

namespace AudioSystem
{
    public abstract class SoundPlayer: MonoBehaviour
    {
        [SerializeField] protected AudioPlayer AudioPlayer;
    }
}