using AudioSystem;
using UnityEngine;

namespace PackagingGame
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private float _thickness;
        [SerializeField] private float _minDistanceBetweenPoints;
        [SerializeField] private Transform _drawPoint;
        [SerializeField] private Material _drawMaterial;
        [SerializeField] private LayerMask _drawLayers;
        [SerializeField] private DraggableItem _draggable;
        [SerializeField] private MarkerDrawer _markerDrawer;
        
        private bool _isDrawing;
        private Vector2 _lastPosition;
        private Camera _camera;
        private LineRenderer _line;
        private Transform _currentSurface;
        private AudioPlayer _audioPayer;

        public void Construct(AudioPlayer audioPayer)
        {
            _audioPayer = audioPayer;
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            if(!_draggable.IsDragged || !CheckSurface()) return;
            if (Input.GetMouseButton(1) && !_isDrawing)
            {
                StartDrawing();
            }
            else if(!Input.GetMouseButton(1) && _isDrawing)
            {
                EndDrawing();
            }

            if (_isDrawing)
                Draw();
        }
        
        private void Draw()
        {
            if (_line is null)
                CreateNewLine();

            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(_drawPoint.position, _lastPosition) > _minDistanceBetweenPoints)
            {
                _line.positionCount++;
                _line.SetPosition(_line.positionCount - 1,_drawPoint.position);
                _lastPosition = mousePosition;
            }
        }
        
        private void StartDrawing()
        {
            _audioPayer.Play(Sounds.Drawing);
            _isDrawing = true;
            CreateNewLine();
            _line.transform.SetParent(_currentSurface);
            _line.endColor = _color;
            _line.startColor = _color;
        }
        
        private void EndDrawing()
        {
            _audioPayer.Stop(Sounds.Drawing);
            if (_line != null)
            {
                _markerDrawer.AddLine(_line);
            }
            _isDrawing = false;
            _line = null;
        }

        private void CreateNewLine()
        {
            GameObject lineObject = new GameObject();
            _line = lineObject.AddComponent<LineRenderer>();
            _line.material = _drawMaterial;
            _line.startWidth = _thickness;
            _line.endWidth = _thickness;
            _line.positionCount = 1;
            _line.useWorldSpace = false;
            _line.SetPosition(0,_drawPoint.position);
        }

        private bool CheckSurface()
        {
            Collider2D collider = Physics2D.OverlapPoint(_drawPoint.transform.position, _drawLayers);
            if (collider != null)
            {
                _currentSurface = collider.transform;
                return true;
            }
            if(_isDrawing)
                EndDrawing();
            return false;
        }
    }
}