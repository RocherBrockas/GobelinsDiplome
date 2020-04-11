using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public LoadPositions positionStorage;
    public LevelLoader LevelLoader;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & !collision.isTrigger)
        {
            positionStorage.initialValue = playerPosition;
            positionStorage.perception = PerceptionManager.instance.perception;
            LevelLoader.LoadNextLevel(sceneToLoad);
        }
    }
}
