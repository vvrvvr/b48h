using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    //private void Update()
    //{
    //    RaycastHit rayHit;
    //    if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
    //    {
    //        GameManager.Singleton.MovePointer(rayHit.point);
    //    }
    //}

    //private void OnMouseDown()
    //{

    //    RaycastHit rayHit;
    //    if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
    //    {
    //        GameManager.Singleton.MovePointer(rayHit.point);
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit rayHit;
    //        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
    //        {
    //            //Debug.Log("hit");
    //            var pointer = gameManager.pointer;
    //            pointer.position = rayHit.point;
    //        }
    //    }
    //}

}
