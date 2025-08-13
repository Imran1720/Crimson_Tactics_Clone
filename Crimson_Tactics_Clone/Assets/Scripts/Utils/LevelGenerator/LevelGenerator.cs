using CrimsonTactics.Tile;
using System;
using UnityEditor;
using UnityEngine;

namespace CrimsonTactics.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Level-Data")]
        [SerializeField] private Transform gameService;

        [SerializeField] private TileController tilePrefab;
        [SerializeField] private Transform levelContainerPrefab;

        [SerializeField] private LevelTileDataSO levelTileDataSO;
        [SerializeField] private LevelLayoutSettings layoutSettings;

        [SerializeField] private float obstacleOffset;

        private Transform levelContainer;

        private TileType[,] gridTileArray;

        private int gridSizeX;
        private int gridSizeY;

        //Method to create the gridSizeX x gridSizeY sized TileType Array
        public void CreateGridTileArray()
        {
            gridSizeX = layoutSettings.gridSizeX;
            gridSizeY = layoutSettings.gridSizeY;

            gridTileArray = new TileType[gridSizeX, gridSizeY];
            levelTileDataSO.tileDataList.Clear();
            levelTileDataSO.row = gridSizeX;
            levelTileDataSO.col = gridSizeY;
        }

        //This method Instantiates the level tiles in the scene based on the grid size provided (default: 10x10).
        public void GenerateGrid()
        {
            Vector3 tilePosition = Vector3.zero;
            if (gridTileArray == null)
            {
                return;
            }

            levelContainer = Instantiate(levelContainerPrefab, transform.position, Quaternion.identity);
            levelContainer.SetParent(gameService, false);

            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    tilePosition = GetTilePosition(i, j);

                    TileController spawnedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                    spawnedTile.transform.SetParent(levelContainer, false);

                    Vector3Int position = new Vector3Int(i, 0, j);
                    levelTileDataSO.AddItem(position, gridTileArray[i, j]);
                }
            }
            SaveScriptableData(levelTileDataSO);
        }

        public void ClearLevel()
        {
            if (levelContainer != null)
            {
                DestroyImmediate(levelContainer.gameObject);
            }
            gridTileArray = null;
        }

        public LevelLayoutSettings GetLevelLayoutSettings() => layoutSettings;
        public bool IsGridArrayEmpty() => gridTileArray == null;

        //Returns the tile position relative to the start offset,
        //ensuring that the tile generated at the current transform is the center tile.
        private Vector3 GetTilePosition(int x, int z)
        {
            return new Vector3(x, transform.position.y, z);
        }

        private void SaveScriptableData(ScriptableObject scriptableData)
        {
            EditorUtility.SetDirty(scriptableData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
