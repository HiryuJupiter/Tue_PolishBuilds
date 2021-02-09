using System.Collections;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D somthing: " + collision.gameObject.layer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D somthing: " + collision.gameObject.layer);
    }
}