public class Player
{
    private int id;
    private Winds wind;
    private TilesContainer mainTiles;
    private TilesContainer tripletTiles;
    private TilesContainer flowerTiles;
    public Player(int id)
    {
        this.id = id;
        this.mainTiles = new TilesContainer();
        this.tripletTiles = new TilesContainer();
        this.flowerTiles = new TilesContainer();
    }
    public void DrawTile(TileQueue tileQueue)
    {
        Tile tile = tileQueue.DrawFromFront();
        while (tile.IsFlower())
        {
            flowerTiles.Add(tile);
            tile = tileQueue.DrawFromBack();
        }
        mainTiles.Add(tile);
    }
    private void DrawTiles(int count, TileQueue tileQueue)
    {
        for (int i = 0; i < count; i++)
        {
            DrawTile(tileQueue);
        }
    }
    public void DrawStartingTiles(TileQueue tileQueue)
    {
        int count = 13;
        if (wind == Winds.East)
        {
            count += 1;
        }
        DrawTiles(count, tileQueue);
    }
    public void SortTiles()
    {
        mainTiles.Sort();
        flowerTiles.Sort();
    }
    public TilesContainer GetMainTiles()
    {
        return mainTiles;
    }
    public TilesContainer GetFlowerTiles()
    {
        return flowerTiles;
    }
    public void DiscardTile(int tileIndex, TilesContainer discardedTilesContainer)
    {
        Tile toDiscard = mainTiles.RemoveTile(tileIndex);
        discardedTilesContainer.Add(toDiscard);
    }
    public void SetWind(Winds wind)
    {
        this.wind = wind;
    }
    public Winds GetWind()
    {
        return this.wind;
    }
    public int GetId()
    {
        return this.id;
    }
}