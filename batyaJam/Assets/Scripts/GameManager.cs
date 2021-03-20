using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
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
    }

    private void Update()
    {
        switch(currentState)
        {
            case CHOOSE_POINT:
                RaycastHit rayHit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
                {
                    pointer.SetActive(true);
                    MovePointer(rayHit.point);
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
                }
                
                break;
            case POINT_SET:
                if(player.rb.velocity.magnitude > 0)
                {
                    pointer_set.SetActive(false);
                    currentState = CHARACTER_MOVING;
                }
                break;
            case CHARACTER_MOVING:

                break;
            case WAIT:
                break;
        }
        
    }

    public void MovePointer(Vector3 pos)
    {
        pointer.GetComponent<Transform>().position = pos;
    }



}
