using NUnit.Framework;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData item;
    public void DropItem()
    {
        GameObject newDrop=Instantiate(dropPrefab,transform.position,Quaternion.identity);
        Vector2 randomVel = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));


        newDrop.GetComponent<ItemObject>().SetupItem(item, randomVel);
    }
}
