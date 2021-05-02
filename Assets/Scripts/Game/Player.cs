public class Player
{
    private Winds winds;
    private TilesContainer mainTiles;
    private TilesContainer tripletTiles;
    private TilesContainer flowerTiles;
    public Player()
    {
        mainTiles = new TilesContainer();
        tripletTiles = new TilesContainer();
        flowerTiles = new TilesContainer();
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
        if (winds == Winds.East)
        {
            count += 1;
        }
        DrawTiles(count, tileQueue);
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
}