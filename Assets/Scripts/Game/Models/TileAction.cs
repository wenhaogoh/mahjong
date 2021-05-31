public class TileAction
{
    private TileActionTypes tileActionType;
    private TilesContainer tiles;

    private Tile triggerTile;
    public TileAction(TileActionTypes tileActionType, TilesContainer tiles, Tile triggerTile)
    {
        this.tileActionType = tileActionType;
        this.tiles = tiles;
        this.triggerTile = triggerTile;
    }
    public TileActionTypes GetTileActionType()
    {
        return this.tileActionType;
    }
    public TilesContainer GetTiles()
    {
        return this.tiles;
    }
    public TilesContainer GetTilesWithoutTriggerTile()
    {
        TilesContainer filteredTiles = new TilesContainer();
        foreach (Tile tile in tiles.GetTiles())
        {
            if (tile != triggerTile)
            {
                filteredTiles.AddTile(tile);
            }
        }
        return filteredTiles;
    }
}