using UnityEngine;

namespace PC2D
{
    public class SimpleUpDown : MonoBehaviour
    {
        public float upDownAmount;
        public float speed;

        private MovingPlatformMotor2D _mpMotor;
        private float _startingY;
        private Vector2 origin;

        // Use this for initialization
        void Start()
        {
            _mpMotor = GetComponent<MovingPlatformMotor2D>();
            _startingY = transform.position.y;
            origin = new Vector2(transform.position.x, _startingY);
            _mpMotor.velocity = Vector2.up * speed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_mpMotor.needReset && ((_startingY - _mpMotor.position.y) > 0f) && (origin.x - _mpMotor.position.x) < 0f)
            {
                _mpMotor.velocity = (origin - _mpMotor.position).normalized * speed;
            }
            else
            {
                _mpMotor.needReset = false;
                if (_mpMotor.velocity.y < 0 && _startingY - _mpMotor.position.y >= upDownAmount)
                {
                    _mpMotor.position += Vector2.up * ((_startingY - _mpMotor.position.y) - upDownAmount);
                    _mpMotor.velocity = Vector2.up * speed;
                }
                else if (_mpMotor.velocity.y > 0 && _mpMotor.position.y - _startingY >= upDownAmount)
                {
                    _mpMotor.position += -Vector2.up * ((_mpMotor.position.y - _startingY) - upDownAmount);
                    _mpMotor.velocity = -Vector2.up * speed;
                }
            }
        }
    }
}
