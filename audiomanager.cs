using UnityEngine;
using UnityEngine.Audio;
using System;

public class audiomanager : MonoBehaviour
{
    public static audiomanager instance;
    public Sound[] sounds;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
