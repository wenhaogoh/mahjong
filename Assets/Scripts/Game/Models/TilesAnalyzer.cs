using System.Collections.Generic;
using System.Linq;
using System;

public class TilesAnalyzer
{
    public static int GetAutoDiscardTileIndex(List<Tile> mainTiles, Dictionary<Tile, int> unavailableTiles)
    {
        return 0;
    }
    public static TileAction AutoSelectTileAction(List<TileAction> tileActions, List<Tile> mainTiles, Dictionary<Tile, int> unavailableTiles)
    {
        return null;
    }
    public static List<TileAction> GetPossibleTileActionsFromDrawnTile(List<Tile> mainTiles, Tile drawnTile)
    {
        List<TileAction> actions = new List<TileAction>();
        TileAction huAction = GetHuAction(mainTiles, drawnTile);
        if (huAction != null)
        {
            actions.Add(huAction);
        }
        TileAction kongAction = GetKongAction(mainTiles, drawnTile);
        if (kongAction != null)
        {
            actions.Add(kongAction);
        }
        return actions;
    }
    public static List<TileAction> GetPossibleTileActionsFromOfferedTile(List<Tile> mainTiles, Tile offeredTile, bool isFromPreviousPlayer)
    {
        List<TileAction> actions = new List<TileAction>();
        TileAction huAction = GetHuAction(mainTiles, offeredTile);
        if (huAction != null)
        {
            actions.Add(huAction);
        }
        TileAction kongAction = GetKongAction(mainTiles, offeredTile);
        if (kongAction != null)
        {
            actions.Add(kongAction);
        }
        TileAction pongAction = GetPongAction(mainTiles, offeredTile);
        if (pongAction != null)
        {
            actions.Add(pongAction);
        }
        if (isFromPreviousPlayer)
        {
            List<TileAction> chowActions = GetChowActions(mainTiles, offeredTile);
            if (chowActions != null)
            {
                actions.AddRange(chowActions);
            }
        }
        return actions;
    }
    private static List<Tile> GetWaitingTiles(List<Tile> mainTiles)
    {
        List<Tile> waitingTiles = new List<Tile>();
        foreach (TileTypes tileType in TileTypes.GetValues(typeof(TileTypes)))
        {
            switch (tileType)
            {
                case TileTypes.BAMBOO:
                case TileTypes.CHARACTER:
                case TileTypes.DOT:
                    for (int i = 1; i <= 9; i++)
                    {
                        List<Tile> tilesClone = new List<Tile>(mainTiles);
                        Tile tile = TileUtils.GetTile(tileType, i);
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
                        List<Tile> tilesClone = new List<Tile>(mainTiles);
                        Tile tile = TileUtils.GetTile(tileType, (int)honourType);
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
    private static TileAction GetHuAction(List<Tile> mainTiles, Tile triggerTile)
    {
        List<Tile> waitingTiles = GetWaitingTiles(mainTiles); // TODO: Optimise + change location (eg. get waiting tiles after discarding tile)
        if (waitingTiles.Contains(triggerTile))
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(triggerTile);
            return new TileAction(TileActionTypes.HU, actionTiles, triggerTile);
        }
        return null;
    }
    private static bool CanHu(List<Tile> tiles)
    {
        Dictionary<int, int> initDict = new Dictionary<int, int>();
        for (int i = 0; i <= 9; i++)
        {
            initDict.Add(i, 0);
        }
        Dictionary<int, int> bambooTiles = new Dictionary<int, int>(initDict);
        Dictionary<int, int> characterTiles = new Dictionary<int, int>(initDict);
        Dictionary<int, int> dotTiles = new Dictionary<int, int>(initDict);
        Dictionary<int, int> honourTiles = new Dictionary<int, int>(initDict);
        int numBambooTiles = 0;
        int numCharacterTiles = 0;
        int numDotTiles = 0;
        int numHonourTiles = 0;
        foreach (Tile tile in tiles)
        {
            int tileValue = tile.GetValue();
            switch (tile.GetTileType())
            {
                case TileTypes.BAMBOO:
                    bambooTiles[tileValue]++;
                    numBambooTiles++;
                    break;
                case TileTypes.CHARACTER:
                    characterTiles[tileValue]++;
                    numCharacterTiles++;
                    break;
                case TileTypes.DOT:
                    dotTiles[tileValue]++;
                    numDotTiles++;
                    break;
                case TileTypes.HONOUR:
                    honourTiles[tileValue]++;
                    numHonourTiles++;
                    break;
                default:
                    break;
            }
        }
        bool canBambooHu = CanHuHelper(bambooTiles, numBambooTiles, false, false);
        bool canCharacterHu = CanHuHelper(characterTiles, numCharacterTiles, false, false);
        bool canDotHu = CanHuHelper(dotTiles, numDotTiles, false, false);
        bool canHonourHu = CanHuHelper(honourTiles, numHonourTiles, true, false);
        return (canBambooHu && canCharacterHu && canDotHu && canHonourHu &&
            (numBambooTiles % 3 + numCharacterTiles % 3 + numDotTiles % 3 + numHonourTiles % 3 == 2));
    }
    private static bool CanHuHelper(Dictionary<int, int> tilesDict, int numTilesLeft, bool isHonourType, bool hasEye)
    {
        if (numTilesLeft == 0)
        {
            return true;
        }
        if (numTilesLeft % 3 == 1)
        {
            return false;
        }
        bool canHuByRemovingEye = false;
        bool canHuByRemovingPong = false;
        bool canHuByRemovingChow = false;
        int firstValue = GetFirstValue(tilesDict);
        if (!hasEye && tilesDict[firstValue] >= 2)
        {
            Dictionary<int, int> tilesDictCopy = new Dictionary<int, int>(tilesDict);
            tilesDictCopy[firstValue] -= 2;
            canHuByRemovingEye = CanHuHelper(tilesDictCopy, numTilesLeft - 2, isHonourType, true);
        }
        if (tilesDict[firstValue] >= 3)
        {
            Dictionary<int, int> tilesDictCopy = new Dictionary<int, int>(tilesDict);
            tilesDictCopy[firstValue] -= 3;
            canHuByRemovingPong = CanHuHelper(tilesDictCopy, numTilesLeft - 3, isHonourType, hasEye);
        }
        int nextId1 = firstValue + 1;
        int nextId2 = firstValue + 2;
        if (!isHonourType && firstValue <= 7 && tilesDict[nextId1] != 0 && tilesDict[nextId2] != 0)
        {
            Dictionary<int, int> tilesDictCopy = new Dictionary<int, int>(tilesDict);
            tilesDictCopy[firstValue]--;
            tilesDictCopy[nextId1]--;
            tilesDictCopy[nextId2]--;
            canHuByRemovingChow = CanHuHelper(tilesDictCopy, numTilesLeft - 3, isHonourType, hasEye);
        }
        return canHuByRemovingEye || canHuByRemovingPong || canHuByRemovingChow;
    }
    private static int GetFirstValue(Dictionary<int, int> tilesDict)
    {
        for (int i = 0; i <= 9; i++)
        {
            if (tilesDict[i] != 0)
            {
                return i;
            }
        }
        throw new Exception("Something went wrong!");
    }
    private static TileAction GetKongAction(List<Tile> mainTiles, Tile triggerTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(triggerTile);
        foreach (Tile tile in mainTiles)
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
    private static TileAction GetPongAction(List<Tile> mainTiles, Tile triggerTile)
    {
        TilesContainer actionTiles = new TilesContainer();
        actionTiles.AddTile(triggerTile);
        foreach (Tile tile in mainTiles)
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
    private static List<TileAction> GetChowActions(List<Tile> mainTiles, Tile triggerTile)
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
            partners[i] = TileUtils.GetTile(triggerTileType, triggerTile.GetValue() + i - 2);
        }
        if (mainTiles.Contains(partners[0]) && mainTiles.Contains(partners[1])) // CHOW Sequence: X X N
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(partners[0]);
            actionTiles.AddTile(partners[1]);
            actionTiles.AddTile(triggerTile);
            chowActions.Add(new TileAction(TileActionTypes.CHOW, actionTiles, triggerTile));
        }
        if (mainTiles.Contains(partners[1]) && mainTiles.Contains(partners[3])) // CHOW Sequence: X N X
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(partners[1]);
            actionTiles.AddTile(triggerTile);
            actionTiles.AddTile(partners[3]);
            chowActions.Add(new TileAction(TileActionTypes.CHOW, actionTiles, triggerTile));
        }
        if (mainTiles.Contains(partners[3]) && mainTiles.Contains(partners[4])) // CHOW Sequence: N X X
        {
            TilesContainer actionTiles = new TilesContainer();
            actionTiles.AddTile(triggerTile);
            actionTiles.AddTile(partners[3]);
            actionTiles.AddTile(partners[4]);
            chowActions.Add(new TileAction(TileActionTypes.CHOW, actionTiles, triggerTile));
        }
        return chowActions.Any() ? chowActions : null;
    }
}