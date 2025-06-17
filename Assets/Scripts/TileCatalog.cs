using UnityEngine;

[CreateAssetMenu(menuName = "Tile/TileCatalog")]
public class TileCatalog : ScriptableObject
{
    public Sprite[] shapes;
    public Color[] colors;
    public Sprite[] animals;
}