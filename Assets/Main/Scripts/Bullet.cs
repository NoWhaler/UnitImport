using Main.Scripts.ObjectPooling;
using UnityEngine;

namespace Main.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] private float MovementSpeed { get; set; }

        private Rigidbody _rigidBody;
    
        private BulletsPool _bulletsPool;
        private ExplosionPool _explosionPool;
        
        public TargetMovement _target;

        private void OnEnable()
        {
            _rigidBody = GetComponent<Rigidbody>();
        
            _target = FindObjectOfType<TargetMovement>();
            _bulletsPool = GetComponentInParent<BulletsPool>();
            _explosionPool = FindObjectOfType<ExplosionPool>();
        }

        private void OnDisable()
        {
            _rigidBody.velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            MoveBullet();
        }
    
        private void MoveBullet()
        {
            if (_target == null) return;
        
            var direction = (_target.transform.position - transform.position).normalized;
        
            _rigidBody.velocity = direction * MovementSpeed;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<TargetMovement>();
        
            if (target == null) return;

            SpawnExplosion(other.ClosestPoint(transform.position));

            _bulletsPool.ReturnToPool(this);
        }

        private void SpawnExplosion(Vector3 position)
        {
            var objectToPool = _explosionPool.Get();

            if (objectToPool == null) return;

            var explosion = objectToPool.transform;
            
            explosion.position = position;
            objectToPool.gameObject.SetActive(true);
        }
    }
}