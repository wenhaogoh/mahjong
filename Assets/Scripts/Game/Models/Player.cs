using System.Collections.Generic;

public class Player
{
    private int id;
    private Winds wind;
    private TilesContainer mainTiles;
    private TilesContainer flowerTiles;
    private const int STARTING_TILES_COUNT = 13;
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
    public void ExecuteTileAction(TileAction tileAction)
    {
        switch (tileAction.GetTileActionType())
        {
            case TileActionTypes.KONG:
                flowerTiles.AddTiles(tileAction.GetTiles());
                mainTiles.RemoveTiles(tileAction.GetTiles());
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
        if (GetId() == 0)
        {
            return player.GetId() == 4;
        }
        else
        {
            return player.GetId() - GetId() == -1;
        }
    }
}