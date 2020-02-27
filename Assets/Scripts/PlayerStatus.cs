using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int health = 100;
    public int glidePower = 1;
    public bool isDead = false;
    public bool isInvulnerable = false;
    public GameObject deathEffect;
    public float invulnerabilityTimer = 1.0f;

    private PlayerController _playerController;
    private BoxCollider2D _boxCollider;
    private Scene _currentScene;
    private int max_health = 100;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
        _currentScene = SceneManager.GetActiveScene();
    }

    public void AdjustHealth(int amount)
    {   
        
        if (amount < 0)
        {
            //taking damage
            if (!isInvulnerable)
            {
                health += amount;
            } else
            {
                //play damage sound
                StartCoroutine(Invulnerable());
            }
            if (health <= 0 && !isDead)
            {
                    isDead = true;
            }
        }
        else 
        {
            //healing
            health += amount;
            //play healing sound + healing effect
        }

        if (health < 0)
            health = 0;
        if (health > max_health)
            health = max_health;
    }

    public void AdjustGlide(int amount)
    {

    }
    public void Die()
    {

    }

    #region Coroutines

    private IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        //add invulnerability animation
        yield return new WaitForSeconds(invulnerabilityTimer);
        isInvulnerable = false;
    }
    #endregion
}
