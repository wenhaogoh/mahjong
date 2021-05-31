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
            List<TileAction> chowActions = GetChowActions(offeredTile);
            if (chowActions != null)
            {
                actions.AddRange(chowActions);
            }
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
        TileAction pongAction = GetPongAction(offeredTile);
        if (pongAction != null)
        {
            actions.Add(pongAction);
        }
        return actions;
    }
    private List<TileAction> GetChowActions(Tile triggerTile)
    {
        if (triggerTile.GetTileType() == TileTypes.HONOUR)
        {
            return null;
        }
        List<TileAction> chowActions = new List<TileAction>();
        TileTypes triggerTileType = triggerTile.GetTileType();

        Tile[] partners = new Tile[5]; // Find CHOW partners of newTile N: N-2, N-1, N (ignored), N+1, N+2 
        for (int i = 0; i < 5; i++)
        {
            partners[i] = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
            partners[i].SetTileType(triggerTileType);
            partners[i].SetValue(triggerTile.GetValue() + i - 2);
        }
        if (tiles.Contains(partners[0]) && tiles.Contains(partners[1])) // CHOW Sequence: X X N
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(partners[0]);
            actionTiles.AddTile(partners[1]);
            actionTiles.AddTile(triggerTile);
            chowActions.Add(new TileAction(TileActionTypes.CHOW, actionTiles, triggerTile));
        }
        if (tiles.Contains(partners[1]) && tiles.Contains(partners[3])) // CHOW Sequence: X N X
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(partners[1]);
            actionTiles.AddTile(triggerTile);
            actionTiles.AddTile(partners[3]);
            chowActions.Add(new TileAction(TileActionTypes.CHOW, actionTiles, triggerTile));
        }
        if (tiles.Contains(partners[3]) && tiles.Contains(partners[4])) // CHOW Sequence: N X X
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(triggerTile);
            actionTiles.AddTile(partners[3]);
            actionTiles.AddTile(partners[4]);
            chowActions.Add(new TileAction(TileActionTypes.CHOW, actionTiles, triggerTile));
        }

        return chowActions.Any() ? chowActions : null;
    }
    private TileAction GetPongAction(Tile triggerTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(triggerTile);
        foreach (Tile tile in tiles)
        {
            if (tile.Equals(triggerTile))
            {
                actionTiles.AddTile(tile);
                if (actionTiles.Count() == 3)
                {
                    return new TileAction(TileActionTypes.PONG, actionTiles, triggerTile);
                }
            }
        }
        return null;
    }
    private TileAction GetKongAction(Tile triggerTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(triggerTile);
        foreach (Tile tile in tiles)
        {
            if (tile.Equals(triggerTile))
            {
                actionTiles.AddTile(tile);
                if (actionTiles.Count() == 4)
                {
                    return new TileAction(TileActionTypes.KONG, actionTiles, triggerTile);
                }
            }
        }
        return null;
    }
    private TileAction GetHuAction(Tile triggerTile)
    {
        List<Tile> waitingTiles = GetWaitingTiles(); // TODO: Optimise + change location (eg. get waiting tiles after discarding tile)
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(triggerTile);
        if (waitingTiles.Contains(triggerTile))
        {
            return new TileAction(TileActionTypes.HU, actionTiles, triggerTile);
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
        return false; // TODO: Implement hu detection algorithm
    }
}