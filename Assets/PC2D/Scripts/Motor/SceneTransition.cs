using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public LoadPositions positionStorage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & !collision.isTrigger)
        {
            positionStorage.initialValue = playerPosition;
            positionStorage.perception = PerceptionManager.instance.perception;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
