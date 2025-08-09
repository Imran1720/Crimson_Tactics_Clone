using CrimsonTactics.Events;
using CrimsonTactics.Tile;
using System;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private EventService eventService;

        [Header("Ray-Data")]
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask detectionlayer;

        private Ray ray;
        private RaycastHit rayHit;

        private Camera currentCamera;
        private Vector2 oldHoverGridPosition;
        private TileController targtTile;

        private bool isInputEnabled;

        private void Start()
        {
            EnableInput();
            currentCamera = Camera.main;
        }

        public void InitializeData(EventService eventService)
        {
            this.eventService = eventService;
            eventService.onPlayerReachedTarget.AddEventListener(PlayerReachedTarget);
        }

        private void Update()
        {
            if (isInputEnabled)
            {
                RunHoverDetection();

                if (Input.GetMouseButtonDown(0))
                {
                    MovePlayerUnit();
                }
            }
        }

        private void MovePlayerUnit()
        {
            //player movment logic
            eventService.onTargetTileSelected.InvokeEvent(targtTile);
            DisableInput();
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

                UpdateGridPosition(tile);
            }
        }

        private void UpdateGridPosition(TileController tile)
        {
            targtTile = tile;
            oldHoverGridPosition = tile.GetTileGridPosition();
            eventService.onTilePositionUpdated.InvokeEvent(oldHoverGridPosition, tile.GetTileType());
        }

        private void PlayerReachedTarget()
        {
            EnableInput();
        }

        private void EnableInput() => isInputEnabled = true;
        private void DisableInput() => isInputEnabled = false;

        private bool IsInvalidTile(TileController tile) => tile == null;
        private bool IsOldPosition(TileController tile) => oldHoverGridPosition == tile.GetTileGridPosition();

    }
}