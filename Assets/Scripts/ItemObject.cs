using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemData;

    public void OnHandlePickUp()
    {
        InventorySystem.Instance.Add(itemData);
        Debug.Log("antes");
        Destroy(gameObject);
        Debug.Log("depsues");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnHandlePickUp();
        }
    }
}
