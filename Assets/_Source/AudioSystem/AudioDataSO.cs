using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioDataSO", menuName = "SO/AudioDataSO")]
    public class AudioDataSO: ScriptableObject
    {
        [field: SerializeField] public Sound[] Sounds { get; private set;}
        
        [HideInInspector]
        public AudioSource MusicSource;
        
        public Dictionary<Sounds,Sound> GetSounds()
        {
            return Sounds.ToDictionary(sound => sound.SoundType);
        }
    }
}