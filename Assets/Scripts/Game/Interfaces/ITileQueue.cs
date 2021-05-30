public interface ITileQueue
{
    public Tile DrawFromFront();
    public Tile DrawFromBack();
    public void Randomize();
    public int Count();
}