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
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile.SetTileType(BAMBOO_TILE_TYPE);
        tile.SetValue(INTEGER_TEN);
        Assert.AreEqual(BAMBOO_TILE_TYPE, tile.GetTileType());
        Assert.AreEqual(INTEGER_TEN, tile.GetValue());
    }
    [Test]
    public void IsFlower()
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile.SetTileType(FLOWER_TILE_TYPE);
        tile.SetValue(INTEGER_TEN);
        Assert.True(tile.IsFlower());
    }
    [Test]
    public void CompareTo_SameObject()
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile.SetTileType(BAMBOO_TILE_TYPE);
        tile.SetValue(INTEGER_TEN);
        Assert.AreEqual(0, tile.CompareTo(tile));
    }
    [Test]
    public void CompareTo_DifferentTileType()
    {
        Tile smallerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        smallerTile.SetTileType(BAMBOO_TILE_TYPE);
        Tile largerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        largerTile.SetTileType(FLOWER_TILE_TYPE);
        Assert.AreEqual(-1, smallerTile.CompareTo(largerTile));
        Assert.AreEqual(1, largerTile.CompareTo(smallerTile));
    }
    [Test]
    public void CompareTo_SameTileType_DifferentValue()
    {
        Tile smallerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        smallerTile.SetTileType(BAMBOO_TILE_TYPE);
        smallerTile.SetValue(INTEGER_TEN);
        Tile largerTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        largerTile.SetTileType(BAMBOO_TILE_TYPE);
        largerTile.SetValue(INTEGER_TWENTY);
        Assert.AreEqual(-1, smallerTile.CompareTo(largerTile));
        Assert.AreEqual(1, largerTile.CompareTo(smallerTile));
    }
    [Test]
    public void CompareTo_SameTileType_SameValue()
    {
        Tile tile1 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile1.SetTileType(BAMBOO_TILE_TYPE);
        tile1.SetValue(INTEGER_TEN);
        Tile tile2 = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile2.SetTileType(BAMBOO_TILE_TYPE);
        tile2.SetValue(INTEGER_TEN);
        Assert.AreEqual(0, tile1.CompareTo(tile2));
        Assert.AreEqual(0, tile2.CompareTo(tile1));
    }
    [Test]
    public void Equals_Null()
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Assert.False(tile.Equals(null));
    }
    [Test]
    public void Equals_NonTile()
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Assert.False(tile.Equals(INTEGER_TEN));
    }
    [Test]
    public void Equals_SameObject()
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        Assert.True(tile.GetHashCode() == tile.GetHashCode());
    }
}
