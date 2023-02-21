using UnityEngine;

namespace Flexalon.Examples
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 50;

        private Vector3 _lastMousePosition;
        private float xRotation;
        private float yRotation;
        private Transform _arm;
        private bool _rotating = false;

        void Start()
        {
            var euler = transform.rotation.eulerAngles;
            xRotation = euler.x;
            yRotation = euler.y;
            var armGO = new GameObject("Camera Arm");
            armGO.transform.rotation = transform.rotation;
            transform.parent = armGO.transform;
            _arm = armGO.transform;
        }

        void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (FlexalonInteractable.SelectedObject != null)
                {
                    return;
                }

                _lastMousePosition = Input.mousePosition;
                _rotating = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _rotating = false;
            }

            if (_rotating)
            {
                var delta = Input.mousePosition - _lastMousePosition;
                _lastMousePosition = Input.mousePosition;
                xRotation -= delta.y * _speed * Time.deltaTime;
                yRotation += delta.x * _speed * Time.deltaTime;
                _arm.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            }

            var scrollDelta = Input.mouseScrollDelta.y;
            if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
            {
                scrollDelta = 0;
            }


            transform.position += (transform.forward * 5 * scrollDelta * _speed * Time.deltaTime);
        }
    }
}