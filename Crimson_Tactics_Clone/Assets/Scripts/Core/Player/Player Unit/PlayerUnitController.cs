
using CrimsonTactics.Events;
using CrimsonTactics.Level;
using CrimsonTactics.Tile;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerUnitController
    {
        PlayerUnitView playerUnitView;
        private EventService eventService;

        private TileController targetTile;

        public PlayerUnitController(PlayerUnitView playerUnitView, EventService eventService)
        {
            this.playerUnitView = playerUnitView;
            this.eventService = eventService;

            playerUnitView.SetPlayerUnitController(this);
            eventService.onTargetTileSelected.AddEventListener(OnTragetTileSelected);
        }

        private void OnTragetTileSelected(TileController tileController)
        {
            playerUnitView.SetTarget(tileController.GetTileGridPosition());
        }

        public void InvokePlayerDestinationReached()
        {
            eventService.onPlayerReachedTarget.InvokeEvent();
        }
    }
}
