using CrimsonTactics.Tile;
using System;
using UnityEngine;

namespace CrimsonTactics.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Level-Data")]
        [SerializeField] private Transform gameService;
        [SerializeField] private TileController tilePrefab;
        [SerializeField] private ObstacleTileDataSO tileDataSO;
        [SerializeField] private Transform levelContainerPrefab;
        [SerializeField] private LevelLayoutSettings layoutSettings;
        [SerializeField] private float obstacleOffset;

        private Transform levelContainer;


        private TileType[,] gridTileArray;

        private int gridSizeX;
        private int gridSizeY;
        private Vector3 startOffset;
        private int obstacleCount;

        //Method to create the gridSizeX x gridSizeY sized TileType Array
        public void CreateGridTileArray()
        {
            gridSizeX = layoutSettings.gridSizeX;
            gridSizeY = layoutSettings.gridSizeY;

            gridTileArray = new TileType[gridSizeX, gridSizeY];
        }

        //This method Instantiates the level tiles in the scene based on the grid size provided (default: 10x10).
        public void GenerateGrid()
        {
            Vector3 tilePosition = Vector3.zero;
            tileDataSO.InitializeList(obstacleCount);

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
                    if (gridTileArray[i, j] == TileType.OBSTACLE)
                    {
                        spawnedTile.ToggleTileStatus();
                        tileDataSO.AddObstacleTilePosition(tilePosition);
                    }
                }
            }
        }

        public void ToggleTileStatus(int x, int y)
        {
            if ((gridTileArray[x, y] == TileType.OBSTACLE))
            {
                gridTileArray[x, y] = TileType.FREE;
                if (obstacleCount > 0)
                {
                    obstacleCount--;
                }
            }
            else
            {
                gridTileArray[x, y] = TileType.OBSTACLE;
                obstacleCount++;
            }
        }

        public TileType GettileType(int x, int y)
        {
            return gridTileArray[x, y];
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
            startOffset = new Vector3(gridSizeX / 2, transform.position.y, gridSizeY / 2);
            Vector3 tilePosition = new Vector3(x, obstacleOffset, z) - startOffset;
            return tilePosition;
        }
    }
}
