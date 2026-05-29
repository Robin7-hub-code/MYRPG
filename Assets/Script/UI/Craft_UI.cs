using UnityEngine;
using UnityEngine.EventSystems;

public class Craft_UI : ItemSlot_UI
{
    private void OnEnable()
    {
        UpdateSlot(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
       ItemData_Equipment AimCraft =item.data as ItemData_Equipment;
        Debug.Log("I m creating");
        Inventory.Instance.CanCraft(AimCraft, AimCraft.craftMat);
    }
}
