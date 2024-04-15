using System;
using UnityEngine;

namespace TaskSystem
{
    [CreateAssetMenu(fileName = "DemonParts",menuName = "SO/DemonPartsData")]
    public class DemonParts: ScriptableObject
    {
        [field: SerializeField] public DemonPart[] Head;
        [field: SerializeField] public DemonPart[] Body;
        [field: SerializeField] public DemonPart[] Legs;
        [field: SerializeField] public DemonPart[] HeadClothes;
        [field: SerializeField] public DemonPart[] BodyClothes;
        [field: SerializeField] public DemonPart[] LegsClothes;
    }
    
    [Serializable]
    public class DemonPart
    {
        public Sprite Sprite;
        public string[] Descriptions;
    }
}
