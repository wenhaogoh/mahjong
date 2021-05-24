using UnityEngine;

public class TileActionsContainerController : MonoBehaviour
{
    public GameObject tileActionPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
                tilesContainerController.DisplayLargeTiles(tileAction.GetTiles());
                tileActionObject.transform.SetParent(this.transform);
                break;
            case TileActionTypes.KONG:
                tilesContainerController.DisplayLargeTiles(tileAction.GetTiles());
                tileActionObject.transform.SetParent(this.transform);
                break;
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
