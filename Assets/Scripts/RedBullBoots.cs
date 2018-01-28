using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullBoots : Item
{
    public float EffectDuration = 4f;

    private void Awake()
    {
        UseType = ItemUsedType.Boots;
    }
    public override void UseItem(GameObject target)
    {
        Progresser progresserChecker = target.GetComponent<Progresser>();
        if (progresserChecker != null)
        {
            progresserChecker.setStatus(false, 1, EffectDuration);
        }
        Destroy(this.gameObject, 0.5f);
    }
}
