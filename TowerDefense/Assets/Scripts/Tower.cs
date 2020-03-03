using UnityEngine;

/// <summary>
/// Management of towers' actions
/// </summary>
public class Tower : Loader<Tower>
{
    public TowerData towerData;
    public GameObject projectile;
    public LayerMask enemyLayer;

    [HideInInspector]
    public int sellingPrice;

    /// <summary>
    /// Current value of the tower's shoot cooldown
    /// </summary>
    private float currCooldown;

    void Start()
    {
        sellingPrice = towerData.buildPrice / 2;
    }

    void Update()
    {
        if (CanShoot())
            SearchTarget();

        if (currCooldown > 0)
            currCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Firing a projectile from a tower to an enemy
    /// </summary>
    /// <param name="enemy">Current target enemy</param>
    void Shoot(Transform enemy)
    {
        currCooldown = towerData.shootInterval;
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetTarget(enemy);
        newProjectile.GetComponent<Projectile>().damage = towerData.damage;
    }

    /// <summary>
    /// Detecting the enemy that was being the longest time in the tower's attack range 
    /// </summary>
    void SearchTarget()
    {
        Transform targetEnemy = null;
        float maxTimeInRange = 0;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float currDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (currDistance <= towerData.range)
            {
                enemy.GetComponent<Enemy>().IsInTowerRange = true;

                if (maxTimeInRange < enemy.GetComponent<Enemy>().timeInTowerRange && !enemy.GetComponent<Enemy>().isDead)
                {
                    maxTimeInRange = enemy.GetComponent<Enemy>().timeInTowerRange;
                    targetEnemy = enemy.transform;
                }
            }
        }

        if (targetEnemy != null)
            Shoot(targetEnemy);
    }

    /// <summary>
    /// Determines whether the tower's shoot cooldown is ended
    /// </summary>
    bool CanShoot()
    {
        if (currCooldown <= 0)
            return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TowerSlot")   
            collision.GetComponent<TowerMenu>().buildedTower = this;
    }
}
