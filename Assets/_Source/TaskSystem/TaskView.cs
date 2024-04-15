using TMPro;
using UnityEngine;

namespace TaskSystem
{
    public class TaskView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _taskText;
        
        public void SetTaskText(DemonPart head,DemonPart body,DemonPart legs,DemonPart headClothes,DemonPart bodyClothes,DemonPart legsClothes)
        {
             if(head.Descriptions.Length == 0) return;
            _taskText.text =
                $"Голова - {head.Descriptions[Random.Range(0, head.Descriptions.Length)]}\n" +
                $"Тело - {body.Descriptions[Random.Range(0, body.Descriptions.Length)]}\n" +
                $"Ноги - {legs.Descriptions[Random.Range(0, legs.Descriptions.Length)]}\n" +
                $"Головной убор - {bodyClothes.Descriptions[Random.Range(0, bodyClothes.Descriptions.Length)]}\n" +
                $"Верхняя одежда - {legsClothes.Descriptions[Random.Range(0, legsClothes.Descriptions.Length)]}\n" +
                $"Низ - {headClothes.Descriptions[Random.Range(0, headClothes.Descriptions.Length)]}\n";
        }
    }
}