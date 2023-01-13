using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;

        [SerializeField] private float _minForce
        {
            get { return _minMaxForce.x; }
            set { _minMaxForce.x = value; }
        }
        [SerializeField] private float _maxForce
        {
            get { return _minMaxForce.y; }
            set { _minMaxForce.y = value; }
        }
        [SerializeField] private float _minSize
        {
            get { return _minMaxSize.x; }
            set { _minMaxSize.x = value; }
        }
        [SerializeField] private float _maxSize
        {
            get { return _minMaxSize.y; }
            set { _minMaxSize.y = value; }
        }
        [SerializeField] private float _minTorque
        {
            get { return _minMaxTorque.x; }
            set { _minMaxTorque.x = value; }
        }
        [SerializeField] private float _maxTorque
        {
            get { return _minMaxTorque.y; }
            set { _minMaxTorque.y = value; }
        }

        [Header("Config:")]
        [SerializeField] private Vector2 _minMaxForce;
        [SerializeField] private Vector2 _minMaxSize;
        [SerializeField] private Vector2 _minMaxTorque;

        [Header("References:")]
        [SerializeField] private Transform _shape;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
            
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
               HitByLaser();
            }
        }

        private void HitByLaser()
        {
            _onAsteroidDestroyed.Raise(_instanceId);
            Destroy(gameObject);
        }

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
        public void OnHitByLaser(IntReference asteroidId)
        {
            if (_instanceId == asteroidId.GetValue())
            {
                Destroy(gameObject);
            }
        }
        
        public void OnHitByLaserInt(int asteroidId)
        {
            if (_instanceId == asteroidId)
            {
                Destroy(gameObject);
            }
        }
        
        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(_minForce, _maxForce);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(_minTorque, _maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize()
        {
            var size = Random.Range(_minSize, _maxSize);
            _shape.localScale = new Vector3(size, size, 0f);
        }
    }
}
