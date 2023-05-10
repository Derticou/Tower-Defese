using System.Collections;

using UnityEngine;

public class MainMenu_EnemySpawn : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator SpawnWave()
    {
        
        PlayerStats.Rounds++;
        for (int i = 0; i < 100; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
