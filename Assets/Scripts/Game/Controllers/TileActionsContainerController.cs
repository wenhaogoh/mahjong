using UnityEngine;

public class TileActionsContainerController : MonoBehaviour
{
    public GameObject tileActionPrefab;
    void Start()
    {

    }
    void Update()
    {

    }
    public void DisplayTileAction(TileAction tileAction)
    {
        GameObject tileActionObject = Instantiate(tileActionPrefab);
        TilesContainerController tilesContainerController = tileActionObject.transform.GetChild(0).GetComponent<TilesContainerController>();
        tileActionObject.GetComponent<TileActionController>().tileAction = tileAction;
        switch (tileAction.GetTileActionType())
        {
            case TileActionTypes.HU:
            case TileActionTypes.KONG:
            case TileActionTypes.CHOW:
            case TileActionTypes.PONG:
                tilesContainerController.DisplayLargeTiles(tileAction.GetTiles());
                tileActionObject.transform.SetParent(this.transform);
                break;
            default:
                break;
        }
    }
    public void ClearTileActionsDisplay()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
