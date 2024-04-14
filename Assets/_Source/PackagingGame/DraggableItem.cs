using System;
using DG.Tweening;
using UnityEngine;

namespace PackagingGame
{
    public class DraggableItem : MonoBehaviour
    {
        [SerializeField] private float _scaleFactor = 1.2f;
        [SerializeField] private bool _returnToStartPosition;
        private Vector2 _normalScale;
        private Vector2 _offset;
        private Vector2 _defaultPosition;

        public Action OnDragStart;
        public Action OnDragEnd;
        
        public bool IsDragged { get; private set; }

        private void Start()
        {
            _defaultPosition = transform.localPosition;
            IsDragged = false;
            _normalScale = transform.localScale;
        }
        
        public void OnMouseDown()
        {
            if (!enabled) return;
            IsDragged = true;
            transform.localScale = _normalScale * _scaleFactor;
            transform.SetAsLastSibling();
            _offset = transform.position - Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            OnDragStart?.Invoke();
        }
        
        public void OnMouseUp()
        {
            IsDragged = false;
            transform.localScale = _normalScale;
            OnDragEnd?.Invoke();
            if (_returnToStartPosition)
                ReturnToDefaultPosition();
        }
        
        public void OnMouseDrag()
        {
            if (!enabled) return;
            Vector2 mouseWorldPos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseWorldPos + _offset;
        }
        
        public void ReturnToDefaultPosition()
        {
            transform.DOLocalMove(_defaultPosition, 0.2f);
        }
    }
}
