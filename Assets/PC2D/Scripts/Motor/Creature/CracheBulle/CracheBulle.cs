using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class CracheBulle : MonoBehaviour
{
    public float bulleLaunchStrength;
    public float bulleSpawnTiming;
    public float bonusLifeSpan = 240;
    [Range(1,100)]
    public float randomStrengthVariation;
    [Range(0,0.5f)]
    public float randomSizeVariation;
    public PerceptionTypes AwakenPerception;
    public GameObject bulle;
    public SpriteRenderer sprite;
    public FluxMemory trigger;
    public bool playSounds;

    public PlayerController2D playerController;

    private bool faceleft;
    private float _randomStrengthModifier;
    private float _randomSizeModifier;
    public bool isActive;
    public LayerMask collisionMask;
    private float _internTiming;
    private Vector2 _force;


    private void Start()
    {
        //Debug.Log(this.transform.position);
        if (trigger != null)
        {
            isActive = trigger.active;
        }
        _force = new Vector2(bulleLaunchStrength, 0);
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            if (_internTiming < 0)
            {
                _randomStrengthModifier = Random.Range(-randomStrengthVariation, randomStrengthVariation);
                _randomSizeModifier = Random.Range(1 - randomSizeVariation, 1 + randomSizeVariation);
                faceleft = (playerController.transform.position.x < this.transform.position.x);
                GameObject currentBulle = Instantiate(bulle);
                currentBulle.transform.position = this.transform.position;
                //Debug.Log(currentBulle.transform.position);
                currentBulle.GetComponent<Bulle>().isDestroyable = true;
                sprite.flipX = faceleft;
                if (playSounds)
                AudioManager.instance.Play("spew");
                currentBulle.GetComponent<Bulle>().forceLancement = (bulleLaunchStrength + _randomStrengthModifier) * (faceleft ? -1 : 1);
                currentBulle.transform.localScale = currentBulle.transform.localScale * _randomSizeModifier;
                currentBulle.GetComponent<Bulle>().lifespan = bulleSpawnTiming + bonusLifeSpan;

                _internTiming = bulleSpawnTiming;
            }
            else
            {
                _internTiming--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PoofRange>() != null)
        {
            if (collisionMask == (collisionMask| (1 << collision.gameObject.layer)) && PerceptionManager.instance.perception.perceptionType == AwakenPerception)
            {
                Debug.Log("Activate crache bulle");
                if(playSounds)
                AudioManager.instance.Play("cbwakeup2");
                isActive = true;
            } else
            {
                if (playSounds)
                AudioManager.instance.Play("cbwakeup1");
                Debug.Log("smthing collided crache bulle");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PoofRange>() != null)
        {
            if (collisionMask == (collisionMask | (1 << collision.gameObject.layer)) && PerceptionManager.instance.perception.perceptionType == AwakenPerception)
            {
                if (trigger != null)
                {
                    isActive = trigger.active;
                    if (!trigger.active)
                        AudioManager.instance.Play("cbSleep");
                }

            }
            else
            {
                Debug.Log("smthing collided crache bulle");
            }
        }
    }
}
