using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Inventory : Singleton<Inventory>
{
    public const int numSlots = 5;
    public Slot slotPrefab;
    public GameObject slotsParent;
    Item[] items = new Item[numSlots];
    Slot[] slots = new Slot[numSlots];

    Slot CreateNewSlotForItem(string name, Item item)
    {
        Slot newSlot = Instantiate<Slot>(slotPrefab);
        newSlot.name = name;
        newSlot.transform.SetParent(slotsParent.transform);
        Transform imageTransform = newSlot.transform.Find("ItemBackground/Item");
        Image itemImage = imageTransform.gameObject.GetComponent<Image>();
        itemImage.sprite = item.sprite;
        if (item.stackable)
        {
            newSlot.SetCount(item.quantity);
        }
        else
        {
            newSlot.DisableCounter();
        }
        return newSlot;
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < numSlots; i++)
        {
            if (items[i] != null &&
              items[i].itemType == itemToAdd.itemType && itemToAdd.stackable)
            {
                items[i].quantity++;
                slots[i].SetCount(items[i].quantity);
                return true;
            }
            else
               if (items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                slots[i] = CreateNewSlotForItem("ItemSlot" + i, items[i]);
                return true;
            }
        }
        return false;
    }
}

