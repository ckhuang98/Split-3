﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    public GameObject player;

    PlayerController playerController;
    Item item;

    void Start(){
        playerController = player.GetComponent<PlayerController>();
    }
    
    public void AddItem(Item newItem){
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot(){
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton (){
        Inventory.instance.Remove(item);
    }

    public void UseItem(){
        if (item != null){
            item.Use();
            if(item.name == "Food"){
                playerController.currentHunger += 10;
                Inventory.instance.Remove(item);
            } else if(item.name == "Water"){
                playerController.currentThirst += 15;
                Inventory.instance.Remove(item);
            }
        }
    }
}
