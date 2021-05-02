using UnityEngine;

public class TilesContainerController : MonoBehaviour
{
    public GameObject smallTilePrefab;
    public GameObject largeTilePrefab;
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
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject smallTileObject = Instantiate(smallTilePrefab);
            smallTileObject.GetComponent<TileController>().tile = tile;
            smallTileObject.transform.SetParent(this.transform);
        }
    }
    public void DisplayLargeTiles(TilesContainer tilesContainer)
    {
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject largeTileObject = Instantiate(largeTilePrefab);
            largeTileObject.GetComponent<TileController>().tile = tile;
            largeTileObject.transform.SetParent(this.transform);
        }
    }
}
