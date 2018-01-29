using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usb : Item
{
    public RequestItem type;

    private void Awake()
    {
        type = RequestItem.data;
        UseType = ItemUsedType.Boots;
        objItem = gameItem.usb;
    }
    public override void UseItem(GameObject target)
    {
        Progresser progress = target.GetComponent<Progresser>();

        if (progress.itemRequest == type)
        {
            print("use");
            progress.sendTranmission();
            Destroy(this.gameObject);
        }
        else
        {
            progress.setStatus(false, 2, 3f);
            Destroy(this.gameObject);
        }
    }
}
