using UnityEngine;

public class TilesContainerController : MonoBehaviour
{
    public GameObject smallTilePrefab;
    public GameObject largeTilePrefab;
    public GameObject largeTileButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void DisplaySmallTiles(TilesContainer tilesContainer)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject smallTileObject = Instantiate(smallTilePrefab);
            smallTileObject.GetComponent<TileController>().tile = tile;
            smallTileObject.transform.SetParent(this.transform);
        }
    }
    public void DisplayLargeTiles(TilesContainer tilesContainer)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject largeTileObject = Instantiate(largeTilePrefab);
            largeTileObject.GetComponent<TileController>().tile = tile;
            largeTileObject.transform.SetParent(this.transform);
        }
    }
    public void DisplayLargeTileButtons(TilesContainer tilesContainer)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject largeTileButtonObject = Instantiate(largeTileButtonPrefab);
            largeTileButtonObject.GetComponent<TileController>().tile = tile;
            largeTileButtonObject.transform.SetParent(this.transform);
        }
    }
}
