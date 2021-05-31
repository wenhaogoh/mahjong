using System.Collections.Generic;
using NUnit.Framework;

public class TileUtilsTests
{
    [Test]
    public void GenerateSameTiles()
    {
        Assert.AreEqual(1, new HashSet<Tile>(TileUtils.GenerateSameTiles()).Count);
    }
    [Test]
    public void GenerateTilesForChow()
    {
        Assert.AreEqual(3, new HashSet<Tile>(TileUtils.GenerateTilesForChow()).Count);
    }
}
