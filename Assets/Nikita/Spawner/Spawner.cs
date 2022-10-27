using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float delay;

    private void Start()
    {
        StartCoroutine(WaitoSpawn());
    }
    IEnumerator WaitoSpawn()
    {
        yield return new WaitForSeconds(delay);
        Spawn();
    }

    void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
        StartCoroutine(WaitoSpawn());
    }

   
}
