using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinemachineMoving : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject army;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera armyCamera;

    private bool isSwitching = false;
    private float switchDuration = 0.5f;

    private void Start()
    {
        playerCamera.Follow = player.transform;
        armyCamera.Follow = null;
        armyCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2) && !isSwitching)
        {
            if (playerCamera.gameObject.activeSelf)
                StartCoroutine(SwitchCamera(playerCamera, armyCamera));
            else if (armyCamera.gameObject.activeSelf)
                StartCoroutine(SwitchCamera(armyCamera, playerCamera));
        }
    }

    IEnumerator SwitchCamera(CinemachineVirtualCamera fromCamera, CinemachineVirtualCamera toCamera)
    {
        isSwitching = true;

        Vector3 startPos = fromCamera.transform.position;
        Vector3 endPos = toCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < switchDuration)
        {
            float t = elapsedTime / switchDuration;
            fromCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fromCamera.gameObject.SetActive(false);
        toCamera.gameObject.SetActive(true);

        if (toCamera == playerCamera)
        {
            armyCamera.Follow = null;
            playerCamera.Follow = player.transform;
        }
        else if (toCamera == armyCamera)
        {
            playerCamera.Follow = null;
            armyCamera.Follow = army.transform;
        }

        isSwitching = false;
    }
}
