using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pumpkinScript : MonoBehaviour
{
    public float Radius;
    public float Force;
    public AudioSource explosion;
    public GameObject prefabExplosion;

    void Update()
    {
            
    }

    public void Explosion()
    {
        ExplosionForce();
        explosion.Play();
        Destroy(gameObject);
        Instantiate(prefabExplosion, transform.position, transform.rotation);
        

    }

    public void ExplosionForce()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, Radius);
        foreach(Collider hit in col)
        {
            Rigidbody rg = hit.GetComponent<Rigidbody>();

            if (rg)
            {
                rg.AddExplosionForce(Force, transform.position, Radius, 3f);
            }

        }
    }
}
