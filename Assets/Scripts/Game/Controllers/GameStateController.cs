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
    public TilesContainerController discardedTilesContainerController;

    public static GameStateController instance = null;

    private TileQueue tileQueue;
    private static TilesContainer discardedTilesContainer;

    private static Player player;
    private Player opponent1; // Right
    private Player opponent2; // Top
    private Player opponent3; // Left

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tileQueue = new TileQueue();
        discardedTilesContainer = new TilesContainer();
        player = new Player();
        opponent1 = new Player();
        opponent2 = new Player();
        opponent3 = new Player();
        player.DrawStartingTiles(tileQueue);
        opponent1.DrawStartingTiles(tileQueue);
        opponent2.DrawStartingTiles(tileQueue);
        opponent3.DrawStartingTiles(tileQueue);
        RefreshDisplays();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void DiscardPlayerTile(int index)
    {
        player.DiscardTile(index, discardedTilesContainer);
        RefreshDisplays();
    }
    private void DisplayPlayerMainTiles()
    {
        playerMainTilesContainerController.DisplayLargeTileButtons(player.GetMainTiles());
    }
    private void DisplayPlayerFlowerTiles()
    {
        playerFlowerTilesContainerController.DisplaySmallTiles(player.GetFlowerTiles());
    }
    private void DisplayDiscardedTiles()
    {
        discardedTilesContainerController.DisplayLargeTiles(discardedTilesContainer);
    }
    private void RefreshDisplays()
    {
        DisplayPlayerMainTiles();
        DisplayPlayerFlowerTiles();
        DisplayDiscardedTiles();
    }
    private void SetPlayersWinds()
    {

    }
}