using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    public float muzzleTorque;
    public GameObject bullet;
    public Vector3 bulletOffset;
    public Vector3 flashOffset;
    private bool CanShoot = true;
    public float coolDown;
    public AudioSource playerAudio;
    public AudioClip shootingSound;
    public AudioClip impactSound;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject muzzleObject;
    public GameObject gameOverScreen;

    [SerializeField] private Camera camera;
    private Ray selectRay;
    private GameObject selectedEnemy;
    private bool isRotating;
    private Quaternion lookRotation;

    //Scripts
    private GameManager gameManager;
    private SpawnManager spawnManager;

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

    private void Update()
    {
        //if (gameManager.isGameActive)
        //{
        //    RotateHead();
        //}
    }

    void FixedUpdate()
    {
        if (Quaternion.Angle(transform.rotation, lookRotation) < 0.1f)
        {
            isRotating = false;
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 15f * Time.deltaTime);
            muzzleObject.transform.rotation = transform.rotation;
        }

        if (selectedEnemy != null && !isRotating && CanShoot)
        {
            StartCoroutine(ShootWithCoolDown());
            muzzleObject.GetComponent<Rigidbody>().AddTorque(muzzleTorque * Time.deltaTime * Vector3.forward);
        }

        float XInput = Input.GetAxis("Horizontal");
        if (gameManager.isGameActive)
        {
            //transform.Rotate(Vector3.up * XInput * Torque * Time.deltaTime);
            //muzzleObject.transform.position = transform.position + transform.TransformDirection(new Vector3(0, 0.5f, 0.7f));
            if (Input.GetKey(KeyCode.Mouse0) && CanShoot)
            {
                SelectEnemy();
            }
        }
    }

    void SelectEnemy()
    {
        Vector3 mousePos = Input.mousePosition;
        selectRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (gameManager.isGameActive && Physics.Raycast(selectRay, out hit, Mathf.Infinity, enemyLayer))
        {
            selectedEnemy = hit.collider.gameObject;
            lookRotation = Quaternion.LookRotation((selectedEnemy.transform.position - transform.position).normalized);
            isRotating = true;   
        }
    }

    //void RotateHead()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = Mathf.Abs(camera.transform.position.y - transform.position.y); //give ScreenToWorldPoint depth value so it doesn't default to 0
    //    Vector3 worldMousePosition = camera.ScreenToWorldPoint(mousePos);

    //    Vector3 dir = (worldMousePosition - transform.position).normalized;
    //    dir.y = 0;
    //    transform.rotation = Quaternion.LookRotation(dir);
    //}

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

    IEnumerator ShootWithCoolDown()
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