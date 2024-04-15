using System;
using AudioSystem;
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
        [SerializeField] private SpriteOutliner _spriteOutliner;
        [SerializeField] private Sprite _extinguishSprite;
        [SerializeField] private Sprite _lightenedSprite;
        [SerializeField] private Sprite _activatedSprite;
        
        private bool _isLighted = false;
        private bool _isSetuped= false;
        private AudioPlayer _audioPlayer;
        
        public event Action OnCandleSetup;
        public event Action OnCandleLighted;

        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }
        
        private void Start()
        {
            _spriteRenderer.sprite = _extinguishSprite;
            _draggable.OnDragEnd += CheckSurface;
            _draggable.OnDragStart += PlayCandleSound;
            _draggable.OnDragEnd += PlayCandleSound;
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
            _spriteOutliner.EnableOutline();
        }

        public void DisableHighlight()
        {
            _spriteOutliner.DisableOutline();
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

        private void PlayCandleSound()
        {
            _audioPlayer.Play(Sounds.CandleInteraction);
        }
        
        public void Reset()
        {
            if (_isSetuped)
            {
                _draggable.transform.SetParent(transform.parent.parent.parent);
                _isSetuped = false;
            }
            
            ExtinguishCandle();
            _draggable.enabled = true;
            _draggable.ReturnToDefaultPosition();
        }

        private void OnDestroy()
        {
            _draggable.OnDragEnd -= CheckSurface;
            _draggable.OnDragStart -= PlayCandleSound;
            _draggable.OnDragEnd -= PlayCandleSound;
        }
    }
}
