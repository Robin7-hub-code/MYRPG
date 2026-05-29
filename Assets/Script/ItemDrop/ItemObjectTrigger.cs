using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();
    private void OnTriggerEnter2D(Collider2D colliders)
    {
        if (colliders.GetComponent<Player>() != null)
        {
            myItemObject.PickUpItem();
        }
    }
}
