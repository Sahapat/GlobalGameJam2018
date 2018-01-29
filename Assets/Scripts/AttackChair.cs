using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChair : Item
{

    public float EffectDuration = 3f;

    private void Awake()
    {
        UseType = ItemUsedType.Attack;
        objItem = gameItem.chair;
    }
    public override void UseItem(GameObject target)
    {
        Player playerChecker = target.GetComponent<Player>();
        Progresser progresserChecker = target.GetComponent<Progresser>();

        if (playerChecker != null)
        {
            //Effect on Player
            playerChecker.setStatus(true, -4f, EffectDuration);
        }
        else if (progresserChecker != null)
        {
            //Effect on Progresser
            progresserChecker.setStatus(true, 4, EffectDuration * 2);
        }
        Destroy(this.gameObject, 0.5f);
    }
}
