using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ray-Data")]
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask detectionlayer;

    private Ray ray;
    private RaycastHit rayHit;
    private Vector3 mouseWorldPosition;
    private Camera currentCamera;

    private void Start()
    {
        currentCamera = Camera.main;
    }
    private void Update()
    {
        RunHoverDetection();
    }

    private void RunHoverDetection()
    {
        ray = currentCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit, rayDistance, detectionlayer))
        {

            TileController tile = rayHit.collider.GetComponent<TileController>();
            if (tile == null)
            {
                return;
            }
            Debug.Log(tile.GetTilePosition());
        }
    }
}
