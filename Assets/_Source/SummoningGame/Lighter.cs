using System;
using System.Collections;
using System.Collections.Generic;
using PackagingGame;
using UnityEngine;
using UnityEngine.Serialization;
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
        
        private void Start()
        {
            _pentagramSpriteRenderer.sprite = _defaultPentagram;
            _draggable.enabled = false;
            _draggable.OnDrag += CheckSurface;
            _box.OnDragEnd += CheckBox;
            _box.enabled = false;
            ShuffleCandlesOrder();
            foreach (var candle in _candles)
            {
                candle.OnCandleSetup += SetupCandle;
            }
        }

        private void SetupCandle()
        {
            _setupedCandles++;
            if (_setupedCandles >= _candles.Length)
            {
                _spriteRenderer.sprite = _lightenedSprite;
                _draggable.enabled = true;
                StartCoroutine(OrderHighlighting());
            }
        }
        
        private void ShuffleCandlesOrder()
        {
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
            _lightedCandles.Clear();
            _draggable.enabled = false;
            _box.enabled = false;
            _box.ReturnToDefaultPosition();
            _box.gameObject.SetActive(true);
            _setupedCandles = 0;
            _spriteRenderer.sprite = _closedSprite;
            ShuffleCandlesOrder();
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
            yield return new WaitForSeconds(1);
            _pentagramSpriteRenderer.sprite = _activatedPentagram;
            yield return new WaitForSeconds(1);
            _box.gameObject.SetActive(false);
            foreach (var c in _candles)
            {
                c.ExtinguishCandle();
            }
            _pentagramSpriteRenderer.sprite = _defaultPentagram;
            yield return new WaitForSeconds(1);
            // open end panel
        }
        
        private void OnDestroy()
        {
            _draggable.OnDrag -= CheckSurface;
            foreach (var candle in _candles)
            {
                candle.OnCandleSetup -= SetupCandle;
            }
        }
    }
}
