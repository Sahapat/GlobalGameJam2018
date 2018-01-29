using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackVirus : Item
{
    private void Awake()
    {
        UseType = ItemUsedType.Boots;
        objItem = gameItem.virus;
    }
    public override void UseItem(GameObject target)
    {
        Progresser progresserChecker = target.GetComponent<Progresser>();
        if (progresserChecker != null)
        {
            progresserChecker.progressHub.totalProgress -= 10;
        }
        Destroy(this.gameObject, 0.5f);
    }
}
