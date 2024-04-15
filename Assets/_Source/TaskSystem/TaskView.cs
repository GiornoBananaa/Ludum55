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
                "<u><b>Body parts</b></u>\n" +
                $"Head - {head.Descriptions[Random.Range(0, head.Descriptions.Length)]}\n" +
                $"Body - {body.Descriptions[Random.Range(0, body.Descriptions.Length)]}\n" +
                $"Legs - {legs.Descriptions[Random.Range(0, legs.Descriptions.Length)]}\n" +
                $"<u><b>Clothes</b></u>\n" +
                $"Headdress - {headClothes.Descriptions[Random.Range(0, headClothes.Descriptions.Length)]}\n" +
                $"Top - {bodyClothes.Descriptions[Random.Range(0, bodyClothes.Descriptions.Length)]}\n" +
                $"Bottom - {legsClothes.Descriptions[Random.Range(0, legsClothes.Descriptions.Length)]}\n";
        }
    }
}