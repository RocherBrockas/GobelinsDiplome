using System.Collections;
using UnityEngine;



/// <summary>
/// This class is a simple example of how to build a controller that interacts with PlatformerMotor2D.
/// </summary>
[RequireComponent(typeof(PlatformerMotor2D))]
public class PlayerController2D : MonoBehaviour
{
    private PlatformerMotor2D _motor;
    private bool _restored = true;
    private bool _enableOneWayPlatforms;
    private bool _oneWayPlatformsAreWalls;
    private bool _canInspire = true;
    public PerceptionManager perceptionManager;
    public GameObject inspireRange;
    public GameObject expireRange;
    public LayerMask waterMask;
    public bool isInBubble;
    private Bulle _bubble;
    public LoadPositions startingPosition;

    public void Setbubble(Bulle bulle)
    {
        _bubble = bulle;
        _motor.velocity = Vector2.zero;
        _motor.frozen = true;
    }

    public void JumpOutOfBubble()
    {
        _motor.frozen = false;
        _motor.ForceJump();
        _bubble.Pop();
        _bubble = null;
    }

    // Use this for initialization
    void Start()
    {
        this.transform.position = startingPosition.initialValue;
        PerceptionManager.instance.perception = startingPosition.perception;
        _motor = GetComponent<PlatformerMotor2D>();
        perceptionManager = GetComponent<PerceptionManager>();
        _motor.AbilityChange(perceptionManager.perception);
    }

    // before enter en freedom state for ladders
    void FreedomStateSave(PlatformerMotor2D motor)
    {
        if (!_restored) // do not enter twice
            return;

        _restored = false;
        _enableOneWayPlatforms = _motor.enableOneWayPlatforms;
        _oneWayPlatformsAreWalls = _motor.oneWayPlatformsAreWalls;
    }
    // after leave freedom state for ladders
    void FreedomStateRestore(PlatformerMotor2D motor)
    {
        if (_restored) // do not enter twice
            return;

        _restored = true;
        _motor.enableOneWayPlatforms = _enableOneWayPlatforms;
        _motor.oneWayPlatformsAreWalls = _oneWayPlatformsAreWalls;
    }

    // Update is called once per frame
    void Update()
    {
        // use last state to restore some ladder specific values
        if (_motor.motorState != PlatformerMotor2D.MotorState.FreedomState)
        {
            // try to restore, sometimes states are a bit messy because change too much in one frame
            FreedomStateRestore(_motor);
        }

        // Jump?
        // If you want to jump in ladders, leave it here, otherwise move it down
        if (Input.GetButtonDown(PC2D.Input.JUMP))
        {
            if (isInBubble)
            {
                JumpOutOfBubble();
            }
            else
            {
                _motor.Jump();
            }
            _motor.DisableRestrictedArea();

        }

        _motor.jumpingHeld = Input.GetButton(PC2D.Input.JUMP);

        // XY freedom movement
        if (_motor.motorState == PlatformerMotor2D.MotorState.FreedomState)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
            _motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);

            return; // do nothing more
        }

        // X axis movement
        if ((Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL)) > PC2D.Globals.INPUT_THRESHOLD) && !isInBubble)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
        }
        else
        {
            _motor.normalizedXMovement = 0;
        }

        if (Input.GetAxis(PC2D.Input.VERTICAL) != 0)
        {
            bool up_pressed = Input.GetAxis(PC2D.Input.VERTICAL) > 0;
            if (_motor.IsOnLadder())
            {
                if (
                    (up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Top)
                    ||
                    (!up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Bottom)
                 )
                {
                    // do nothing!
                }
                // if player hit up, while on the top do not enter in freeMode or a nasty short jump occurs
                else
                {
                    // example ladder behaviour

                    _motor.FreedomStateEnter(); // enter freedomState to disable gravity
                    _motor.EnableRestrictedArea();  // movements is retricted to a specific sprite bounds

                    // now disable OWP completely in a "trasactional way"
                    FreedomStateSave(_motor);
                    _motor.enableOneWayPlatforms = false;
                    _motor.oneWayPlatformsAreWalls = false;

                    // start XY movement
                    _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
                    _motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);
                }
            }
        }
        else if (Input.GetAxis(PC2D.Input.VERTICAL) < -PC2D.Globals.FAST_FALL_THRESHOLD)
        {
            _motor.fallFast = false;
        }

        if (Input.GetButtonDown(PC2D.Input.POOF)  && !_motor.frozen)
        {
            if (_canInspire)
            {
                if (perceptionManager.perception.perceptionType != PC2D.PerceptionTypes.Death)
                {
                    expireRange.transform.position = this.transform.position + Vector3.up;
                    expireRange.SetActive(true);
                    StartCoroutine(ExpireCooldown());
                }
            }
        }

        if (Input.GetButtonDown(PC2D.Input.INSPIRE) && !_motor.frozen)
        {
            if (_canInspire)
            {
                inspireRange.SetActive(true);
                StartCoroutine(InspireCooldown());
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (perceptionManager.triggerMask == (perceptionManager.triggerMask | (1 << collision.gameObject.layer)))
        {
            if (perceptionManager.perception != collision.gameObject.GetComponent<PerceptionZone>().perception)
            {
                if (collision.gameObject.GetComponent<Totem>() != null)
                {
                    Totem totem = collision.gameObject.GetComponent<Totem>();
                    if (totem.isActive)
                    {
                        PerceptionManager.instance.activeTotem = totem;
                        if (!totem.activated)
                        {
                            totem.totemMemory.ActivateFlux();
                            totem.ActivateFlux();
                        }
                        perceptionManager.perception = collision.gameObject.GetComponent<PerceptionZone>().perception;

                        _motor.AbilityChange(perceptionManager.perception);
                    }
                }
                else
                {
                    if (collision.gameObject.GetComponent<PerceptionZone>().needCollidingChange)
                    {
                        perceptionManager.perception = collision.gameObject.GetComponent<PerceptionZone>().perception;
                        _motor.AbilityChange(perceptionManager.perception);
                        if (collision.gameObject.GetComponent<PerceptionZone>().CoupeFlux)
                        {
                            AudioManager.instance.Play("cfluxActive");
                        }
                    }
                }
            }
            else
            {
                if (collision.gameObject.GetComponent<Totem>() != null)
                {
                    Totem totem = collision.gameObject.GetComponent<Totem>();
                    if (totem.isActive)
                    {
                        PerceptionManager.instance.activeTotem = totem;
                        if (!totem.activated)
                        {
                            collision.gameObject.GetComponent<Totem>().totemMemory.ActivateFlux();
                            totem.ActivateFlux();
                        }
                    }
                }
            }
        }
        if (waterMask == (waterMask | (1 << collision.gameObject.layer)))
        {
            _motor.isInWater = true;
        }

    }

    IEnumerator ExpireCooldown()
    {
        _canInspire = false;
        expireRange.GetComponent<PoofRange>().AnimatePoof();
        AudioManager.instance.Play("Expiration");
        yield return new WaitForSeconds(perceptionManager.perception.expireCooldown);
        expireRange.SetActive(false);
        expireRange.transform.position = this.transform.position;
        expireRange.transform.SetParent(this.transform);
        expireRange.GetComponent<PoofRange>().ResetComponents();

        _canInspire = true;
    }

    IEnumerator InspireCooldown()
    {
        _canInspire = false;
        if (inspireRange.GetComponent<inspireCollisionTrigger>().detectedPerception)
        {
            inspireRange.GetComponent<inspireCollisionTrigger>().ps.Play();
            AudioManager.instance.Play("Inspiration");
            if (inspireRange.GetComponent<inspireCollisionTrigger>().feltPerception != null && (perceptionManager.perception != inspireRange.GetComponent<inspireCollisionTrigger>().feltPerception))
            {
                perceptionManager.perception = inspireRange.GetComponent<inspireCollisionTrigger>().feltPerception;
                _motor.AbilityChange(perceptionManager.perception);
                inspireRange.GetComponent<inspireCollisionTrigger>().detectedPerception = false;
            }
        }
        yield return new WaitForSeconds(perceptionManager.perception.inspireCooldown);
        inspireRange.SetActive(false);
        _canInspire = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (waterMask == (waterMask | (1 << collision.gameObject.layer)))
        {
            _motor.isInWater = false;
        }
    }
}
