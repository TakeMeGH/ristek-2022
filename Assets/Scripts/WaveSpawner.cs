using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> spawnedEnemy = new List<GameObject>();
    public List<GameObject> enemyToSpawn = new List<GameObject>();
    public List<Transform> spawnPosition = new List<Transform>();

    int currentWave;

    [SerializeField] GameObject infantry;
    [SerializeField] int waveCount;
    [SerializeField] int spawnPerWaveCount;
    [SerializeField] float spawnTimePerWave;
    float spawnTimePerUnit;

    Coroutine spawnRoutine;

    bool isTriggerWave = false;
    void Start()
    {
        spawnTimePerUnit = spawnTimePerWave / spawnPerWaveCount;
        generateWave();
    }

    public void runWave(){
        isTriggerWave = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isTriggerWave || currentWave == waveCount) return;

        if(spawnedEnemy.Count == 0 && spawnRoutine == null){
            spawnRoutine = StartCoroutine(spawnEnemy());
        }
        else{
            if(spawnedEnemy.Count > 0 && spawnedEnemy[0] == null){
                spawnedEnemy.RemoveAt(0);
            }
        }
    }
    
    public bool isFinishRunning(){
        return currentWave == waveCount;
    }
    private IEnumerator spawnEnemy(){
        yield return new WaitForSeconds(3f);

        for(int i = 0; i < spawnPerWaveCount; i++){
            int spwanPosIdx = Random.Range(0, spawnPosition.Count);
            GameObject enemy = Instantiate(enemyToSpawn[i], spawnPosition[spwanPosIdx].position, Quaternion.identity, infantry.transform);
            enemy.GetComponent<EnemyController>().turnOnChasing();
            spawnedEnemy.Add(enemy);
            yield return new WaitForSeconds(spawnTimePerUnit);
        }
        enemyToSpawn.Clear();
        spawnRoutine = null;
        currentWave++;
        generateEnemies();
    }
    void generateWave(){
        generateEnemies();
    }

    void generateEnemies(){
        for(int i = 0; i < spawnPerWaveCount; i++){
            enemyToSpawn.Add(enemies[0].enemyPrefab);
        }
    }
}


[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}