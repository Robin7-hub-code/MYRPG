using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("仓库UI")]

    private bool weapon;
    private bool armor;
    private bool amulet;
    private bool flask;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform materialSlotParent;
    [SerializeField] private Transform equipmentHaveEquiptSlotParent;

    private ItemSlot_UI[] equipmentItemSlots;//所有装备插槽的ui
    private ItemSlot_UI[] materialItemSlots;//所有材料插槽的ui
    private EquipmentSlots_UI[] equipmentHaveEquipSlots;//已装备的插槽
    //private ItemSlot_UI[] equipmentHaveEquipSlots;

    public static Inventory Instance;//作为全局唯一的仓库实例
    public List<InventoryItem> equipmentItems;//记录当前存下了装备,有多少个
    public List<InventoryItem> materialItems;//记录当前存下了材料,有多少个
    public List<InventoryItem> equipmentHaveEquipItems;//记录当前已经装备的装备

    public Dictionary<ItemData, InventoryItem> equipmentDictionary;//记录当前存下了哪些装备到这些东西有多少的映射
    public Dictionary<ItemData, InventoryItem> materialDictionary;//记录当前存下了哪些材料到这些东西有多少的映射
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentHaveEquipDictionary;//记录装备映射

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        equipmentItems = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData, InventoryItem>();

        materialItems = new List<InventoryItem>();
        materialDictionary = new Dictionary<ItemData, InventoryItem>();

        equipmentHaveEquipItems = new List<InventoryItem>();
        equipmentHaveEquipDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        equipmentItemSlots = equipmentSlotParent.GetComponentsInChildren<ItemSlot_UI>();
        materialItemSlots = materialSlotParent.GetComponentsInChildren<ItemSlot_UI>();
        equipmentHaveEquipSlots = equipmentHaveEquiptSlotParent.GetComponentsInChildren<EquipmentSlots_UI>();
    }
    public void EquipItem(ItemData _item)
    {

        ItemData_Equipment newequipment = (ItemData_Equipment)(_item);
        InventoryItem newItem = new InventoryItem(newequipment);

        ItemData_Equipment itemToDel = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentHaveEquipDictionary)
        {
            if (item.Key.equipmentType == newequipment.equipmentType)
            {
                itemToDel = item.Key;
            }
        }
        if (itemToDel != null)
        {
            UnEquipt(itemToDel);
            AddItem(itemToDel);//装上新装备前，先将旧装备卸下
            itemToDel.RemoveModifiers();
        }

        equipmentHaveEquipItems.Add(newItem);
        equipmentHaveEquipDictionary.Add(newequipment, newItem);
        newequipment.AddModifiers();
        RemoveItem(_item);
        UpdateSlotUI();
    }

    public void UnEquipt(ItemData_Equipment itemToDel)
    {
        if (itemToDel == null)
        {
            return;
        }
        if (equipmentHaveEquipDictionary.TryGetValue(itemToDel, out InventoryItem value))
        {
            equipmentHaveEquipItems.Remove(value);
            equipmentHaveEquipDictionary.Remove(itemToDel);
        }
    }

    public void AddItem(ItemData item)
    {
        if (item == null)
            return;
        if (item.itemType == ItemType.Material)
        {
            AddToMaterial(item);
        }
        if (item.itemType == ItemType.Equipment)
        {
            AddToEquipment(item);
        }
        UpdateSlotUI();
    }

    private void AddToEquipment(ItemData item)
    {
        if (equipmentDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            equipmentItems.Add(newItem);
            equipmentDictionary.Add(item, newItem);
        }
    }
    private void AddToMaterial(ItemData item)
    {
        if (materialDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            materialItems.Add(newItem);
            materialDictionary.Add(item, newItem);
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentHaveEquipSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentHaveEquipDictionary)
            {
                if (item.Key.equipmentType == equipmentHaveEquipSlots[i].equipmentType)
                {
                    equipmentHaveEquipSlots[i].UpdateSlot(item.Value);
                    UnityEngine.Debug.Log("i m equipt" + equipmentHaveEquipSlots[i].equipmentType);
                }
            }
        }

        for (int i = 0; i < equipmentItemSlots.Length; i++)
        {
            equipmentItemSlots[i].ClearUpSlots();
        }
        for (int i = 0; i < materialItemSlots.Length; i++)
        {
            materialItemSlots[i].ClearUpSlots();
        }




        for (int i = 0; i < equipmentItems.Count; i++)
        {
            equipmentItemSlots[i].UpdateSlot(equipmentItems[i]);
        }
        for (int i = 0; i < materialItems.Count; i++)
        {
            materialItemSlots[i].UpdateSlot(materialItems[i]);
        }
    }
    public void RemoveItem(ItemData item)
    {
        if (item.itemType == ItemType.Material)
        {
            RemoveItemFromMaterial(item);
        }
        if (item.itemType == ItemType.Equipment)
        {
            RemoveItemFromEquipment(item);
        }
        UpdateSlotUI();
    }

    private void RemoveItemFromEquipment(ItemData item)
    {
        if (equipmentDictionary.TryGetValue(item, out InventoryItem value))
        {
            if (value.stack <= 1)
            {
                value.RemoveStack();
                equipmentItems.Remove(value);
                equipmentDictionary.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
    }
    private void RemoveItemFromMaterial(ItemData item)
    {
        if (materialDictionary.TryGetValue(item, out InventoryItem value))
        {
            if (value.stack <= 1)
            {
                value.RemoveStack();
                materialItems.Remove(value);
                materialDictionary.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
    }

    public bool CanCraft(ItemData_Equipment AimEquipment, List<InventoryItem> _requirement)
    {
        List<InventoryItem> matToRemove = new List<InventoryItem>();
        if(_requirement.Count!=0)
        {
            UnityEngine.Debug.Log("需求不为空");
        }
        else
        {
            UnityEngine.Debug.Log("需求为空");
        }


        for (int j = 0; j < _requirement.Count; j++)
        {
            if (materialDictionary.TryGetValue(_requirement[j].data, out InventoryItem mat))
            {
                if (mat.stack < _requirement[j].stack)
                {
                    UnityEngine.Debug.Log(" not enough");
                    return false;
                }
                else
                {
                    matToRemove.Add(mat);
                }
            }
            else
            {
                UnityEngine.Debug.Log(" not enough");
                return false;
            }
        }

        for (int i = 0; i < matToRemove.Count; i++)
        {
            RemoveItem(matToRemove[i].data);
        }
        AddItem(AimEquipment);
        UnityEngine.Debug.Log("制作成功");
        return true;
    }

}
