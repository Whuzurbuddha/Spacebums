using UnityEngine;

namespace Scenes.scripts.movements
{
    public class StarshipMovement : MonoBehaviour
    {
        private const float Fast = 12.0f;
        private const float Slow = 5.0f;
        private float _playerspeed;
        private Camera _mainCam;
        
        private void Start()
        {
            _mainCam = Camera.main;
        }

        private void Update()
        {
            _playerspeed = Input.GetKey(KeyCode.LeftShift) ? Fast : Slow;

            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            var transform1 = transform;
            var playerPos = transform1.position;

            playerPos = playerPos.x switch
            {
                < -40 => new Vector3(-40, playerPos.y, playerPos.z),
                > 0 => new Vector3(0, playerPos.y, playerPos.z),
                _ => playerPos
            };

            playerPos = playerPos.y switch
            {
                < 4.0f => new Vector3(playerPos.x, 4.5f, playerPos.z),
                > 24 => new Vector3(playerPos.x, 24, playerPos.z),
                _ => playerPos
            };

            transform1.position = playerPos;

            if (_mainCam == false) return;
            var mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);

            var direction = mousePos - playerPos;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            var movement = new Vector3(horizontalInput, verticalInput, 0) * (_playerspeed * Time.deltaTime);
            transform.Translate(movement);

            FindObjectOfType<Shooting>().PlayerPos = playerPos;
            FindObjectOfType<Shooting>().Angle = angle;
            FindObjectOfType<Shooting>().Direction = direction;
        }
    }
}
