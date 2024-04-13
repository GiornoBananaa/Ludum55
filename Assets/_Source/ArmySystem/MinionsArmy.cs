using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    public class MinionsArmy: MonoBehaviour
    {
        private readonly List<Minion> _minions = new();

        public void AddMinion(GameObject unit)
        {
            AddMinion(unit.GetComponent<Minion>());
        }
    
        public void AddMinion(Minion unit)
        {
            _minions.Add(unit);
        }

        public void RemoveMinion(GameObject unit)
        {
            RemoveMinion(unit.GetComponent<Minion>());
        }
        
        public void RemoveMinion(Minion unit)
        {
            _minions.Add(unit);
        }

        public void MoveTo(Vector2 position)
        {
            foreach (var minion in _minions)
            {
                minion.MoveWithArmyTo(position);
            }
        }
    }
}
