using UnityEngine;

namespace PackagingGame
{
    public class Mark : MonoBehaviour
    {
        [SerializeField] private DraggableItem _draggable;
        [SerializeField] private LayerMask _boxLayerMask;

        private Transform _defaultParent;
        
        private void Start()
        {
            _draggable.OnDragEnd += CheckSurface;
            _defaultParent = transform.parent;
        }

        public void ReturnToDefaultParent()
        {
            transform.parent = _defaultParent;
        }
        
        private void CheckSurface()
        {
            Collider2D collider = Physics2D.OverlapPoint(transform.position, _boxLayerMask);
            if (collider != null)
            {
                _draggable.enabled = false;
                transform.SetParent(collider.transform);
            }
            else
            {
                _draggable.ReturnToDefaultPosition();
            }
        }
        
        private void OnDestroy()
        {
            _draggable.OnDragEnd -= CheckSurface;
        }
    }
}