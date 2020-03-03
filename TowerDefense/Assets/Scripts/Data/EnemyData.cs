using UnityEngine;


/// <summary>
/// Container with unique data for each enemy
/// </summary>
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int damage;
    public float movingSpeed;

    /// <summary>
    /// Minimum amount of coins for killing enemy
    /// </summary>
    public int minCoins;

    /// <summary>
    /// Maximum amount of coins for killing enemy
    /// </summary>
    public int maxCoins;
}
