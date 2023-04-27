using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private void Start()
    {
        hitPoints.health = hitPoints.startingHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CanBePickedUp"))
        {
            Item item = collision.GetComponent<Consumable>().item;
            if (item != null)
            {
                bool shouldDisappear = false;
                switch (item.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = Inventory.Instance.AddItem(item);
                        break;
                    case Item.ItemType.HEALTH:
                        if (hitPoints.health < hitPoints.maxHealth)
                        {
                            AdjustHitPoints(item.quantity);
                            shouldDisappear = true;
                        }
                        break;


                }
                if (shouldDisappear)
                {
                    Debug.Log("Recogido: " + item.objectName);
                    collision.gameObject.SetActive(false);
                }
            }
            
        }
    }
    public void AdjustHitPoints(int amount)
    {
        hitPoints.health += amount;
        if (hitPoints.health > hitPoints.maxHealth)
            hitPoints.health = hitPoints.maxHealth;
        Debug.Log("Health: " + hitPoints.health);
    }

}
