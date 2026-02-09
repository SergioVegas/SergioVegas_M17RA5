using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    private CharacterController _controller;
    public float rotateSpeed = 20f;
    private void Awake()
    { 
        _controller = GetComponent<CharacterController>();
    }

    public void ExecuteMovement(Vector3 direction, float speed)
    {
        // Handle rotation
        if (direction.x != 0)
        {
            transform.Rotate(new Vector3(0, direction.x * rotateSpeed * Time.deltaTime, 0));
        }

        // Handle movement
        if (direction.z != 0)
        {
            if (direction.z < 0)
            { 
               // transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180f, 0);
            }
            Vector3 move = transform.forward * direction.z;
            _controller.Move(move * speed * Time.deltaTime);
        }
    }
}

