using System;
using UnityEngine;

namespace CrimsonTactics.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Level-Data")]
        [SerializeField] private LevelLayoutSettings layoutSettings;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform levelContainer;


        private TileType[,] gridTileArray;

        private int gridSizeX;
        private int gridSizeY;
        private Vector3 startOffset;

        public void InitializeLevelData()
        {
            gridSizeX = layoutSettings.gridSizeX;
            gridSizeY = layoutSettings.gridSizeY;
        }

        //Method to create the gridSizeX x gridSizeY sized TileType Array
        public void CreateGridTileArray()
        {
            gridTileArray = new TileType[gridSizeX, gridSizeY];
        }

        //This method Instantiates the level tiles in the scene based on the grid size provided (default: 10x10).
        public void GenerateGrid()
        {
            if (gridTileArray == null)
            {
                return;
            }

            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    GameObject spawnedTile = Instantiate(tilePrefab, GetTilePosition(i, j), Quaternion.identity);
                    spawnedTile.transform.SetParent(levelContainer, false);
                }
            }
        }

        public LevelLayoutSettings GetLevelLayoutSettings() => layoutSettings;
        public bool IsGridArrayEmpty() => gridTileArray == null;

        //Returns the tile position relative to the start offset,
        //ensuring that the tile generated at the current transform is the center tile.
        private Vector3 GetTilePosition(int x, int z)
        {
            startOffset = new Vector3(gridSizeX / 2, transform.position.y, gridSizeY / 2);
            Vector3 tilePosition = new Vector3(x, transform.position.y, z) - startOffset;
            return tilePosition;
        }

    }
}
