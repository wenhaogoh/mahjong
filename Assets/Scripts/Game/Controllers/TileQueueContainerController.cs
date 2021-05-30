using UnityEngine;
using UnityEngine.UI;

public class TileQueueContainerController : MonoBehaviour
{
    public GameObject tileQueueTilePrefab;
    void Start()
    {

    }
    void Update()
    {

    }
    public void AddGridLayoutGroupComponent()
    {
        RectTransform tileQueueTilePrefabRectTransform = tileQueueTilePrefab.GetComponent<RectTransform>();
        gameObject.AddComponent<GridLayoutGroup>();
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        gridLayoutGroup.cellSize = new Vector2(tileQueueTilePrefabRectTransform.rect.width, tileQueueTilePrefabRectTransform.rect.height);
    }
    public void RemoveGridLayoutGroupComponent()
    {
        Destroy(GetComponent<GridLayoutGroup>());
    }
    public void SpawnTiles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject tileQueueTileGameObject = Instantiate(tileQueueTilePrefab);
            tileQueueTileGameObject.transform.SetParent(this.transform, false);
        }
    }
}
