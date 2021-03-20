using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject pointer;
    [SerializeField] public GameObject pointer_set;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Player player;
    public static GameManager Singleton;
    private Camera mainCamera;

    private const float CHOOSE_POINT = 0;
    private const float POINT_SET = 1;
    private const float CHARACTER_MOVING = 2;
    private const float WAIT = 3;
    private float currentState;
    [HideInInspector] public bool EndGame;



    private void Awake()
    {
        if(Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        mainCamera = Camera.main;
        pointer.SetActive(false);
        pointer_set.SetActive(false);
        currentState = CHOOSE_POINT;
        EndGame = false;
    }

    private void Update()
    {
        if (player != null)
        {
            if (player.rb.velocity.magnitude > 0)
            {
                currentState = CHARACTER_MOVING;
                pointer.SetActive(false);
                pointer_set.SetActive(false);
            }
            switch (currentState)
            {
                case CHOOSE_POINT:
                    player.hascontrol = false;

                    RaycastHit rayHit;
                    if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
                    {
                        pointer.SetActive(true);
                        player.directionArrow.SetActive(true);
                        MovePointer(rayHit.point);
                        player.SetArrowDirection(pointer.GetComponent<Transform>());
                        if (Input.GetMouseButton(0))
                        {
                            pointer.SetActive(false);
                            pointer_set.SetActive(true);
                            pointer_set.GetComponent<Transform>().position = rayHit.point;
                            currentState = POINT_SET;
                        }
                    }
                    else
                    {
                        pointer.SetActive(false);
                        player.directionArrow.SetActive(false);
                    }

                    break;
                case POINT_SET:
                    player.hascontrol = true;
                    if (player.rb.velocity.magnitude > 0)
                    {
                        pointer_set.SetActive(false);
                        currentState = CHARACTER_MOVING;
                    }
                    break;
                case CHARACTER_MOVING:
                    player.directionArrow.SetActive(false);
                    player.hascontrol = false;
                    if (player.rb.velocity.magnitude <= 0)
                    {
                        currentState = CHOOSE_POINT;
                    }
                    break;
                case WAIT:
                    //заглушка на случай

                    break;
            }
        }
    }

    public void MovePointer(Vector3 pos)
    {
        pointer.GetComponent<Transform>().position = pos;
    }

    public void PlayerDeath()
    {
        player.Death();
    }

    public void Endgame()
    {
       
        player.hascontrol = false;
        player.rb.velocity = Vector3.zero;
        pointer.SetActive(false);
        pointer_set.SetActive(false);
        player.directionArrow.SetActive(false);
        currentState = WAIT;
        EndGame = true;
        player = null;
    }

   
}
