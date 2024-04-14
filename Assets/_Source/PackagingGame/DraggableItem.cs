using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PackagingGame
{
    public class DraggableItem : MonoBehaviour
    {
        [SerializeField] private float _scaleFactor = 1f;
        private Vector3 _normalScale;
        private Vector3 _offset;
        private Shadow _shadow;
        
        private void Start()
        {
            _normalScale = transform.localScale;
            TryGetComponent(out _shadow);
            if (_shadow)
                _shadow.enabled = false;
        }
    
        public void OnMouseDown()
        {
            transform.localScale = _normalScale * _scaleFactor;
            transform.SetAsLastSibling();
            if (_shadow)
                _shadow.enabled = true;
        }
    
        public void OnMouseUp()
        {
            transform.localScale = _normalScale;
        }

        public void OnBeginDrag()
        {
            transform.localScale = _normalScale * _scaleFactor;
        }

        public void OnDrag()
        {
            //transform.
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if (eventData.button != PointerEventData.InputButton.Left) return;

            transform.localScale = _normalScale;
        
            if (_shadow)
                _shadow.enabled = false;
        }
    }
}
