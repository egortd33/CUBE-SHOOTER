using UnityEngine;

public class flyingEnemy : Enemy
{
    public float flyHeight = 10f;


    private void Start()
    {
        Move();
        Attack();
    }

    public override void Move()
    {
        Debug.Log($"{enemyName} {flyHeight} {speed} {health}");
    }

    public override void Attack()
    {
        Debug.Log($"{enemyName}");
    }


    public override void TakeDamager(int damage)
    {
        health -= damage;
    }
}