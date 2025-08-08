using System;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace CrimsonTactics.Level
{
    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : Editor
    {
        private const int VERTICAL_GAP = 20;
        private LevelGenerator levelInstance;
        private LevelLayoutSettings levelLayoutSettings;

        private int gridSizeX;
        private int gridSizeY;
        private float actionButtonWidth;
        private float actionButtonHeight;
        public override void OnInspectorGUI()
        {
            levelInstance = (LevelGenerator)target;
            DrawDefaultInspector();
            InitializeLevelData();

            AddVerticalSpace();

            if (GUILayout.Button("Draw Grid"))
            {
                levelInstance.CreateGridTileArray();
            }

            if (IsGridDimensionsInvalid() || levelInstance.IsGridArrayEmpty())
            {
                return;
            }
            GenerateGridButtons();
            AddVerticalSpace();

            SpawnTiles();
        }

        private static void AddVerticalSpace()
        {
            GUILayout.Space(VERTICAL_GAP);
        }

        private void SpawnTiles()
        {
            if (GUILayout.Button("Spawn Tiles", GUILayout.Width(actionButtonWidth), GUILayout.Height(actionButtonHeight)))
            {
                levelInstance.GenerateGrid();
            }
        }

        // this checks for valid gridsize 
        // retuns true for values less than 1
        // retuns false for values greater than 0
        private bool IsGridDimensionsInvalid()
        {
            return levelLayoutSettings.gridSizeX <= 0 || levelLayoutSettings.gridSizeY <= 0;
        }

        private void GenerateGridButtons()
        {
            float buttonSize = levelLayoutSettings.tileButtonWidth;
            for (int i = 0; i < gridSizeX; i++)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < gridSizeY; j++)
                {
                    GUI.color = Color.green;
                    if (GUILayout.Button("", GUILayout.Width(buttonSize * 2), GUILayout.Height(buttonSize * 2)))
                    {
                        Debug.Log("Grid " + gridSizeX);
                        DoSomething();
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUI.color = Color.white;
        }

        private void DoSomething()
        {
            Debug.Log("\n  AA");
        }

        // Initializes level data, including grid size, button size, and other related settings.
        private void InitializeLevelData()
        {
            levelLayoutSettings = levelInstance.GetLevelLayoutSettings();
            if (levelLayoutSettings != null)
            {
                gridSizeX = levelLayoutSettings.gridSizeX;
                gridSizeY = levelLayoutSettings.gridSizeY;
                actionButtonWidth = levelLayoutSettings.actionButtonWidth;
                actionButtonHeight = levelLayoutSettings.actionButtonHeight;
            }
        }
    }
}
