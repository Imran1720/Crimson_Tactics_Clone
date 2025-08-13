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

        [SerializeField] private TextMeshProUGUI tileStatusText;
        [SerializeField] private TextMeshProUGUI tilePositionText;

        [SerializeField] private LevelTileDataSO tileLevelDataSO;

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

        public TileType GetTileType(int x, int y) => tileLevelDataSO.tileDataList[(x * 10) + y].GetTileType();
        private void OnDisable()
        {
            eventService.onTilePositionUpdated.RemoveEventListener(UpdateTilePositionUI);
        }

        private void UpdateTilePositionUI(Vector3Int position)
        {
            tilePositionUI.SetTilePosition(position, GetTileType(position.x, position.z));
        }

        public void HideTilePositionUI() => HideUI(tilePositionUIGO);
        public void ShowTilePositionUI() => ShowUI(tilePositionUIGO);

        public void ShowUI(GameObject uiObject) => uiObject.SetActive(true);
        public void HideUI(GameObject uiObject) => uiObject.SetActive(false);
    }
}