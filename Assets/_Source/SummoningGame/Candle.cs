using System;
using PackagingGame;
using UnityEngine;
using UnityEngine.Serialization;

namespace SummoningGame
{
    [RequireComponent(typeof(DraggableItem))]
    public class Candle : MonoBehaviour
    {
        [SerializeField] private DraggableItem _draggable;
        [SerializeField] private LayerMask _candleStandLayerMask;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _extinguishSprite;
        [SerializeField] private Sprite _lightenedSprite;
        [SerializeField] private Sprite _activatedSprite;
        
        private bool _isLighted = false;
        private bool _isSetuped= false;
        
        public event Action OnCandleSetup;
        public event Action OnCandleLighted;
        
        private void Start()
        {
            _spriteRenderer.sprite = _extinguishSprite;
            _draggable.OnDragEnd += CheckSurface;
        }

        public bool LightCandle()
        {
            if (_isLighted || !_isSetuped) return false;
            _spriteRenderer.sprite = _lightenedSprite;
            _isLighted = true;
            Debug.Log(_spriteRenderer.sprite);
            OnCandleLighted?.Invoke();
            return true;
        }

        public void ExtinguishCandle()
        {
            _isLighted = false;
            _spriteRenderer.sprite = _extinguishSprite;
        }

        public void ActivateCandle()
        {
            _spriteRenderer.sprite = _activatedSprite;
        }

        public void EnableHighlight()
        {
            
        }

        public void DisableHighlight()
        {
            
        }
        private void CheckSurface()
        {
            Collider2D collider = Physics2D.OverlapPoint(transform.position, _candleStandLayerMask);
            if (collider != null)
            {
                _draggable.enabled = false;
                transform.SetParent(collider.transform);
                _isSetuped = true;
                OnCandleSetup?.Invoke();
            }
            else
            {
                _draggable.ReturnToDefaultPosition();
            }
        }
        
        public void Reset()
        {
            _isSetuped = false;
            ExtinguishCandle();
            _draggable.ReturnToDefaultPosition();
        }

        private void OnDestroy()
        {
            _draggable.OnDragEnd -= CheckSurface;
        }
    }
}
