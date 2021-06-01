using NUnit.Framework;
using UnityEngine;

public class TileTests
{
    private const TileTypes BAMBOO_TILE_TYPE = TileTypes.BAMBOO;
    private const TileTypes FLOWER_TILE_TYPE = TileTypes.FLOWER;
    private const int INTEGER_TEN = 10;
    private const int INTEGER_TWENTY = 20;
    [Test]
    public void Tile()
    {
        Tile tile = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TEN);
        Assert.AreEqual(BAMBOO_TILE_TYPE, tile.GetTileType());
        Assert.AreEqual(INTEGER_TEN, tile.GetValue());
    }
    [Test]
    public void IsFlower()
    {
        Tile tile = TileUtils.GetTile(FLOWER_TILE_TYPE, INTEGER_TEN);
        Assert.True(tile.IsFlower());
    }
    [Test]
    public void CompareTo_SameObject()
    {
        Tile tile = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TEN);
        Assert.AreEqual(0, tile.CompareTo(tile));
    }
    [Test]
    public void CompareTo_DifferentTileType()
    {
        Tile smallerTile = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TEN);
        Tile largerTile = TileUtils.GetTile(FLOWER_TILE_TYPE, INTEGER_TEN);
        Assert.AreEqual(-1, smallerTile.CompareTo(largerTile));
        Assert.AreEqual(1, largerTile.CompareTo(smallerTile));
    }
    [Test]
    public void CompareTo_SameTileType_DifferentValue()
    {
        Tile smallerTile = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TEN);
        Tile largerTile = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TWENTY);
        Assert.AreEqual(-1, smallerTile.CompareTo(largerTile));
        Assert.AreEqual(1, largerTile.CompareTo(smallerTile));
    }
    [Test]
    public void CompareTo_SameTileType_SameValue()
    {
        Tile tile1 = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TEN);
        Tile tile2 = TileUtils.GetTile(BAMBOO_TILE_TYPE, INTEGER_TEN);
        Assert.AreEqual(0, tile1.CompareTo(tile2));
        Assert.AreEqual(0, tile2.CompareTo(tile1));
    }
    [Test]
    public void Equals_Null()
    {
        Tile tile = TileUtils.GetRedDragonTile();
        Assert.False(tile.Equals(null));
    }
    [Test]
    public void Equals_NonTile()
    {
        Tile tile = TileUtils.GetRedDragonTile();
        Assert.False(tile.Equals(INTEGER_TEN));
    }
    [Test]
    public void Equals_SameObject()
    {
        Tile tile = TileUtils.GetRedDragonTile();
        Assert.True(tile.GetHashCode() == tile.GetHashCode());
    }
    [Test]
    public void GetHashCode_DifferentObjects()
    {
        Tile tile1 = TileUtils.GetRedDragonTile();
        Tile tile2 = TileUtils.GetRedDragonTile();
        Assert.True(tile1.GetHashCode() == tile2.GetHashCode());
    }
}
