using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        RotateDoor();
    }
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
}
