using CrimsonTactics.Events;
using CrimsonTactics.Tile;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerController : MonoBehaviour
    {
        private EventService eventService;

        [Header("Ray-Data")]
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask detectionlayer;

        private Ray ray;
        private RaycastHit rayHit;

        private Camera currentCamera;
        private Vector2 oldHoverGridPosition;

        private void Start()
        {
            currentCamera = Camera.main;
        }

        public void InitializeData(EventService eventService)
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

                UpdateGridPosition(tile);
            }
        }

        private void UpdateGridPosition(TileController tile)
        {
            oldHoverGridPosition = tile.GetTileGridPosition();
            eventService.onTilePositionUpdated.InvokeEvent(oldHoverGridPosition, tile.GetTileType());
        }

        private bool IsInvalidTile(TileController tile) => tile == null;
        private bool IsOldPosition(TileController tile) => oldHoverGridPosition == tile.GetTileGridPosition();
    }
}