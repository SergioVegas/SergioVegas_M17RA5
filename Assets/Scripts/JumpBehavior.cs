using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 jump = new Vector3(0, 1, 0);
    private float jumpForce = 3f;
    private CharacterController _controller;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
    }
    public void Jump()
    {
        _controller.Move(jump * jumpForce * Time.deltaTime);
        //_rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    }
 
}
