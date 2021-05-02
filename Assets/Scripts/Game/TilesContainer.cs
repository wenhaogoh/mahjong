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
    public List<Tile> GetTiles()
    {
        return tiles;
    }
    public Tile RemoveTile(int index)
    {
        Tile toRemove = tiles[index];
        tiles.Remove(toRemove);
        return toRemove;
    }
    public void Sort()
    {
        tiles.Sort();
    }
}