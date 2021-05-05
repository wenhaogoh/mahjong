using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public TilesContainerController playerMainTilesContainerController;
    public TilesContainerController playerFlowerTilesContainerController;
    public TilesContainerController discardedTilesContainerController;
    public static GameStateController instance = null;
    public GameStates gameState;
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
        gameState = GameStates.PROCESSING;
        turnProcessor = new TurnProcessor();
        turnProcessor.NewGame();
    }
    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameStates.PLAYER0_DRAWING:
                turnProcessor.DrawPlayerTile();
                break;
            case GameStates.OPPONENT1_DRAWING:
                turnProcessor.AutoPlay();
                break;
            case GameStates.OPPONENT2_DRAWING:
                turnProcessor.AutoPlay();
                break;
            case GameStates.OPPONENT3_DRAWING:
                turnProcessor.AutoPlay();
                break;
            default:
                break;
        }
    }
    public void DiscardPlayerTile(int index)
    {
        turnProcessor.DiscardPlayerTile(index);
    }
    public void RefreshDisplays()
    {
        DisplayPlayerMainTiles();
        DisplayPlayerFlowerTiles();
        DisplayDiscardedTiles();
    }
    private void DisplayPlayerMainTiles()
    {
        playerMainTilesContainerController.DisplayLargeTileButtons(turnProcessor.GetPlayerMainTiles());
    }
    private void DisplayPlayerFlowerTiles()
    {
        playerFlowerTilesContainerController.DisplaySmallTiles(turnProcessor.GetPlayerFlowerTiles());
    }
    private void DisplayDiscardedTiles()
    {
        discardedTilesContainerController.DisplaySmallTiles(turnProcessor.GetDiscardedTiles());
    }
}