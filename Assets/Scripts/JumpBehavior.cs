using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    public float jumpHeight = 1.0f;
    private float _gravity = -9.81f;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    public bool IsGrounded => _controller.isGrounded;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (IsGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    public void JumpDelayed()
    {
        Invoke("Jump", 0.3f);
    }
    public void Jump()
    {
        _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * _gravity);
    }
 
}
