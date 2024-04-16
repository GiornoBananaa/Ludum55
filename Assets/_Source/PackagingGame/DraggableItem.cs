using System;
using AudioSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PackagingGame
{
    public class DraggableItem : MonoBehaviour
    {
        [SerializeField] private float _scaleFactor = 1.2f;
        [SerializeField] private bool _returnToStartPosition;
        private Vector2 _normalScale;
        private Vector2 _offset;
        private Vector2 _defaultPosition;
        private AudioPlayer _audioPlayer;
        private bool _isAnimation;

        public Action OnDragStart;
        public Action OnDragEnd;
        public Action OnDrag;
        
        public bool IsDragged { get; private set; }

        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }
        
        private void Start()
        {
            _defaultPosition = transform.localPosition;
            IsDragged = false;
            _normalScale = transform.localScale;
        }
        
        public void OnMouseDown()
        {
            if (!enabled || _isAnimation || EventSystem.current.IsPointerOverGameObject()) return;
            if(_audioPlayer!= null)
                _audioPlayer.Play(Sounds.ItemTake);
            IsDragged = true;
            transform.localScale = _normalScale * _scaleFactor;
            transform.SetAsLastSibling();
            _offset = transform.position - Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            OnDragStart?.Invoke();
        }
        
        public void OnMouseUp()
        {
            if (!enabled || _isAnimation || !IsDragged) return;
            if(_audioPlayer!= null)
                _audioPlayer.Play(Sounds.ItemTake);
            IsDragged = false;
            transform.localScale = _normalScale;
            OnDragEnd?.Invoke();
            if (_returnToStartPosition)
                ReturnToDefaultPosition();
        }
        
        public void Update()
        {
            if (!IsDragged || !enabled || _isAnimation) return;
            Vector2 mouseWorldPos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseWorldPos + _offset;
            OnDrag?.Invoke();
        }
        
        public void ReturnToDefaultPosition()
        {
            _isAnimation = true;
            if(IsDragged)
            {
                IsDragged = false;
                transform.localScale = _normalScale;
                OnDragEnd?.Invoke();
            }
            transform.DOLocalMove(_defaultPosition, 0.2f).OnComplete(()=>_isAnimation=false);
        }
    }
}
