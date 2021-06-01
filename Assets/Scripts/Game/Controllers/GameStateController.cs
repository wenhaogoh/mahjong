using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateController : MonoBehaviour
{
    public const int DISCARD_TIMER_DURATION = 5;
    public const int OFFER_TIMER_DURATION = 5;
    public const int PRE_GAME_DELAY = 2;
    public const int AUTO_PLAY_DELAY = 2;
    public const int HU_DELAY = 5;
    public TilesContainerController player0MainTilesContainerController;
    public TilesContainerController player0FlowerTilesContainerController;
    public TilesContainerController opponent1MainTilesContainerController;
    public TilesContainerController opponent1FlowerTilesContainerController;
    public TilesContainerController opponent2MainTilesContainerController;
    public TilesContainerController opponent2FlowerTilesContainerController;
    public TilesContainerController opponent3MainTilesContainerController;
    public TilesContainerController opponent3FlowerTilesContainerController;
    public DiscardedTilesContainerController discardedTilesContainerController;
    public TileActionsContainerController huActionContainerController;
    public TileActionsContainerController tileActionsContainerController;
    public TileQueueContainersController tileQueueContainersController;
    public static GameStateController instance = null;
    public GameStates gameState;
    private TurnProcessor turnProcessor;
    private IEnumerator offerTimerCoroutine;
    private IEnumerator discardTimerCoroutine;
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
    void Start()
    {
        gameState = GameStates.PROCESSING;
        turnProcessor = new TurnProcessor();
        turnProcessor.NewGame();
    }
    void Update()
    {
        switch (gameState)
        {
            case GameStates.PLAYER0_DRAWING:
                turnProcessor.DrawPlayer0Tile();
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
    public void DiscardPlayer0Tile(int index)
    {
        ClearTileActionsDisplay();
        StopCoroutine(discardTimerCoroutine);
        turnProcessor.DiscardPlayer0Tile(index);
    }
    public void ExecutePlayer0TileAction(TileAction tileAction)
    {
        ClearTileActionsDisplay();
        StopCoroutine(discardTimerCoroutine);
        turnProcessor.ExecutePlayer0TileAction(tileAction);
    }
    public void ProcessPlayer0TileActionRequest(TileAction tileAction)
    {
        ClearTileActionsDisplay();
        StopOfferTimerCoroutine();
        turnProcessor.ProcessPlayer0TileActionRequest(tileAction);
    }
    public void RefreshAllPlayersTilesDisplay(bool showOpponentContent, bool isAfterDrawingTile)
    {
        RefreshPlayer0TilesDisplays(isAfterDrawingTile);
        RefreshOpponent1TilesDisplay(showOpponentContent);
        RefreshOpponent2TilesDisplay(showOpponentContent);
        RefreshOpponent3TilesDisplay(showOpponentContent);
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
    public void DisplayRemoveLastDiscardedTile()
    {
        discardedTilesContainerController.RemoveLastDiscardedTile();
    }
    public void DisplayRemoveTileFromTileQueueFront()
    {
        tileQueueContainersController.RemoveTileFromFront();
    }
    public void DisplayRemoveTileFromTileQueueBack()
    {
        tileQueueContainersController.RemoveTileFromBack();
    }
    public void DisplayRollDiceToSetPlayerWinds(int diceValue1, int diceValue2, int diceValue3)
    {
        int totalDiceValue = diceValue1 + diceValue2 + diceValue3; // TODO: Display dice roll
        turnProcessor.SetPlayerWindsBeforeGameStart(totalDiceValue);
    }
    public void DisplayRollDiceToDrawStartingTiles(int diceValue1, int diceValue2, int diceValue3)
    {
        int totalDiceValue = diceValue1 + diceValue2 + diceValue3; // TODO: Display dice roll
        StartCoroutine(StartRoundCoroutine(turnProcessor.GetEastWindPlayerId(), totalDiceValue));
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
    public void StartHuTimerCoroutine(int huPlayerId)
    {
        StartCoroutine(HuTimerCoroutine(huPlayerId));
    }
    private void ClearTileActionsDisplay()
    {
        huActionContainerController.ClearTileActionsDisplay();
        tileActionsContainerController.ClearTileActionsDisplay();
    }
    private void RefreshPlayer0TilesDisplays(bool isAfterDrawingTile = false)
    {
        DisplayPlayer0MainTiles(isAfterDrawingTile);
        DisplayPlayer0FlowerTiles();
    }
    private void RefreshOpponent1TilesDisplay(bool showContent)
    {
        DisplayOpponent1MainTiles(showContent);
        DisplayOpponent1FlowerTiles();
    }
    private void RefreshOpponent2TilesDisplay(bool showContent)
    {
        DisplayOpponent2MainTiles(showContent);
        DisplayOpponent2FlowerTiles();
    }
    private void RefreshOpponent3TilesDisplay(bool showContent)
    {
        DisplayOpponent3MainTiles(showContent);
        DisplayOpponent3FlowerTiles();
    }
    private void DisplayPlayer0MainTiles(bool isAfterDrawingTile)
    {
        player0MainTilesContainerController.DisplayLargeTileButtons(turnProcessor.GetPlayer0MainTiles(), isAfterDrawingTile);
    }
    private void DisplayPlayer0FlowerTiles()
    {
        player0FlowerTilesContainerController.DisplaySmallTiles(turnProcessor.GetPlayer0FlowerTiles());
    }
    private void DisplayOpponent1MainTiles(bool showContent)
    {
        opponent1MainTilesContainerController.DisplaySmallTiles(turnProcessor.GetOpponent1MainTiles(), showContent);
    }
    private void DisplayOpponent1FlowerTiles()
    {
        opponent1FlowerTilesContainerController.DisplaySmallTiles(turnProcessor.GetOpponent1FlowerTiles());
    }
    private void DisplayOpponent2MainTiles(bool showContent)
    {
        opponent2MainTilesContainerController.DisplaySmallTiles(turnProcessor.GetOpponent2MainTiles(), showContent);
    }
    private void DisplayOpponent2FlowerTiles()
    {
        opponent2FlowerTilesContainerController.DisplaySmallTiles(turnProcessor.GetOpponent2FlowerTiles());
    }
    private void DisplayOpponent3MainTiles(bool showContent)
    {
        opponent3MainTilesContainerController.DisplaySmallTiles(turnProcessor.GetOpponent3MainTiles(), showContent);
    }
    private void DisplayOpponent3FlowerTiles()
    {
        opponent3FlowerTilesContainerController.DisplaySmallTiles(turnProcessor.GetOpponent3FlowerTiles());
    }
    public void DisplayDiscardedTile(Tile discardedTile, int discardingPlayerId)
    {
        discardedTilesContainerController.DiscardTile(discardedTile, discardingPlayerId);
    }
    private void StartAutoPlayCoroutine()
    {
        StartCoroutine(AutoPlayCoroutine());
    }
    private void StopOfferTimerCoroutine()
    {
        StopCoroutine(offerTimerCoroutine);
    }
    private IEnumerator StartRoundCoroutine(int eastWindPlayerId, int diceValueForWhereToStartDrawingTiles)
    {
        tileQueueContainersController.Reset(eastWindPlayerId, diceValueForWhereToStartDrawingTiles);
        RefreshAllPlayersTilesDisplay(false, false);
        yield return new WaitForSecondsRealtime(PRE_GAME_DELAY);
        turnProcessor.StartRound();
        RefreshAllPlayersTilesDisplay(false, false);
    }
    private IEnumerator DiscardTimerCoroutine()
    {
        yield return new WaitForSeconds(DISCARD_TIMER_DURATION);
        DiscardPlayer0Tile(0);
    }
    private IEnumerator OfferTimerCoroutine()
    {
        yield return new WaitForSeconds(OFFER_TIMER_DURATION);
        ProcessPlayer0TileActionRequest(null);
    }
    private IEnumerator HuTimerCoroutine(int huPlayerId)
    {
        yield return new WaitForSecondsRealtime(HU_DELAY);
        turnProcessor.NextRound(huPlayerId);
    }
    private IEnumerator AutoPlayCoroutine()
    {
        turnProcessor.AutoPlayDraw();
        yield return new WaitForSecondsRealtime(AUTO_PLAY_DELAY);
        turnProcessor.AutoPlayDiscard();
    }
}