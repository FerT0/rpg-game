using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("colliding");
        }
    }
}
