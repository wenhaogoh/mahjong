using System;
using UnityEngine;

public class MapperUtils
{
    public static int MapGameStateToPlayerId(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.PLAYER0_DRAWING:
            case GameStates.PLAYER0_DISCARDING:
            case GameStates.PLAYER0_OFFERING:
                return 0;
            case GameStates.OPPONENT1_DRAWING:
            case GameStates.OPPONENT1_DISCARDING:
            case GameStates.OPPONENT1_OFFERING:
                return 1;
            case GameStates.OPPONENT2_DRAWING:
            case GameStates.OPPONENT2_DISCARDING:
            case GameStates.OPPONENT2_OFFERING:
                return 2;
            case GameStates.OPPONENT3_DRAWING:
            case GameStates.OPPONENT3_DISCARDING:
            case GameStates.OPPONENT3_OFFERING:
                return 3;
            default:
                Debug.LogWarning("Unable to map game state to player id!");
                return -1;
        }
    }
    public static GameStates MapPlayerIdToDrawingGameState(int playerId)
    {
        switch (playerId)
        {
            case 0:
                return GameStates.PLAYER0_DRAWING;
            case 1:
                return GameStates.OPPONENT1_DRAWING;
            case 2:
                return GameStates.OPPONENT2_DRAWING;
            case 3:
                return GameStates.OPPONENT3_DRAWING;
            default:
                throw new Exception("No such player!");
        }
    }
    public static GameStates MapPlayerIdToDiscardingGameState(int playerId)
    {
        switch (playerId)
        {
            case 0:
                return GameStates.PLAYER0_DISCARDING;
            case 1:
                return GameStates.OPPONENT1_DISCARDING;
            case 2:
                return GameStates.OPPONENT2_DISCARDING;
            case 3:
                return GameStates.OPPONENT3_DISCARDING;
            default:
                throw new Exception("No such player!");
        }
    }
    public static GameStates MapPlayerIdToOfferingGameState(int playerId)
    {
        switch (playerId)
        {
            case 0:
                return GameStates.PLAYER0_OFFERING;
            case 1:
                return GameStates.OPPONENT1_OFFERING;
            case 2:
                return GameStates.OPPONENT2_OFFERING;
            case 3:
                return GameStates.OPPONENT3_OFFERING;
            default:
                throw new Exception("No such player!");
        }
    }
}