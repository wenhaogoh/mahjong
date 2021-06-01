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
        int tilesCount = tilesContainer.Count();
        EqualizeTileGameObjects(tilesCount, smallTilePrefab);
        SetTileContents(tilesContainer, showTileContent);
    }
    public void DisplayLargeTiles(TilesContainer tilesContainer, bool showTileContent = true)
    {
        int tilesCount = tilesContainer.Count();
        EqualizeTileGameObjects(tilesCount, largeTilePrefab);
        SetTileContents(tilesContainer, showTileContent);
    }
    public void DisplayLargeTileButtons(TilesContainer tilesContainer, bool isAfterDrawingTile, bool showTileContent = true)
    {
        int tilesCount = tilesContainer.Count();
        EqualizeTileGameObjects(tilesCount, largeTileButtonPrefab);
        SetTileContents(tilesContainer, showTileContent);
        if (isAfterDrawingTile)
        {
            Destroy(transform.GetChild(tilesCount - 1).gameObject);
            GameObject newLargeTileButtonGameObject = Instantiate(newLargeTileButtonPrefab);
            newLargeTileButtonGameObject.GetComponentInChildren<TileController>().SetTileContent(tilesCount - 1, tilesContainer.GetLastTile(), showTileContent);
            newLargeTileButtonGameObject.transform.SetParent(transform, false);
        }
    }
    private void EqualizeTileGameObjects(int tilesCount, GameObject prefab)
    {
        if (transform.childCount < tilesCount)
        {
            int difference = tilesCount - transform.childCount;
            for (int i = 0; i < difference; i++)
            {
                GameObject prefabObject = Instantiate(prefab);
                prefabObject.transform.SetParent(transform, false);
            }
        }
        else if (transform.childCount > tilesCount)
        {
            int difference = transform.childCount - tilesCount;
            for (int i = 0; i < difference; i++)
            {
                Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject); // transform.childCount does not change immediately after destroying child object
            }
        }
    }
    private void SetTileContents(TilesContainer tilesContainer, bool showTileContent)
    {
        List<Tile> tiles = tilesContainer.GetTiles();
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            transform.GetChild(i).GetComponent<TileController>().SetTileContent(i, tile, showTileContent);
        }
    }
}
