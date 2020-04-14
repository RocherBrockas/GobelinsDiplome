using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class MainMenu : MonoBehaviour
{
    public LoadPositions actualPositions;
    public LoadPositions startPositions;
    public LevelLoader Loader;

    public void BeginGame(string s)
    {
        actualPositions.initialValue = startPositions.initialValue;
        actualPositions.perception = startPositions.perception;
        Loader.LoadNextLevel(s);
    }
}
