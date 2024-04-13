using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    public class UnitsPool<T> where T : Unit
    {
        private HashSet<T> _activeObjects = new HashSet<T>();
        private Stack<T> _inactiveObjects = new Stack<T>();
        private Dictionary<MinionType, T> _prefabs;

        public UnitsPool(Dictionary<MinionType, T> prefabses)
        {
            _prefabs = prefabses;
        }
        
        public T GetObject(MinionType minionType)
        {
            T obj;
            if (_inactiveObjects.Count == 0)
            {
                obj = Object.Instantiate(_prefabs[minionType]);
            }
            else
            {
                obj = _inactiveObjects.Pop();
            }

            return obj;
        }

        public void ReturnObject(T obj)
        {
            _activeObjects.Remove(obj);
            _inactiveObjects.Push(obj);
        }

        public void Clear()
        {
            _activeObjects.Clear();
            _inactiveObjects.Clear();
        }
    }
}