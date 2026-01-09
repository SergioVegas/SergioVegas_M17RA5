using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void MoveCharacter(Vector3 direction, float speed)
    {
        _rb.linearVelocity = new Vector3(direction.x * speed, _rb.linearVelocity.y,direction.z * speed);
    }
}
