using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum gameItem
{
    chair,
    redbull,
    ice,
    keyboard,
    penpad,
    usb,
    virus,
    artAsset
};
public class Item : MonoBehaviour
{
    public int onIndex;
    public ItemUsedType UseType
    {
        get;
        protected set;
    }
    public bool isSpecial;
    public bool isOnRedBull;
    public gameItem objItem
    {
        get;
        protected set;
    }

    public virtual void UseItem(GameObject target)
    {

    }
    public virtual void UseItem(Vector2 direction,GameObject whoUse)
    {

    }
    public void Pickup(GameObject whoPicks)
    {
        Inventory inventory = whoPicks.GetComponent<Inventory>();
        if (inventory.AddItemToSlot(this))
        {
            transform.position = new Vector3(200, 200, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.pickupObj = this.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.pickupObj = null ;
        }
    }
}
