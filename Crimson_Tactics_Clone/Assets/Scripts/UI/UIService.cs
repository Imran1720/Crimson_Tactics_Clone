using CrimsonTactics.Events;
using TMPro;
using UnityEngine;

namespace CrimsonTactics.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService eventService;

        [SerializeField] private float visibleDuration;
        [SerializeField] private GameObject tilePositionUIGO;
        [SerializeField] private TextMeshProUGUI tilePositionText;

        private TilePositionUI tilePositionUI;

        private void Start()
        {
            tilePositionUI = new TilePositionUI(visibleDuration, this, tilePositionText);
        }

        private void Update()
        {
            tilePositionUI.Update();
        }
        public void InitializeServices(EventService eventService)
        {
            this.eventService = eventService;
            eventService.onTilePositionUpdated.AddEventListener(UpdateTilePositionUI);
        }

        private void OnDisable()
        {
            eventService.onTilePositionUpdated.RemoveEventListener(UpdateTilePositionUI);
        }

        private void UpdateTilePositionUI(Vector2 position)
        {
            tilePositionUI.SetTilePosition(position);
        }

        public void HideTilePositionUI() => HideUI(tilePositionUIGO);
        public void ShowTilePositionUI() => ShowUI(tilePositionUIGO);

        public void ShowUI(GameObject uiObject) => uiObject.SetActive(true);
        public void HideUI(GameObject uiObject) => uiObject.SetActive(false);
    }
}