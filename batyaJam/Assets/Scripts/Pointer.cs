using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform pointerTransform;
    private Camera mainCamera;
    [SerializeField] LayerMask groundLayer;

    void Awake()
    {
        mainCamera = Camera.main;
    }


    private void OnMouseDown()
    {
        
        RaycastHit rayHit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
        {
            pointerTransform.position = rayHit.point;
            
        }
    }

}
