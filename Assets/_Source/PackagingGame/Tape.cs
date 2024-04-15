using System;
using AudioSystem;
using UnityEngine;

namespace PackagingGame
{
    public class Tape : MonoBehaviour
    {
        [SerializeField] private LayerMask _maskLayerMask;
        [SerializeField] private Transform _mask;
        [SerializeField] private DraggableItem _tape;
        [SerializeField] private float _maskStartY;
        [SerializeField] private float _maskEndY;
        private float _lastTapeY;
        private AudioPlayer _audioPlayer;
        private bool _puttingTape;
        private Vector2 _maskDefault;
        
        public Action OnTapePutted;
        
        public bool PuttingTape
        {
            get => _puttingTape;
            set
            {
                if(_puttingTape &&  !value)
                    _audioPlayer.Stop(Sounds.TapePutting);
                else if(!_puttingTape &&  value)
                    _audioPlayer.Play(Sounds.TapePutting);
                _puttingTape = value;
            }
        }

        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }
        
        private void Awake()
        {
            _lastTapeY = _mask.position.y;
            _maskDefault = _mask.localPosition;
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
                        PuttingTape = true;
                        _lastTapeY = _tape.transform.position.y;
                        float percent = (_lastTapeY-_maskStartY)/(_maskEndY-_maskStartY);
                        _mask.position = new Vector2(_mask.position.x, 
                            Mathf.Lerp(_maskStartY,_maskEndY, percent));

                        if (percent >= 1)
                        {
                            _audioPlayer.Play(Sounds.TapePuttingEnd);
                            OnTapePutted?.Invoke();
                        }
                    }
                    else
                    {
                        PuttingTape = false;
                    }
                }
                else
                {
                    PuttingTape = false;
                }
            }
            else
            {
                PuttingTape = false;
            }
        }
        
        public void Reset()
        {
            _mask.localPosition = _maskDefault;
            _lastTapeY = _mask.position.y;
            _tape.enabled = true;
            _tape.ReturnToDefaultPosition();
        }
    }
}
