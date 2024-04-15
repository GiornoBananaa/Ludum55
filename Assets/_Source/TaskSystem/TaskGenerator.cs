using UnityEngine;

namespace TaskSystem
{
    public class TaskGeneration
    {
        private TaskView _taskView;
        private DemonParts _demonParts;

        public DemonPart Head { get; private set; }
        public DemonPart BodyTask{ get; private set; }
        public DemonPart LegsTask{ get; private set; }
        public DemonPart HeadClothesTask{ get; private set; }
        public DemonPart BodyClothesTask{ get; private set; }
        public DemonPart LegsClothesTask{ get; private set; }
        
        public TaskGeneration(TaskView taskView, DemonParts demonParts)
        {
            _taskView = taskView;
            _demonParts = demonParts;
            GenerateTask();
        }
        
        public (DemonPart head,DemonPart body,DemonPart legs,DemonPart headClothes,DemonPart bodyClothes,DemonPart legsClothes) GenerateTask()
        {
            Head = _demonParts.Head[Random.Range(0,_demonParts.Head.Length)];
            BodyTask = _demonParts.Body[Random.Range(0,_demonParts.Body.Length)];
            LegsTask = _demonParts.Legs[Random.Range(0,_demonParts.Legs.Length)];
            HeadClothesTask = _demonParts.HeadClothes[Random.Range(0,_demonParts.HeadClothes.Length)];
            BodyClothesTask = _demonParts.BodyClothes[Random.Range(0,_demonParts.BodyClothes.Length)];
            LegsClothesTask = _demonParts.LegsClothes[Random.Range(0,_demonParts.LegsClothes.Length)];
            
            _taskView.SetTaskText(Head, BodyTask, LegsTask, HeadClothesTask, BodyClothesTask, LegsClothesTask);
            
            return (Head, BodyTask, LegsTask, HeadClothesTask, BodyClothesTask, LegsClothesTask);
        }
    }
}
