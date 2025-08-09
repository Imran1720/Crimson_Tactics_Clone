
using CrimsonTactics.Level;
using CrimsonTactics.Tile;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTileDataSO", menuName = "SO/LevelTileDataSO")]
public class LevelTileDataSO : ScriptableObject
{
    public int row;
    public int col;
    public List<TileStorageData> tileDataList;
}
