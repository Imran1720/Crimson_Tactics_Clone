using UnityEngine;

namespace CrimsonTactics.Events
{
    public class EventService
    {
        public EventController<Vector2> onTilePositionUpdated;

        public EventService()
        {
            onTilePositionUpdated = new EventController<Vector2>();
        }
    }
}
