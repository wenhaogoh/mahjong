using NUnit.Framework;

public class PlayerTests
{
    private const int INTEGER_ZERO = 0;
    [Test]
    public void Player()
    {
        Player player = new Player(INTEGER_ZERO);
        Assert.AreEqual(INTEGER_ZERO, player.GetId());
    }
    [Test]
    public void DrawTile()
    {
        Player player = new Player(INTEGER_ZERO);
        TileQueue tileQueue = new TileQueue();
        for (int i = 0; i < 109; i++)
        {
            player.DrawTile(tileQueue);
        }
        Assert.AreEqual(109, player.GetMainTiles().Count());
        Assert.AreEqual(1, player.GetFlowerTiles().Count());
    }
    [Test]
    public void DrawStartingTiles()
    {
        Player player = new Player(INTEGER_ZERO);
        TileQueue tileQueue = new TileQueue();
        player.DrawStartingTiles(tileQueue);
        Assert.AreEqual(13, player.GetMainTiles().Count());
    }
    [Test]
    public void DiscardTile()
    {
        Player player = new Player(INTEGER_ZERO);
        TileQueue tileQueue = new TileQueue();
        TilesContainer tilesContainer = new TilesContainer();
        player.DrawStartingTiles(tileQueue);
        player.DiscardTile(0, tilesContainer);
        Assert.AreEqual(12, player.GetMainTiles().Count());
        Assert.AreEqual(1, tilesContainer.Count());
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_NoActions()
    {
        Player player = new Player(INTEGER_ZERO);
        TileQueue tileQueue = new TileQueue();
        player.DrawTile(tileQueue);
        Assert.AreEqual(0, player.GetPossibleTileActionsFromDrawnTile().Count);
    }
    [Test]
    public void ExecuteTileAction_Kong()
    {
        Player player = new Player(INTEGER_ZERO);
        TileQueue tileQueue = new TileQueue();
        for (int i = 0; i < 4; i++)
        {
            player.DrawTile(tileQueue);
        }
        TilesContainer tilesContainer = new TilesContainer();
        tilesContainer.AddTiles(player.GetMainTiles());
        TileAction tileAction = new TileAction(TileActionTypes.KONG, tilesContainer);
        player.ExecuteTileAction(tileAction);
        Assert.AreEqual(4, player.GetFlowerTiles().Count());
        Assert.AreEqual(0, player.GetMainTiles().Count());
    }
    [Test]
    public void SetWind()
    {
        Player player = new Player(INTEGER_ZERO);
        player.SetWind(Winds.EAST);
        Assert.AreEqual(Winds.EAST, player.GetWind());
    }
}