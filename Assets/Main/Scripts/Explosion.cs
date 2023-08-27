using Main.Scripts.ObjectPooling;
using UnityEngine;

namespace Main.Scripts
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _maxLifeTime;

        private ExplosionPool _explosionPool;

        private void OnEnable()
        {
            _explosionPool = GetComponentInParent<ExplosionPool>();
        }

        private void Update()
        {
            CheckForLifeTime();
        }

        private void CheckForLifeTime()
        {
            if (_lifeTime < _maxLifeTime)
            {
                _lifeTime += Time.deltaTime;
            }
            else
            {
                _lifeTime = 0;
                _explosionPool.ReturnToPool(this);
            }
        }
    }
}