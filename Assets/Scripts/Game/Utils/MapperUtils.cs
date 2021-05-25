using System;

public class MapperUtils
{
    public static int MapGameStateToPlayerId(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.PLAYER0_DRAWING:
            case GameStates.PLAYER0_DISCARDING:
            case GameStates.PLAYER0_OFFERING:
                return PlayerUtils.PLAYER0_ID;
            case GameStates.OPPONENT1_DRAWING:
            case GameStates.OPPONENT1_DISCARDING:
            case GameStates.OPPONENT1_OFFERING:
                return PlayerUtils.OPPONENT1_ID;
            case GameStates.OPPONENT2_DRAWING:
            case GameStates.OPPONENT2_DISCARDING:
            case GameStates.OPPONENT2_OFFERING:
                return PlayerUtils.OPPONENT2_ID;
            case GameStates.OPPONENT3_DRAWING:
            case GameStates.OPPONENT3_DISCARDING:
            case GameStates.OPPONENT3_OFFERING:
                return PlayerUtils.OPPONENT3_ID;
            default:
                throw new Exception("Unable to game state to player id!");
        }
    }
    public static GameStates MapPlayerIdToDrawingGameState(int playerId)
    {
        switch (playerId)
        {
            case PlayerUtils.PLAYER0_ID:
                return GameStates.PLAYER0_DRAWING;
            case PlayerUtils.OPPONENT1_ID:
                return GameStates.OPPONENT1_DRAWING;
            case PlayerUtils.OPPONENT2_ID:
                return GameStates.OPPONENT2_DRAWING;
            case PlayerUtils.OPPONENT3_ID:
                return GameStates.OPPONENT3_DRAWING;
            default:
                throw new Exception("No such player!");
        }
    }
    public static GameStates MapPlayerIdToDiscardingGameState(int playerId)
    {
        switch (playerId)
        {
            case PlayerUtils.PLAYER0_ID:
                return GameStates.PLAYER0_DISCARDING;
            case PlayerUtils.OPPONENT1_ID:
                return GameStates.OPPONENT1_DISCARDING;
            case PlayerUtils.OPPONENT2_ID:
                return GameStates.OPPONENT2_DISCARDING;
            case PlayerUtils.OPPONENT3_ID:
                return GameStates.OPPONENT3_DISCARDING;
            default:
                throw new Exception("No such player!");
        }
    }
    public static GameStates MapPlayerIdToOfferingGameState(int playerId)
    {
        switch (playerId)
        {
            case PlayerUtils.PLAYER0_ID:
                return GameStates.PLAYER0_OFFERING;
            case PlayerUtils.OPPONENT1_ID:
                return GameStates.OPPONENT1_OFFERING;
            case PlayerUtils.OPPONENT2_ID:
                return GameStates.OPPONENT2_OFFERING;
            case PlayerUtils.OPPONENT3_ID:
                return GameStates.OPPONENT3_OFFERING;
            default:
                throw new Exception("No such player!");
        }
    }
}