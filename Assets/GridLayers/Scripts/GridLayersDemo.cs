using System.Collections;
using UnityEngine;

namespace Flexalon.Examples
{
    public class GridLayersDemo : MonoBehaviour
    {
        public FlexalonGridLayout grid;
        public RadialEffector effector;
        public Pan pan;

        void Start()
        {
            StartCoroutine(Demo());
        }

        IEnumerator Demo()
        {
            pan.enabled = false;
            TurnPhysicsOn();

            yield return new WaitForSeconds(2);

            yield return Build();

            TurnPhysicsOn();

            yield return new WaitForSeconds(2);

            grid.Rows = 3;
            grid.Columns = 3;
            effector.GetComponent<FlexalonGridCell>().Cell = new Vector3Int(1, 1, 3);
            pan.enabled = true;

            yield return Build();

            TurnPhysicsOn();
        }

        void TurnPhysicsOn()
        {
            grid.enabled = false;
            effector.enabled = true;

            foreach (Transform child in grid.transform)
            {
                var lerp = child.GetComponent<FlexalonLerpAnimator>();
                if (lerp)
                {
                    lerp.InterpolationSpeed = 0;
                }

                var rb = child.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = false;
                }
            }
        }

        void TurnPhysicsOff()
        {
            grid.enabled = true;
            grid.MarkDirty();
            effector.enabled = false;
        }

        IEnumerator Build()
        {
            TurnPhysicsOff();

            foreach (Transform child in grid.transform)
            {
                var lerp = child.GetComponent<FlexalonLerpAnimator>();
                if (lerp)
                {
                    lerp.InterpolationSpeed = 0;
                }
            }

            foreach (Transform child in grid.transform)
            {
                var lerp = child.GetComponent<FlexalonLerpAnimator>();
                if (lerp)
                {
                    lerp.InterpolationSpeed = 5;
                    yield return new WaitForSeconds(0.05f);
                }

                var rb = child.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = true;
                }
            }

            // Force a re-layout so that the lerp animators start running.
            grid.MarkDirty();

            yield return new WaitForSeconds(1.0f);
        }
    }
}