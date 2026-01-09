using UnityEngine;
using UnityEngine.UIElements;

public class MoveBehavior : MonoBehaviour
{
    private CharacterController _controller;
    public Vector3 CurrentVelocity { get; private set; }
    public float rotateSpeed = 5f;
    private void Awake()
    { 
        _controller = GetComponent<CharacterController>();
    }
    public void MoveCharacter(Vector3 direction, float speed)
    {
        Vector3 move = new Vector3(direction.x, 0f, direction.z); 
        if(move.z >0)
            _controller.Move( transform.forward * move.z * speed * Time.deltaTime );
        //_controller.Move(move * speed * Time.deltaTime);
        CurrentVelocity = move * speed;
    }
    public void RotateCharacter(Vector3 moveDir)
    {
        if (moveDir.sqrMagnitude > 0.01f) 
        {
            //float angle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = moveDir.x;
            transform.Rotate(new Vector3(0, angle * Time.deltaTime * rotateSpeed, 0));
            //Quaternion targetRotation = Quaternion.Euler(0,angle,0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}

