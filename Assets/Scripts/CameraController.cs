using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Positions")]
    public Transform thirdPersonPosition;
    public Transform firstPersonPosition;

    [Header("Camera Settings")]
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Transform targetPosition;

    void Start()
    {
        // Default to third person view
        targetPosition = thirdPersonPosition;
        transform.position = thirdPersonPosition.position;
        transform.rotation = thirdPersonPosition.rotation;
    }

    void LateUpdate()
    {
        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetPosition.rotation, smoothTime);
    }

    public void SwitchView(bool isFirstPerson)
    {
        targetPosition = isFirstPerson ? firstPersonPosition : thirdPersonPosition;
    }
}
