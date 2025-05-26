using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cinemachine;
    [SerializeField] float rotationSpeed;
    bool isRotating;

    private void Start()
    {
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRotating)
        {
            StartCoroutine("Rotating");
        }
    }

    IEnumerator Rotating()
    {
        isRotating = true;
        float currentValue = cinemachine.m_XAxis.Value;

        if (currentValue >= 179 && currentValue <= 180)
        {
            currentValue = -179;
            cinemachine.m_XAxis.Value = -179;
        }
        else if(currentValue >= 89 && currentValue <= 95)
        {
            currentValue = 89;
            cinemachine.m_XAxis.Value = 89;
        }

        while (cinemachine.m_XAxis.Value <= currentValue + 90)
        {
            cinemachine.m_XAxis.Value += rotationSpeed * Time.deltaTime;
            yield return null;
        }
        isRotating = false;
    }
}