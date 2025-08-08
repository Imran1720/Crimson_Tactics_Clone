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

        public TilePositionUI(float visibleDuration, UIService uiService, TextMeshProUGUI text)
        {
            this.uiService = uiService;
            this.tilePositionUIText = text;
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

        public void SetTilePosition(Vector2 tilePosition)
        {
            timer = visibleDuration;
            uiService.ShowTilePositionUI();
            tilePositionUIText.text = $"({tilePosition.x},{tilePosition.y})";
        }
    }
}