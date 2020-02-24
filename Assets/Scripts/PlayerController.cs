using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using System;
using GlobalTypes;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D.CharacterCollisionState2D flags;
    public float walkSpeed = 6.0f;
    public float jumpSpeed = 16.0f;
    public float gravity = 20.0f;
    public float wallJumpXAmount = 1f;
    public float wallJumpYAmount = 1f;
    public float WJlostcontrolTime = 0.5f;
    public float slopeSlideSpeed = 4.0f;
    public float glideAmount = 2.0f;
    public float glideTimer = 2f;
    public float grappleCooldown = 0.5f;
    public float grappleTractionStrength = 1.0f;

    //to pass private in the future
    public float grappleAirControlLoss = 0.7f;

    public float crosshairDistance = 1.0f;
    public LayerMask layerMask;

    public GameObject crossHair;
    public GameObject grabRange;

    //player ability toggles
    private bool canSlopesliding = true;
    private bool canGlide = true;
    private bool canWallJump = true;


    #region Getters and Setters

    public bool getSlopeSliding()
    {
        return canSlopesliding;
    }

    public bool getWallJump()
    {
        return canWallJump;
    }

    public bool getGlide()
    {
        return canGlide;
    }

    public void SetGlide(bool value)
    {
        canGlide = value;
    }

    public void SetWallJump(bool value)
    {
        canWallJump = value;
    }

    public void SetSlopeSliding(bool value)
    {
        canSlopesliding = value;
    }
    #endregion

    #region Player States


    public bool isGrounded;
    public bool isJumping;


    //factoriser en une liste d'état et le joueur switch d'un état à un autre.
    [HideInInspector]
    public bool isFacingRight = true;
    public bool wallJumped;
    public bool isSlopeSliding;
    public bool isGliding;

    #endregion

    //private variables
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _GrappleDirection = Vector3.zero;
    private CharacterController2D _characterController;
    private float _slopeAngle;
    private Vector3 _slopeGradient = Vector3.zero;
    private bool _startGlide;
    private float _currentGlideTimer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _grabZone;
    private Vector3 _frontBottomCorner;
    private Vector3 _backBottomCorner;
    private bool _canAirControl = true;
    [SerializeField]
    private GrabCollisionHandle _colliderGrapple;
    public GroundType _groundType;
    private GameObject _tempOneWayPlatform;
    private GameObject _tempMovingPlatform;
    private float _jumpPadAmout;
    private float _tempMoveDirection;
    private EffectorType _currentEffectorType = EffectorType.None;
    private Vector3 _currentEffectorAdjustment = Vector3.zero;


    // Movement of the crosshair using Analog stick
    //Maybe improve with left stick and right stick priority
    private void getAngleCrossHair()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        float aim_angle = 0.0f;
        // USED TO CHECK OUTPUT
        //Debug.Log(" horz: " + x + "   vert: " + y);

        // CANCEL ALL INPUT BELOW THIS FLOAT
        float R_analog_threshold = 0.20f;

        float magnitudeish = Mathf.Abs(y) + Mathf.Abs(x); // less intense than Vector2(horizontal, vertical).magnitude
        if (magnitudeish < R_analog_threshold) // inside deadzone
            x = y = 0.0f;
        aim_angle = Mathf.Atan2(x, -y) * Mathf.Rad2Deg;
        // CALCULATE ANGLE AND ROTATE
        if (x != 0.0f || y != 0.0f)
        {

            aim_angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            // ANGLE GUN
            grabRange.transform.rotation = Quaternion.AngleAxis(aim_angle, Vector3.forward);
        }
    }

    private void grabMangement()
    {
        if (Input.GetAxis("Grab") > 0.5f)
        {
            _grabZone.enabled = true;
            if (_colliderGrapple.instance.getHadGrabbed())
            {
                _groundType = GroundType.None;
                _GrappleDirection = _colliderGrapple.getPointTransform().position - transform.position;
                _GrappleDirection = _GrappleDirection.normalized;
                _canAirControl = false;
                Debug.Log(_GrappleDirection);
                StartCoroutine(GrappleControlLoss());
                isJumping = true;
                //Mettre un temps de pose préGrapin pour marquer le move. Actually ok
                _moveDirection.x = _GrappleDirection.x * grappleTractionStrength;
                _moveDirection.y = _GrappleDirection.y * grappleTractionStrength;
            }
            _colliderGrapple.instance.Reset();
            StartCoroutine(GrappleLaunched());

        }
    }

    private void GetGroundType()
    {


        //slope Management
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 2f, layerMask);

        if(!hit.collider)
        {
            _frontBottomCorner = new Vector3(transform.position.x + _boxCollider.size.x / 2, transform.position.y, 0);
            _backBottomCorner = new Vector3(transform.position.x - _boxCollider.size.x / 2, transform.position.y, 0);
            RaycastHit2D hitFrontFloor = Physics2D.Raycast(_frontBottomCorner, -Vector2.up, 2f,layerMask);
            RaycastHit2D hitBackFloor = Physics2D.Raycast(_backBottomCorner, -Vector2.up, 2f,layerMask);
            if (hitFrontFloor.collider && !hitBackFloor.collider)
            {
                hit = hitFrontFloor;
            } else if (!hitFrontFloor.collider && hitBackFloor.collider)
            {
                hit = hitBackFloor;
            }
        }
        if (hit)
        {
            _slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            _slopeGradient = hit.normal;

            if ((_slopeAngle > _characterController.slopeLimit) && canSlopesliding)
            {
                isSlopeSliding = true;
            }
            else
            {
                isSlopeSliding = false;
            }

            string layerName = LayerMask.LayerToName(hit.transform.gameObject.layer);
            if (layerName == "OneWayPlatform" || layerName == "MovingPlatform")
            {
                _groundType = GroundType.OneWayPlatform;
                if (!_tempOneWayPlatform)
                {
                    _tempOneWayPlatform = hit.transform.gameObject;
                }
                if (layerName == "MovingPlatform")
                {
                    _groundType = GroundType.MovingPlatform;

                    if (!_tempMovingPlatform)
                    {
                        _tempMovingPlatform = hit.transform.gameObject;
                        transform.SetParent(hit.transform);
                    }
                }
            }
            else if (layerName == "LevelGeometry")
            {
                _groundType = GroundType.LevelGeometry;
            }
            else if (layerName == "Collapsable")
            {
                _groundType = GroundType.Collapsable;
                transform.SetParent(hit.transform);
                StartCoroutine(
                    DisablePlatform(hit.transform.gameObject.GetComponent<Collapsableplatform>().collapseStartUp,
                    hit.transform.gameObject.GetComponent<Collapsableplatform>().reappearTime,
                    hit.transform.gameObject));
            }
            else if (layerName == "JumpPad")
            {
                //to use for making bumpers
                _groundType = GroundType.JumpPad;
                _jumpPadAmout = hit.transform.gameObject.GetComponent<Jumpad>().jumpAmount;

            }
        }
        else
        {
            _groundType = GroundType.None;
        }
        if (!_groundType.Equals(GroundType.MovingPlatform) || _groundType.Equals(GroundType.None))
        {
            transform.SetParent(null);
            _tempMovingPlatform = null;
        }
    }

    private void MovementManager()
    {

        //if statemement to remove for interesting wall climb property
        if ((wallJumped == false) && _canAirControl)
        {
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.x *= walkSpeed;
        }
        if (isGrounded)
        {
            _currentGlideTimer = glideTimer;

            GetGroundType();

            if (_canAirControl)
            {
                _moveDirection.y = 0;
            }
            isJumping = false;

            if (_moveDirection.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isFacingRight = false;
            }
            else if (_moveDirection.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isFacingRight = true;
            }

            if (isSlopeSliding)
            {
                _moveDirection = new Vector3(_slopeGradient.x * slopeSlideSpeed, -_slopeGradient.y * slopeSlideSpeed, 0f);
            }
            if(_groundType.Equals(GroundType.JumpPad))
            {
                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = _tempMoveDirection + _jumpPadAmout;
                    _tempMoveDirection = _moveDirection.y / 2;
                } else
                {
                    _tempMoveDirection = 0f;
                    _moveDirection.y = _jumpPadAmout;
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                if ((_groundType == GroundType.OneWayPlatform || _groundType == GroundType.MovingPlatform) && (Input.GetAxis("Vertical") < -0.1f))
                {
                    StartCoroutine(DisableOneWayPlatform());
                }
                else
                {
                    _moveDirection.y = jumpSpeed;
                }
                isJumping = true;
                transform.SetParent(null);
                _tempMovingPlatform = null;

            }
        }
        else //player is in the air
        {
            //Dangereux
            _groundType = GroundType.None;
            //Attention


            if (Input.GetButtonUp("Jump") && _canAirControl)
            {
                if (_moveDirection.y > 0)
                {
                    _moveDirection.y = _moveDirection.y * .5f;
                }
            }
        }

        //input a réarranger
        if (canGlide && (Input.GetAxis("Vertical") > 0.2f) && _characterController.velocity.y < 0.2f)
        {
            if (_currentGlideTimer > 0f)
            {
                isGliding = true;
                if (_startGlide)
                {
                    _moveDirection.y = 0;
                    _startGlide = false;
                }
                _moveDirection.y -= glideAmount * Time.deltaTime;
                _currentGlideTimer -= Time.deltaTime;
            }
            else
            {
                isGliding = false;
                _moveDirection.y -= gravity * Time.deltaTime;
            }

        }
        else
        {
            isGliding = false;
            _startGlide = true;
            _moveDirection.y -= gravity * Time.deltaTime;
        }



        _characterController.move(_moveDirection * Time.deltaTime);
        flags = _characterController.collisionState;
        isGrounded = flags.below;

        if (flags.above)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        if (flags.left || flags.right)
        {
            if (canWallJump)
            {
                if (Input.GetButtonDown("Jump") && !wallJumped && !isGrounded)
                {
                    if (_moveDirection.x < 0)
                    {
                        _moveDirection.x = jumpSpeed * wallJumpXAmount;
                        _moveDirection.y = jumpSpeed * wallJumpYAmount;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        //_lastWJLeft = false;
                    }
                    else if (_moveDirection.x > 0)
                    {
                        _moveDirection.x = -jumpSpeed * wallJumpXAmount;
                        _moveDirection.y = jumpSpeed * wallJumpYAmount;
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        //_lastWJLeft = true;
                    }
                    StartCoroutine(WallJumpWaiter());
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController2D>();
        _currentGlideTimer = glideTimer;
        _boxCollider = GetComponent<BoxCollider2D>();
        _grabZone = grabRange.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getAngleCrossHair();
        grabMangement();
        if (!_currentEffectorType.Equals(EffectorType.None))
            EffectorAdjustment();
        MovementManager();
    }

    #region Coroutines
    IEnumerator WallJumpWaiter()
    {
        wallJumped = true;
        yield return new WaitForSeconds(WJlostcontrolTime);
        wallJumped = false;
    }

    IEnumerator GrappleLaunched()
    {
        yield return new WaitForSeconds(grappleCooldown);
        _grabZone.enabled = false;
    }

    IEnumerator GrappleControlLoss()
    {
        yield return new WaitForSeconds(grappleAirControlLoss);
        _canAirControl = true;
    }

    IEnumerator DisableOneWayPlatform()
    {
        if (_tempOneWayPlatform)
        {
            _tempOneWayPlatform.GetComponent<EdgeCollider2D>().enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        if (_tempOneWayPlatform)
        {
            _tempOneWayPlatform.GetComponent<EdgeCollider2D>().enabled = true;
            _tempOneWayPlatform = null;
        }
    }

    IEnumerator DisablePlatform(float collapseStartUp, float reappearTime, GameObject gameObject)
    {
        yield return new WaitForSeconds(collapseStartUp);
        gameObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(reappearTime);
        gameObject.gameObject.SetActive(true);
    }
    #endregion

    private void EffectorAdjustment()
    {
        switch (_currentEffectorType)
        {
            case EffectorType.TractorBeam:
                {
                    //Freeze player Movement
                    _moveDirection.y = _currentEffectorAdjustment.y;
                    break;
                }
            case EffectorType.Ladder:
                {
                    //Not implemented because not useful regarding level design
                    break;
                }
            case EffectorType.FloatZone:
                {
                    //More Movement Freedom
                    _moveDirection += _currentEffectorAdjustment;
                    break;  
                }
            default:
                    break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_characterController.triggerMask == ( _characterController.triggerMask | (1 << collision.gameObject.layer)))
        { 
            string layerName = LayerMask.LayerToName(collision.gameObject.layer);
            if (layerName == "Effector")
            {
                Effector effector = collision.gameObject.GetComponent<Effector>();
                if (effector)
                {
                    _currentEffectorType = effector.effectorType;
                    _currentEffectorAdjustment = effector.effectorAdjustmenent;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_characterController.triggerMask == (_characterController.triggerMask | (1 << collision.gameObject.layer)))
        {
            Debug.Log("stay");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_characterController.triggerMask == (_characterController.triggerMask | (1 << collision.gameObject.layer)))
        {
            _currentEffectorType =EffectorType.None;
            _currentEffectorAdjustment = Vector3.zero;
        }
    }
}
