using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] showingSprite = null;
    [SerializeField]
    private GameObject showingObj = null;

    private SpriteRenderer spriteChange = null;

    private void Awake()
    {
        spriteChange = showingObj.GetComponent<SpriteRenderer>();
    }
    public void showRequest(RequestItem item)
    {
        switch(item)
        {
            case RequestItem.ArtAsset:
                spriteChange.sprite = showingSprite[0];
                break;
            case RequestItem.data:
                spriteChange.sprite = showingSprite[1];
                break;
            case RequestItem.RedBull:
                spriteChange.sprite = showingSprite[2];
                break;
            case RequestItem.None:
                spriteChange.sprite = null;
                break;
        }
    }
}
