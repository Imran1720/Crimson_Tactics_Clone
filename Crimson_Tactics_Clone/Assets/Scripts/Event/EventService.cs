using CrimsonTactics.Level;
using CrimsonTactics.Tile;
using UnityEngine;

namespace CrimsonTactics.Events
{
    public class EventService
    {
        public EventController onEnemyReachedTarget;
        public EventController<Vector3Int> onPlayerReachedTarget;
        public EventController<TileController> onTargetTileSelected;
        public EventController<Vector3Int, TileType> onTilePositionUpdated;

        public EventService()
        {
            onEnemyReachedTarget = new EventController();
            onPlayerReachedTarget = new EventController<Vector3Int>();
            onTargetTileSelected = new EventController<TileController>();
            onTilePositionUpdated = new EventController<Vector3Int, TileType>();
        }
    }
}
