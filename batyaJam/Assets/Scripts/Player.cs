using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PowerSlider powerSlider;
    [SerializeField] public float MoveMaxForce;
    [SerializeField] private float forceStep;
    [SerializeField] public GameObject directionArrow;
    [SerializeField] private Transform directionArrowTransform;
    [SerializeField] private ParticleSystem particle;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool hascontrol;

    //sounds
    [SerializeField] AudioClip moveSound;
    private AudioSource aud;

    

    private float moveCurrentForce;
    private Vector3 direction;
    private GameManager gameManager;
    private Transform transf;
    private bool increasePowerValue;

    void Awake()
    {
        //directionArrow.SetActive(false);
        direction = Vector3.zero;
        gameManager = GameManager.Singleton;
        rb = GetComponent<Rigidbody>();
        powerSlider.SetMaxPower(MoveMaxForce);
        moveCurrentForce = 0;
        transf = GetComponent<Transform>();
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hascontrol)
        {
            //кнопка держи
            if (Input.GetKey(KeyCode.Space))
            {
                if (increasePowerValue)
                {
                    if (moveCurrentForce < MoveMaxForce)
                    {
                        moveCurrentForce += forceStep * Time.deltaTime;
                    }
                    else
                    {
                        moveCurrentForce = MoveMaxForce;
                        increasePowerValue = false;
                    }
                }
                else if(!increasePowerValue)
                {
                    if (moveCurrentForce > 0 )
                    {
                        moveCurrentForce -= forceStep * Time.deltaTime;
                    }
                    else
                    {
                        moveCurrentForce = 0;
                        increasePowerValue = true;
                    }
                }
                powerSlider.Setpower(moveCurrentForce);
            }
            //кнопка отпустил и понеслась
            if (Input.GetKeyUp(KeyCode.Space))
            {
                increasePowerValue = true;
                //проверка либо на состояние, либо на то, есть поинтер или нет
                var pointer = gameManager.pointer_set.GetComponent<Transform>();
                direction = new Vector3(pointer.position.x - transform.position.x, 0f, pointer.position.z - transform.position.z).normalized;
                rb.AddForce(direction * moveCurrentForce, ForceMode.Impulse);
                direction = Vector3.zero;
                moveCurrentForce = 0;
                powerSlider.Setpower(0f);
                aud.Play();
                
            }
        }
    }

    public void SetArrowDirection(Transform pointToLook)
    {
        var lookPos = new Vector3(pointToLook.position.x, transf.position.y, pointToLook.position.z);
        directionArrowTransform.LookAt(lookPos);
    }

    public void Death()
    {
        Instantiate(particle, transf.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
