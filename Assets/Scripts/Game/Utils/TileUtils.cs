using UnityEngine;

public class TileUtils
{
    public static Tile GetRedDragonTile()
    {
        return GetTile(TileTypes.HONOUR, (int)HonourTypes.RED_DRAGON);
    }
    public static Tile GetTile(TileTypes tileType, int value)
    {
        Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile.SetTileType(tileType);
        tile.SetValue(value);
        return tile;
    }
}