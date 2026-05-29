using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;



public class EquipmentSlots_UI : ItemSlot_UI
{
    public EquipmentType equipmentType;

    public void OnValidate()
    {
        gameObject.name="Equipment slot"+equipmentType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.Instance.UnEquipt(item.data as ItemData_Equipment);
        Inventory.Instance.AddItem(item.data as ItemData_Equipment);//将卸下的装备放入背包中
        ClearUpSlots();
    }
}
 