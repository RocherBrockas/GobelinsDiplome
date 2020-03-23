using UnityEngine;

namespace PC2D
{
    public class MultiDirectionnalMovementPlatform : MonoBehaviour
    {
        public float leftRightAmount;
        public float upDownAmount;
        public float speed;

        private MovingPlatformMotor2D _mpMotor;
        private float _startingX;
        private float _startingY;
        // Use this for initialization
        void Start()
        {
            _mpMotor = GetComponent<MovingPlatformMotor2D>();
            _startingX = transform.position.x;
            _startingY = transform.position.y;
            _mpMotor.velocity = (-Vector2.right -Vector2.up)*speed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
                if ( _startingX - transform.position.x >= leftRightAmount)
                {
                    transform.position += Vector3.right * ((_startingX - transform.position.x) - leftRightAmount);
                    _mpMotor.velocity = Vector2.right;
                }
                else if (transform.position.x - _startingX >= leftRightAmount)
                {
                    transform.position += -Vector3.right * ((transform.position.x - _startingX) - leftRightAmount);
                    _mpMotor.velocity = -Vector2.right ;
                }
                if (_startingY - _mpMotor.position.y >= upDownAmount)
                {
                    _mpMotor.position += Vector2.up * ((_startingY - _mpMotor.position.y) - upDownAmount);
                    _mpMotor.velocity = Vector2.up;
                }
                else if ( _mpMotor.position.y - _startingY >= upDownAmount)
                {
                    _mpMotor.position += -Vector2.up * ((_mpMotor.position.y - _startingY) - upDownAmount);
                    _mpMotor.velocity = -Vector2.up;
                }
            _mpMotor.velocity *= speed;
        }
    }
}

