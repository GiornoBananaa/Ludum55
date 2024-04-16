using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GameStatesSystem;
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
        private LinkedListNode<CameraData> _start;
        
        public bool IsMoving { get; private set; }
        
        private void Awake()
        {
            _camerasInCircle = new LinkedList<CameraData>(_cameraDatas);
            _currentCameraNode = _camerasInCircle.First;
            IsMoving = false;
            _start = _currentCameraNode;
        }

        public TweenerCore<Vector3,Vector3,VectorOptions> MoveLeft()
        {
            if(IsMoving) return null;
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
            
            return MoveCamera(_currentCameraNode.Value.CameraFollowObject.transform.position);
        }
    
        public TweenerCore<Vector3,Vector3,VectorOptions> MoveRight()
        {
            if(IsMoving) return null;
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
            
            return MoveCamera(_currentCameraNode.Value.CameraFollowObject.transform.position);
        }

        public TweenerCore<Vector3,Vector3,VectorOptions> MoveStart()
        {
            while (_currentCameraNode.Value != _start.Value)
            {
                _currentCameraNode = _camerasInCircle.First;
                _camerasInCircle.RemoveFirst();
                _camerasInCircle.AddLast(_currentCameraNode);
            }
            
            var currPos = _currentCameraNode.Previous.Value.CameraFollowObject.position;
            Vector2 newPos = new Vector2(currPos.x+_camerasMinDistance,currPos.y);
            
            _currentCameraNode.Value.CameraFollowObject.position = newPos;
            
            return MoveCamera(_currentCameraNode.Value.CameraFollowObject.transform.position);
        }
        
        
        private TweenerCore<Vector3,Vector3,VectorOptions> MoveCamera(Vector2 position)
        {
            IsMoving = true;
            TweenerCore<Vector3, Vector3, VectorOptions> tween =
                _currentCamera.transform.DOMove(
                    new Vector3(position.x, position.y, _currentCamera.transform.position.z),
                    _switchDuration);
            tween.onComplete += () => IsMoving = false;
            return tween;
        }
    }
}
