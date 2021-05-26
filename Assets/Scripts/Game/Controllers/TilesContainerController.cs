using UnityEngine;
using System.Collections.Generic;

public class TilesContainerController : MonoBehaviour
{
    public GameObject smallTilePrefab;
    public GameObject largeTilePrefab;
    public GameObject largeTileButtonPrefab;
    public GameObject newLargeTileButtonPrefab;
    void Start()
    {

    }
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
    public void DisplayLargeTileButtons(TilesContainer tilesContainer, bool isAfterDrawingTile)
    {
        List<Tile> tiles = tilesContainer.GetTiles();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            GameObject largeTileButtonObject;
            if (isAfterDrawingTile && i == tiles.Count - 1)
            {
                largeTileButtonObject = Instantiate(newLargeTileButtonPrefab);
                largeTileButtonObject.GetComponentInChildren<TileController>().tile = tile;
            }
            else
            {
                largeTileButtonObject = Instantiate(largeTileButtonPrefab);
                largeTileButtonObject.GetComponent<TileController>().tile = tile;
            }
            largeTileButtonObject.transform.SetParent(this.transform);
        }
    }
}
