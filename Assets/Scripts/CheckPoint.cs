using UnityEngine;

public class CheckPoint : MonoBehaviour, IInteractable
{
    public void Interact()
    {   
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveGame();
        }
        else
        {
            Debug.LogError("GameManager Instance is null!");
        }
    }
}