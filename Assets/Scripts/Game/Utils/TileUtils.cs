using UnityEngine;
using System.Collections.Generic;

public class TileUtils
{
    public static Tile GetRedDragonTile()
    {
        return GetTile(TileTypes.HONOUR, (int)HonourTypes.RED_DRAGON);
    }
    public static Tile GetTile(TileTypes tileType, int value)
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile.SetTileType(tileType);
        tile.SetValue(value);
        return tile;
    }
    public static List<Tile> GenerateTiles()
    {
        List<Tile> tiles = new List<Tile>();

        foreach (TileTypes tileType in TileTypes.GetValues(typeof(TileTypes)))
        {
            switch (tileType)
            {
                case TileTypes.BAMBOO:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            tiles.Add(GetTile(tileType, i));
                        }
                    }
                    break;
                case TileTypes.CHARACTER:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            tiles.Add(GetTile(tileType, i));
                        }
                    }
                    break;
                case TileTypes.DOT:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            tiles.Add(GetTile(tileType, i));
                        }
                    }
                    break;
                case TileTypes.FLOWER:
                    for (int i = 1; i <= 12; i++) // Cat + Mouse + Rooster + Centipede + 2 * 4 Flowers
                    {
                        tiles.Add(GetTile(tileType, i));
                    }
                    break;
                case TileTypes.HONOUR:
                    foreach (HonourTypes honourType in HonourTypes.GetValues(typeof(HonourTypes))) // 4 * White Dragon + 4 * Red Dragon + 4 * Green Dragon + 4 * 4 Winds
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            tiles.Add(GetTile(tileType, (int)honourType));
                        }
                    }
                    break;
                default:
                    Debug.LogWarning("Reached default case when generating tiles!");
                    break;
            }
        }

        return tiles;
    }
    public static List<Tile> GenerateSameTiles()
    {
        List<Tile> tiles = new List<Tile>();
        for (int i = 0; i < 148; i++)
        {
            tiles.Add(GetRedDragonTile());
        }
        return tiles;
    }
    public static List<Tile> GenerateTilesForChow()
    {
        List<Tile> tiles = new List<Tile>();
        for (int i = 0; i < 148; i++)
        {
            tiles.Add(TileUtils.GetTile(TileTypes.BAMBOO, Mathf.FloorToInt(i * 3 / 148) + 1));
        }
        return tiles;
    }
}