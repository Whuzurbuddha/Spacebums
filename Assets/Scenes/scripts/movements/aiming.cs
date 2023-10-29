using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Scenes.scripts.movements
{
    public class Aiming : MonoBehaviour
    {
        public Transform target; 
        public float smoothSpeed = 300f;
        public Vector3 offset;

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var targetPosition = mousePosition + offset;

            targetPosition.z = target.position.z;

            if (target == null) return;
            target.position = Vector3.Lerp(target.position, targetPosition, smoothSpeed);
        }
    }
}