using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    public static Sound[] _sounds;

    private void Awake()
    {
        _sounds = sounds;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
        }
    }

    private void Start() { Play("Music"); }

    public static void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.source.Play();

        Debug.Log("play " + name);
    }


    [Serializable]
    public class Sound{
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)] public float volume;
        public bool loop;

        [HideInInspector] public AudioSource source;
    }
    
    enum SoundName
    {
        MusicBackground,
        movingSound,
        winSound,
        loseSound
    }
}
