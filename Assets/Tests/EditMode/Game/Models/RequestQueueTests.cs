using NUnit.Framework;

public class RequestQueueTests
{
    [Test]
    public void GetHighestPriorityRequest()
    {
        GameStateController.instance = new GameStateController();
        GameStateController.instance.gameState = GameStates.PLAYER0_OFFERING;
        Player player0 = PlayerUtils.GetPlayer0();
        Player opponent1 = PlayerUtils.GetOpponent1();
        Player opponent2 = PlayerUtils.GetOpponent2();
        Player opponent3 = PlayerUtils.GetOpponent3();
        RequestQueue requestQueue = new RequestQueue();
        Request kongRequest = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Request pongRequest = new Request(GetTileAction(TileActionTypes.PONG), opponent2);
        Request chowRequest = new Request(GetTileAction(TileActionTypes.CHOW), opponent3);
        requestQueue.SetOfferingPlayer(player0);
        requestQueue.Add(chowRequest);
        requestQueue.Add(pongRequest);
        requestQueue.Add(kongRequest);
        Assert.AreEqual(kongRequest, requestQueue.GetHighestPriorityRequest());
    }
    [Test]
    public void IsFull()
    {
        GameStateController.instance = new GameStateController();
        GameStateController.instance.gameState = GameStates.PLAYER0_OFFERING;
        Player player0 = PlayerUtils.GetPlayer0();
        Player opponent1 = PlayerUtils.GetOpponent1();
        Player opponent2 = PlayerUtils.GetOpponent2();
        Player opponent3 = PlayerUtils.GetOpponent3();
        RequestQueue requestQueue = new RequestQueue();
        Request kongRequest = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Request pongRequest = new Request(GetTileAction(TileActionTypes.PONG), opponent2);
        Request chowRequest = new Request(GetTileAction(TileActionTypes.CHOW), opponent3);
        requestQueue.SetOfferingPlayer(player0);
        requestQueue.Add(chowRequest);
        requestQueue.Add(pongRequest);
        requestQueue.Add(kongRequest);
        Assert.True(requestQueue.IsFull());
    }
    [Test]
    public void Reset()
    {
        GameStateController.instance = new GameStateController();
        GameStateController.instance.gameState = GameStates.PLAYER0_OFFERING;
        Player player0 = PlayerUtils.GetPlayer0();
        Player opponent1 = PlayerUtils.GetOpponent1();
        Player opponent2 = PlayerUtils.GetOpponent2();
        Player opponent3 = PlayerUtils.GetOpponent3();
        RequestQueue requestQueue = new RequestQueue();
        Request kongRequest = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Request pongRequest = new Request(GetTileAction(TileActionTypes.PONG), opponent2);
        Request chowRequest = new Request(GetTileAction(TileActionTypes.CHOW), opponent3);
        requestQueue.SetOfferingPlayer(player0);
        requestQueue.Add(chowRequest);
        requestQueue.Add(pongRequest);
        requestQueue.Add(kongRequest);
        requestQueue.Reset();
        Assert.False(requestQueue.IsFull());
    }
    private TileAction GetTileAction(TileActionTypes tileActionType)
    {
        TileAction tileAction = new TileAction(tileActionType, null, null);
        return tileAction;
    }
}
