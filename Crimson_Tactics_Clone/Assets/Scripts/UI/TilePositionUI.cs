using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TilePositionUI : MonoBehaviour
{
    [SerializeField] private float visibleDuration;
    [SerializeField] private TextMeshProUGUI tilePositionText;

    private float timer;

    private void Awake()
    {
        HideUI();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            HideUI();
        }
    }

    private void HideUI()
    {
        gameObject.SetActive(false);
    }

    private void ShowUI()
    {
        gameObject.SetActive(true);
    }
    public void SetTilePosition(Vector2 tilePosition)
    {
        tilePositionText.text = $"({tilePosition.x},{tilePosition.y})";
        timer = visibleDuration;
        ShowUI();
    }
}
