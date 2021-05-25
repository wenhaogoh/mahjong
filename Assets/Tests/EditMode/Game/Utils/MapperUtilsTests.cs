using NUnit.Framework;
using System;

public class MapperUtilsTests
{
    private const int INVALID_ID = 4;
    [Test]
    public void MapGameStateToPlayerId()
    {
        Assert.AreEqual(PlayerUtils.PLAYER0_ID, MapperUtils.MapGameStateToPlayerId(GameStates.PLAYER0_DRAWING));
        Assert.AreEqual(PlayerUtils.PLAYER0_ID, MapperUtils.MapGameStateToPlayerId(GameStates.PLAYER0_DISCARDING));
        Assert.AreEqual(PlayerUtils.PLAYER0_ID, MapperUtils.MapGameStateToPlayerId(GameStates.PLAYER0_OFFERING));

        Assert.AreEqual(PlayerUtils.OPPONENT1_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT1_DRAWING));
        Assert.AreEqual(PlayerUtils.OPPONENT1_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT1_DISCARDING));
        Assert.AreEqual(PlayerUtils.OPPONENT1_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT1_OFFERING));

        Assert.AreEqual(PlayerUtils.OPPONENT2_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT2_DRAWING));
        Assert.AreEqual(PlayerUtils.OPPONENT2_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT2_DISCARDING));
        Assert.AreEqual(PlayerUtils.OPPONENT2_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT2_OFFERING));

        Assert.AreEqual(PlayerUtils.OPPONENT3_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT3_DRAWING));
        Assert.AreEqual(PlayerUtils.OPPONENT3_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT3_DISCARDING));
        Assert.AreEqual(PlayerUtils.OPPONENT3_ID, MapperUtils.MapGameStateToPlayerId(GameStates.OPPONENT3_OFFERING));

        Assert.Throws<Exception>(() => MapperUtils.MapGameStateToPlayerId(GameStates.PROCESSING));
    }
    [Test]
    public void MapPlayerIdToDiscardingGameState()
    {
        Assert.AreEqual(GameStates.PLAYER0_DISCARDING, MapperUtils.MapPlayerIdToDiscardingGameState(PlayerUtils.PLAYER0_ID));
        Assert.AreEqual(GameStates.OPPONENT1_DISCARDING, MapperUtils.MapPlayerIdToDiscardingGameState(PlayerUtils.OPPONENT1_ID));
        Assert.AreEqual(GameStates.OPPONENT2_DISCARDING, MapperUtils.MapPlayerIdToDiscardingGameState(PlayerUtils.OPPONENT2_ID));
        Assert.AreEqual(GameStates.OPPONENT3_DISCARDING, MapperUtils.MapPlayerIdToDiscardingGameState(PlayerUtils.OPPONENT3_ID));

        Assert.Throws<Exception>(() => MapperUtils.MapPlayerIdToDiscardingGameState(INVALID_ID));
    }
    [Test]
    public void MapPlayerIdToDrawingGameState()
    {
        Assert.AreEqual(GameStates.PLAYER0_DRAWING, MapperUtils.MapPlayerIdToDrawingGameState(PlayerUtils.PLAYER0_ID));
        Assert.AreEqual(GameStates.OPPONENT1_DRAWING, MapperUtils.MapPlayerIdToDrawingGameState(PlayerUtils.OPPONENT1_ID));
        Assert.AreEqual(GameStates.OPPONENT2_DRAWING, MapperUtils.MapPlayerIdToDrawingGameState(PlayerUtils.OPPONENT2_ID));
        Assert.AreEqual(GameStates.OPPONENT3_DRAWING, MapperUtils.MapPlayerIdToDrawingGameState(PlayerUtils.OPPONENT3_ID));

        Assert.Throws<Exception>(() => MapperUtils.MapPlayerIdToDrawingGameState(INVALID_ID));
    }
    [Test]
    public void MapPlayerIdToOfferingGameState()
    {
        Assert.AreEqual(GameStates.PLAYER0_OFFERING, MapperUtils.MapPlayerIdToOfferingGameState(PlayerUtils.PLAYER0_ID));
        Assert.AreEqual(GameStates.OPPONENT1_OFFERING, MapperUtils.MapPlayerIdToOfferingGameState(PlayerUtils.OPPONENT1_ID));
        Assert.AreEqual(GameStates.OPPONENT2_OFFERING, MapperUtils.MapPlayerIdToOfferingGameState(PlayerUtils.OPPONENT2_ID));
        Assert.AreEqual(GameStates.OPPONENT3_OFFERING, MapperUtils.MapPlayerIdToOfferingGameState(PlayerUtils.OPPONENT3_ID));

        Assert.Throws<Exception>(() => MapperUtils.MapPlayerIdToOfferingGameState(INVALID_ID));
    }
}