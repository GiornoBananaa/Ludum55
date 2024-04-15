﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameStatesSystem
{
    public class EndResultView: MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private TaskCall _taskCall;
        [SerializeField] private TMP_Text _taskResultText;
        [SerializeField] private TMP_Text _overallPointsText;
        private Game _game;

        public void Construct(Game game)
        {
            _game = game;
        }
        
        private void Start()
        {
            _continueButton.onClick.AddListener(NextTask);
        }

        public void SetResults(int head,int body,int legs,int headClothes,int bodyClothes,int legsClothes)
        {
            _taskResultText.text = 
                $"Голова - {head} XP\n" +
                $"Тело - {body} XP\n" +
                $"Ноги - {legs} XP\n" +
                $"Головной убор - {headClothes} XP\n" +
                $"Верхняя одежда - {bodyClothes} XP\n" +
                $"Низ - {legsClothes} XP\n";
            _overallPointsText.text = (head + body+ legs + headClothes+ bodyClothes + legsClothes)+"XP";
        }

        public void OpenPanel()
        {
            _taskCall.TaskEnable();
        }
        
        public void ClosePanel()
        {
            _taskCall.TaskDisable();
        }
        
        private void NextTask()
        {
            _game.ChangeState(GameScreen.DemonBodyChoice);
        }
    }
}