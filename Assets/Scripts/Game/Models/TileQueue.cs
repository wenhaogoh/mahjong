using Nito.Collections;

public class TileQueue
{
    private Deque<Tile> deque;
    public static TileQueue GetRandomizedTileQueue()
    {
        TileQueue tileQueue = new TileQueue();
        tileQueue.Randomize();
        return tileQueue;
    }
    public TileQueue()
    {
        deque = new Deque<Tile>(TileUtils.GenerateTiles());
    }
    public Tile DrawFromFront()
    {
        return deque.RemoveFromFront();
    }
    public Tile DrawFromBack()
    {
        return deque.RemoveFromBack();
    }
    public void Randomize()
    {
        RandomNumberGenerator.Shuffle<Tile>(deque);
    }
    public int Count()
    {
        return deque.Count;
    }
}