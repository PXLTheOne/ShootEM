using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject player;

    public float spawnRate;

    private float minDistance = 20f;
    private float maxDistance = 25f;
    private Vector3 randomPos;
    public float waveCount;
    public float enemyCount;
    public float maxEnemies;
    public float enemiesSlain;

    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI WaveText;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        waveCount = 1;
        enemyCount = 0;
        maxEnemies = 10;
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
       if (enemiesSlain >= maxEnemies) //If all the enemies in the wave are slain
       {
            enemyCount = 0;
            enemiesSlain = 0;
            waveCount++;
            UpdateWaveCount(); //Updates wave text
            maxEnemies += 2;
            StartCoroutine(SpawnEnemies()); //This starts the next wave
       }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateRandPos(), transform.rotation);
        enemyCount++;
    }

    public IEnumerator SpawnEnemies()
    {
        while (gameManager.isGameActive && enemyCount < maxEnemies) 
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnEnemy();
        }
    }

    Vector3 GenerateRandPos()
    {
        do {
            randomPos = new Vector3(Random.Range(-maxDistance, maxDistance), 1.09f, Random.Range(-maxDistance, maxDistance));
        } while (Vector3.Distance(player.transform.position, randomPos) < minDistance);

        return randomPos;
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "SCORE : " + score;
    }
    public void UpdateWaveCount()
    {
        WaveText.text = "WAVE : " + waveCount;
    }


}
