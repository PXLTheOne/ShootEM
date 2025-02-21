using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isGameActive = false;
    private SpawnManager spawnManager;
    public GameObject titleScreen;
    public GameObject scoreText;
    public TextMeshProUGUI healthText;
    PlayerController playerController;
    public GameObject healthTextObject;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player/TurretHead").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP : " + playerController.health.ToString();
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;
        spawnManager.spawnRate /= difficulty;
        StartCoroutine(spawnManager.SpawnEnemies());
        titleScreen.SetActive(false);
        scoreText.SetActive(true);
        healthTextObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
