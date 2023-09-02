using Main.Scripts.ObjectPooling;
using UnityEngine;

namespace Main.Scripts
{
    public class Turret : MonoBehaviour
    {
        [Header("Configuration")] 
        
        [SerializeField] private float _mountRotationSpeed;

        [SerializeField] private float _gunRotationSpeed;
        
        [Space]

        [SerializeField] private Transform _bulletSpawnPosition;
        
        private TargetMovement _currentTarget;

        private ParticleSystem _particleSystem;        
        
        private BulletsPool _bulletsPool;

        private Animator _animator;

        private TurretRotation _turretRotation;

        private const float GUN_TARGET_ROTATION = -45f;


        private void Awake()
        {
            _bulletsPool = FindObjectOfType<BulletsPool>();
            
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _turretRotation = GetComponentInChildren<TurretRotation>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            if (_currentTarget != null)
            {
                var directionToTarget = _currentTarget.transform.position - transform.position;
                directionToTarget.y = 0;

                var targetRotation = Quaternion.LookRotation(directionToTarget);
                
                _turretRotation.RotateObjectsTowardsTarget(_mountRotationSpeed, _gunRotationSpeed,
                    GUN_TARGET_ROTATION, targetRotation, _animator);
            }
            else
            {
                var targetRotation = Quaternion.identity;

                _turretRotation.RotateObjectsTowardsTarget(_mountRotationSpeed, _gunRotationSpeed,
                    0, targetRotation, _animator);
            }
        }

        public void Shoot()
        {
            _particleSystem.Play();
            SpawnBullet();
        }

        private void SpawnBullet()
        {
            var objectToPool = _bulletsPool.Get();

            if (objectToPool == null) return;

            var bullet = objectToPool.transform;
            
            bullet.position = _bulletSpawnPosition.position;
            objectToPool.gameObject.SetActive(true);
            
            var targetPosition = _currentTarget.transform.position;

            objectToPool.SetInitialVelocity(targetPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<TargetMovement>();
            
            if (target != null)
            {
                _currentTarget = target;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<TargetMovement>();
            
            if (target != null)
            {
                _currentTarget = null;
            }
        }
    }
}