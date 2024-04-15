using UnityEngine;

namespace SummoningGame
{
    public class SpriteOutliner : MonoBehaviour
    {
        [SerializeField] private Material _outlineMaterial;
        [SerializeField] private Renderer _render;
        
        private Material _defaultMaterial;

        private void Awake()
        {
            _defaultMaterial = _render.material;
        }

        public void EnableOutline()
        {
            _render.material = _outlineMaterial;
        }
        
        public void DisableOutline()
        {
            _render.material = _defaultMaterial;
        }
    }
}
