using Nito.Collections;

public class TileQueue : ITileQueue
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
        GameStateController.instance.DisplayRemoveTileFromTileQueueFront();
        return deque.RemoveFromFront();
    }
    public Tile DrawFromBack()
    {
        GameStateController.instance.DisplayRemoveTileFromTileQueueBack();
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
    public bool IsEmpty()
    {
        return deque.Count == 0;
    }
}