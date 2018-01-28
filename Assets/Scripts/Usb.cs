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
    }
    public override void UseItem(GameObject target)
    {

    }
}
