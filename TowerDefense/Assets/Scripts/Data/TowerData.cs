using UnityEngine;

/// <summary>
/// Container with unique data for each tower
/// </summary>
[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower")]
public class TowerData : ScriptableObject
{
    public int buildPrice;
    public int damage;
    public float range;
    public float shootInterval;
}
