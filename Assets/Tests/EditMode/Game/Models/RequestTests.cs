using NUnit.Framework;
using System;
using NSubstitute;
public class RequestTests
{
    [Test]
    public void Request()
    {
        Player requestingPlayer = PlayerUtils.GetPlayer(PlayerUtils.PLAYER0_ID);
        TileAction kongTileAction = GetTileAction(TileActionTypes.KONG);
        Request request = new Request(kongTileAction, requestingPlayer);
        Assert.AreEqual(requestingPlayer, request.GetRequestingPlayer());
        Assert.AreEqual(kongTileAction, request.GetTileAction());
    }
    [Test]
    public void IsEmpty()
    {
        Player requestingPlayer = PlayerUtils.GetPlayer(PlayerUtils.PLAYER0_ID);
        Request emptyRequest = new Request(null, requestingPlayer);
        Assert.True(emptyRequest.IsEmpty());
    }
    [Test]
    public void CompareTo_Hu_Kong()
    {
        Request huRequest = new Request(GetTileAction(TileActionTypes.HU), null);
        Request kongRequest = new Request(GetTileAction(TileActionTypes.KONG), null);
        Assert.AreEqual(1, huRequest.CompareTo(kongRequest));
        Assert.AreEqual(-1, kongRequest.CompareTo(huRequest));
    }
    [Test]
    public void CompareTo_Kong_Pong()
    {
        Request kongRequest = new Request(GetTileAction(TileActionTypes.KONG), null);
        Request pongRequest = new Request(GetTileAction(TileActionTypes.PONG), null);
        Assert.AreEqual(1, kongRequest.CompareTo(pongRequest));
        Assert.AreEqual(-1, pongRequest.CompareTo(kongRequest));
    }
    [Test]
    public void CompareTo_Pong_Chow()
    {
        Request pongRequest = new Request(GetTileAction(TileActionTypes.PONG), null);
        Request chowRequest = new Request(GetTileAction(TileActionTypes.CHOW), null);
        Assert.AreEqual(1, pongRequest.CompareTo(chowRequest));
        Assert.AreEqual(-1, chowRequest.CompareTo(pongRequest));
    }
    [Test]
    public void CompareTo_SameTile_DifferentPlayer_Player0Turn()
    {
        GameStateController.instance = Substitute.For<GameStateController>();
        GameStateController.instance.gameState = GameStates.PLAYER0_OFFERING;
        Player opponent1 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT1_ID);
        Player opponent2 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT2_ID);
        Player opponent3 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT3_ID);
        Request opponent1Request = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Request opponent2Request = new Request(GetTileAction(TileActionTypes.KONG), opponent2);
        Request opponent3Request = new Request(GetTileAction(TileActionTypes.KONG), opponent3);
        Assert.AreEqual(1, opponent1Request.CompareTo(opponent2Request));
        Assert.AreEqual(-1, opponent2Request.CompareTo(opponent1Request));
        Assert.AreEqual(1, opponent2Request.CompareTo(opponent3Request));
        Assert.AreEqual(-1, opponent3Request.CompareTo(opponent2Request));
        Assert.AreEqual(1, opponent1Request.CompareTo(opponent3Request));
        Assert.AreEqual(-1, opponent3Request.CompareTo(opponent1Request));
    }
    [Test]
    public void CompareTo_SameTile_DifferentPlayer_Opponent1Turn()
    {
        GameStateController.instance = Substitute.For<GameStateController>();
        GameStateController.instance.gameState = GameStates.OPPONENT1_OFFERING;
        Player player0 = PlayerUtils.GetPlayer(PlayerUtils.PLAYER0_ID);
        Player opponent2 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT2_ID);
        Player opponent3 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT3_ID);
        Request player0Request = new Request(GetTileAction(TileActionTypes.KONG), player0);
        Request opponent2Request = new Request(GetTileAction(TileActionTypes.KONG), opponent2);
        Request opponent3Request = new Request(GetTileAction(TileActionTypes.KONG), opponent3);
        Assert.AreEqual(1, opponent2Request.CompareTo(opponent3Request));
        Assert.AreEqual(-1, opponent3Request.CompareTo(opponent2Request));
        Assert.AreEqual(1, opponent3Request.CompareTo(player0Request));
        Assert.AreEqual(-1, player0Request.CompareTo(opponent3Request));
        Assert.AreEqual(1, opponent2Request.CompareTo(player0Request));
        Assert.AreEqual(-1, player0Request.CompareTo(opponent2Request));
    }
    [Test]
    public void CompareTo_SameTile_DifferentPlayer_Opponent2Turn()
    {
        GameStateController.instance = Substitute.For<GameStateController>();
        GameStateController.instance.gameState = GameStates.OPPONENT2_OFFERING;
        Player player0 = PlayerUtils.GetPlayer(PlayerUtils.PLAYER0_ID);
        Player opponent1 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT1_ID);
        Player opponent3 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT3_ID);
        Request player0Request = new Request(GetTileAction(TileActionTypes.KONG), player0);
        Request opponent1Request = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Request opponent3Request = new Request(GetTileAction(TileActionTypes.KONG), opponent3);
        Assert.AreEqual(1, opponent3Request.CompareTo(opponent1Request));
        Assert.AreEqual(-1, opponent1Request.CompareTo(opponent3Request));
        Assert.AreEqual(1, opponent3Request.CompareTo(player0Request));
        Assert.AreEqual(-1, player0Request.CompareTo(opponent3Request));
        Assert.AreEqual(1, player0Request.CompareTo(opponent1Request));
        Assert.AreEqual(-1, opponent1Request.CompareTo(player0Request));
    }
    [Test]
    public void CompareTo_SameTile_DifferentPlayer_Opponent3Turn()
    {
        GameStateController.instance = Substitute.For<GameStateController>();
        GameStateController.instance.gameState = GameStates.OPPONENT3_OFFERING;
        Player player0 = PlayerUtils.GetPlayer(PlayerUtils.PLAYER0_ID);
        Player opponent1 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT1_ID);
        Player opponent2 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT2_ID);
        Request player0Request = new Request(GetTileAction(TileActionTypes.KONG), player0);
        Request opponent1Request = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Request opponent2Request = new Request(GetTileAction(TileActionTypes.KONG), opponent2);
        Assert.AreEqual(1, opponent1Request.CompareTo(opponent2Request));
        Assert.AreEqual(-1, opponent2Request.CompareTo(opponent1Request));
        Assert.AreEqual(1, player0Request.CompareTo(opponent2Request));
        Assert.AreEqual(-1, opponent2Request.CompareTo(player0Request));
        Assert.AreEqual(1, player0Request.CompareTo(opponent1Request));
        Assert.AreEqual(-1, opponent1Request.CompareTo(player0Request));
    }
    [Test]
    public void CompareTo_SameTile_OfferingPlayerSentRequest()
    {
        GameStateController.instance = Substitute.For<GameStateController>();
        GameStateController.instance.gameState = GameStates.PLAYER0_OFFERING;
        Player player0 = PlayerUtils.GetPlayer(PlayerUtils.PLAYER0_ID);
        Player opponent1 = PlayerUtils.GetPlayer(PlayerUtils.OPPONENT1_ID);
        Request player0Request = new Request(GetTileAction(TileActionTypes.KONG), player0);
        Request opponent1Request = new Request(GetTileAction(TileActionTypes.KONG), opponent1);
        Assert.Throws<Exception>(() => player0Request.CompareTo(opponent1Request));
    }
    private TileAction GetTileAction(TileActionTypes tileActionType)
    {
        TileAction tileAction = new TileAction(tileActionType, null, null);
        return tileAction;
    }
}
