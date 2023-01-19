using UnityEngine;

namespace Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Engine : MonoBehaviour
    {
        [SerializeField] private ShipSettings _shipSettings;

        private Rigidbody2D _rigidbody;

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Throttle();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                SteerLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                SteerRight();
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Throttle()
        {
            _rigidbody.AddForce(transform.up * _shipSettings.ThrottlePower, ForceMode2D.Force);
        }

        public void SteerLeft()
        {
            _rigidbody.AddTorque(_shipSettings.RotationPower, ForceMode2D.Force);
        }

        public void SteerRight()
        {
            _rigidbody.AddTorque(-_shipSettings.RotationPower, ForceMode2D.Force);
        }
    }
}
