using System;
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

namespace Scenes.scripts.movements
{
    public class Shooting : MonoBehaviour, INotifyPropertyChanged
    {
        private static float _projectileSpeed = 40.0f;
        public GameObject flash;
        public GameObject laser1;
        public Vector2 gunPos;

        public event PropertyChangedEventHandler PropertyChanged;
        
        private Vector2 _playerPos;
        private Vector2 _mousePos;

        public Vector2 PlayerPos
        {
            get => _playerPos;
            set
            {
                _playerPos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerPos)));
            }
        }
        
        public Vector2 MousePos
        {
            get => _mousePos;
            set
            {
                _mousePos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MousePos)));
            }
        }
        
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SpawnProjectile();
            }
        }

        private void SpawnProjectile()
        {
            var gun = GameObject.FindGameObjectWithTag("Gun1");
            gunPos = gun.transform.position;
            
            var direction = MousePos - PlayerPos;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            // var newFlash = Instantiate(flash, gunPos, Quaternion.identity);
            // newFlash.transform.rotation = Quaternion.Euler(0, 0, angle);
            
            var newProjectile = Instantiate(laser1, gunPos, Quaternion.identity);
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            
            var distanceToMouse = Vector2.Distance(PlayerPos, MousePos);
            
            var velocity = direction.normalized * _projectileSpeed;
            
            StartCoroutine(MoveProjectile(newProjectile, velocity, distanceToMouse));
        }
        private IEnumerator MoveProjectile(GameObject projectile, Vector2 velocity, float distance)
        {
            var currentDistance = 0f;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            while (currentDistance < distance - 3.0f)
            {
                rb.velocity = velocity;
                
                currentDistance = Vector2.Distance(projectile.transform.position, PlayerPos);
                
                yield return null;
            }
            Destroy(projectile);
        }
    }
}