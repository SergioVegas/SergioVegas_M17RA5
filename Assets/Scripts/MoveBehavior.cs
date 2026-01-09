using UnityEngine;
using UnityEngine.UIElements;

public class MoveBehavior : MonoBehaviour
{
    private CharacterController _controller;
    private void Awake()
    { 
        _controller = GetComponent<CharacterController>();
    }
    public void MoveCharacter(Vector3 direction, float speed)
    {
        Vector3 move = new Vector3(direction.x, 0f, direction.z); 
        _controller.Move(move * speed * Time.deltaTime);
    }
    public void RotateCharacter(Vector3 moveDir)
    {
        if (moveDir.sqrMagnitude > 0.01f) 
        { 
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}

