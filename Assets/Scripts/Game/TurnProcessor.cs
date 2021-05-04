using System;

public class TurnProcessor
{
    private TileQueue tileQueue;
    private TilesContainer discardedTilesContainer;
    private RequestProcessor requestProcessor;
    private Player player; // Bottom
    private Player opponent1; // Right
    private Player opponent2; // Top
    private Player opponent3; // Left
    private Player[] players;
    private int currentTurnIndex;

    public void NewGame()
    {
        tileQueue = new TileQueue();
        discardedTilesContainer = new TilesContainer();
        requestProcessor = new RequestProcessor();
        this.player = new Player(0);
        this.opponent1 = new Player(1);
        this.opponent2 = new Player(2);
        this.opponent3 = new Player(3);
        this.players = new Player[] { player, opponent1, opponent2, opponent3 };
        player.DrawStartingTiles(tileQueue);
        opponent1.DrawStartingTiles(tileQueue);
        opponent2.DrawStartingTiles(tileQueue);
        opponent3.DrawStartingTiles(tileQueue);
        SetPlayerWinds(player, Winds.East);
        GameStateController.instance.RefreshDisplays();
    }
    public void DiscardPlayerTile(int index)
    {
        DiscardTile(index, player.GetId());
    }
    public TilesContainer GetPlayerMainTiles()
    {
        return this.player.GetMainTiles();
    }
    public TilesContainer GetPlayerFlowerTiles()
    {
        return this.player.GetFlowerTiles();
    }
    public TilesContainer GetDiscardedTiles()
    {
        return this.discardedTilesContainer;
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
        GameStateController.instance.gameState = (GameStates)drawingPlayer.GetId();
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
            DrawTile(nextPlayer);
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
}