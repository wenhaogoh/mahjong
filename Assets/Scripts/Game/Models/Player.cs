using System.Collections.Generic;

public class Player
{
    private const int STARTING_TILES_COUNT = 13;
    private const int MAX_PLAYER_ID = PlayerUtils.OPPONENT3_ID;
    private int id;
    private Winds wind;
    private TilesContainer mainTiles;
    private TilesContainer flowerTiles;
    public Player(int id)
    {
        this.id = id;
        this.mainTiles = new TilesContainer();
        this.flowerTiles = new TilesContainer();
    }
    public void DrawTile(ITileQueue tileQueue)
    {
        Tile tile = tileQueue.DrawFromFront();
        while (tile.IsFlower())
        {
            flowerTiles.AddTile(tile);
            tile = tileQueue.DrawFromBack();
        }
        mainTiles.AddTile(tile);
    }
    public void DrawStartingTiles(ITileQueue tileQueue)
    {
        DrawTiles(STARTING_TILES_COUNT, tileQueue);
        SortTiles();
    }
    public void DiscardTile(int tileIndex, TilesContainer discardedTilesContainer)
    {
        Tile toDiscard = mainTiles.RemoveTile(tileIndex);
        discardedTilesContainer.AddTile(toDiscard);
    }
    public List<TileAction> GetPossibleTileActionsFromDrawnTile()
    {
        Tile drawnTile = mainTiles.RemoveLastTile();
        List<TileAction> tileActions = TilesAnalyzer.GetPossibleTileActionsFromDrawnTile(mainTiles.GetTiles(), drawnTile);
        mainTiles.AddTile(drawnTile);
        return tileActions;
    }
    public List<TileAction> GetPossibleTileActionsFromOfferedTile(Tile offeredTile, Player offeringPlayer)
    {
        return TilesAnalyzer.GetPossibleTileActionsFromOfferedTile(mainTiles.GetTiles(), offeredTile, IsPreviousPlayer(offeringPlayer));
    }
    public void ExecuteTileAction(TileAction tileAction, bool isFromOffer)
    {
        switch (tileAction.GetTileActionType())
        {
            case TileActionTypes.PONG:
            case TileActionTypes.CHOW:
            case TileActionTypes.KONG:
                flowerTiles.AddTiles(tileAction.GetTiles());
                if (isFromOffer)
                {
                    mainTiles.RemoveTiles(tileAction.GetTilesWithoutTriggerTile());
                }
                else
                {
                    mainTiles.RemoveTiles(tileAction.GetTiles());
                }
                break;
            case TileActionTypes.HU:
                if (isFromOffer)
                {
                    mainTiles.AddTiles(tileAction.GetTiles());
                }
                break;
            default:
                break;
        }
    }
    public void SortTiles()
    {
        mainTiles.Sort();
        flowerTiles.Sort();
    }
    public void SetWind(Winds wind)
    {
        this.wind = wind;
    }
    public TilesContainer GetMainTiles()
    {
        return mainTiles;
    }
    public TilesContainer GetFlowerTiles()
    {
        return flowerTiles;
    }
    public Winds GetWind()
    {
        return this.wind;
    }
    public int GetId()
    {
        return this.id;
    }
    public void Reset()
    {
        this.mainTiles = new TilesContainer();
        this.flowerTiles = new TilesContainer();
    }
    public bool IsEastWindPlayer()
    {
        return wind == Winds.EAST;
    }
    private void DrawTiles(int count, ITileQueue tileQueue)
    {
        for (int i = 0; i < count; i++)
        {
            DrawTile(tileQueue);
        }
    }
    private bool IsPreviousPlayer(Player player)
    {
        if (this.id == PlayerUtils.PLAYER0_ID)
        {
            return player.id == MAX_PLAYER_ID;
        }
        else
        {
            return player.id - this.id == -1;
        }
    }
}