using System.Collections;
using UnityEngine;

namespace Scenes.scripts.enemy.movement
{
    public class Blackhole : MonoBehaviour
    {
        private float _rotationSpeed = 20;
        private void Start()
        {
            StartCoroutine(RotateBlackhole());
        }
        IEnumerator RotateBlackhole()
        {
            while (true)
            {
                Debug.Log("STARTET ROTATION");
                transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}