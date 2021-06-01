using System.Collections.Generic;
using System.Linq;

public class TilesContainer
{
    private List<Tile> tiles;
    public TilesContainer()
    {
        this.tiles = new List<Tile>();
    }
    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }
    public void AddTiles(TilesContainer tiles)
    {
        this.tiles.AddRange(tiles.GetTiles());
    }
    public Tile RemoveTile(int index)
    {
        Tile toRemove = tiles[index];
        tiles.Remove(toRemove);
        return toRemove;
    }
    public void RemoveTiles(TilesContainer tiles)
    {
        foreach (Tile tile in tiles.GetTiles())
        {
            this.tiles.Remove(tile);
        }
    }
    public Tile RemoveLastTile()
    {
        Tile lastTile = GetLastTile();
        tiles.RemoveAt(tiles.Count - 1);
        return lastTile;
    }
    public List<Tile> GetTiles()
    {
        return tiles;
    }
    public Tile GetLastTile()
    {
        return tiles[tiles.Count - 1];
    }
    public void Sort()
    {
        tiles.Sort();
    }
    public int Count()
    {
        return tiles.Count();
    }
}