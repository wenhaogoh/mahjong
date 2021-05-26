using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateController : MonoBehaviour
{
    private const int DISCARD_TIMER_DURATION = 5;
    private const int OFFER_TIMER_DURATION = 5;
    private const int AUTO_PLAY_DELAY = 2;
    public TilesContainerController playerMainTilesContainerController;
    public TilesContainerController playerFlowerTilesContainerController;
    public TilesContainerController discardedTilesContainerController;
    public TileActionsContainerController huActionContainerController;
    public TileActionsContainerController tileActionsContainerController;
    public static GameStateController instance = null;
    public GameStates gameState;
    private TurnProcessor turnProcessor;
    private IEnumerator offerTimerCoroutine;
    private IEnumerator discardTimerCoroutine;
    void Awake()
    {
        // Singleton Pattern
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
                StartAutoPlayCoroutine();
                break;
            case GameStates.OPPONENT2_DRAWING:
                StartAutoPlayCoroutine();
                break;
            case GameStates.OPPONENT3_DRAWING:
                StartAutoPlayCoroutine();
                break;
            default:
                break;
        }
    }
    public void DiscardPlayerTile(int index)
    {
        ClearTileActionsDisplay();
        StopCoroutine(discardTimerCoroutine);
        turnProcessor.DiscardPlayerTile(index);
    }
    public void ExecutePlayerTileAction(TileAction tileAction)
    {
        ClearTileActionsDisplay();
        StopCoroutine(discardTimerCoroutine);
        turnProcessor.ExecutePlayerTileAction(tileAction);
    }
    public void ProcessPlayerTileActionRequest(TileAction tileAction)
    {
        ClearTileActionsDisplay();
        StopOfferTimerCoroutine();
        turnProcessor.ProcessPlayerTileActionRequest(tileAction);
    }
    public void RefreshDisplays()
    {
        DisplayPlayerMainTiles();
        DisplayPlayerFlowerTiles();
        DisplayDiscardedTiles();
    }
    public void DisplayTileActions(List<TileAction> tileActions)
    {
        foreach (TileAction tileAction in tileActions)
        {
            switch (tileAction.GetTileActionType())
            {
                case TileActionTypes.HU:
                    huActionContainerController.DisplayTileAction(tileAction);
                    break;
                default:
                    tileActionsContainerController.DisplayTileAction(tileAction);
                    break;
            }

        }
    }
    public void StartDiscardTimerCoroutine()
    {
        discardTimerCoroutine = DiscardTimerCoroutine();
        StartCoroutine(discardTimerCoroutine);
    }
    public void StartOfferTimerCoroutine()
    {
        offerTimerCoroutine = OfferTimerCoroutine();
        StartCoroutine(offerTimerCoroutine);
    }
    private void ClearTileActionsDisplay()
    {
        huActionContainerController.ClearTileActionsDisplay();
        tileActionsContainerController.ClearTileActionsDisplay();
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
    private void StartAutoPlayCoroutine()
    {
        StartCoroutine(AutoPlayCoroutine());
    }
    private void StopOfferTimerCoroutine()
    {
        StopCoroutine(offerTimerCoroutine);
    }
    private IEnumerator DiscardTimerCoroutine()
    {
        yield return new WaitForSeconds(DISCARD_TIMER_DURATION);
        DiscardPlayerTile(0);
    }
    private IEnumerator OfferTimerCoroutine()
    {
        yield return new WaitForSeconds(OFFER_TIMER_DURATION);
        ProcessPlayerTileActionRequest(null);
    }
    private IEnumerator AutoPlayCoroutine()
    {
        turnProcessor.AutoPlayDraw();
        yield return new WaitForSecondsRealtime(AUTO_PLAY_DELAY);
        turnProcessor.AutoPlayDiscard();
    }
}