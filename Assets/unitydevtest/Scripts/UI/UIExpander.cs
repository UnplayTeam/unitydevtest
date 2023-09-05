using DG.Tweening;
using UnityEngine;

namespace JoshBowersDEV
{
    /// <summary>
    /// A simple UI script for expanding the size delta to/from a target.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UIExpander : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private bool _isExpanded = true;

        [SerializeField]
        private float _tweenDuration = 0.2f;

        [SerializeField]
        private Vector2 _targetVector = Vector2.zero;

        private Vector2 _originalVector;

        private void Awake()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            _originalVector = _rectTransform.sizeDelta;
            _targetVector = new Vector2(_rectTransform.sizeDelta.x, _targetVector.y);

            ToggleExpanded();
        }

        public void ToggleExpanded()
        {
            _isExpanded = !_isExpanded;

            if (_isExpanded)
            {
                Expand(_originalVector);
            }
            else
            {
                Expand(_targetVector);
            }
        }

        // Simple sequence that updates the sizeDelta to the target vector
        private Sequence Expand(Vector2 target)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectTransform.DOSizeDelta(target, _tweenDuration));
            return sequence;
        }
    }
}