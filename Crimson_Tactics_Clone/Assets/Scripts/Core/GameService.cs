using CrimsonTactics.Events;
using CrimsonTactics.Player;
using CrimsonTactics.UI;
using UnityEngine;

namespace CrimsonTactics.Core
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;

        [SerializeField] private UIService uiService;
        [SerializeField] private PlayerController playerController;

        private void Start()
        {
            eventService = new EventService();

            InitializeServices();
        }

        private void InitializeServices()
        {
            uiService.InitializeServices(eventService);
            playerController.InitializeService(eventService);
        }

        public EventService GetEventService() => eventService;
    }
}
