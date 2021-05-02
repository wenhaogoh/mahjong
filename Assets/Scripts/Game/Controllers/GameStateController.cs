using UnityEngine;

enum Directions
{
    East,
    South,
    West,
    North
}

public class GameStateController : MonoBehaviour
{
    public TilesContainerController playerMainTilesContainerController;
    public TilesContainerController playerFlowerTilesContainerController;

    private TileQueue tileQueue;

    private Player player;
    private Player opponent1; // Right
    private Player opponent2; // Top
    private Player opponent3; // Left

    // Start is called before the first frame update
    void Start()
    {
        tileQueue = new TileQueue();
        player = new Player();
        opponent1 = new Player();
        opponent2 = new Player();
        opponent3 = new Player();
        player.DrawStartingTiles(tileQueue);
        playerMainTilesContainerController.DisplayLargeTiles(player.GetMainTiles());
        playerFlowerTilesContainerController.DisplaySmallTiles(player.GetFlowerTiles());
    }
    // Update is called once per frame
    void Update()
    {

    }
}