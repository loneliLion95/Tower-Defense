using UnityEngine;

/// <summary>
/// Container with unique data for each enemy wave
/// </summary>
[CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave")]
public class EnemyWaveData : ScriptableObject
{
    public float duration;
    public float spawnRate;

    /// <summary>
    /// Coefficient that determines the number of enemies in the wave
    /// </summary>
    [Range(0, 1)]
    public float enemyCoefficient;
}
