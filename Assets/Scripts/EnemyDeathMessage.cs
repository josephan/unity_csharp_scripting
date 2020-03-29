using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathMessage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SendMessageUpwards("OnDeath");
        }
    }
}
