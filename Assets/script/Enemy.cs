using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int health;
    public float speed;


    private void Start()
    {
       Move();
       Attack();
    }


    public virtual void Move()
    {
        Debug.Log($"{enemyName} {speed} { health}");
    }


    public  virtual void Attack() 
    {
        Debug.Log($"{enemyName}");
    }


    public virtual void TakeDamager(int damage)
    {
        health -= damage;
    }



}
