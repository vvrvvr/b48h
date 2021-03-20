using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField]  private SimpleEnemy enemy;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            enemy.SetPlayerInArea();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            enemy.SetInactive();
    }
}
