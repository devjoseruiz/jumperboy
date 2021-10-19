using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0.0f, 2f);
    }

    public void StopGenerator()
    {
        CancelInvoke("CreateEnemy");
    }
}
