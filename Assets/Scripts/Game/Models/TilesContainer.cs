using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            if (this.tiles.Contains(tile))
            {
                this.tiles.Remove(tile);
            }
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
    public int Count()
    {
        return tiles.Count();
    }
    public List<TileAction> GetPossibleTileActionsFromDrawnTile(Tile drawnTile)
    {
        List<TileAction> actions = new List<TileAction>();
        TileAction huAction = GetHuAction(drawnTile);
        if (huAction != null)
        {
            actions.Add(huAction);
        }
        TileAction kongAction = GetKongAction(drawnTile);
        if (kongAction != null)
        {
            actions.Add(kongAction);
        }
        return actions;
    }
    public List<TileAction> GetPossibleTileActionsFromOfferedTile(Tile offeredTile, bool isFromPreviousPlayer)
    {
        List<TileAction> actions = new List<TileAction>();
        if (isFromPreviousPlayer)
        {
            // Add GetChowAction here
        }
        TileAction huAction = GetHuAction(offeredTile);
        if (huAction != null)
        {
            actions.Add(huAction);
        }
        TileAction kongAction = GetKongAction(offeredTile);
        if (kongAction != null)
        {
            actions.Add(kongAction);
        }
        // Add GetPongAction here
        return actions;
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
    private TileAction GetKongAction(Tile newTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(newTile);
        foreach (Tile tile in tiles)
        {
            if (tile.Equals(newTile))
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
    private TileAction GetHuAction(Tile newTile)
    {
        List<Tile> waitingTiles = GetWaitingTiles(); // TODO: Optimise + change location (eg. get waiting tiles after discarding tile)
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(newTile);
        if (waitingTiles.Contains(newTile))
        {
            return new TileAction(TileActionTypes.HU, actionTiles);
        }
        return null;
    }
    public List<Tile> GetWaitingTiles()
    {
        List<Tile> waitingTiles = new List<Tile>();
        foreach (TileTypes tileType in TileTypes.GetValues(typeof(TileTypes)))
        {
            switch (tileType)
            {
                case TileTypes.BAMBOO:
                    for (int i = 1; i <= 9; i++)
                    {
                        List<Tile> tilesClone = new List<Tile>(tiles);
                        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        tile.SetTileType(tileType);
                        tile.SetValue(i);
                        tilesClone.Add(tile);
                        if (CanHu(tilesClone))
                        {
                            waitingTiles.Add(tile);
                        }
                    }
                    break;
                case TileTypes.CHARACTER:
                    for (int i = 1; i <= 9; i++)
                    {
                        List<Tile> tilesClone = new List<Tile>(tiles);
                        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        tile.SetTileType(tileType);
                        tile.SetValue(i);
                        tilesClone.Add(tile);
                        if (CanHu(tilesClone))
                        {
                            waitingTiles.Add(tile);
                        }
                    }
                    break;
                case TileTypes.DOT:
                    for (int i = 1; i <= 9; i++)
                    {
                        List<Tile> tilesClone = new List<Tile>(tiles);
                        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        tile.SetTileType(tileType);
                        tile.SetValue(i);
                        tilesClone.Add(tile);
                        if (CanHu(tilesClone))
                        {
                            waitingTiles.Add(tile);
                        }
                    }
                    break;
                case TileTypes.HONOUR:
                    foreach (HonourTypes honourType in HonourTypes.GetValues(typeof(HonourTypes)))
                    {
                        List<Tile> tilesClone = new List<Tile>(tiles);
                        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                        tile.SetTileType(tileType);
                        tile.SetValue((int)honourType);
                        tilesClone.Add(tile);
                        if (CanHu(tilesClone))
                        {
                            waitingTiles.Add(tile);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        return waitingTiles;
    }
    private bool CanHu(List<Tile> tiles)
    {
        tiles.Sort();
        return CanHuHelper(tiles, false);
    }
    private bool CanHuHelper(List<Tile> tiles, bool hasEye)
    {
        if (tiles.Count == 0)
        {
            return hasEye;
        }
        if (tiles.Count == 1)
        {
            return false; // Should never enter this condition block
        }
        if (tiles.Count == 2)
        {
            if (hasEye)
            {
                return false;
            }
            return tiles[0].Equals(tiles[1]);
        }
        bool canHuByRemovingEye = false;
        bool canHuByRemovingTriplet = false;
        bool canHuByRemovingConsecutiveTrio = false;
        Tile firstTile = tiles[0];
        if (!hasEye)
        {
            if (firstTile.Equals(tiles[1]))
            {
                List<Tile> tilesCopy = new List<Tile>(tiles);
                tilesCopy.RemoveRange(0, 2);
                canHuByRemovingEye = CanHuHelper(tilesCopy, true);
            }
        }
        if (tiles.Count(tile => firstTile.Equals(tile)) >= 3)
        {
            List<Tile> tilesCopy = new List<Tile>(tiles);
            tilesCopy.RemoveRange(0, 3);
            canHuByRemovingTriplet = CanHuHelper(tilesCopy, hasEye);
        }
        int index1 = tiles.FindIndex(tile => firstTile.GetTileType() == tile.GetTileType()
                                                && firstTile.GetValue() + 1 == tile.GetValue());
        int index2 = tiles.FindIndex(tile => firstTile.GetTileType() == tile.GetTileType()
                                                && firstTile.GetValue() + 2 == tile.GetValue());
        if (index1 != -1 && index2 != -1)
        {
            List<Tile> tilesCopy = new List<Tile>(tiles);
            tilesCopy.RemoveAt(0);
            tilesCopy.RemoveAt(index1);
            tilesCopy.RemoveAt(index2);
            canHuByRemovingConsecutiveTrio = CanHuHelper(tilesCopy, hasEye);
        }
        return canHuByRemovingEye || canHuByRemovingTriplet || canHuByRemovingConsecutiveTrio;
    }
}