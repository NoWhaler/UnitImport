using UnityEngine;

namespace Main.Scripts
{
    public class TargetMovement : MonoBehaviour
    {
        [SerializeField] private Collider groundCollider;
        [SerializeField] private float _moveSpeed;

        private Rigidbody _rigidbody;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            GenerateRandomTargetPosition();
        }

        private void FixedUpdate()
        {
            MoveToTargetPosition();
        }

        private void GenerateRandomTargetPosition()
        {
            var randomPoint = new Vector3(
                Random.Range(groundCollider.bounds.min.x, groundCollider.bounds.max.x),
                transform.position.y,
                Random.Range(groundCollider.bounds.min.z, groundCollider.bounds.max.z)
            );

            _targetPosition = randomPoint;
        }

        private void MoveToTargetPosition()
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            _rigidbody.velocity = moveDirection * _moveSpeed;

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                GenerateRandomTargetPosition();
            }
        }
    }
}
