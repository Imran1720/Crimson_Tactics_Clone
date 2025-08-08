using CrimsonTactics.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIService : MonoBehaviour
{

    [SerializeField] private TilePositionUI tilePositionUI;
    private EventService eventService;

    void Start()
    {

    }

    public void InitializeServices(EventService eventService)
    {
        this.eventService = eventService;
        eventService.onTilePositionUpdated.AddEventListener(OnTilePositionChanged);
    }

    private void OnDisable()
    {
        eventService.onTilePositionUpdated.RemoveEventListener(OnTilePositionChanged);
    }


    private void OnTilePositionChanged(Vector2 position)
    {
        tilePositionUI.SetTilePosition(position);
    }
}
