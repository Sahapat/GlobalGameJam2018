using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtAsset : Item
{
    public RequestItem type;

    private void Awake()
    {
        type = RequestItem.ArtAsset;
        UseType = ItemUsedType.Boots;
    }
    public override void UseItem(GameObject target)
    {

    }
}
