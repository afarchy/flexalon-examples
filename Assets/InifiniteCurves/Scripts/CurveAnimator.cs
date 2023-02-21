using UnityEngine;

namespace Flexalon.Samples
{
    public class CurveAnimator : MonoBehaviour
    {
        public float _speed = 1.0f;

        void Update()
        {
            GetComponent<FlexalonCurveLayout>().StartAt += _speed * Time.deltaTime;
        }
    }
}