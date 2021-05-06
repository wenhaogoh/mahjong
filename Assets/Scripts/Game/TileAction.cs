public class TileAction
{
    private TileActionTypes tileActionType;
    private TilesContainer tiles;
    public TileAction(TileActionTypes tileActionType, TilesContainer tiles)
    {
        this.tileActionType = tileActionType;
        this.tiles = tiles;
    }
    public TileActionTypes GetTileActionType()
    {
        return this.tileActionType;
    }
    public TilesContainer GetTiles()
    {
        return this.tiles;
    }
}