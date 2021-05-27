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
    public void DisplaySmallTiles(TilesContainer tilesContainer, bool showTileContent = true)
    {
        DestroyAllChildren();
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject smallTileObject = Instantiate(smallTilePrefab);
            smallTileObject.GetComponent<TileController>().tile = tile;
            smallTileObject.GetComponent<TileController>().showTileContent = showTileContent;
            smallTileObject.transform.SetParent(this.transform, false);
        }
    }
    public void DisplayLargeTiles(TilesContainer tilesContainer, bool showTileContent = true)
    {
        DestroyAllChildren();
        foreach (Tile tile in tilesContainer.GetTiles())
        {
            GameObject largeTileObject = Instantiate(largeTilePrefab);
            largeTileObject.GetComponent<TileController>().tile = tile;
            largeTileObject.GetComponent<TileController>().showTileContent = showTileContent;
            largeTileObject.transform.SetParent(this.transform);
        }
    }
    public void DisplayLargeTileButtons(TilesContainer tilesContainer, bool isAfterDrawingTile, bool showTileContent = true)
    {
        List<Tile> tiles = tilesContainer.GetTiles();
        DestroyAllChildren();
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            GameObject largeTileButtonObject;
            if (isAfterDrawingTile && i == tiles.Count - 1)
            {
                largeTileButtonObject = Instantiate(newLargeTileButtonPrefab);
                largeTileButtonObject.GetComponentInChildren<TileController>().tile = tile;
                largeTileButtonObject.GetComponentInChildren<TileController>().index = i;
                largeTileButtonObject.GetComponentInChildren<TileController>().showTileContent = showTileContent;
            }
            else
            {
                largeTileButtonObject = Instantiate(largeTileButtonPrefab);
                largeTileButtonObject.GetComponent<TileController>().tile = tile;
                largeTileButtonObject.GetComponent<TileController>().index = i;
                largeTileButtonObject.GetComponentInChildren<TileController>().showTileContent = showTileContent;
            }
            largeTileButtonObject.transform.SetParent(this.transform);
        }
    }
    private void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
