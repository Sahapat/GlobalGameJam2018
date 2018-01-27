using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressChecker : MonoBehaviour
{
    public GameObject targetObject = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Progresser"))
        {
            targetObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetObject == collision.gameObject)
        {
            targetObject = null;
        }
    }
}
