public interface ITileQueue
{
    Tile DrawFromFront();
    Tile DrawFromBack();
    int Count();
    bool IsEmpty();
}