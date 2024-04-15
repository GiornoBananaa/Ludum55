using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using GameStatesSystem;
using PackagingGame;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SummoningGame
{
    public class Lighter : MonoBehaviour
    {
        [SerializeField] private DraggableItem _draggable;
        [SerializeField] private DraggableItem _box;
        [SerializeField] private Transform _lighterPivot;
        [SerializeField] private LayerMask _candleLayerMask;
        [SerializeField] private LayerMask _pentagramLayerMask;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _lightenedSprite;
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private SpriteRenderer _pentagramSpriteRenderer;
        [SerializeField] private Sprite _defaultPentagram;
        [SerializeField] private Sprite _activatedPentagram;
        [SerializeField] private float _orderHighlitingDuration;
        [SerializeField] private Candle[] _candles;
        
        private List<Candle> _candelsInOrder = new List<Candle>();
        private List<Candle> _lightedCandles = new List<Candle>();
        private Collider2D _lastCollider;
        private int _setupedCandles;
        private bool _isBurning;
        private AudioPlayer _audioPlayer;
        private Game _game;
        
        public bool IsBurning
        {
            get => _isBurning;
            set
            {
                if(!_isBurning &&  value)
                {
                    _draggable.enabled = true;
                    _spriteRenderer.sprite = _lightenedSprite;
                }
                else if(_isBurning &&  !value)
                {
                    _draggable.enabled = false;
                    _draggable.ReturnToDefaultPosition();
                    _spriteRenderer.sprite = _closedSprite;
                }
                _isBurning = value;
            }
        }
        
        public void Construct(AudioPlayer audioPlayer, Game game)
        {
            _audioPlayer = audioPlayer;
            _game = game;
        }

        public void SetBox(GameObject box)
        {
            _box = box.GetComponent<DraggableItem>();
            _box.OnDragEnd += CheckBox;
            _box.enabled = false;
        }
        
        private void Start()
        {
            _pentagramSpriteRenderer.sprite = _defaultPentagram;
            _draggable.enabled = false;
            _draggable.OnDrag += CheckSurface;
            ShuffleCandlesOrder();
            foreach (var candle in _candles)
            {
                candle.OnCandleSetup += SetupCandle;
            }
            _draggable.OnDragStart += PlayFireSound;
            _draggable.OnDragEnd += StopFireSound;
        }

        private void SetupCandle()
        {
            _setupedCandles++;
            if (_setupedCandles >= _candles.Length)
            {
                IsBurning = true;
                StartCoroutine(OrderHighlighting());
            }
        }
        
        private void ShuffleCandlesOrder()
        {
            _candelsInOrder.Clear();
            List<Candle> buf = new List<Candle>(_candles);
            for (int i = 0; i < _candles.Length; i++)
            {
                Candle c = buf[Random.Range(0, buf.Count)];
                _candelsInOrder.Add(c);
                buf.Remove(c);
            }
        }
        
        private void CheckSurface()
        {
            Collider2D collider = Physics2D.OverlapPoint(_lighterPivot.position, _candleLayerMask);
            if (collider != null)
            {
                if (_lastCollider != collider)
                {
                    Candle candle = collider.GetComponent<Candle>();
                    if (candle.LightCandle())
                    {
                        if (_candelsInOrder[_lightedCandles.Count] != candle)
                        {
                            foreach (var c in _candles)
                            {
                                c.ExtinguishCandle();
                            }
                            _lightedCandles.Clear();
                            _draggable.ReturnToDefaultPosition();
                            StartCoroutine(OrderHighlighting());
                        }
                        else
                        {
                            _audioPlayer.Play(Sounds.LighterInteraction);
                            _lightedCandles.Add(candle);
                            if (_lightedCandles.Count == _candles.Length)
                            {
                                foreach (var c in _candles)
                                {
                                    c.ActivateCandle();
                                }
                                _box.enabled = true;
                                _draggable.enabled = false;
                                _draggable.ReturnToDefaultPosition();
                            } 
                        }
                    }
                    _lastCollider = collider;
                }
            }
            else
            {
                _lastCollider = null;
            }
        }
        
        public void Reset()
        {
            foreach (var candle in _candles)
            {
                candle.Reset();
            }
            _box.transform.SetParent(transform.parent);
            _lightedCandles.Clear();
            IsBurning = false;
            _box.enabled = false;
            _box.gameObject.SetActive(true);
            _box.ReturnToDefaultPosition();
            _setupedCandles = 0;
            _pentagramSpriteRenderer.sprite = _defaultPentagram;
            _draggable.enabled = false;
            ShuffleCandlesOrder();
        }
        
        private void PlayFireSound()
        {
            _audioPlayer.Play(Sounds.BurningLighter);
        }
        
        private void StopFireSound()
        {
            _audioPlayer.Stop(Sounds.BurningLighter);
        }
        
        private void CheckBox()
        {
            Collider2D collider = Physics2D.OverlapPoint(_box.transform.position, _pentagramLayerMask);
            if (collider != null)
            {
                _box.enabled = false;
                _box.enabled = false;
                _box.transform.SetParent(collider.transform);
                StartCoroutine(BoxSending());
            }
            else
            {
                _draggable.ReturnToDefaultPosition();
            }
        }
        
        private IEnumerator OrderHighlighting()
        {
            yield return new WaitForSeconds(_orderHighlitingDuration);
            foreach (var candle in _candelsInOrder)
            {
                candle.EnableHighlight();
                yield return new WaitForSeconds(_orderHighlitingDuration);
                candle.DisableHighlight();
            }
        }
        
        private IEnumerator BoxSending()
        {
            yield return new WaitForSeconds(0.5f);
            _pentagramSpriteRenderer.sprite = _activatedPentagram;
            _audioPlayer.Play(Sounds.PentagramActivation);
            yield return new WaitForSeconds(1);
            _audioPlayer.Play(Sounds.PackageSend);
            _box.gameObject.SetActive(false);
            foreach (var c in _candles)
            {
                c.ExtinguishCandle();
            }
            _pentagramSpriteRenderer.sprite = _defaultPentagram;
            yield return new WaitForSeconds(1);
            _game.ChangeState(GameScreen.EndResult);
        }
        
        private void OnDestroy()
        {
            _draggable.OnDrag -= CheckSurface;
            _draggable.OnDragStart -= PlayFireSound;
            _draggable.OnDragEnd -= StopFireSound;
            foreach (var candle in _candles)
            {
                candle.OnCandleSetup -= SetupCandle;
            }
        }
    }
}
