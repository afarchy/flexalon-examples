using UnityEngine;

namespace Flexalon.Examples
{
    public class Bounce : MonoBehaviour
    {
        public float speed = 1.0f;
        public float distance = 0.1f;

        private float offset;

        void Start()
        {
            offset = Random.Range(0f, 2f * Mathf.PI);
        }

        void Update()
        {
            transform.localPosition = Vector3.up * distance * Mathf.Sin(Time.time * speed + offset);
        }
    }
}