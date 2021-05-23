using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TurnProcessor
{
    private TileQueue tileQueue;
    private TilesContainer discardedTilesContainer;
    private RequestQueue requestQueue;
    private Player player0; // Bottom
    private Player opponent1; // Right
    private Player opponent2; // Top
    private Player opponent3; // Left
    private Player[] players;
    private const int AUTO_PLAY_DELAY = 2;

    public void NewGame()
    {
        tileQueue = new TileQueue();
        tileQueue.Randomize();
        discardedTilesContainer = new TilesContainer();
        requestQueue = new RequestQueue();
        this.player0 = new Player(0);
        this.opponent1 = new Player(1);
        this.opponent2 = new Player(2);
        this.opponent3 = new Player(3);
        this.players = new Player[] { player0, opponent1, opponent2, opponent3 };
        player0.DrawStartingTiles(tileQueue);
        opponent1.DrawStartingTiles(tileQueue);
        opponent2.DrawStartingTiles(tileQueue);
        opponent3.DrawStartingTiles(tileQueue);
        SetPlayerWinds(player0, Winds.EAST);
        GameStateController.instance.RefreshDisplays();
    }
    public void DrawPlayerTile()
    {
        DrawTile(player0);
    }
    public void DiscardPlayerTile(int index)
    {
        DiscardTile(index, player0);
    }
    public void ExecutePlayerTileAction(TileAction tileAction)
    {
        ExecuteTileAction(tileAction, player0);
    }
    public void ProcessPlayerTileActionRequest(TileAction tileAction)
    {
        ProcessTileActionRequest(tileAction, player0);
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
        Player player = players[MapperUtils.MapGameStateToPlayerId(GameStateController.instance.gameState)];
        GameStateController.instance.gameState = GameStates.PROCESSING;
        GameStateController.instance.StartCoroutine(AutoPlayCoroutine(player, AUTO_PLAY_DELAY));
    }
    private void SetPlayerWinds(Player player, Winds wind)
    {
        for (int i = 0; i < 4; i++)
        {
            if (wind == Winds.EAST)
            {
                GameStateController.instance.gameState = (GameStates)player.GetId();
            }
            player.SetWind(wind);
            player = GetNextPlayer(player);
            wind = wind.Next<Winds>();
        }
    }
    private void DrawTile(Player drawingPlayer)
    {
        drawingPlayer.DrawTile(tileQueue);
        if (drawingPlayer.GetId() == 0)
        {
            GameStateController.instance.DisplayTileActions(drawingPlayer.GetPossibleTileActionsFromDrawnTile());
        }
        GameStateController.instance.RefreshDisplays();
        GameStateController.instance.gameState = MapperUtils.MapPlayerIdToDiscardingGameState(drawingPlayer.GetId());
    }
    private void DiscardTile(int tileIndex, Player discardingPlayer)
    {
        GameStateController.instance.gameState = GameStates.PROCESSING;
        discardingPlayer.DiscardTile(tileIndex, discardedTilesContainer);
        discardingPlayer.SortTiles();
        GameStateController.instance.RefreshDisplays();
        Tile discardedTile = discardedTilesContainer.GetLastTile();
        Offer(discardedTile, discardingPlayer);
    }
    private void ExecuteRequest(Request request)
    {
        ExecuteTileAction(request.GetTileAction(), request.GetRequestingPlayer());
    }
    private void ExecuteTileAction(TileAction tileAction, Player executingPlayer)
    {
        executingPlayer.ExecuteTileAction(tileAction);
        switch (tileAction.GetTileActionType())
        {
            case TileActionTypes.KONG:
                DrawTile(executingPlayer);
                break;
            default:
                break;
        }
    }
    private void ProcessTileActionRequest(TileAction tileAction, Player requestingPlayer)
    {
        requestQueue.Add(new Request(tileAction, requestingPlayer));
        if (requestQueue.IsFull())
        {
            Request request = requestQueue.GetHighestPriorityRequest();
            requestQueue.Reset();
            if (request.IsEmpty())
            {
                int currentTurnPlayerId = MapperUtils.MapGameStateToPlayerId(GameStateController.instance.gameState);
                Player nextTurnPlayer = GetNextPlayer(currentTurnPlayerId);
                GameStateController.instance.gameState = MapperUtils.MapPlayerIdToDrawingGameState(nextTurnPlayer.GetId());
            }
            else
            {
                discardedTilesContainer.RemoveLastTile();
                ExecuteRequest(request);
            }
        }
    }
    private void Offer(Tile discardedTile, Player offeringPlayer)
    {
        GameStateController.instance.gameState = MapperUtils.MapPlayerIdToOfferingGameState(offeringPlayer.GetId());
        requestQueue.SetOfferingPlayer(offeringPlayer);
        for (int i = 0; i < players.Length; i++)
        {
            Player player = players[i];
            if (player == offeringPlayer)
            {
                continue;
            }
            if (player.GetId() == 0)
            {
                List<TileAction> tileActions = player.GetPossibleTileActionsFromOfferedTile(discardedTile, offeringPlayer);
                if (tileActions.Count == 0)
                {
                    ProcessTileActionRequest(null, player);
                }
                else
                {
                    GameStateController.instance.DisplayTileActions(tileActions);
                }
                continue;
            }
            else
            {
                ProcessTileActionRequest(null, player);
            }
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
        DiscardTile(0, player);
    }
}