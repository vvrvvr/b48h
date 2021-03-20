using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private Transform transf;
    private Rigidbody rb;
    [SerializeField] private float timeBetweeenAttacks;
    [SerializeField] private Transform player;
    [SerializeField] private float moveForce;

    private const float INACTIVE = 0;
    private const float PLAYER_IN_AREA = 1;
    private const float PLAYER_VISIBLE = 2;
    private const float WAIT = 3;
    private float currentState;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transf = GetComponent<Transform>();
    }

    private void Update()
    {

        switch (currentState)
        {
            case INACTIVE:
                Debug.Log("inactive");
                break;
            case PLAYER_IN_AREA:
                Debug.Log("player in area");

                break;
            case PLAYER_VISIBLE:
                break;
            case WAIT:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            currentState = PLAYER_IN_AREA;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            currentState = INACTIVE;
    }




}
