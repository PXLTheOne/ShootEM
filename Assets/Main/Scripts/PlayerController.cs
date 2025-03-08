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

    //[SerializeField] private GameObject muzzleObject;

    [SerializeField] private Camera camera;

    private GameManager gameManager;
    private SpawnManager spawnManager;
    public GameObject gameOverScreen;
    public float fireRate = 0.2f;
    public float health;
    //private Rigidbody muzzleRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //muzzleRb = muzzleObject.GetComponent<Rigidbody>();
        muzzleFlash.Stop();

        health = 100.0f;
    }

    private void Update()
    {
        if (gameManager.isGameActive)
        {
            RotateHead();
        }
    }

    void FixedUpdate()
    {
        float XInput = Input.GetAxis("Horizontal");
        if (gameManager.isGameActive)
        {
            transform.Rotate(Vector3.up * XInput * Torque * Time.deltaTime);
            //muzzleObject.transform.position = transform.position + transform.TransformDirection(new Vector3(0, 0.5f, 0.7f));
            if (Input.GetKey(KeyCode.Mouse0) && CanShoot)
            {
                StartCoroutine(ShootCoolDown());
                //Vector3 localTorqueAxis = muzzleObject.transform.TransformDirection(Vector3.forward);
                //muzzleObject.transform.Rotate(Vector3.forward * -500 * Time.deltaTime, Space.Self);
            }
        }
    }

    void RotateHead()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(camera.transform.position.y - transform.position.y);
        Vector3 worldMousePosition = camera.ScreenToWorldPoint(mousePos);

        Vector3 dir = (worldMousePosition - transform.position).normalized;
        dir.y = 0;

        transform.rotation = Quaternion.LookRotation(dir);
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