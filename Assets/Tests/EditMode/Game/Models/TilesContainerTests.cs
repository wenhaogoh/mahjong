using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TilesContainerTests
{
    [Test]
    public void AddTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile = TileUtils.GetRedDragonTile();
        tilesContainer.AddTile(tile);
        Assert.AreEqual(tile, tilesContainer.GetTiles()[0]);
    }
    [Test]
    public void AddTiles()
    {
        TilesContainer tilesContainer1 = new TilesContainer();
        TilesContainer tilesContainer2 = new TilesContainer();
        Tile tile1 = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile tile2 = TileUtils.GetRedDragonTile();
        tilesContainer1.AddTile(tile1);
        tilesContainer2.AddTile(tile2);
        tilesContainer1.AddTiles(tilesContainer2);
        Assert.AreEqual(2, tilesContainer1.Count());
        Assert.AreEqual(tile1, tilesContainer1.GetTiles()[0]);
        Assert.AreEqual(tile2, tilesContainer1.GetTiles()[1]);
    }
    [Test]
    public void RemoveTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile tile2 = TileUtils.GetRedDragonTile();
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(tile2, tilesContainer.RemoveTile(1));
    }
    [Test]
    public void RemoveTiles()
    {
        TilesContainer tilesContainer1 = new TilesContainer();
        TilesContainer tilesContainer2 = new TilesContainer();
        Tile tile1 = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile tile2 = TileUtils.GetRedDragonTile();
        tilesContainer1.AddTile(tile1);
        tilesContainer1.AddTile(tile2);
        tilesContainer2.AddTile(tile1);
        tilesContainer1.RemoveTiles(tilesContainer2);
        Assert.AreEqual(1, tilesContainer1.Count());
        Assert.AreEqual(tile2, tilesContainer1.GetTiles()[0]);
    }
    [Test]
    public void Sort()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile largerTile = TileUtils.GetTile(TileTypes.FLOWER, 1);
        Tile smallerTile = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        tilesContainer.AddTile(largerTile);
        tilesContainer.AddTile(smallerTile);
        tilesContainer.Sort();
        Assert.AreEqual(smallerTile, tilesContainer.GetTiles()[0]);
        Assert.AreEqual(largerTile, tilesContainer.GetTiles()[1]);
    }
    [Test]
    public void GetLastTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile tile2 = TileUtils.GetRedDragonTile();
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(tile2, tilesContainer.GetLastTile());
    }
    [Test]
    public void RemoveLastTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile tile2 = TileUtils.GetRedDragonTile();
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(tile2, tilesContainer.RemoveLastTile());
        Assert.AreEqual(1, tilesContainer.Count());
    }
    [Test]
    public void Count()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile tile2 = TileUtils.GetRedDragonTile();
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(2, tilesContainer.Count());
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_NoActions()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile drawnTile = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Assert.AreEqual(0, tilesContainer.GetPossibleTileActionsFromDrawnTile(drawnTile).Count);
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_Kong()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile drawnTile = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        for (int i = 0; i < 3; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, 1);
            tilesContainer.AddTile(tile);
        }
        Assert.AreEqual(1, tilesContainer.GetPossibleTileActionsFromDrawnTile(drawnTile).Count);
        Assert.AreEqual(TileActionTypes.KONG, tilesContainer.GetPossibleTileActionsFromDrawnTile(drawnTile)[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Chow_HonourTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile offeredTile = TileUtils.GetTile(TileTypes.HONOUR, (int)HonourTypes.EAST);
        for (int i = 1; i < 9; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            tilesContainer.AddTile(tile);
        }
        Assert.AreEqual(0, tilesContainer.GetPossibleTileActionsFromOfferedTile(offeredTile, true).Count);
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Chow_NonhonourTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile offeredTile = TileUtils.GetTile(TileTypes.BAMBOO, 3);
        for (int i = 1; i < 9; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            tilesContainer.AddTile(tile);
        }
        Assert.AreEqual(3, tilesContainer.GetPossibleTileActionsFromOfferedTile(offeredTile, true).Count);
        Assert.AreEqual(TileActionTypes.CHOW, tilesContainer.GetPossibleTileActionsFromOfferedTile(offeredTile, true)[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Hu_OnHonourPong()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile greenDragonTile = TileUtils.GetTile(TileTypes.HONOUR, (int)HonourTypes.GREEN_DRAGON);
        Tile redDragonTile = TileUtils.GetRedDragonTile();
        for (int i = 1; i <= 9; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            tilesContainer.AddTile(tile);
        }
        for (int i = 0; i < 2; i++)
        {
            tilesContainer.AddTile(greenDragonTile);
            tilesContainer.AddTile(redDragonTile);
        }
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromOfferedTile(redDragonTile, true)[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromOfferedTile(greenDragonTile, true)[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_Hu_OnNonHonourChow()
    {
        TilesContainer tilesContainer = new TilesContainer();
        for (int i = 4; i <= 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
                tilesContainer.AddTile(tile);
            }
        }
        for (int i = 7; i <= 8; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            tilesContainer.AddTile(tile);
        }
        for (int i = 0; i < 2; i++)
        {
            Tile redDragonTile = TileUtils.GetRedDragonTile();
            tilesContainer.AddTile(redDragonTile);
        }
        Tile bamboo6Tile = TileUtils.GetTile(TileTypes.BAMBOO, 6);
        Tile bamboo9Tile = TileUtils.GetTile(TileTypes.BAMBOO, 9);
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromOfferedTile(bamboo6Tile, true)[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromOfferedTile(bamboo9Tile, true)[0].GetTileActionType());
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_Hu_OnNonHonourEyes()
    {
        TilesContainer tilesContainer = new TilesContainer();
        for (int i = 1; i <= 7; i++)
        {
            Tile tile = TileUtils.GetTile(TileTypes.BAMBOO, i);
            tilesContainer.AddTile(tile);
        }
        Tile bamboo1Tile = TileUtils.GetTile(TileTypes.BAMBOO, 1);
        Tile bamboo4Tile = TileUtils.GetTile(TileTypes.BAMBOO, 4);
        Tile bamboo7Tile = TileUtils.GetTile(TileTypes.BAMBOO, 7);
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromDrawnTile(bamboo1Tile)[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromDrawnTile(bamboo4Tile)[0].GetTileActionType());
        Assert.AreEqual(TileActionTypes.HU, tilesContainer.GetPossibleTileActionsFromDrawnTile(bamboo7Tile)[0].GetTileActionType());
    }
}
