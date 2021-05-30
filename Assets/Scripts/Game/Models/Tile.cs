using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Tile : ScriptableObject, IComparable<Tile>
{
    private TileTypes tileType;
    private int value;
    public bool IsFlower()
    {
        return this.tileType == TileTypes.FLOWER;
    }
    public TileTypes GetTileType()
    {
        return this.tileType;
    }
    public int GetValue()
    {
        return this.value;
    }
    public void SetTileType(TileTypes type)
    {
        this.tileType = type;
    }
    public void SetValue(int value)
    {
        this.value = value;
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
    public override bool Equals(object other)
    {
        if (other == null)
        {
            return false;
        }
        if (!(other as Tile))
        {
            return false;
        }
        return (this.tileType == ((Tile)other).tileType) && (this.value == ((Tile)other).value);
    }
    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }
    public override string ToString()
    {
        return tileType + " : " + value;
    }
}