using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtAsset : Item
{
    public RequestItem type;

    private void Awake()
    {
        type = RequestItem.ArtAsset;
        objItem = gameItem.artAsset;
        UseType = ItemUsedType.Boots;
    }
    public override void UseItem(GameObject target)
    {
        Progresser progress = target.GetComponent<Progresser>();
        if (progress.itemRequest == type)
        {
            progress.sendTranmission();
        }
        else
        {
            progress.setStatus(false, 1, 3f);
        }
    }
}
