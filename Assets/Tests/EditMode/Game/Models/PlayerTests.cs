using NUnit.Framework;
using NSubstitute;

public class PlayerTests
{
    [Test]
    public void Player()
    {
        Player player = PlayerUtils.GetPlayer0();
        Assert.AreEqual(PlayerUtils.PLAYER0_ID, player.GetId());
    }
    [Test]
    public void DrawTile()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        player.DrawTile(tileQueue);
        Assert.AreEqual(1, player.GetMainTiles().Count());
    }
    [Test]
    public void DrawStartingTiles()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        player.DrawStartingTiles(tileQueue);
        Assert.AreEqual(13, player.GetMainTiles().Count());
    }
    [Test]
    public void DiscardTile()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        TilesContainer tilesContainer = new TilesContainer();
        player.DrawStartingTiles(tileQueue);
        player.DiscardTile(0, tilesContainer);
        Assert.AreEqual(12, player.GetMainTiles().Count());
        Assert.AreEqual(1, tilesContainer.Count());
    }
    [Test]
    public void GetPossibleActionsFromDrawnTile_NoAction()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        player.DrawTile(tileQueue);
        Assert.AreEqual(0, player.GetPossibleTileActionsFromDrawnTile().Count);
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_ChowAction_Player0()
    {
        Player player = PlayerUtils.GetPlayer0();
        Player previousPlayer = PlayerUtils.GetOpponent3();
        ITileQueue tileQueue = GetTileQueue();
        player.GetMainTiles().AddTile(TileUtils.GetTile(TileTypes.BAMBOO, 1));
        player.GetMainTiles().AddTile(TileUtils.GetTile(TileTypes.BAMBOO, 3));
        Assert.AreEqual(1, player.GetPossibleTileActionsFromOfferedTile(TileUtils.GetTile(TileTypes.BAMBOO, 2), previousPlayer).Count);
    }
    [Test]
    public void GetPossibleActionsFromOfferedTile_NoAction_NotPlayer0()
    {
        Player player = PlayerUtils.GetOpponent1();
        Player notPreviousPlayer = PlayerUtils.GetOpponent2();
        ITileQueue tileQueue = GetTileQueue();
        player.GetMainTiles().AddTile(TileUtils.GetTile(TileTypes.BAMBOO, 1));
        player.GetMainTiles().AddTile(TileUtils.GetTile(TileTypes.BAMBOO, 3));
        Assert.AreEqual(0, player.GetPossibleTileActionsFromOfferedTile(TileUtils.GetTile(TileTypes.BAMBOO, 2), notPreviousPlayer).Count);
    }
    [Test]
    public void ExecuteTileAction_Kong_Drawn()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        for (int i = 0; i < 4; i++)
        {
            player.DrawTile(tileQueue);
        }
        TilesContainer tilesContainer = new TilesContainer();
        tilesContainer.AddTiles(player.GetMainTiles());
        Tile triggerTile = tilesContainer.GetLastTile();
        TileAction tileAction = new TileAction(TileActionTypes.KONG, tilesContainer, triggerTile);
        player.ExecuteTileAction(tileAction, false);
        Assert.AreEqual(4, player.GetFlowerTiles().Count());
        Assert.AreEqual(0, player.GetMainTiles().Count());
    }
    [Test]
    public void ExecuteTileAction_Kong_Offered()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        for (int i = 0; i < 4; i++)
        {
            player.DrawTile(tileQueue);
        }
        TilesContainer tilesContainer = new TilesContainer();
        tilesContainer.AddTiles(player.GetMainTiles());
        Tile triggerTile = tilesContainer.GetLastTile();
        TileAction tileAction = new TileAction(TileActionTypes.KONG, tilesContainer, triggerTile);
        player.ExecuteTileAction(tileAction, true);
        Assert.AreEqual(4, player.GetFlowerTiles().Count());
        Assert.AreEqual(1, player.GetMainTiles().Count());
    }
    [Test]
    public void ExecuteTileAction_Chow()
    {
        Player player = PlayerUtils.GetPlayer0();
        ITileQueue tileQueue = GetTileQueue();
        for (int i = 0; i < 3; i++)
        {
            player.DrawTile(tileQueue);
        }
        TilesContainer tilesContainer = new TilesContainer();
        tilesContainer.AddTiles(player.GetMainTiles());
        Tile triggerTile = tilesContainer.GetLastTile();
        TileAction tileAction = new TileAction(TileActionTypes.CHOW, tilesContainer, triggerTile);
        player.ExecuteTileAction(tileAction, true);
        Assert.AreEqual(3, player.GetFlowerTiles().Count());
        Assert.AreEqual(1, player.GetMainTiles().Count());
    }
    [Test]
    public void ExecuteTileAction_Pong_Offered()
    {
        Player player = PlayerUtils.GetPlayer0();
        for (int i = 0; i < 3; i++)
        {
            player.GetMainTiles().AddTile(TileUtils.GetRedDragonTile());
        }
        TilesContainer tilesContainer = new TilesContainer();
        tilesContainer.AddTiles(player.GetMainTiles());
        Tile triggerTile = tilesContainer.GetLastTile();
        TileAction tileAction = new TileAction(TileActionTypes.PONG, tilesContainer, triggerTile);
        player.ExecuteTileAction(tileAction, true);
        Assert.AreEqual(3, player.GetFlowerTiles().Count());
        Assert.AreEqual(1, player.GetMainTiles().Count());
    }
    [Test]
    public void ExecuteTileAction_Hu_Offered()
    {
        Player player = PlayerUtils.GetPlayer0();
        for (int i = 0; i < 1; i++)
        {
            player.GetMainTiles().AddTile(TileUtils.GetRedDragonTile());
        }
        TilesContainer tilesContainer = new TilesContainer();
        tilesContainer.AddTile(TileUtils.GetRedDragonTile());
        TileAction tileAction = new TileAction(TileActionTypes.HU, tilesContainer, TileUtils.GetRedDragonTile());
        player.ExecuteTileAction(tileAction, true);
        Assert.AreEqual(0, player.GetFlowerTiles().Count());
        Assert.AreEqual(2, player.GetMainTiles().Count());
    }
    [Test]
    public void SetWind()
    {
        Player player = PlayerUtils.GetPlayer0();
        player.SetWind(Winds.EAST);
        Assert.AreEqual(Winds.EAST, player.GetWind());
    }
    [Test]
    public void Reset()
    {
        Player player = PlayerUtils.GetPlayer0();
        player.GetMainTiles().AddTile(TileUtils.GetRedDragonTile());
        player.GetFlowerTiles().AddTile(TileUtils.GetRedDragonTile());
        player.Reset();
        Assert.AreEqual(0, player.GetMainTiles().Count());
        Assert.AreEqual(0, player.GetFlowerTiles().Count());
    }
    private ITileQueue GetTileQueue()
    {
        ITileQueue tileQueue = Substitute.For<ITileQueue>();
        tileQueue.DrawFromFront().Returns(x => TileUtils.GetRedDragonTile());
        tileQueue.DrawFromBack().Returns(x => TileUtils.GetRedDragonTile());
        return tileQueue;
    }
}
