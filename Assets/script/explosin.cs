using System;
using UnityEngine;

public class explosin : MonoBehaviour
{
   public float ExplosionRadius = 10f;
   public float ExplosionForce = 500f;
   public float Ap = 3f;

   public GameObject Barl;
   public GameObject BrokenBarl;
   public GameObject ÅffectBarl;

    private void Start()
    {
        Detonate();
    }

    private void Detonate()
    {
        Destroy(Barl);

        Instantiate(BrokenBarl,transform.position,Quaternion.identity);
        Instantiate(ÅffectBarl, transform.position,Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
    
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);

            Rigidbody rigidbody = collider.gameObject.GetComponent<Rigidbody>();

            if (rigidbody != null) 
            {
                rigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, Ap, ForceMode.Impulse); 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
