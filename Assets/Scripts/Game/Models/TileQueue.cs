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
                case TileTypes.Bamboo:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Tile bambooTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            bambooTile.SetTileType(TileTypes.Bamboo);
                            bambooTile.SetValue(i);
                            tiles.Add(bambooTile);
                        }
                    }
                    break;
                case TileTypes.Character:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Tile characterTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            characterTile.SetTileType(TileTypes.Character);
                            characterTile.SetValue(i);
                            tiles.Add(characterTile);
                        }
                    }
                    break;
                case TileTypes.Dot:
                    for (int i = 1; i <= 9; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Tile dotTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                            dotTile.SetTileType(TileTypes.Dot);
                            dotTile.SetValue(i);
                            tiles.Add(dotTile);
                        }
                    }
                    break;
                case TileTypes.Flower:
                    for (int i = 1; i <= 12; i++) // Cat + Mouse + Rooster + Centipede + 2 * 4 Flowers
                    {
                        Tile flowerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        flowerTile.SetTileType(TileTypes.Flower);
                        flowerTile.SetValue(i);
                        tiles.Add(flowerTile);
                    }
                    break;
                case TileTypes.Honour:
                    for (int i = 1; i <= 28; i++) // 4 * White Dragon + 4 * Red Dragon + 4 * Green Dragon + 4 * 4 Winds
                    {
                        Tile honourTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        honourTile.SetTileType(TileTypes.Honour);
                        honourTile.SetValue(i);
                        tiles.Add(honourTile);
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