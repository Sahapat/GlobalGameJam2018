using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerBubble : MonoBehaviour
{
    public Sprite[] display;
    public GameObject showItem;
    public GameObject Player;
    private Inventory inventory;
    private SpriteRenderer change;
    private Item show;
    private void Start()
    {
        inventory = Player.GetComponent<Inventory>();
        change = showItem.GetComponent<SpriteRenderer>();
        show = inventory.useItem();
    }

    private void Update()
    {
        show = inventory.useItem();
        setSprite();
    }
    private void setSprite()
    {
        if (show != null)
        {
            switch (show.objItem)
            {
                case gameItem.artAsset:
                    change.sprite = display[0];
                    break;
                case gameItem.chair:
                    change.sprite = display[1];
                    break;
                case gameItem.ice:
                    change.sprite = display[2];
                    break;
                case gameItem.keyboard:
                    change.sprite = display[3];
                    break;
                case gameItem.penpad:
                    change.sprite = display[4];
                    break;
                case gameItem.redbull:
                    change.sprite = display[5];
                    break;
                case gameItem.usb:
                    change.sprite = display[6];
                    break;
                case gameItem.virus:
                    change.sprite = display[7];
                    break;
            }
        }
    }
}
