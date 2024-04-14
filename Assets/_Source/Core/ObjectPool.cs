using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private HashSet<T> _activeObjects = new HashSet<T>();
        private Stack<T> _inactiveObjects = new Stack<T>();
        private T _prefab;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
        }
        
        public T GetObject()
        {
            T obj;
            if (_inactiveObjects.Count == 0)
            {
                obj = Object.Instantiate(_prefab);
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