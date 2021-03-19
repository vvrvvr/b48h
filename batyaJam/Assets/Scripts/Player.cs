using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] PowerSlider powerSlider;
    [SerializeField] public float MoveMaxForce;
    private float moveCurrentForce;
    [SerializeField] private float forceStep;
    private Rigidbody rb;
    private Vector3 direction;

    void Awake()
    {
        direction = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        powerSlider.SetMaxPower(MoveMaxForce);
        moveCurrentForce = 0;
    }

    void Update()
    {
       //if(Input.GetKeyDown(KeyCode.Space))
       // {
       //     direction = new Vector3( pointer.position.x - transform.position.x, 0f, pointer.position.z - transform.position.z).normalized;
       // }

        //кнопка нажал

        //кнопка держи
        if (Input.GetKey(KeyCode.Space))
        {
            if (moveCurrentForce < MoveMaxForce)
            {
                moveCurrentForce += forceStep;
            }
            else
                moveCurrentForce = MoveMaxForce;
            powerSlider.Setpower(moveCurrentForce);
        }
        //кнопка отпустил и понеслась
        if (Input.GetKeyUp(KeyCode.Space))
        {
            direction = new Vector3(pointer.position.x - transform.position.x, 0f, pointer.position.z - transform.position.z).normalized;
            rb.AddForce(direction * moveCurrentForce, ForceMode.Impulse);
            direction = Vector3.zero;
            moveCurrentForce = 0;
            powerSlider.Setpower(0f);
        }

    }

    //private void MoveCharacter()
    //{
    //    rb.AddForce(direction * MoveMaxForce, ForceMode.Impulse);
    //    direction = Vector3.zero;
    //}

    //private void FixedUpdate()
    //{
    //    MoveCharacter();
    //}

}
