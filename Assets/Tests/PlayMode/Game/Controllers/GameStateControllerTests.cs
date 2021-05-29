using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameStateControllerTests
{
    [UnityTest]
    public IEnumerator Start()
    {
        GameStateController gameStateController = GetGameStateController();
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual(14, gameStateController.player0MainTilesContainerController.gameObject.transform.childCount);
        Assert.AreEqual(13, gameStateController.opponent1MainTilesContainerController.gameObject.transform.childCount);
        Assert.AreEqual(13, gameStateController.opponent2MainTilesContainerController.gameObject.transform.childCount);
        Assert.AreEqual(13, gameStateController.opponent3MainTilesContainerController.gameObject.transform.childCount);
        yield return new WaitForSecondsRealtime(GameStateController.DISCARD_TIMER_DURATION);
        Assert.AreEqual(13, gameStateController.player0MainTilesContainerController.gameObject.transform.childCount);
        Assert.AreEqual(14, gameStateController.opponent1MainTilesContainerController.gameObject.transform.childCount);
        Assert.AreEqual(13, gameStateController.opponent2MainTilesContainerController.gameObject.transform.childCount);
        Assert.AreEqual(13, gameStateController.opponent3MainTilesContainerController.gameObject.transform.childCount);
    }
    private GameStateController GetGameStateController()
    {
        GameObject gameStateGameObject = new GameObject();
        gameStateGameObject.AddComponent<GameStateController>();
        GameStateController gameStateController = gameStateGameObject.GetComponent<GameStateController>();
        gameStateController.player0MainTilesContainerController = GetTilesContainerController();
        gameStateController.player0FlowerTilesContainerController = GetTilesContainerController();
        gameStateController.opponent1MainTilesContainerController = GetTilesContainerController();
        gameStateController.opponent1FlowerTilesContainerController = GetTilesContainerController();
        gameStateController.opponent2MainTilesContainerController = GetTilesContainerController();
        gameStateController.opponent2FlowerTilesContainerController = GetTilesContainerController();
        gameStateController.opponent3MainTilesContainerController = GetTilesContainerController();
        gameStateController.opponent3FlowerTilesContainerController = GetTilesContainerController();
        gameStateController.discardedTilesContainerController = GetDiscardedTilesContainerController();
        gameStateController.huActionContainerController = GetTileActionsContainerController();
        gameStateController.tileActionsContainerController = GetTileActionsContainerController();
        return gameStateController;
    }
    private TilesContainerController GetTilesContainerController()
    {
        GameObject tilesContainerGameObject = new GameObject();
        tilesContainerGameObject.AddComponent<TilesContainerController>();
        TilesContainerController tilesContainerController = tilesContainerGameObject.GetComponent<TilesContainerController>();
        tilesContainerController.smallTilePrefab = Resources.Load("Prefabs/SmallTile") as GameObject;
        tilesContainerController.largeTilePrefab = Resources.Load("Prefabs/LargeTile") as GameObject;
        tilesContainerController.largeTileButtonPrefab = Resources.Load("Prefabs/LargeTileButton") as GameObject;
        tilesContainerController.newLargeTileButtonPrefab = Resources.Load("Prefabs/NewLargeTileButton") as GameObject;
        return tilesContainerController;
    }
    private DiscardedTilesContainerController GetDiscardedTilesContainerController()
    {
        GameObject discardedTilesContainerGameObject = new GameObject();
        discardedTilesContainerGameObject.AddComponent<DiscardedTilesContainerController>();
        DiscardedTilesContainerController discardedTilesContainerController = discardedTilesContainerGameObject.GetComponent<DiscardedTilesContainerController>();
        discardedTilesContainerController.discardedTilePrefab = Resources.Load("Prefabs/DiscardedTile") as GameObject;
        return discardedTilesContainerController;
    }
    private TileActionsContainerController GetTileActionsContainerController()
    {
        GameObject tilesActionGameObject = new GameObject();
        tilesActionGameObject.AddComponent<TileActionsContainerController>();
        TileActionsContainerController tileActionsContainerController = tilesActionGameObject.GetComponent<TileActionsContainerController>();
        tileActionsContainerController.tileActionPrefab = Resources.Load("Prefabs/TileAction") as GameObject;
        return tileActionsContainerController;
    }
}
