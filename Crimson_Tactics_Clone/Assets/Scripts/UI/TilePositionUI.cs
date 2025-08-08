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
        private TextMeshProUGUI tilePositionUIText;
        private TextMeshProUGUI tileStatusUIText;

        public TilePositionUI(float visibleDuration, UIService uiService, TextMeshProUGUI text, TextMeshProUGUI statusText)
        {
            this.uiService = uiService;
            tilePositionUIText = text;
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

        public void SetTilePosition(Vector2 tilePosition, TileType tileType)
        {
            timer = visibleDuration;
            uiService.ShowTilePositionUI();
            tilePositionUIText.text = $"({tilePosition.x},{tilePosition.y})";
            tileStatusUIText.text = tileType == TileType.OBSTACLE ? "obstacle" : "Free";
        }
    }
}