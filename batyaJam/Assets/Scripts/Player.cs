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
    [SerializeField] private GameObject lookat;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool hascontrol;

    private float moveCurrentForce;
    private Vector3 direction;
    private GameManager gameManager;
    private Transform transf;

    void Awake()
    {
        //directionArrow.SetActive(false);
        direction = Vector3.zero;
        gameManager = GameManager.Singleton;
        rb = GetComponent<Rigidbody>();
        powerSlider.SetMaxPower(MoveMaxForce);
        moveCurrentForce = 0;
        transf = GetComponent<Transform>();
    }

    void Update()
    {
        if (hascontrol)
        {
            //������ �����
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
            //������ �������� � ���������
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //�������� ���� �� ���������, ���� �� ��, ���� ������� ��� ���
                var pointer = gameManager.pointer_set.GetComponent<Transform>();
                direction = new Vector3(pointer.position.x - transform.position.x, 0f, pointer.position.z - transform.position.z).normalized;
                rb.AddForce(direction * moveCurrentForce, ForceMode.Impulse);
                direction = Vector3.zero;
                moveCurrentForce = 0;
                powerSlider.Setpower(0f);
            }
        }
    }

    public void SetArrowDirection(Transform pointToLook)
    {
        var lookPos = new Vector3(pointToLook.position.x, transf.position.y, pointToLook.position.z);
        directionArrowTransform.LookAt(lookPos);
    }
}
