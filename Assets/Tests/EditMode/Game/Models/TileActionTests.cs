using NUnit.Framework;

public class TileActionTests
{
    [Test]
    public void TileAction()
    {
        TilesContainer tilesContainer = new TilesContainer();
        TileAction tileAction = new TileAction(TileActionTypes.PONG, tilesContainer);
        Assert.AreEqual(tilesContainer, tileAction.GetTiles());
        Assert.AreEqual(TileActionTypes.PONG, tileAction.GetTileActionType());
    }
}
