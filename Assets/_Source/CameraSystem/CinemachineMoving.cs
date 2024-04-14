using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameraSystem
{
    public class CinemachineMoving : MonoBehaviour
    {
        [Serializable]
        public class CameraData
        {
            public Transform CameraFollowObject;
        }
    
        [SerializeField] private CinemachineVirtualCamera _currentCamera;
        [SerializeField] private float _camerasMinDistance = 18f;
        [SerializeField] private float _switchDuration = 0.3f;
        [SerializeField] private CameraData[] _cameraDatas;
    
        private LinkedList<CameraData> _camerasInCircle;
        private LinkedListNode<CameraData> _currentCameraNode;
        private Coroutine _currentCoroutine;
        private CameraData _cameraIndex;
        
        public bool IsMoving { get; private set; }
        
        private void Awake()
        {
            _camerasInCircle = new LinkedList<CameraData>(_cameraDatas);
            _currentCameraNode = _camerasInCircle.First;
            IsMoving = false;
        }

        private void Update()
        {
            Debug.Log(IsMoving);
        }

        public void MoveLeft()
        {
            if(IsMoving) return;
            var currPos = _currentCamera.transform.position;
            Vector2 newPos = new Vector2(currPos.x-_camerasMinDistance,currPos.y);
            if(_currentCameraNode.Previous == null)
            {
                _currentCameraNode = _camerasInCircle.Last;
                _camerasInCircle.RemoveLast();
                _camerasInCircle.AddFirst(_currentCameraNode);
                _currentCameraNode.Value.CameraFollowObject.position = newPos;
            }
            else
            {
                _currentCameraNode = _currentCameraNode.Previous;
            }
            
            MoveCamera(_currentCameraNode.Value.CameraFollowObject.transform.position);
        }
    
        public void MoveRight()
        {
            if(IsMoving) return;
            var currPos = _currentCamera.transform.position;
            Vector2 newPos = new Vector2(currPos.x+_camerasMinDistance,currPos.y);
            if(_currentCameraNode.Next == null)
            {
                _currentCameraNode = _camerasInCircle.First;
                _camerasInCircle.RemoveFirst();
                _camerasInCircle.AddLast(_currentCameraNode);
                _currentCameraNode.Value.CameraFollowObject.position = newPos;
            }
            else
            {
                _currentCameraNode = _currentCameraNode.Next;
            }
            
            MoveCamera(_currentCameraNode.Value.CameraFollowObject.transform.position);
        }
    
        public void MoveCamera(Vector2 position)
        {
            IsMoving = true;
            _currentCamera.transform.DOMove(new Vector3(position.x,position.y,_currentCamera.transform.position.z),_switchDuration).OnComplete(()=>IsMoving=false);
        }
    }
}
