using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Enemy waves' management
/// </summary>
public class EnemyWaves : Loader<EnemyWaves>
{
    public EnemyWaveData[] wavesData;
    /// <summary>
    /// Prefabs of all existing in the game enemies
    /// </summary>
    public GameObject[] enemies;
    /// <summary>
    /// Text that shows when all enemies are dead
    /// </summary>
    public TextMeshProUGUI winText;

    [HideInInspector]
    public bool lastWave;
    /// <summary>
    /// All enemies of the last wave
    /// </summary>
    [HideInInspector]
    public static List<Enemy> lastEnemies = new List<Enemy>();

    private Timer timer;
    private Balance balance;
    
    void Start()
    {
        timer = Timer.Instance;
        balance = Balance.Instance;
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (lastWave && lastEnemies.Count == 0)
            winText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Spawn enemies with certain time intervals
    /// </summary>
    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timer.timeUntilFirstWave);

        // In my case amount of enemy types is equals to amount of enemy waves
        for (int i = 0; i < enemies.Length; i++)
        {
            if (i == wavesData.Length - 1)
                lastWave = true;

            timer.currentTime = wavesData[i].duration;
            // Display the number of the current wave
            balance.wavesCount.text = (i + 1) + " / " + wavesData.Length;
            float currentWaveDuration = wavesData[i].duration;
            int enemiesInWave = Mathf.RoundToInt(wavesData[i].enemyCoefficient * currentWaveDuration/wavesData[i].spawnRate);

            for (int j = 0; j < enemiesInWave; j++)
            {
                GameObject newEnemy = Instantiate(enemies[i]);
                newEnemy.transform.position = transform.position;
                currentWaveDuration -= wavesData[i].spawnRate;

                if (lastWave)
                    lastEnemies.Add(newEnemy.GetComponent<Enemy>());

                yield return new WaitForSeconds(wavesData[i].spawnRate);
            }

            // Waiting remaining time for the next wave
            yield return new WaitForSeconds(currentWaveDuration);
        }
    }
}
