using Main.Scripts.ObjectPooling;
using UnityEngine;

namespace Main.Scripts
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawnPosition;

        private ParticleSystem _particleSystem;        
        
        private BulletsPool _bulletsPool;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _bulletsPool = FindObjectOfType<BulletsPool>();
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
        }
    }
}