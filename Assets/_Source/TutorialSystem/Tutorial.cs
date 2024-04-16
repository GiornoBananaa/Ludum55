using System;
using GameStatesSystem;
using InputSystem;
using SummoningGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace TutorialSystem
{
    public class Tutorial : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _tutorialPanel;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _tutorialSprites;
        
        private int _currentImage;
        private Game _game;
        private InputListener _inputListener;

        public void Construct(Game game, InputListener inputListener)
        {
            _game = game;
            _inputListener = inputListener;
        }
        
        public void StartTutorial()
        {
            _currentImage = 0;
            _image.sprite = _tutorialSprites[_currentImage];
            _tutorialPanel.gameObject.SetActive(true);
            Show();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            NextSprite();
        }
        
        public void EndTutorial()
        {
            _tutorialPanel.gameObject.SetActive(false);
            Hide();
        }
        
        private void NextSprite()
        {
            if (_currentImage < _tutorialSprites.Length-1) 
            {
                _currentImage++;
                _image.sprite = _tutorialSprites[_currentImage];
                CheckTutorialPlan();
            }
            else
            {
                EndTutorial();
            }
        }
        
        private void CheckTutorialPlan()
        {
            switch (_currentImage)
            {
                case 3:
                    Hide();
                    _game.OnGameStateChanged += OnPackageSection;
                    break;
                case 6:
                    Hide();
                    _game.OnGameStateChanged += OnPentagramSection;
                    break;
                case 9:
                    Hide();
                    _game.OnGameStateChanged += OnEndResult;
                    break;
            }
        }

        private void OnPackageSection(GameScreen gameScreen)
        {
            if (gameScreen!=GameScreen.Packaging) return;
            
            Show();
            _game.OnGameStateChanged -= OnPackageSection;
        }
        
        private void OnPentagramSection(GameScreen gameScreen)
        {
            if (gameScreen!=GameScreen.DemonSummoning) return;
            
            Show();
            _game.OnGameStateChanged -= OnPentagramSection;
        }
        
        private void OnEndResult(GameScreen gameScreen)
        {
            if (gameScreen!=GameScreen.EndResult) return;
            
            Show();
            _game.OnGameStateChanged -= OnEndResult;
        }
        
        private void Hide()
        {
            _inputListener.enabled = true;
            _image.gameObject.SetActive(false);
        }
        
        private void Show()
        {
            _inputListener.enabled = false;
            _image.gameObject.SetActive(true);
        }
    }
}
