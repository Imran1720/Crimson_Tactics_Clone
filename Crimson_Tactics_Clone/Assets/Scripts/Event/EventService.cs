using CrimsonTactics.Level;
using CrimsonTactics.Tile;
using UnityEngine;

namespace CrimsonTactics.Events
{
    public class EventService
    {
        public EventController onPlayerReachedTarget;
        public EventController<TileController> onTargetTileSelected;
        public EventController<Vector3Int, TileType> onTilePositionUpdated;

        public EventService()
        {
            onPlayerReachedTarget = new EventController();
            onTargetTileSelected = new EventController<TileController>();
            onTilePositionUpdated = new EventController<Vector3Int, TileType>();
        }
    }
}
