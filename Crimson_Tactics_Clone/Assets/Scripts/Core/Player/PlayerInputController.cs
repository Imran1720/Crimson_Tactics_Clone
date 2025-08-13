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
        private Vector3Int oldHoverGridPosition;
        private TileController targtTile;

        private bool isInputEnabled;

        private LevelTileDataSO levelTileDataSO;

        private void Start()
        {
            EnableInput();
            targtTile = null;
            currentCamera = Camera.main;
        }

        public void InitializeData(EventService eventService, LevelTileDataSO levelTileDataSO)
        {
            this.eventService = eventService;
            this.levelTileDataSO = levelTileDataSO;
            eventService.onEnemyReachedTarget.AddEventListener(OnEnemyReachedTarget);
        }

        private void Update()
        {
            if (isInputEnabled)
            {
                RunHoverDetection();
                if (Input.GetMouseButtonDown(0) && IsTileFree())
                {
                    MovePlayerUnit();
                }
            }
        }

        private bool IsTileFree()
        {
            int x = targtTile.GetTileGridPosition().x;
            int y = targtTile.GetTileGridPosition().z;
            return levelTileDataSO.tileDataList[(x * 10) + y].GetTileType() == Level.TileType.FREE;
        }

        private void MovePlayerUnit()
        {
            //player movment logic
            if (targtTile == null)
            {
                return;
            }
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
            eventService.onTilePositionUpdated.InvokeEvent(oldHoverGridPosition);
        }

        private void OnEnemyReachedTarget() => EnableInput();

        private void EnableInput() => isInputEnabled = true;
        private void DisableInput() => isInputEnabled = false;

        private bool IsInvalidTile(TileController tile) => tile == null;
        private bool IsOldPosition(TileController tile) => oldHoverGridPosition == tile.GetTileGridPosition();
    }
}