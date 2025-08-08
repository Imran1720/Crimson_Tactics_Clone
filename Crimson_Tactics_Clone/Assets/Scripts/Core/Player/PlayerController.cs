using CrimsonTactics.Events;
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
    private Camera currentCamera;
    private Vector2 oldHoverGridPosition;

    private EventService eventService;
    private void Start()
    {
        currentCamera = Camera.main;
    }

    public void InitializeService(EventService eventService)
    {
        this.eventService = eventService;
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
            if (IsInvalidTile(tile) || IsOldPosition(tile))
            {
                return;
            }

            oldHoverGridPosition = tile.GetTileGridPosition();
            eventService.onTilePositionUpdated.InvokeEvent(oldHoverGridPosition);
        }
    }

    private bool IsOldPosition(TileController tile)
    {
        return oldHoverGridPosition == tile.GetTileGridPosition();
    }

    private static bool IsInvalidTile(TileController tile)
    {
        return tile == null;
    }
}
