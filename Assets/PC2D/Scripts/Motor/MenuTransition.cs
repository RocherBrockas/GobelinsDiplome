using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuTransition : MonoBehaviour
{
    public LoadPositions positionStorage;
    public LoadPositions startposition;
    public LevelLoader LevelLoader;
    public string sceneToLoad;
    public bool end;
    public FluxMemory[] triggers;

    private void Start()
    {
        if (!end || AudioManager.instance.IsPlayed("First theme"))
        AudioManager.instance.Play("First theme");
    }

    public void StartGame()
    {
        foreach(FluxMemory f in triggers)
        {
            f.active = false;
        }
        positionStorage.initialValue = startposition.initialValue;
        positionStorage.perception = startposition.perception;
        LevelLoader.LoadNextLevel(sceneToLoad);
        AudioManager.instance.Play("BackGround Theme");
        if (PauseMenu.instance)
        {
            for(int i = 0; i < PauseMenu.instance.activeTab.Length; ++i)
            {
                PauseMenu.instance.activeTab[i] = false;
            }
            foreach(GameObject g in PauseMenu.instance.morceauMasque)
            {
                g.SetActive(false);
            }
        }
    }

    public void toMenu()
    {
        LevelLoader.LoadNextLevel("Main Menu");
    }

    public void toCredits()
    {
        LevelLoader.LoadNextLevel("EndSceneVS");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
