using CrimsonTactics.Level;
using CrimsonTactics.Tile;
using UnityEngine;

namespace CrimsonTactics.Events
{
    public class EventService
    {
        public EventController<Vector2Int, TileType> onTilePositionUpdated;
        public EventController<TileController> onTargetTileSelected;
        public EventController onPlayerReachedTarget;

        public EventService()
        {
            onTilePositionUpdated = new EventController<Vector2Int, TileType>();
            onTargetTileSelected = new EventController<TileController>();
            onPlayerReachedTarget = new EventController();
        }
    }
}
