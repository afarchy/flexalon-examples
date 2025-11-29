using System.Collections;
using UnityEngine;

namespace Flexalon.Examples
{
    public class FruitGame : MonoBehaviour
    {
        public Transform Grid;
        public GameObject[] FruitPrefabs;
        public float SpawnSpeed = 0.5f;
        public float GrowSpeed = 10f;

        private FlexalonInteractable _selected;

        void Start()
        {
            foreach (var interactable in Grid.GetComponentsInChildren<FlexalonInteractable>())
            {
                Debug.Log("Adding listener to " + interactable.name);
                interactable.SelectStart.AddListener(OnSelected);
            }
        }

        private void OnSelected(FlexalonInteractable interactable)
        {
            if (!_selected)
            {
                _selected = interactable;
            }
            else if (_selected != interactable)
            {
                if (_selected.name.Split(' ')[0] == interactable.name.Split(' ')[0])
                {
                    Destroy(_selected.gameObject);
                    Destroy(interactable.gameObject);
                    StartCoroutine(SpawnNewFruit());
                    StartCoroutine(SpawnNewFruit());
                }
                else
                {
                    _selected = interactable;
                }
            }
        }

        private IEnumerator SpawnNewFruit()
        {
            yield return new WaitForSeconds(SpawnSpeed);

            var fruit = Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)]);
            var scale = Random.Range(0.5f, 2.0f);

            foreach (Transform pos in Grid.transform)
            {
                if (pos.childCount == 0)
                {
                    fruit.transform.parent = pos;
                    fruit.transform.localPosition = Vector3.zero;
                    break;
                }
            }

            fruit.transform.localScale = Vector3.zero;
            fruit.GetComponent<FlexalonInteractable>().SelectStart.AddListener(OnSelected);

            while (fruit && fruit.transform.localScale.x < 1f)
            {
                fruit.transform.localScale = Vector3.Lerp(fruit.transform.localScale, Vector3.one * scale, Time.deltaTime * GrowSpeed);
                yield return null;
            }
        }
    }
}