using CrimsonTactics.Events;
using CrimsonTactics.Level;
using CrimsonTactics.Tile;
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
        [SerializeField] private TextMeshProUGUI tileStatusText;

        private TilePositionUI tilePositionUI;

        private void Start()
        {
            tilePositionUI = new TilePositionUI(visibleDuration, this, tilePositionText, tileStatusText);
        }

        private void Update()
        {
            tilePositionUI.Update();
        }
        public void InitializeData(EventService eventService)
        {
            this.eventService = eventService;
            eventService.onTilePositionUpdated.AddEventListener(UpdateTilePositionUI);
        }

        private void OnDisable()
        {
            eventService.onTilePositionUpdated.RemoveEventListener(UpdateTilePositionUI);
        }

        private void UpdateTilePositionUI(Vector2Int position, TileType tileType)
        {
            tilePositionUI.SetTilePosition(position, tileType);
        }

        public void HideTilePositionUI() => HideUI(tilePositionUIGO);
        public void ShowTilePositionUI() => ShowUI(tilePositionUIGO);

        public void ShowUI(GameObject uiObject) => uiObject.SetActive(true);
        public void HideUI(GameObject uiObject) => uiObject.SetActive(false);
    }
}