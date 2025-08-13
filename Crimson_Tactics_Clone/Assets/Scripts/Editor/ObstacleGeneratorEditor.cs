using CrimsonTactics.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleGenerator))]
public class ObstacleGeneratorEditor : Editor
{
    private int rows;
    private int columns;
    private float gridButtonSize;

    ObstacleGenerator generator;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        generator = (ObstacleGenerator)target;


        if (GUILayout.Button("Initialize Data"))
        {
            generator.InitializeObstacleGenerator();
        }

        InitializeData();

        if (generator.IsGridEmpty() || generator.IsTileDataEmpty())
        {
            return;
        }

        DrawGrid();

        DrawActionButtons();
    }

    private void DrawActionButtons()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Data"))
        {
            generator.SaveData();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            generator.ClearGrid();
        }

        GUILayout.EndHorizontal();
    }

    private void DrawGrid()
    {
        GUILayout.Space(10);
        for (int i = 0; i < rows; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < columns; j++)
            {
                GUI.color = GetTileColor(i, j);
                if (GUILayout.Button("", GUILayout.Width(gridButtonSize * 2), GUILayout.Height(gridButtonSize * 2)))
                {
                    generator.ToggleTileStatus(i, j);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(10);

        GUI.color = Color.white;
    }

    private Color GetTileColor(int x, int y)
    {
        TileType type = generator.GetTileTypeOf(x, y);
        if (type == TileType.FREE)
        {
            return Color.green;
        }
        return Color.red;
    }

    private void InitializeData()
    {
        rows = generator.GetRowCount();
        columns = generator.GetColumnCount();
        gridButtonSize = generator.GetGridButtonSize();
    }
}
