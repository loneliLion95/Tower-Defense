using UnityEngine;
using TMPro;

/// <summary>
/// Countdown to the next wave
/// </summary>
public class Timer : Loader<Timer>
{
    public int timeUntilFirstWave;

    [HideInInspector]
    public float currentTime;

    private TextMeshProUGUI nextWave;
    private EnemyWaves enemyWaves;

    void Start()
    {
        enemyWaves = EnemyWaves.Instance;
        nextWave = GameObject.Find("nextWave").GetComponent<TextMeshProUGUI>();
        currentTime = timeUntilFirstWave;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (enemyWaves.lastWave)
            nextWave.text = "Last Wave";
        else
            nextWave.text = "Next Wave In: " + currentTime.ToString("0");

        if (currentTime <= 0)
            currentTime = 0;
    }
}
