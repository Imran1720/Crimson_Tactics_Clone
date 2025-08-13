using CrimsonTactics.Level;
using CrimsonTactics.Tile;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTileDataSO", menuName = "SO/LevelTileDataSO")]
public class LevelTileDataSO : ScriptableObject
{
    public int row;
    public int col;
    public List<TileStorageData> tileDataList;

    public void AddItem(Vector3Int position, TileType type)
    {
        tileDataList.Add(new TileStorageData(position, type));
    }
}
