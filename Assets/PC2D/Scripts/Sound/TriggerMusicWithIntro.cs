using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusicWithIntro : MonoBehaviour
{
    public string musicToPlayIntro;
    public string musicToPlayLoop;
    public string[] musicToStop;
    public bool played;


    private const float volume = 0.750f;
    private void Start()
    {
        played = AudioManager.instance.IsPlayed(musicToPlayLoop);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!played && collision.CompareTag("Player"))
        {
            if (musicToStop != null)
            {
                Debug.Log("here");
                foreach (string s in musicToStop)
                {
                    AudioManager.instance.SetVolume(s, 0f);
                    AudioManager.instance.Stop(s);
                }
            }
            AudioManager.instance.PlayAfter(musicToPlayIntro, musicToPlayLoop);
            played = true;
        }
    }
}
