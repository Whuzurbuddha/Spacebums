using Scenes.scripts.enemy.movement;
using UnityEngine;

namespace Scenes.scripts.movements
{
    public class StarshipMovement : MonoBehaviour
    {
        private const float Fast = 24.0f;
        private const float Slow = 12.0f;
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

            transform1.position = playerPos;

            if (_mainCam == false) return;
            var mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            var currentMousePos = new Vector2(mousePos.x, mousePos.y);
            
            var angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            var movement = new Vector2(horizontalInput, verticalInput) * (_playerspeed * Time.deltaTime);
            transform.Translate(movement);
            
            var shooting = FindObjectOfType<Shooting>();
            if (!shooting) return;
            shooting.PlayerPos = playerPos;
            shooting.MousePos = currentMousePos;
            
            var blackhole = FindObjectOfType<EnemyMovement>();
            if (!blackhole) return;
            blackhole.PlayerPos = playerPos;
        }
    }
}
