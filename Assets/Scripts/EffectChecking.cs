using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectChecking : MonoBehaviour
{
    public GameObject targetObject = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(targetObject == collision.gameObject)
        {
            targetObject = null;
        }
    }
}
