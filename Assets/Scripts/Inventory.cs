using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemUsedType
{
    AttackAim,
    Attack,
    Boots,
    None
};

public class Inventory : MonoBehaviour
{
    public byte numOfSlot;
    public bool isSlotAvilable = false;
    public Item items;
    private void Awake()
    {
        numOfSlot = 1;
    }
    private void Update()
    {
        if(numOfSlot <= 0)
        {
            isSlotAvilable = false;
        }
        else
        {
            isSlotAvilable = true;
        }
    }
    public bool AddItemToSlot(Item InItem)
    {
        if(isSlotAvilable)
        {
            numOfSlot = 0;
            items = InItem;
            return true;
        }
        else
        {
            return false;
        }
    }
    public Item useItem()
    {
        if(isSlotAvilable)
        {
            return null;
        }
        else
        {
            return items;
        }
    }
    public void removeItem()
    {
        numOfSlot = 1;
    }
}
