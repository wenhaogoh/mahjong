using System.Collections.Generic;
using UnityEngine;

public class TileQueueUtils
{
    public static List<Tile> GenerateSameTiles()
    {
        List<Tile> tiles = new List<Tile>();
        for (int i = 0; i < 148; i++)
        {
            Tile bambooTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
            bambooTile.SetTileType(TileTypes.BAMBOO);
            bambooTile.SetValue(1);
            tiles.Add(bambooTile);
        }
        return tiles;
    }
}