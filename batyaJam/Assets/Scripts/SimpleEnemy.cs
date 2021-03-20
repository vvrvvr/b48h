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
    [SerializeField] private LayerMask playerLayer;

    private const float INACTIVE = 0;
    private const float PLAYER_IN_AREA = 1;
    private const float PLAYER_VISIBLE = 2;
    private const float ENEMY_MOVING = 3;
    private float currentState;

    private float nextAttackTime;
    private float distanceMagnitude;
    private Vector3 direction;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transf = GetComponent<Transform>();
        nextAttackTime = 0f;
        direction = Vector3.zero;
    }

    private void Update()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case INACTIVE:
                // Debug.Log("inactive");
                break;
            case PLAYER_IN_AREA:
                //Debug.Log("player in area");
                RaycastHit hit;
                if (Physics.Raycast(transf.position, player.position - transf.position, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transf.position, player.position - transf.position, Color.yellow);
                    if (hit.collider.name == "Player")
                        currentState = PLAYER_VISIBLE;
                    //Debug.Log($"hit: {hit.collider.name}");
                }
                break;
            case PLAYER_VISIBLE:
                var distance = player.position - transf.position;
                if (Physics.Raycast(transf.position, distance, out hit, Mathf.Infinity))
                {
                    distanceMagnitude = distance.magnitude;
                    Debug.DrawRay(transf.position, player.position - transf.position, Color.yellow);
                    if (hit.collider.name != "Player")
                        currentState = PLAYER_IN_AREA;
                }
                //attack logic here
                if(Time.time > nextAttackTime)
                {
                    nextAttackTime += timeBetweeenAttacks;
                    Debug.Log("attack");
                    if(rb.velocity.magnitude <= 0)
                    {
                        MoveEnemy(distanceMagnitude);
                    }
                }
                break;
            case ENEMY_MOVING:

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

    private void MoveEnemy(float magnitude)
    {
        Debug.Log(magnitude - 0.5f);
        //проверка либо на состояние, либо на то, есть поинтер или нет
        direction = new Vector3(player.position.x - transf.position.x, 0f, player.position.z - transf.position.z).normalized;
        rb.AddForce(direction * moveForce, ForceMode.Impulse);
        direction = Vector3.zero;   
    }



}
