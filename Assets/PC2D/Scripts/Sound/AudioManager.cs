using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play( string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name : " + name + " was not found.");
            return;

        }
        s.source.Play();
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name : " + name + " was not found.");
            return;

        }
        if (s.source.isPlaying)
            s.source.volume = volume;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name : " + name + " was not found.");
            return;

        }
        s.source.Stop();
    }

    public bool IsPlayed(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name : " + name + " was not found.");
            return false;
        }
        return s.played;
    }

    public void SetPlayed(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name : " + name + " was not found.");
            return ;
        }
        s.played = true;
    }

    public void PlayAfter(string firstSound, string secondSound)
    {
        Sound sound1 = Array.Find(sounds, sound => sound.name == firstSound);
        Sound sound2 = Array.Find(sounds, sound => sound.name == secondSound);
        if ( sound1 == null || sound2 == null)
        {
            Debug.LogWarning("Sound with name : " + sound1 + " or " + sound2 + " was not found.");
            return;
        }
        sound1.source.Play();
        StartCoroutine(WaitforSong(sound1, sound2));
    }

    IEnumerator WaitforSong(Sound s1, Sound s2)
    {
        yield return new WaitForSeconds(s1.source.clip.length);
        s1.source.Stop();
        s2.source.Play();
    }
}
