using UnityEngine;

namespace Flexalon.Examples
{
    public class RadialEffector : MonoBehaviour
    {
        public float force;
        public float falloff;

        private Collider[] _colliders = new Collider[5 * 5 * 5];

        void FixedUpdate()
        {
            int cnt = Physics.OverlapSphereNonAlloc(transform.position, falloff, _colliders);
            for (int i = 0; i < cnt; i++)
            {
                Rigidbody rb = _colliders[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 dir = (rb.position - transform.position) + 0.2f * Random.onUnitSphere;
                    dir.y = 0;

                    float dist = dir.magnitude;
                    if (dist < falloff)
                    {
                        float f = force * (1 - dist / falloff) * Time.deltaTime;
                        rb.AddForce(dir.normalized * f);
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, falloff);
        }
    }
}