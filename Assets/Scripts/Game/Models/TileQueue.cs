using Nito.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TileQueue
{
    private Deque<Tile> deque;
    public TileQueue()
    {
        deque = new Deque<Tile>(GenerateTiles());
    }
    public Tile DrawFromFront()
    {
        return deque.RemoveFromFront();
    }
    public Tile DrawFromBack()
    {
        return deque.RemoveFromBack();
    }
    private List<Tile> GenerateTiles()
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
    public void Randomize()
    {
        RandomNumberGenerator.Shuffle<Tile>(deque);
    }
    public int Count()
    {
        return deque.Count;
    }
}