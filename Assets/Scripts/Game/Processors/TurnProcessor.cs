using System;
using System.Collections;
using UnityEngine;

public class TurnProcessor
{
    private TileQueue tileQueue;
    private TilesContainer discardedTilesContainer;
    private RequestProcessor requestProcessor;
    private Player player0; // Bottom
    private Player opponent1; // Right
    private Player opponent2; // Top
    private Player opponent3; // Left
    private Player[] players;
    private int currentTurnIndex;
    private const int AUTO_PLAY_DELAY = 2;

    public void NewGame()
    {
        tileQueue = new TileQueue();
        discardedTilesContainer = new TilesContainer();
        requestProcessor = new RequestProcessor();
        this.player0 = new Player(0);
        this.opponent1 = new Player(1);
        this.opponent2 = new Player(2);
        this.opponent3 = new Player(3);
        this.players = new Player[] { player0, opponent1, opponent2, opponent3 };
        player0.DrawStartingTiles(tileQueue);
        opponent1.DrawStartingTiles(tileQueue);
        opponent2.DrawStartingTiles(tileQueue);
        opponent3.DrawStartingTiles(tileQueue);
        SetPlayerWinds(player0, Winds.East);
        GameStateController.instance.RefreshDisplays();
    }
    public void DrawPlayerTile()
    {
        DrawTile(player0);
        GameStateController.instance.gameState = GameStates.PLAYER0_DISCARDING;
    }
    public void DiscardPlayerTile(int index)
    {
        DiscardTile(index, player0.GetId());
    }
    public TilesContainer GetPlayerMainTiles()
    {
        return this.player0.GetMainTiles();
    }
    public TilesContainer GetPlayerFlowerTiles()
    {
        return this.player0.GetFlowerTiles();
    }
    public TilesContainer GetDiscardedTiles()
    {
        return this.discardedTilesContainer;
    }
    public void AutoPlay()
    {
        Player player = MapGameStateToPlayer(GameStateController.instance.gameState);
        GameStateController.instance.gameState = GameStates.PROCESSING;
        GameStateController.instance.StartCoroutine(AutoPlayCoroutine(player, AUTO_PLAY_DELAY));
    }
    private void SetPlayerWinds(Player player, Winds wind)
    {
        for (int i = 0; i < 4; i++)
        {
            if (wind == Winds.East)
            {
                GameStateController.instance.gameState = (GameStates)player.GetId();
            }
            player.SetWind(wind);
            player = GetNextPlayer(player);
            wind = wind.Next<Winds>();
        }
    }
    private void DiscardTile(int tileIndex, int discardingPlayerId)
    {
        GameStateController.instance.gameState = GameStates.PROCESSING;
        players[discardingPlayerId].DiscardTile(tileIndex, discardedTilesContainer);
        GameStateController.instance.RefreshDisplays();
        Tile discardedTile = discardedTilesContainer.GetLastTile();
        Offer(discardedTile, discardingPlayerId);
    }
    private void DrawTile(Player drawingPlayer)
    {
        drawingPlayer.DrawTile(tileQueue);
        GameStateController.instance.RefreshDisplays();
    }
    private void Offer(Tile discardedTile, int offeringPlayerId)
    {
        for (int i = 0; i < players.Length; i++)
        {
            // Offer tile to all other players to Pong, Kong, Chow or Hu
        }
        if (requestProcessor.isEmpty())
        {
            Player nextPlayer = GetNextPlayer(offeringPlayerId);
            GameStateController.instance.gameState = MapPlayerToDrawingGameState(nextPlayer);
        }
        else
        {

        }
    }
    private Player GetNextPlayer(Player currentPlayer)
    {
        int currentPlayerIndex = Array.IndexOf(players, currentPlayer);
        return GetNextPlayer(currentPlayerIndex);
    }
    private Player GetNextPlayer(int currentPlayerIndex)
    {
        int nextPlayerIndex = currentPlayerIndex + 1;
        if (nextPlayerIndex >= players.Length)
        {
            nextPlayerIndex = 0;
        }
        return players[nextPlayerIndex];
    }
    private IEnumerator AutoPlayCoroutine(Player player, int delayInSeconds)
    {
        DrawTile(player);
        yield return new WaitForSecondsRealtime(delayInSeconds);
        DiscardTile(0, player.GetId());
    }
    private Player MapGameStateToPlayer(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.PLAYER0_DRAWING:
                return player0;
            case GameStates.PLAYER0_DISCARDING:
                return player0;
            case GameStates.OPPONENT1_DRAWING:
                return opponent1;
            case GameStates.OPPONENT1_DISCARDING:
                return opponent1;
            case GameStates.OPPONENT2_DRAWING:
                return opponent2;
            case GameStates.OPPONENT2_DISCARDING:
                return opponent2;
            case GameStates.OPPONENT3_DRAWING:
                return opponent3;
            case GameStates.OPPONENT3_DISCARDING:
                return opponent3;
            default:
                return null;
        }
    }
    private GameStates MapPlayerToDrawingGameState(Player player)
    {
        switch (player.GetId())
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
}