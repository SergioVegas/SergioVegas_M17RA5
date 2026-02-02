using UnityEngine;

public class FollowItem : MonoBehaviour
{
    [SerializeField] private GameObject itemToFollow;

    void LateUpdate()
    {
        FollowTheItem();
    }

    public void FollowTheItem()
    {
       
        if (itemToFollow != null)
        {
            transform.position = itemToFollow.transform.position;
        }
    }
}
