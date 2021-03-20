using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Singleton.PlayerDeath();
        }
        else if(other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<SimpleEnemy>().Death();
        }
    }
}
