using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class CracheBulle : MonoBehaviour
{
    public float bulleLaunchStrength;
    public float bulleSpawnTiming;
    public float bonusLifeSpan = 240;
    public float randomStrengthVariation;
    public PerceptionTypes AwakenPerception;
    public GameObject bulle;
    public SpriteRenderer sprite;

    public PlayerController2D playerController;

    private bool faceleft;
    private float _randomStrengthModifier;
    public bool isActive;
    public LayerMask collisionMask;
    private float _internTiming;
    private Vector2 _force;


    private void Start()
    {
        //Debug.Log(this.transform.position);
        _force = new Vector2(bulleLaunchStrength, 0);
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            if (_internTiming < 0)
            {
                _randomStrengthModifier = Random.Range(-randomStrengthVariation, randomStrengthVariation);
                faceleft = (playerController.transform.position.x < this.transform.position.x);
                GameObject currentBulle = Instantiate(bulle);
                currentBulle.transform.position = this.transform.position;
                //Debug.Log(currentBulle.transform.position);
                currentBulle.GetComponent<Bulle>().isDestroyable = true;
                sprite.flipX = faceleft;
                currentBulle.GetComponent<Bulle>().forceLancement = (bulleLaunchStrength + _randomStrengthModifier) * (faceleft ? -1 : 1);
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
                //Debug.Log("Activate crache bulle");
                isActive = true;
            }
        }
    }
}
