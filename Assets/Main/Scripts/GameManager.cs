using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isGameActive = false;
    private SpawnManager spawnManager;
    private FollowPlayer followPlayerScript;
    //Difficulty Screen
    public GameObject DifficultyScreen;
    public GameObject scoreText;
    public TextMeshProUGUI healthText;
    public GameObject waveText;
    public GameObject HealthBar;
    //Title Screen
    public GameObject TitleScreen;

    PlayerController playerController;
    public GameObject healthTextObject;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        followPlayerScript = Camera.main.GetComponent<FollowPlayer>();
        playerController = GameObject.Find("Player/TurretHead").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerController.health.ToString();
    }

    public void ViewDiffScreen()
    {
        TitleScreen.SetActive(false);
        DifficultyScreen.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame(float difficulty)
    {
        followPlayerScript.isGameStarted = true;
        GameObject Spider = GameObject.Find("THICKSPIDER");
        Destroy(Spider);
        isGameActive = true;
        spawnManager.spawnRate /= difficulty;
        StartCoroutine(spawnManager.SpawnEnemies());
        DifficultyScreen.SetActive(false);
        scoreText.SetActive(true);
        waveText.SetActive(true);
        //healthTextObject.SetActive(true);
        HealthBar.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
