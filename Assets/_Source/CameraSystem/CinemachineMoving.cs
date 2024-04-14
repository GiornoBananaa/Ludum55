using System;
using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinemachineMoving : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _currentCamera;
    [SerializeField] private float switchDuration = 0.3f;
    private bool isSwitching = false;
    
    public void SwitchCamera(CinemachineVirtualCamera toCamera, Transform followTransform)
    {
        StartCoroutine(SwitchingCamera(toCamera, followTransform));
    }
    
    public IEnumerator SwitchingCamera(CinemachineVirtualCamera toCamera, Transform followTransform)
    {
        isSwitching = true;

        Vector3 startPos = _currentCamera.transform.position;
        Vector3 endPos = toCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < switchDuration)
        {
            float t = elapsedTime / switchDuration;
            _currentCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentCamera.gameObject.SetActive(false);
        toCamera.gameObject.SetActive(true);
        
        _currentCamera.Follow = null;
        toCamera.Follow = followTransform;

        _currentCamera = toCamera;

        isSwitching = false;
    }
}
