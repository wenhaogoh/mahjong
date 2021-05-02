using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Tile : ScriptableObject, IComparable<Tile>
{
    private TileTypes tileType;
    private int value;
    public Tile(TileTypes type, int value)
    {
        this.tileType = type;
        this.value = value;
    }
    public void SetType(TileTypes type)
    {
        this.tileType = type;
    }
    public void SetValue(int value)
    {
        this.value = value;
    }
    public TileTypes GetTileType()
    {
        return this.tileType;
    }
    public int GetValue()
    {
        return this.value;
    }
    public bool IsFlower()
    {
        return this.tileType == TileTypes.Flower;
    }
    public int CompareTo(Tile obj)
    {
        if (obj.Equals(this)) return 0;

        if (obj.tileType < this.tileType)
        {
            return 1;
        }
        else if (obj.tileType > this.tileType)
        {
            return -1;
        }
        else
        {
            return this.value.CompareTo(obj.value);
        }
    }
    public override string ToString()
    {
        return tileType.ToString() + " " + value.ToString();
    }
}