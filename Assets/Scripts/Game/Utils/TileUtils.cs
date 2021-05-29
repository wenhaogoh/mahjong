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
                            Tile bambooTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            bambooTile.SetTileType(tileType);
                            bambooTile.SetValue(i);
                            tiles.Add(bambooTile);
                        }
                    }
                    break;
                case TileTypes.CHARACTER:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Tile characterTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            characterTile.SetTileType(tileType);
                            characterTile.SetValue(i);
                            tiles.Add(characterTile);
                        }
                    }
                    break;
                case TileTypes.DOT:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Tile dotTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            dotTile.SetTileType(tileType);
                            dotTile.SetValue(i);
                            tiles.Add(dotTile);
                        }
                    }
                    break;
                case TileTypes.FLOWER:
                    for (int i = 1; i <= 12; i++) // Cat + Mouse + Rooster + Centipede + 2 * 4 Flowers
                    {
                        Tile flowerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        flowerTile.SetTileType(tileType);
                        flowerTile.SetValue(i);
                        tiles.Add(flowerTile);
                    }
                    break;
                case TileTypes.HONOUR:
                    foreach (HonourTypes honourType in HonourTypes.GetValues(typeof(HonourTypes))) // 4 * White Dragon + 4 * Red Dragon + 4 * Green Dragon + 4 * 4 Winds
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Tile honourTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            honourTile.SetTileType(tileType);
                            honourTile.SetValue((int)honourType);
                            tiles.Add(honourTile);
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
            Tile bambooTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
            bambooTile.SetTileType(TileTypes.BAMBOO);
            bambooTile.SetValue(1);
            tiles.Add(bambooTile);
        }
        return tiles;
    }
    public static List<Tile> GenerateTilesForChow()
    {
        List<Tile> tiles = new List<Tile>();
        for (int i = 0; i < 148; i++)
        {
            Tile bambooTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
            bambooTile.SetTileType(TileTypes.BAMBOO);
            bambooTile.SetValue(Mathf.FloorToInt(i * 9 / 148) + 1);
            tiles.Add(bambooTile);
        }
        return tiles;
    }
}