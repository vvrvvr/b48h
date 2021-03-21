using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private Transform transf;
    private Rigidbody rb;

    private float timeBetweeenAttacks;
    [SerializeField] private float minTimeBetweenAttacks;
    [SerializeField] private float maxTimeBetweenAttacks;

    [SerializeField] private Transform player;
    [SerializeField] private float moveMaxForce;
    [SerializeField] private SphereCollider sphereCol;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private ParticleSystem particle;
    private float colRadius;

    private const float INACTIVE = 0;
    private const float PLAYER_IN_AREA = 1;
    private const float PLAYER_VISIBLE = 2;
    private const float ENEMY_MOVING = 3;
    private float currentState;
    private bool skipFirstAttack;

    private float nextAttackTime;
    private float distanceMagnitude;
    private Vector3 direction;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transf = GetComponent<Transform>();
        nextAttackTime = 0f;
        direction = Vector3.zero;
        colRadius = sphereCol.radius;
        skipFirstAttack = true;
    }

    private void Update()
    {
        if (GameManager.Singleton.EndGame)
            return;
        if(player == null)
        {
            currentState = INACTIVE;
        }
       // Debug.Log(currentState);
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
                        //skipFirstAttack = true;
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
                if (Time.time > nextAttackTime)
                {
                    //Debug.Log("attack");
                    
                    nextAttackTime += timeBetweeenAttacks;
                    timeBetweeenAttacks = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
                    if (skipFirstAttack)
                        nextAttackTime = Time.time + timeBetweeenAttacks;
                    //Debug.Log(timeBetweeenAttacks);
                    if (!skipFirstAttack)
                    {
                        
                        if (rb.velocity.magnitude <= 0)
                        {
                            MoveEnemy(distanceMagnitude);
                        }
                    }
                    skipFirstAttack = false;
                }
                break;
            case ENEMY_MOVING:

                break;
        }
    }

    private void MoveEnemy(float magnitude)
    {
        var percent = ((magnitude - 0.5f) / colRadius) * 100;
        var currentForce = 0f;
        //поправить силу удара
        if (percent > 60)
        {
            currentForce = moveMaxForce * 0.3f;
        }
        else if (percent <= 60)
        {
            currentForce = moveMaxForce * 0.5f;
        }
        //Debug.Log(currentForce);
        direction = new Vector3(player.position.x - transf.position.x, 0f, player.position.z - transf.position.z).normalized;
        rb.AddForce(direction * currentForce, ForceMode.Impulse);
        direction = Vector3.zero;
    }

    public void SetPlayerInArea()
    {
        currentState = PLAYER_IN_AREA;
    }
    public void SetInactive()
    {
        currentState = INACTIVE;
    }
    public void Death()
    {
        Instantiate(particle, transf.position, Quaternion.identity);
        Destroy(gameObject);
    }



}
