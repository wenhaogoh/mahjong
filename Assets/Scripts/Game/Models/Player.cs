using System.Collections.Generic;

public class Player
{
    private const int STARTING_TILES_COUNT = 13;
    private const int MAX_PLAYER_ID = 3;
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
    public void DrawTile(TileQueue tileQueue)
    {
        Tile tile = tileQueue.DrawFromFront();
        while (tile.IsFlower())
        {
            flowerTiles.AddTile(tile);
            tile = tileQueue.DrawFromBack();
        }
        mainTiles.AddTile(tile);
    }
    public void DrawStartingTiles(TileQueue tileQueue)
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
        List<TileAction> tileActions = mainTiles.GetPossibleTileActionsFromDrawnTile(drawnTile);
        mainTiles.AddTile(drawnTile);
        return tileActions;
    }
    public void ExecuteTileAction(TileAction tileAction, bool isFromOffer = false)
    {
        switch (tileAction.GetTileActionType())
        {
            case TileActionTypes.CHOW:
            case TileActionTypes.KONG:
                flowerTiles.AddTiles(tileAction.GetTiles());
                if (isFromOffer) 
                {
                    mainTiles.RemoveTiles(tileAction.GetTilesWithoutTriggerTile());
                } else 
                {
                    mainTiles.RemoveTiles(tileAction.GetTiles());
                }
                break;
            default:
                break;
        }
    }
    public List<TileAction> GetPossibleTileActionsFromOfferedTile(Tile offeredTile, Player offeringPlayer)
    {
        return mainTiles.GetPossibleTileActionsFromOfferedTile(offeredTile, IsPreviousPlayer(offeringPlayer));
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
    private void DrawTiles(int count, TileQueue tileQueue)
    {
        for (int i = 0; i < count; i++)
        {
            DrawTile(tileQueue);
        }
    }
    private bool IsPreviousPlayer(Player player)
    {
        if (this.id == 0)
        {
            return player.id == MAX_PLAYER_ID;
        }
        else
        {
            return player.id - this.id == -1;
        }
    }
}