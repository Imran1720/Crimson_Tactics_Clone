using CrimsonTactics.Level;
using UnityEngine;

namespace CrimsonTactics.Events
{
    public class EventService
    {
        public EventController<Vector2, TileType> onTilePositionUpdated;

        public EventService()
        {
            onTilePositionUpdated = new EventController<Vector2, TileType>();
        }
    }
}
