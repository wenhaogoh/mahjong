using NUnit.Framework;
using UnityEngine;

public class TileQueueTests
{
    [Test]
    public void TileQueue()
    {
        TileQueue tileQueue = new TileQueue();
        Assert.AreEqual(148, tileQueue.Count());
    }
    [Test]
    public void DrawFromFront()
    {
        TileQueue tileQueue = new TileQueue();
        Tile firstTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        firstTile.SetTileType(TileTypes.BAMBOO);
        firstTile.SetValue(1);
        Assert.AreEqual(firstTile, tileQueue.DrawFromFront());
        Assert.AreEqual(147, tileQueue.Count());
    }
    [Test]
    public void DrawFromBack()
    {
        TileQueue tileQueue = new TileQueue();
        Tile lastTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        lastTile.SetTileType(TileTypes.HONOUR);
        lastTile.SetValue((int)HonourTypes.NORTH);
        Assert.AreEqual(lastTile, tileQueue.DrawFromBack());
        Assert.AreEqual(147, tileQueue.Count());
    }
}
