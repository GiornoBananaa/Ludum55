using System;
using System.Collections.Generic;
using UnityEngine;

namespace PackagingGame
{
    public class MarksManager : MonoBehaviour
    {
        private List<Mark> _marks;
        
        public Action OnMarkAdded;
        
        public void AddMark(Mark mark)
        {
            _marks.Add(mark);
            OnMarkAdded?.Invoke();
        }
    }
}
