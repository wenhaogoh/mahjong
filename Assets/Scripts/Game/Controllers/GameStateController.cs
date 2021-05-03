using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public TilesContainerController playerMainTilesContainerController;
    public TilesContainerController playerFlowerTilesContainerController;
    public TilesContainerController discardedTilesContainerController;

    public static GameStateController instance = null;
    public GameStates gameState;

    private TileQueue tileQueue;
    private TilesContainer discardedTilesContainer;

    private static Player player;
    private Player opponent1; // Right
    private Player opponent2; // Top
    private Player opponent3; // Left

    private TurnProcessor turnProcessor;

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
        Setup();
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
        turnProcessor.Process(discardedTilesContainer.GetLastTile());
    }
    private void Setup()
    {
        tileQueue = new TileQueue();
        discardedTilesContainer = new TilesContainer();
        player = new Player(0);
        opponent1 = new Player(1);
        opponent2 = new Player(2);
        opponent3 = new Player(3);
        turnProcessor = new TurnProcessor(player, opponent1, opponent2, opponent3);
        turnProcessor.SetWinds(player, Winds.East);
        player.DrawStartingTiles(tileQueue);
        opponent1.DrawStartingTiles(tileQueue);
        opponent2.DrawStartingTiles(tileQueue);
        opponent3.DrawStartingTiles(tileQueue);
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
}