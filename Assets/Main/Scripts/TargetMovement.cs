using UnityEngine;

namespace Main.Scripts
{
    public class TargetMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] _movePoints;
        [SerializeField] private float _moveSpeed = 2.0f;
        [SerializeField] private float _pauseDuration = 1.0f;

        private int _currentPointIndex;
        private bool _isPaused;
        private float _pauseTimer;
    
        private void Update()
        {
            PatrolBetweenPoints();
        }

        private void PatrolBetweenPoints()
        {
            if (!_isPaused)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _movePoints[_currentPointIndex].position, _moveSpeed * Time.deltaTime);

                if (!(Vector3.Distance(transform.position, _movePoints[_currentPointIndex].position) < 0.01f)) return;
                _isPaused = true;
                _pauseTimer = 0f;
            }
            else
            {
                _pauseTimer += Time.deltaTime;
                if (!(_pauseTimer >= _pauseDuration)) return;
                _isPaused = false;
                _currentPointIndex = (_currentPointIndex + 1) % _movePoints.Length;
            }
        }
    }
}
