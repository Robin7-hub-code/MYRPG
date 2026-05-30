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
        // guard against null references
        if (item == null || item.data == null || Inventory.Instance == null)
            return;

        var equipment = item.data as ItemData_Equipment;
        if (equipment == null)
            return;

        Inventory.Instance.UnEquipt(equipment);
        Inventory.Instance.AddItem(equipment); // 将卸下的装备放入背包中
        ClearUpSlots();
    }
}
 