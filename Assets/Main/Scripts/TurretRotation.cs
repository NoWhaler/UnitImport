using UnityEngine;

namespace Main.Scripts
{
    public class TurretRotation : MonoBehaviour
    {
        [SerializeField] private GameObject _mount;

        [SerializeField] private GameObject _wheels;

        [SerializeField] private GameObject _gunPillar;
        
        public void RotateObjectsTowardsTarget(float mountRotationSpeed,
            float gunRotationSpeed,
            float targetXRotation,
            Quaternion targetRotation,
            Animator animator)
        {
            _mount.transform.rotation = Quaternion.RotateTowards(_mount.transform.rotation,
                targetRotation, Time.deltaTime * mountRotationSpeed);

            float currentXRotation = _wheels.transform.localRotation.eulerAngles.x;
            float newRotationX = Mathf.MoveTowardsAngle(currentXRotation, targetXRotation,
                Time.deltaTime * gunRotationSpeed);

            _wheels.transform.localRotation = Quaternion.Euler(newRotationX, 0, 0);
            _gunPillar.transform.localRotation = Quaternion.Euler(newRotationX, 0, 0);
            
            animator.SetFloat("GunRotation", newRotationX);
        }
    }
}