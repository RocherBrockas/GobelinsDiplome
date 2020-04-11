using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuTransition : MonoBehaviour
{
    public LoadPositions positionStorage;
    public LoadPositions startposition;
    public LevelLoader LevelLoader;
    public string sceneToLoad;

    public void StartGame()
    {
        positionStorage.initialValue = startposition.initialValue;
        positionStorage.perception = startposition.perception;
        LevelLoader.LoadNextLevel(sceneToLoad);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartGame();
        }
    }
}
