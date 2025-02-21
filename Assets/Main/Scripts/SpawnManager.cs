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

    public int score;
    public TextMeshProUGUI scoreText;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateRandPos(), transform.rotation);
    }

    public IEnumerator SpawnEnemies()
    {
        if (gameManager.isGameActive)
        {
            while (gameManager.isGameActive)
            {
                yield return new WaitForSeconds(spawnRate);
                SpawnEnemy();
            }
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
        scoreText.text = "Score : " + score;
    }


}
