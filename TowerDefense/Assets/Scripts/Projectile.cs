using UnityEngine;

/// <summary>
/// Description of projectile actions
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Enemy that is current target of the projectile
    /// </summary>
    private Transform target;
    private float flightSpeed = 7;

    [HideInInspector]
    public int damage;

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Move()
    {
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * flightSpeed);
        }
        else
            Destroy(gameObject);
    }
}
