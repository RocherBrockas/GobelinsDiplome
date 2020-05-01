using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class TutoFade : MonoBehaviour
{
    public SpriteRenderer button;
    public Sprite buttonsprite;
    public bool perceptionNeeded;
    public PerceptionTypes perception;
    private float _hitboxSize;
    private float _fadeRatio;
    private Vector3 pos;
    private Color C = Color.white;
    public bool snap;

    private void Start()
    {
        pos = this.transform.position;
        _hitboxSize = this.GetComponent<BoxCollider2D>().size.x / 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!perceptionNeeded || PerceptionManager.instance.perception.perceptionType == perception)
            {
                if (!snap)
                {
                    button.gameObject.transform.position = collision.transform.position + Vector3.up * 4;
                }
                C.a = 0;
                button.color = C;
                button.sprite = buttonsprite;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!perceptionNeeded || PerceptionManager.instance.perception.perceptionType == perception)
            {
                C.a = 1 - Mathf.Abs((button.gameObject.transform.position.x - pos.x) / _hitboxSize);
                if (!snap)
                {
                    button.gameObject.transform.position = collision.transform.position + Vector3.up * 4;
                }
                button.color = C;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!perceptionNeeded || PerceptionManager.instance.perception.perceptionType == perception)
            {
                C.a = 0;
                button.color = C;
            }
        }
    }
}
