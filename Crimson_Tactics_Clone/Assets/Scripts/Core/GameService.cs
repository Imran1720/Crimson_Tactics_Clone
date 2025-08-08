using CrimsonTactics.Events;
using UnityEngine;

namespace CrimsonTactics.Core
{
    public class GameService : MonoBehaviour
    {
        [SerializeField] private UIService uiService;
        [SerializeField] private PlayerController playerController;

        private EventService eventService;

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
