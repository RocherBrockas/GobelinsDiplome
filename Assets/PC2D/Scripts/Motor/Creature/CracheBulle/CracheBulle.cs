using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CracheBulle : MonoBehaviour
{
    public float bulleLaunchStrength;
    public float bulleSpawnTiming;
    public float bonusLifeSpan = 240;
    public float randomStrengthVariation;
    private float _internTiming;
    public GameObject bulle;
    private Vector2 _force;
    public PlayerController2D playerController;
    private bool faceleft;
    private float _randomStrengthModifier;

    private void Start()
    {
        Debug.Log(this.transform.position);
        _force = new Vector2(bulleLaunchStrength, 0);
    }

    void FixedUpdate()
    {
       if (_internTiming < 0)
        {
            _randomStrengthModifier = Random.Range(-randomStrengthVariation, randomStrengthVariation);
            faceleft = (playerController.transform.position.x < this.transform.position.x);
            GameObject currentBulle = Instantiate(bulle);
            currentBulle.transform.position = this.transform.position;
            Debug.Log(currentBulle.transform.position);
            currentBulle.GetComponent<Bulle>().isDestroyable = true;
            currentBulle.GetComponent<Bulle>().forceLancement = (bulleLaunchStrength + _randomStrengthModifier) *(faceleft ? -1 : 1);
            currentBulle.GetComponent<Bulle>().lifespan = bulleSpawnTiming + bonusLifeSpan;

            _internTiming = bulleSpawnTiming;
        } else
        {
            _internTiming--;
        }
    }
}
