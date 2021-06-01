using NUnit.Framework;

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
}
