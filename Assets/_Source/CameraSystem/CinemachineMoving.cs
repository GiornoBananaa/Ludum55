using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace CameraSystem
{
    public class CinemachineMoving : MonoBehaviour
    {
        [Serializable]
        public class CameraData
        {
            public CinemachineVirtualCamera VirtualCamera;
            public Transform CameraFollowObject;
        }
    
        [SerializeField] private CinemachineVirtualCamera _currentCamera;
        [SerializeField] private float _camerasMinDistance = 18f;
        [SerializeField] private float _switchDuration = 0.3f;
        [SerializeField] private CameraData[] _cameraDatas;
    
        private LinkedList<CameraData> _camerasInCircle;
        private LinkedListNode<CameraData> _currentCameraNode;
        private Coroutine _currentCoroutine;
        private bool _isSwitching;
        private CameraData _cameraIndex;

        private void Awake()
        {
            _camerasInCircle = new LinkedList<CameraData>(_cameraDatas);
            _currentCameraNode = _camerasInCircle.First;
        }

        public void MoveLeft()
        {
            var currPos = _currentCamera.transform.position;
            Vector2 newPos = new Vector2(currPos.x-_camerasMinDistance,currPos.y);
            if(_currentCameraNode.Previous == null)
            {
                _currentCameraNode = _camerasInCircle.Last;
                _camerasInCircle.RemoveLast();
                _camerasInCircle.AddFirst(_currentCameraNode);
                _currentCameraNode.Value.VirtualCamera.transform.position = newPos;
                _currentCameraNode.Value.CameraFollowObject.position = newPos;
            }
            else
            {
                _currentCameraNode = _currentCameraNode.Previous;
            }
            SwitchCamera(_currentCameraNode.Value);
        }
    
        public void MoveRight()
        {
            var currPos = _currentCamera.transform.position;
            Vector2 newPos = new Vector2(currPos.x+_camerasMinDistance,currPos.y);
            if(_currentCameraNode.Next == null)
            {
                _currentCameraNode = _camerasInCircle.First;
                _camerasInCircle.RemoveFirst();
                _camerasInCircle.AddLast(_currentCameraNode);
                _currentCameraNode.Value.VirtualCamera.transform.position = newPos;
                _currentCameraNode.Value.CameraFollowObject.position = newPos;
            }
            else
            {
                _currentCameraNode = _currentCameraNode.Next;
            }
            SwitchCamera(_currentCameraNode.Value);
        }
    
        public void SwitchCamera(CameraData nextCamera)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            StartCoroutine(SwitchingCamera(nextCamera));
        }
    
        public IEnumerator SwitchingCamera(CameraData nextCamera)
        {
            _isSwitching = true;

            Vector3 startPos = _currentCamera.transform.position;
            Vector3 endPos = nextCamera.VirtualCamera.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < _switchDuration)
            {
                float t = elapsedTime / _switchDuration;
                _currentCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _currentCamera.gameObject.SetActive(false);
            nextCamera.VirtualCamera.gameObject.SetActive(true);
        
            _currentCamera.Follow = null;
            nextCamera.VirtualCamera.Follow = nextCamera.CameraFollowObject;

            _currentCamera = nextCamera.VirtualCamera;

            _isSwitching = false;
        }
    }
}
