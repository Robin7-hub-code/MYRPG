using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class ItemSlot_UI : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI itemText;
    [SerializeField] private Sprite defaultSprite;
    public InventoryItem item;

  
    public void UpdateSlot(InventoryItem _item)
    {
        item=_item;
        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.data.icon;

            if (item.stack > 1)
            {
                itemText.text = item.stack.ToString();
            }
            else
            {
                itemText.text = string.Empty;
            }
        }
        
    }
    public void ClearUpSlots()
    {
        item=null;
        itemImage.color = Color.white;
        itemImage.sprite = defaultSprite;
        itemText.text = string.Empty;
      
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.data == null || Inventory.Instance == null)
            return;
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.Instance.RemoveItem(item.data);
            return;
        }
        if (item.data.itemType == ItemType.Equipment)
            Inventory.Instance.EquipItem(item.data);
    }
}
