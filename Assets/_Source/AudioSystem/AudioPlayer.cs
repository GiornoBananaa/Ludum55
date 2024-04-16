using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        
        private Dictionary<Sounds,Sound> _sounds;
        private Dictionary<Sounds,AudioSource> _soundSources;
        private Dictionary<Sounds,AudioSource> _spatialSources;
        
        public bool MusicIsMuted { get; private set; }
        public bool SoundIsMuted { get; private set; }
        public float SoundVolume { get; private set; }
        public float MusicVolume { get; private set; }

        public void Construct(AudioDataSO audioDataSo)
        {
            _sounds = audioDataSo.GetSounds();

            if (audioDataSo.MusicSource == null)
                audioDataSo.MusicSource = _musicSource;
            else
                _musicSource = audioDataSo.MusicSource;
            
            _soundSources = new Dictionary<Sounds, AudioSource>();
            _spatialSources = new Dictionary<Sounds, AudioSource>();
            
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume",1f);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume",1f);
            
            foreach (Sound sound in _sounds.Values)
            {
                AddSound(sound);
            }
            
            SoundVolumeChange(SoundVolume);
            MusicVolumeChange(MusicVolume);
        }

        private void Start()
        {
            foreach (var sound in _sounds.Values)
            {
                if (sound.PlayOnAwake)
                {
                    Play(sound);
                }
            }
            
        }

        public void SoundVolumeChange(float volume)
        {
            SoundVolume = volume;
            foreach (Sound sound in _sounds.Values)
            {
                if(sound.IsMusic) continue;
                
                sound.SetSourceVolume(_soundSources[sound.SoundType],SoundVolume);
                if (_spatialSources.TryGetValue(sound.SoundType, out var source))
                {
                    sound.SetSourceVolume(source,SoundVolume);
                }    
            }
        }
        
        public void MusicVolumeChange(float volume)
        {
            MusicVolume = volume;
            foreach (Sound sound in _sounds.Values)
            {
                if(!sound.IsMusic) continue;
                sound.SetSourceVolume(_soundSources[sound.SoundType],MusicVolume);
            }
        }
    
        public void EnableSound(bool enable)
        {
            SoundIsMuted = enable;
            foreach (AudioSource source in _soundSources.Values)
            {
                if(source == _musicSource) continue;
                    source.mute = !enable;
            }
            foreach (AudioSource source in _spatialSources.Values)
            {
                if(source == _musicSource) continue;
                source.mute = !enable;
            }
        }
        
        public void EnableMusic(bool enable)
        {
            MusicIsMuted = enable;
            
            _musicSource.mute = !enable;
        }
        
        private void AddSound(Sound sound)
        {
            AudioSource source;
            source = !sound.IsMusic ? gameObject.AddComponent<AudioSource>() : _musicSource;
            if(!sound.IsMusic)sound.SetSourceVariables(source,MusicVolume);
            _soundSources.Add(sound.SoundType,source);
        }
        
        public void Play(Sound sound)
        {
            Play(sound.SoundType);
        }
        
        public void Play(Sounds soundType)
        {
            if (_soundSources.TryGetValue(soundType, out var source))
            {
                Sound sound = _sounds[soundType];
                if (source == _musicSource)
                {
                    if(sound.Clip == _musicSource.clip)
                        return;
                    sound.SetSourceVariables(_musicSource,MusicVolume);
                }
                source.clip = sound.Clip;
                source.Play();
            }
            else
                Debug.LogWarning("Sound " + name + " not found");
        }
        
        public void RegisterSpatial(Sounds soundType, AudioSource audioSource)
        {
            if (_sounds.TryGetValue(soundType, out var sound))
            {
                sound.SetSourceVariables(audioSource,SoundVolume);
                
                _spatialSources.Add(soundType,audioSource);
            }
            else
                Debug.LogWarning("Sound " + name + " not found");
        }
        
        private void OnDestroy()
        {
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
            PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
            PlayerPrefs.Save();
        }
        
        public void Stop(Sounds soundType)
        {
            if (_soundSources.TryGetValue(soundType, out var source))
            {
                source.Stop();
            }
        }
    }
}
