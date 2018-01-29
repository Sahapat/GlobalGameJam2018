using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKeyboard : Item
{
    public float EffectDuration = 2f;

    private void Awake()
    {
        UseType = ItemUsedType.Attack;
        objItem = gameItem.keyboard;
    }
    public override void UseItem(GameObject target)
    {
        Player playerChecker = target.GetComponent<Player>();
        Progresser progresserChecker = target.GetComponent<Progresser>();

        if (playerChecker != null)
        {
            //Effect on Player
            playerChecker.setStatus(false, -3f, EffectDuration);
        }
        else if (progresserChecker != null)
        {
            //Effect on Progresser
            progresserChecker.setStatus(true, 4, EffectDuration*2);
        }
        Destroy(this.gameObject, 0.5f);
    }
}
