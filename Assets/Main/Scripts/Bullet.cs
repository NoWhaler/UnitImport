using Main.Scripts.ObjectPooling;
using UnityEngine;

namespace Main.Scripts
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidBody;
    
        private BulletsPool _bulletsPool;
        private ExplosionPool _explosionPool;

        private void OnEnable()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _bulletsPool = GetComponentInParent<BulletsPool>();
            _explosionPool = FindObjectOfType<ExplosionPool>();
        }
        
        public void SetInitialVelocity(Vector3 targetPosition)
        {
            const float initialAngle = 60f;
            
            var direction = targetPosition - transform.position; 
            var height = direction.y;  
            direction.y = 0;  
            
            var distance = direction.magnitude ;  
            var launchAngle = initialAngle * Mathf.Deg2Rad;  
            
            direction.y = distance * Mathf.Tan(launchAngle);  
            distance += height / Mathf.Tan(launchAngle);  
            
            var initialVelocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * launchAngle));
            _rigidBody.velocity = initialVelocity * direction.normalized;
        }

        private void OnDisable()
        {
            _rigidBody.velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<TargetMovement>();

            var ground = other.GetComponent<Ground>();
            
            if (target == null && ground == null) return;

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