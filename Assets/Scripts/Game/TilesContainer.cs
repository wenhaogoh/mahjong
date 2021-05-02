using System.Collections.Generic;

public class TilesContainer
{
    private List<Tile> tiles;
    public TilesContainer()
    {
        this.tiles = new List<Tile>();
    }
    public void Add(Tile tile)
    {
        tiles.Add(tile);
    }
    public void Add(List<Tile> tiles)
    {
        tiles.AddRange(tiles);
    }
    public bool HasFlowers()
    {
        return tiles.Exists(tile => tile.IsFlower());
    }
    public List<Tile> RemoveFlowers()
    {
        List<Tile> flowers = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if (tile.IsFlower())
            {
                flowers.Add(tile);
            }
        }
        tiles.RemoveAll(tiles => tiles.IsFlower());
        return flowers;
    }
    public List<Tile> GetTiles()
    {
        return tiles;
    }
    public void Sort()
    {
        tiles.Sort();
    }
}