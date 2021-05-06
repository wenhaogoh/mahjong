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
    public void RemoveTiles(TilesContainer tiles)
    {
        foreach (Tile tile in tiles.GetTiles())
        {
            this.tiles.Remove(tile);
        }
    }
    public void Sort()
    {
        tiles.Sort();
    }
    public Tile GetLastTile()
    {
        return tiles[tiles.Count - 1];
    }
    public Tile RemoveLastTile()
    {
        Tile lastTile = GetLastTile();
        tiles.RemoveAt(tiles.Count - 1);
        return lastTile;
    }
    public List<TileAction> GetPossibleActionsFromDrawnTile(Tile drawnTile)
    {
        List<TileAction> actions = new List<TileAction>();
        TileAction kongAction = GetKongAction(drawnTile);
        if (kongAction != null)
        {
            actions.Add(kongAction);
        }
        return actions;
    }
    public int Count()
    {
        return tiles.Count();
    }
    private TileAction GetPongAction(Tile drawnTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(drawnTile);
        foreach (Tile tile in tiles)
        {
            if (tile.Equals(drawnTile))
            {
                actionTiles.AddTile(tile);
                if (actionTiles.Count() == 3)
                {
                    return new TileAction(TileActionTypes.PONG, actionTiles);
                }
            }
        }
        return null;
    }
    private TileAction GetKongAction(Tile drawnTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(drawnTile);
        foreach (Tile tile in tiles)
        {
            if (tile.Equals(drawnTile))
            {
                actionTiles.AddTile(tile);
                if (actionTiles.Count() == 4)
                {
                    return new TileAction(TileActionTypes.KONG, actionTiles);
                }
            }
        }
        return null;
    }
}