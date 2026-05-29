using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 velocity;
    private void OnValidate()
    {
        if (itemData == null) return;
        GetComponent<SpriteRenderer>().sprite=itemData.icon;
        gameObject.name=itemData.Name;
    }
    private void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        sr.sprite = itemData.icon;
    }
    
    public void SetupItem(ItemData _ItemData,Vector2 _velocity)
    {
        itemData=_ItemData;
        rb.linearVelocity = _velocity;
    }

    public void PickUpItem()
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
