using PackagingGame;
using TaskSystem;
using UnityEngine;

namespace GameStatesSystem
{
    public class EndResultGameState : GameState
    {
        private SwitchBodyParts _switchBodyParts;
        private SwitchClothes _switchClothes;
        private EndResultView _endResultView;
        private TaskGeneration _taskGeneration;
        private Tape _tape;
        private Mark[] _marks;

        public EndResultGameState(SwitchBodyParts switchBodyParts, SwitchClothes switchClothes, 
            EndResultView endResultView, TaskGeneration taskGeneration,Tape tape,Mark[] marks)
        {
            _switchBodyParts = switchBodyParts;
            _switchClothes = switchClothes;
            _endResultView = endResultView;
            _taskGeneration = taskGeneration;
            _tape = tape;
            _marks = marks;
        }

        public override void Enter()
        {
            Owner.OnGameScreenChanged?.Invoke(GameScreen.EndResult);
            _endResultView.SetResults(
                _taskGeneration.HeadTask.Sprite==_switchBodyParts.ImageHead?111:0,
                _taskGeneration.BodyTask.Sprite==_switchBodyParts.ImageBody?111:0,
                _taskGeneration.LegsTask.Sprite==_switchBodyParts.ImageLegs?111:0,
                _taskGeneration.HeadClothesTask.Sprite==_switchClothes.ImageHeadClothes?111:0,
                _taskGeneration.BodyClothesTask.Sprite==_switchClothes.ImageBodyClothes?111:0,
                _taskGeneration.LegsClothesTask.Sprite==_switchClothes.ImageLegsClothes?111:0);
            _endResultView.OpenPanel();
        }

        public override void Exit()
        {
            _endResultView.ClosePanel();
            Owner.Reset();
        }

        public override void Reset()
        {
            _taskGeneration.GenerateTask();
            _tape.Reset();
            foreach (var mark in _marks)
            {
                mark.ReturnToDefaultParent();
            }
        }
    }
}