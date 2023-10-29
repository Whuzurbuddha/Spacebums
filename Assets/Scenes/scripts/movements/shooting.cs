using System.ComponentModel;
using UnityEngine;

namespace Scenes.scripts.movements
{
    public class Shooting : MonoBehaviour, INotifyPropertyChanged
    {
        private static float _projectileSpeed = 3.0f;
        public GameObject projectilePrefab;

        public event PropertyChangedEventHandler PropertyChanged;
        
        private Vector3 _playerPos;
        private float _angle;
        private Vector3 _direction;

        public Vector3 PlayerPos
        {
            get => _playerPos;
            set
            {
                _playerPos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerPos)));
            }
        }

        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Angle)));
            }
        }

        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Direction)));
            }
        }
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            SpawmProjectile();
        }

        private void SpawmProjectile()
        {
            var newProjectile = Instantiate(projectilePrefab, PlayerPos, Quaternion.identity);
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, Angle);
            newProjectile.GetComponent<Rigidbody2D>().velocity = Direction.normalized * _projectileSpeed;
        }
    }
}