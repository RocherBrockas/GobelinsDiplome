using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public LoadPositions positionStorage;
    public LevelLoader LevelLoader;
    public bool door;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & !collision.isTrigger)
        {
            if (!door)
            {
                positionStorage.initialValue = playerPosition;
                positionStorage.perception = PerceptionManager.instance.perception;
                LevelLoader.LoadNextLevel(sceneToLoad);
            }
        }
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if( door && collision.CompareTag("Player") & !collision.isTrigger && (Input.GetButtonDown(PC2D.Input.INTERACT)))
        {
            Debug.Log("Up");
            positionStorage.initialValue = playerPosition;
            positionStorage.perception = PerceptionManager.instance.perception;
            LevelLoader.LoadNextLevel(sceneToLoad);
        }
    }
}
