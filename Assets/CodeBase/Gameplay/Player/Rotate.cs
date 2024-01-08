using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Transform rotateTransform;

        private void FixedUpdate()
        {
            rotateTransform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
