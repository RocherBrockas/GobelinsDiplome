using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummyscript : MonoBehaviour
{
    public string[] songs;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string s in songs)
        {
            AudioManager.instance.Play(s);
        }
    }
}
