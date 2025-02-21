using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    public float Torque;
    public GameObject bullet;
    public Vector3 bulletOffset;
    public Vector3 flashOffset;
    private bool CanShoot = true;
    public float coolDown;
    public AudioSource playerAudio;
    public AudioClip shootingSound;
    public AudioClip impactSound;
    [SerializeField] private ParticleSystem muzzleFlash;

    private GameManager gameManager;
    private SpawnManager spawnManager;
    public GameObject gameOverScreen;
    public float fireRate = 0.2f;
    public float health;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        muzzleFlash.Stop();

        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float XInput = Input.GetAxis("Horizontal");
        

        if (gameManager.isGameActive)
        {
            transform.Rotate(Vector3.up * XInput * Torque * Time.deltaTime);
            if (Input.GetKey(KeyCode.Space) && CanShoot)
            {
                StartCoroutine(ShootCoolDown());
                
            }
        }
    }

    void Shoot()
    {
        Vector3 localBulletOffset = transform.TransformDirection(bulletOffset);
        Vector3 localFlashOffset = transform.TransformDirection(flashOffset); ;
        GameObject ShotBullet = Instantiate(bullet, playerRb.transform.position + localBulletOffset, transform.rotation);
        muzzleFlash.transform.position = transform.position + localFlashOffset;
        muzzleFlash.transform.rotation = transform.rotation;
        muzzleFlash.Play();
        playerAudio.PlayOneShot(shootingSound);
        Destroy(ShotBullet, 4f);
    }

    IEnumerator ShootCoolDown()
    {
        CanShoot = false;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        CanShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if(health > 0)
            {
                health = health - 5;
            }
            if(health <= 0)
            {
                gameOverScreen.SetActive(true);
                gameManager.isGameActive = false;
            }

        }
    }
}