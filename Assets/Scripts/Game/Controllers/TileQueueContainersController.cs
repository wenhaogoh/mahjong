using UnityEngine;
using System.Collections;
using Nito.Collections;

public class TileQueueContainersController : MonoBehaviour
{
    public TileQueueContainerController tileQueueContainerController0;
    public TileQueueContainerController tileQueueContainerController1;
    public TileQueueContainerController tileQueueContainerController2;
    public TileQueueContainerController tileQueueContainerController3;
    private TileQueueContainerController[] tileQueueContainerControllers;
    private Deque<GameObject> tileQueueTileGameObjects;
    void Start()
    {

    }
    void Update()
    {

    }
    public void Reset(int eastWindPlayerId, int diceValueForWhereToStartDrawingTiles)
    {
        StartCoroutine(ResetCoroutine(eastWindPlayerId, diceValueForWhereToStartDrawingTiles));
    }
    public void RemoveTileFromFront()
    {
        GameObject tileQueueTileGameObject = tileQueueTileGameObjects.RemoveFromFront();
        TileQueueTileController tileQueueTileController = tileQueueTileGameObject.GetComponent<TileQueueTileController>();
        tileQueueTileController.DecreaseCount();
        if (tileQueueTileController.IsEmpty())
        {
            Destroy(tileQueueTileGameObject);
        }
        else
        {
            tileQueueTileGameObjects.AddToFront(tileQueueTileGameObject);
        }
    }
    public void RemoveTileFromBack()
    {
        GameObject tileQueueTileGameObject = tileQueueTileGameObjects.RemoveFromBack();
        TileQueueTileController tileQueueTileController = tileQueueTileGameObject.GetComponent<TileQueueTileController>();
        tileQueueTileController.DecreaseCount();
        if (tileQueueTileController.IsEmpty())
        {
            Destroy(tileQueueTileGameObject);
        }
        else
        {
            tileQueueTileGameObjects.AddToBack(tileQueueTileGameObject);
        }
    }
    private int GetNextTileQueueContainerIndex(int currentIndex)
    {
        int nextIndex = currentIndex + 1;
        if (nextIndex >= tileQueueContainerControllers.Length)
        {
            nextIndex = 0;
        }
        return nextIndex;
    }
    private void AddGridLayoutGroupComponents()
    {
        foreach (TileQueueContainerController tileQueueContainerController in tileQueueContainerControllers)
        {
            tileQueueContainerController.AddGridLayoutGroupComponent();
        }
    }
    private void RemoveGridLayoutGroupComponents()
    {
        foreach (TileQueueContainerController tileQueueContainerController in tileQueueContainerControllers)
        {
            tileQueueContainerController.RemoveGridLayoutGroupComponent();
        }
    }
    private void LoadGameObjects(int eastWindPlayerId)
    {
        int currentIndex = eastWindPlayerId;
        for (int i = 0; i < 4; i++)
        {
            TileQueueContainerController tileQueueContainerController = tileQueueContainerControllers[currentIndex];
            for (int j = tileQueueContainerController.transform.childCount - 1; j >= 0; j--)
            {
                tileQueueTileGameObjects.AddToBack(tileQueueContainerController.transform.GetChild(j).gameObject);
            }
            currentIndex = GetNextTileQueueContainerIndex(currentIndex);
        }
    }
    private void RearrangeGameObjects(int diceValueForWhereToStartDrawingTiles)
    {
        for (int i = 0; i < diceValueForWhereToStartDrawingTiles; i++)
        {
            tileQueueTileGameObjects.AddToBack(tileQueueTileGameObjects.RemoveFromFront());
        }
    }
    private IEnumerator ResetCoroutine(int eastWindPlayerId, int diceValueForWhereToStartDrawingTiles)
    {
        tileQueueContainerControllers = new TileQueueContainerController[]
        {
            tileQueueContainerController0,
            tileQueueContainerController1,
            tileQueueContainerController2,
            tileQueueContainerController3
        };
        tileQueueTileGameObjects = new Deque<GameObject>();
        AddGridLayoutGroupComponents();
        switch (eastWindPlayerId)
        {
            case PlayerUtils.PLAYER0_ID:
            case PlayerUtils.OPPONENT2_ID:
                tileQueueContainerController0.SpawnTiles(19);
                tileQueueContainerController1.SpawnTiles(18);
                tileQueueContainerController2.SpawnTiles(19);
                tileQueueContainerController3.SpawnTiles(18);
                break;
            case PlayerUtils.OPPONENT1_ID:
            case PlayerUtils.OPPONENT3_ID:
                tileQueueContainerController0.SpawnTiles(18);
                tileQueueContainerController1.SpawnTiles(19);
                tileQueueContainerController2.SpawnTiles(18);
                tileQueueContainerController3.SpawnTiles(19);
                break;
            default:
                break;
        }
        yield return 0; // Skip a frame for game objects to load
        RemoveGridLayoutGroupComponents();
        LoadGameObjects(eastWindPlayerId);
        RearrangeGameObjects(diceValueForWhereToStartDrawingTiles);
    }
}