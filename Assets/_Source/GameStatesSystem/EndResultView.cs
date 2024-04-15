using System;
using TMPro;
using UI;
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

        private TransitionLauncher _transitionLauncher;
        
        public void Construct(TransitionLauncher transitionLauncher)
        {
            _transitionLauncher = transitionLauncher;
        }
        
        private void Start()
        {
            _continueButton.onClick.AddListener(NextTask);
            _taskCall.TaskDisable();
        }

        public void SetResults(int head,int body,int legs,int headClothes,int bodyClothes,int legsClothes)
        {
            _taskResultText.text = 
                $"Head - {head} XP\n" +
                $"Body - {body} XP\n" +
                $"Legs - {legs} XP\n" +
                $"Headdress - {headClothes} XP\n" +
                $"Top - {bodyClothes} XP\n" +
                $"Bottom - {legsClothes} XP\n";
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
            _transitionLauncher.MoveStart();
        }
    }
}