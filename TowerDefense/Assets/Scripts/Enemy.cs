using UnityEngine;

/// <summary>
/// Description of enemies actions
/// </summary>
public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    /// <summary>
    /// Trajectory movement of enemies
    /// </summary>
    public Transform[] movePoints;

    [HideInInspector]
    public bool IsInTowerRange;
    /// <summary>
    /// For define the target enemy for towers
    /// </summary>
    [HideInInspector]
    public float timeInTowerRange = 0;
    [HideInInspector]
    public Animator anim;

    /// <summary>
    /// Current target point which the enemy moves to
    /// </summary>
    private int targetPoint;
    private Balance balance;
    private int health;
    public bool isDead;
    
    void Start()
    {
        health = enemyData.health;
        balance = Balance.Instance;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            if (targetPoint < movePoints.Length)
            {
                transform.position = Vector2.MoveTowards(transform.position, movePoints[targetPoint].position, enemyData.movingSpeed * Time.deltaTime);
            }

            if (IsInTowerRange)
                timeInTowerRange += Time.deltaTime;
        }
    }

    /// <summary>
    /// Actions of enemy when dying
    /// </summary>
    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            balance.currencyAmount += Random.Range(enemyData.minCoins, enemyData.maxCoins);
            GetComponent<SpriteRenderer>().sortingOrder = -3;
            anim.SetTrigger("isDead");

            if (EnemyWaves.lastEnemies.Contains(this))
                EnemyWaves.lastEnemies.Remove(this);
        }
    }

    /// <summary>
    /// Actions when enemy is hurt
    /// </summary>
    /// <param name="damage">Shooting tower's damage</param>
    private void HitEnemy(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
        else
            anim.Play("Hurt");
    }

    /// <summary>
    /// Handler for death animation's event
    /// </summary>
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "MovingPoint":
                targetPoint++;
                break;
            case "Finish":
                Destroy(gameObject);
                balance.healthPoints -= enemyData.damage;
                if (EnemyWaves.lastEnemies.Contains(this))
                    EnemyWaves.lastEnemies.Remove(this);
                break;
            case "Projectile":
                HitEnemy(collision.GetComponent<Projectile>().damage);
                Destroy(collision.gameObject);
                break;
        }
    }
}
