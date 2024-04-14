using System.Collections.Generic;
using UnityEngine;

namespace PackagingGame
{
    public class MarkerDrawer : MonoBehaviour
    {
        private readonly List<LineRenderer> _lines = new List<LineRenderer>();

        public List<LineRenderer> Lines => _lines;
        
        public void AddLine(LineRenderer line)
        {
            _lines.Add(line);
        }

        private void Reset()
        {
            _lines.Clear();
        }
    }
}
