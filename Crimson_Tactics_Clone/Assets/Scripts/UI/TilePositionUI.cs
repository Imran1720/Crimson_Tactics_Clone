using CrimsonTactics.Level;
using TMPro;
using UnityEngine;

namespace CrimsonTactics.UI
{
    public class TilePositionUI
    {
        private UIService uiService;

        private float timer;
        private float visibleDuration;

        private TextMeshProUGUI tileStatusUIText;
        private TextMeshProUGUI tilePositionUIText;

        public TilePositionUI(float visibleDuration, UIService uiService, TextMeshProUGUI text, TextMeshProUGUI statusText)
        {
            tilePositionUIText = text;
            this.uiService = uiService;
            tileStatusUIText = statusText;
            this.visibleDuration = visibleDuration;
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                uiService.HideTilePositionUI();
            }
        }

        public void SetTilePosition(Vector3Int tilePosition, TileType tileType)
        {
            timer = visibleDuration;
            uiService.ShowTilePositionUI();
            tilePositionUIText.text = $"({tilePosition.x},{tilePosition.z})";
            tileStatusUIText.text = tileType == TileType.OBSTACLE ? "obstacle" : "Free";
        }
    }
}