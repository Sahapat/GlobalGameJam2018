using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullBoots : Item
{
    public float EffectDuration = 4f;
    public RequestItem type;
    private void Awake()
    {
        UseType = ItemUsedType.Boots;
        type = RequestItem.RedBull;
        objItem = gameItem.redbull;
    }
    public override void UseItem(GameObject target)
    {
        Progresser progresserChecker = target.GetComponent<Progresser>();
        if (progresserChecker.itemRequest == type)
        {
            print("use");
            progresserChecker.sendTranmission();
            Destroy(this.gameObject, 0.5f);
        }
        else
        {
            progresserChecker.setStatus(false, 1, EffectDuration);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
