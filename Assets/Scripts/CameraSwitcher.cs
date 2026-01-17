using UnityEngine;
using Cinemachine;
using System;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineFreeLook firstPersonCamera;

    [Header("Player Reference")]
    public Player player;

    void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    void OnEnable()
    {
        if (player != null)
        {
            player.OnAiming += HandleAimStateChanged;
        }
        SetCameraPriorities(false);
    }

    void OnDisable()
    {
        if (player != null)
        {
            player.OnAiming -= HandleAimStateChanged;
        }
    }

    private void HandleAimStateChanged(bool isAiming)
    {
        // Directly use the state from the Player script
        SetCameraPriorities(isAiming);
    }

    private void SetCameraPriorities(bool activeFirstPerson)
    {
        if (activeFirstPerson)
        {
            if (firstPersonCamera != null) firstPersonCamera.Priority = 20;
            if (thirdPersonCamera != null) thirdPersonCamera.Priority = 10;
        }
        else
        {
            if (firstPersonCamera != null) firstPersonCamera.Priority = 10;
            if (thirdPersonCamera != null) thirdPersonCamera.Priority = 20;
        }
    }
}
