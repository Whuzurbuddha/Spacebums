using UnityEngine;

namespace Scenes.scripts.movements
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform target; 
        public float smoothSpeed = 5f;
        public Vector3 offset;
        private void Update()
        {
            if (target == null) return;

            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}

