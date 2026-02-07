using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    public void OpenDoor()
    {
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        isOpen = true;
    }

    public void CloseDoor()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        isOpen = false;
    }
    public void RotateDoor()
    {
      if( !isOpen )
      {
            OpenDoor();
      }
      else
      {
            CloseDoor();
      }
    }

    private void OnEnable() { Player.UseDoor += RotateDoor; }
    private void OnDisable() { Player.UseDoor -= RotateDoor; }
}
