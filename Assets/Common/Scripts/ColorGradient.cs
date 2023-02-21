using UnityEngine;

namespace Flexalon.Examples
{
    // Changes the material or text color of each child to create a gradient.
    [ExecuteAlways, AddComponentMenu("Flexalon Templates/Color Gradient")]
    public class ColorGradient : MonoBehaviour
    {
        // First color of the gradient.
        [SerializeField]
        private Color _color1;
        public Color Color1
        {
            get => _color1;
            set
            {
                _color1 = value;
                UpdateColors(_node);
            }
        }

        // Last color of the gradient.
        [SerializeField]
        private Color _color2;
        public Color Color2
        {
            get => _color2;
            set
            {
                _color2 = value;
                UpdateColors(_node);
            }
        }

        // Should update colors when layout changes?
        [SerializeField]
        private bool _runOnLayoutChange;
        public bool RunOnLayoutChange
        {
            get => _runOnLayoutChange;
            set
            {
                _runOnLayoutChange = value;
                UpdateRunOnLayoutChange();
            }
        }

        private FlexalonNode _node;

        void OnEnable()
        {
            _node = Flexalon.GetOrCreateNode(gameObject);
            UpdateRunOnLayoutChange();
            UpdateColors(_node);
        }

        void UpdateRunOnLayoutChange()
        {
            _node.ResultChanged -= UpdateColors;
            if (_runOnLayoutChange)
            {
                _node.ResultChanged += UpdateColors;
            }
        }

        void OnDisable()
        {
            _node.ResultChanged -= UpdateColors;
        }

        public void Refresh()
        {
            UpdateColors(_node);
        }

        private void UpdateColors(FlexalonNode node)
        {
            foreach (Transform child in transform)
            {
                var color = Color.Lerp(_color1, _color2, (float)(child.GetSiblingIndex()) / transform.childCount);
#if UNITY_TMPRO
                if (child.TryGetComponent<TMPro.TMP_Text>(out var text))
                {
                    text.color = color;
                } else
#endif
                if (child.TryGetComponent<MeshRenderer>(out var mr))
                {
                    var propertyBlock = new MaterialPropertyBlock();
                    propertyBlock.SetColor("_Color", color);
                    mr.SetPropertyBlock(propertyBlock);
                }
            }
        }
    }
}