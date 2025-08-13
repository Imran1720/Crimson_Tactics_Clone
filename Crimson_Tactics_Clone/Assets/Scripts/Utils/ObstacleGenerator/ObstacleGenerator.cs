using CrimsonTactics.Tile;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

namespace CrimsonTactics.Level
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private LevelTileDataSO levelTileDataSO;
        [SerializeField] private ObstacleTileDataSO obstacleTileDataSO;

        [SerializeField] private Transform obstaclePrefab;
        [SerializeField] private Transform obstacleContainerPrefab;

        [SerializeField] private float obstacleOffset;
        [SerializeField] private float gridButtonSize;

        private int rowCount;
        private int columnCount;
        private TileType[,] gridTileArray;

        public void InitializeObstacleGenerator()
        {
            gridTileArray = null;
            rowCount = levelTileDataSO.row;
            columnCount = levelTileDataSO.col;
            if (rowCount == 0 || columnCount == 0)
            {
                return;
            }
            gridTileArray = new TileType[rowCount, columnCount];

            InitializeArray();
        }

        private void InitializeArray()
        {
            List<TileStorageData> tileDataList = levelTileDataSO.tileDataList;

            if (tileDataList.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    gridTileArray[i, j] = tileDataList[(i * 10) + j].GetTileType();
                }
            }
        }

        public void ToggleTileStatus(int x, int y)
        {
            if (levelTileDataSO.tileDataList.Count <= 0)
            {
                return;
            }
            gridTileArray[x, y] = gridTileArray[x, y] == TileType.FREE ? TileType.OBSTACLE : TileType.FREE;
            levelTileDataSO.tileDataList[(x * 10) + y].SetTileType(gridTileArray[x, y]);
        }

        public void ClearGrid()
        {
            obstacleTileDataSO.obstaclePositionList.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    gridTileArray[i, j] = TileType.FREE;
                    levelTileDataSO.tileDataList[(i * 10) + j].SetTileType(TileType.FREE);
                }
            }
            SaveData();
        }

        public bool IsTileDataEmpty() => levelTileDataSO.tileDataList.Count <= 0;

        public void SaveData()
        {
            obstacleTileDataSO.obstaclePositionList.Clear();

            foreach (TileStorageData data in levelTileDataSO.tileDataList)
            {
                if (data.tileType == TileType.OBSTACLE)
                {
                    obstacleTileDataSO.AddObstacleTilePosition(data.GetGridPosition());
                }
            }

            SaveScriptableData(levelTileDataSO);
            SaveScriptableData(obstacleTileDataSO);
        }

        private void SaveScriptableData(ScriptableObject scriptableData)
        {
            EditorUtility.SetDirty(scriptableData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public int GetRowCount() => rowCount;
        public int GetColumnCount() => columnCount;
        public bool IsGridEmpty() => gridTileArray == null;
        public float GetGridButtonSize() => gridButtonSize;
        public TileType GetTileTypeOf(int x, int y) => gridTileArray[x, y];
    }
}
