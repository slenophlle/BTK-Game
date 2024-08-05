using System;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float TopClamp = 70f;
    [SerializeField] private float BottomClap = -40f;

    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;

    private void LateUpdate()
    {
        CameraLogic();
    }
    private void CameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");

        cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, mouseY,BottomClap, TopClamp, true);
        cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);
        ApplyRotations(cinemachineTargetPitch, cinemachineTargetYaw);

    }
    private void ApplyRotations(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }
    private float UpdateRotation(float currentrotation, float input, float min, float max, bool isXAxis)
    {
        currentrotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentrotation, min, max);
    }
    private float GetMouseInput(string axis) 
    {
        return Input.GetAxis(axis) *rotationSpeed *Time.deltaTime;
    }
}
