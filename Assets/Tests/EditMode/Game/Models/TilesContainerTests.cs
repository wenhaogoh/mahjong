using NUnit.Framework;
using UnityEngine;

public class TilesContainerTests
{
    [Test]
    public void AddTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tilesContainer.AddTile(tile);
        Assert.AreEqual(tile, tilesContainer.GetTiles()[0]);
    }
    [Test]
    public void AddTiles()
    {
        TilesContainer tilesContainer1 = new TilesContainer();
        TilesContainer tilesContainer2 = new TilesContainer();
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tilesContainer1.AddTile(tile1);
        tilesContainer2.AddTile(tile2);
        tilesContainer1.AddTiles(tilesContainer1);
        Assert.AreEqual(2, tilesContainer1.Count());
        Assert.AreEqual(tile1, tilesContainer1.GetTiles()[0]);
        Assert.AreEqual(tile2, tilesContainer1.GetTiles()[1]);
    }
    [Test]
    public void RemoveTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(tile2, tilesContainer.RemoveTile(1));
    }
    [Test]
    public void RemoveTiles()
    {
        TilesContainer tilesContainer1 = new TilesContainer();
        TilesContainer tilesContainer2 = new TilesContainer();
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
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
        Tile largerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile smallerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        largerTile.SetTileType(TileTypes.FLOWER);
        smallerTile.SetTileType(TileTypes.BAMBOO);
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
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(tile2, tilesContainer.GetLastTile());
    }
    [Test]
    public void RemoveLastTile()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(tile2, tilesContainer.RemoveLastTile());
        Assert.AreEqual(1, tilesContainer.Count());
    }
    [Test]
    public void Count()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tilesContainer.AddTile(tile1);
        tilesContainer.AddTile(tile2);
        Assert.AreEqual(2, tilesContainer.Count());
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_NoActions()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile drawnTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        drawnTile.SetTileType(TileTypes.BAMBOO);
        drawnTile.SetValue(1);
        Assert.AreEqual(0, tilesContainer.GetPossibleTileActionsFromDrawnTile(drawnTile).Count);
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_Kong()
    {
        TilesContainer tilesContainer = new TilesContainer();
        Tile drawnTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        drawnTile.SetTileType(TileTypes.BAMBOO);
        drawnTile.SetValue(1);
        for (int i = 0; i < 3; i++)
        {
            Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
            tile.SetTileType(TileTypes.BAMBOO);
            tile.SetValue(1);
            tilesContainer.AddTile(tile);
        }
        Assert.AreEqual(1, tilesContainer.GetPossibleTileActionsFromDrawnTile(drawnTile).Count);
        Assert.AreEqual(TileActionTypes.KONG, tilesContainer.GetPossibleTileActionsFromDrawnTile(drawnTile)[0].GetTileActionType());
    }
}
