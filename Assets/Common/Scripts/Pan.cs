using UnityEngine;

namespace Flexalon.Examples
{
    public class Pan : MonoBehaviour
    {
        public Vector3 fromPosition;
        public Vector3 toPosition;
        public Quaternion fromRotation = Quaternion.identity;
        public Quaternion toRotation = Quaternion.identity;
        public float time = 1.0f;

        private float startTime;

        void OnEnable()
        {
            transform.localPosition = fromPosition;
            transform.localRotation = fromRotation;
            startTime = Time.time;
        }

        void Update()
        {
            if (fromPosition != toPosition)
            {
                transform.localPosition = Vector3.Lerp(fromPosition, toPosition, (Time.time - startTime) / time);
            }

            if (fromRotation != toRotation)
            {
                transform.localRotation = Quaternion.Slerp(fromRotation, toRotation, (Time.time - startTime) / time);
            }
        }
    }
}