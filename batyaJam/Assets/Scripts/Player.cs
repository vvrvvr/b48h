using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PowerSlider powerSlider;
    [SerializeField] public float MoveMaxForce;
    [SerializeField] private float forceStep;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool hascontrol;

    private float moveCurrentForce;
    private Vector3 direction;
    private GameManager gameManager;

    void Awake()
    {
        direction = Vector3.zero;
        gameManager = GameManager.Singleton;
        rb = GetComponent<Rigidbody>();
        powerSlider.SetMaxPower(MoveMaxForce);
        moveCurrentForce = 0;
    }

    void Update()
    {
        if (hascontrol)
        {
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
                //проверка либо на состояние, либо на то, есть поинтер или нет
                var pointer = gameManager.pointer_set.GetComponent<Transform>();
                direction = new Vector3(pointer.position.x - transform.position.x, 0f, pointer.position.z - transform.position.z).normalized;
                rb.AddForce(direction * moveCurrentForce, ForceMode.Impulse);
                direction = Vector3.zero;
                moveCurrentForce = 0;
                powerSlider.Setpower(0f);
            }
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
