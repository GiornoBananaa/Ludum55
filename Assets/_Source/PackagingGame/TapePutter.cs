using System;
using UnityEngine;

namespace PackagingGame
{
    public class TapePutter : MonoBehaviour
    {
        [SerializeField] private LayerMask _maskLayerMask;
        [SerializeField] private Transform _mask;
        [SerializeField] private DraggableItem _tape;
        [SerializeField] private float _maskStartY;
        [SerializeField] private float _maskEndY;
        private float _lastTapeY;

        public Action OnTapePutted;
        
        private void Awake()
        {
            _lastTapeY = _mask.position.y;
        }

        private void Update()
        {
            PutTape();
        }

        private void PutTape()
        {
            if (_tape.IsDragged)
            {
                Collider2D collider = Physics2D.OverlapPoint(_tape.transform.position, _maskLayerMask);
                if (collider != null && collider.transform == _mask)
                {
                    if (_tape.transform.position.y < _lastTapeY)
                    {
                        _lastTapeY = _tape.transform.position.y;
                        float percent = (_lastTapeY-_maskStartY)/(_maskEndY-_maskStartY);
                        _mask.position = new Vector2(_mask.position.x, 
                            Mathf.Lerp(_maskStartY,_maskEndY, percent));

                        if (percent >= 1)
                        {
                            OnTapePutted?.Invoke();
                        }
                    }
                }
            }
        }
        
        public void Reset()
        {
            _lastTapeY = 0;
            _mask.position = _mask.position = new Vector2(_mask.position.x, _maskStartY);
        }
    }
}
