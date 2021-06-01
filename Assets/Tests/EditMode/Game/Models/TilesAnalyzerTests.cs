using System.Collections.Generic;
using NUnit.Framework;

public class TilesAnalyzerTests
{
    [Test]
    public void GetPossibleActionsFromDrawnTile_NoActions()
    {
        List<Tile> mainTiles = new List<Tile>();
        List<TileAction> actions = TilesAnalyzer.GetPossibleTileActionsFromDrawnTile(mainTiles, TileUtils.GetRedDragonTile());
        Assert.AreEqual(0, actions.Count);
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_Kong()
    {
        List<Tile> mainTiles = new List<Tile>();
        Tile drawnTile = TileUtils.GetRedDragonTile();
        for (int i = 0; i < 3; i++)
        {
            Tile tile = TileUtils.GetRedDragonTile();
            mainTiles.Add(tile);
        }
        List<TileAction> actions = TilesAnalyzer.GetPossibleTileActionsFromDrawnTile(mainTiles, drawnTile);
        Assert.AreEqual(1, actions.Count);
        Assert.AreEqual(TileActionTypes.KONG, actions[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Chow_HonourTile()
    {
        List<Tile> mainTiles = new List<Tile>();
        Tile offeredTile = TileUtils.GetRedDragonTile();
        for (int i = 1; i < 9; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            mainTiles.Add(tile);
        }
        List<TileAction> actions = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, offeredTile, true);
        Assert.AreEqual(0, actions.Count);
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Chow_NonhonourTile()
    {
        List<Tile> mainTiles = new List<Tile>();
        Tile offeredTile = TileUtils.GetTile(TileTypes.BAMBOO, 3);
        for (int i = 1; i < 9; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            mainTiles.Add(tile);
        }
        List<TileAction> actions = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, offeredTile, true);
        Assert.AreEqual(3, actions.Count);
        Assert.AreEqual(TileActionTypes.CHOW, actions[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Hu_OnHonourPong()
    {
        List<Tile> mainTiles = new List<Tile>();
        for (int i = 1; i <= 9; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            mainTiles.Add(tile);
        }
        for (int i = 0; i < 2; i++)
        {
            mainTiles.Add(TileUtils.GetGreenDragonTile());
            mainTiles.Add(TileUtils.GetRedDragonTile());
        }
        List<TileAction> actions1 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, TileUtils.GetGreenDragonTile(), true);
        List<TileAction> actions2 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, TileUtils.GetRedDragonTile(), true);
        Assert.AreEqual(2, actions1.Count);
        Assert.AreEqual(2, actions2.Count);
        Assert.AreEqual(TileActionTypes.HU, actions1[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, actions2[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Hu_OnNonHonourChow()
    {
        List<Tile> mainTiles = new List<Tile>();
        for (int i = 4; i <= 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
                mainTiles.Add(tile);
            }
        }
        for (int i = 7; i <= 8; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            mainTiles.Add(tile);
        }
        for (int i = 0; i < 2; i++)
        {
            Tile tile = TileUtils.GetRedDragonTile();
            mainTiles.Add(tile);
        }
        Tile bamboo6Tile = TileUtils.GetTile(TileTypes.BAMBOO, 6);
        Tile bamboo9Tile = TileUtils.GetTile(TileTypes.BAMBOO, 9);
        List<TileAction> actions1 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, bamboo6Tile, true);
        List<TileAction> actions2 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, bamboo9Tile, true);
        Assert.AreEqual(6, actions1.Count);
        Assert.AreEqual(2, actions2.Count);
        Assert.AreEqual(TileActionTypes.HU, actions1[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, actions2[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_Hu_OnNonHonourEyes()
    {
        List<Tile> mainTiles = new List<Tile>();
        for (int i = 1; i <= 7; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            mainTiles.Add(tile);
        }
        Tile bamboo1Tile = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile bamboo4Tile = TileUtils.GetTile(TileTypes.BAMBOO, 4);
        Tile bamboo7Tile = TileUtils.GetTile(TileTypes.BAMBOO, 7);
        List<TileAction> actions1 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, bamboo1Tile, true);
        List<TileAction> actions2 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, bamboo4Tile, true);
        List<TileAction> actions3 = TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles, bamboo7Tile, true);
        Assert.AreEqual(TileActionTypes.HU, actions1[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, actions2[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, actions3[0].GetTileActionType());
    }
}
