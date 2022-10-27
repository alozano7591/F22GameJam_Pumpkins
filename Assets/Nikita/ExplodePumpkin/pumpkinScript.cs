using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class pumpkinScript : MonoBehaviour
{
    //public float Radius;
    //public float Force;
    //public AudioSource explosion;
    //public GameObject prefabExplosion;
    //
    //void Update()
    //{
    //        
    //}
    //
    //public void Explosion()
    //{
    //    ExplosionForce();
    //    explosion.Play();
    //    Destroy(gameObject);
    //    Instantiate(prefabExplosion, transform.position, transform.rotation);
    //    
    //
    //}
    //
    //public void ExplosionForce()
    //{
    //    Collider[] col = Physics.OverlapSphere(transform.position, Radius);
    //    foreach(Collider hit in col)
    //    {
    //        Rigidbody rg = hit.GetComponent<Rigidbody>();
    //
    //        if (rg)
    //        {
    //            rg.AddExplosionForce(Force, transform.position, Radius, 3f);
    //        }
    //
    //    }
    //}

    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;



    public AudioSource explosion;
    public float soundDelay = 0;

    private void Start()
    {
        Explode();
        explosion = GetComponent<AudioSource>();
        explosion.PlayDelayed(soundDelay);
    }

    public void Explode()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
            Destroy(t.gameObject, destroyDelay);
        }
    }
    
}
