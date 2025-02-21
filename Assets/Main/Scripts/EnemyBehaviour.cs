using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private GameObject gameOverScreen;
    public GameObject bullet;
    public Vector3 bulletOffset;
    private GameManager gameManager;
    private float minDistance = 7f;
    private bool canShoot = true;
    private float enemyFireRate = 0.2f;
    //public Animator animator;
    //private bool isMoving = true;
    //[SerializeField] private ParticleSystem muzzleFlash;

    public float speed;
    [SerializeField] private Vector3 flashOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameOverScreen = GameObject.Find("GameOver Screen");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        //animator.SetBool("isWalking", isMoving);
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (Mathf.Abs(distance) > minDistance)
        {
            rb.velocity = moveDir * speed * Time.deltaTime;
            //isMoving = true;
        }
        else
        {
            //isMoving = false;
            if (canShoot && gameManager.isGameActive)
            {
                StartCoroutine(ShootCoolDown());
            }
        }
    }

    void Shoot()
    {
        Vector3 localFlashOffset = transform.TransformDirection(flashOffset); ;
        //muzzleFlash.transform.position = transform.position + localFlashOffset;
        //muzzleFlash.transform.rotation = transform.rotation;
        //muzzleFlash.Play();
        Vector3 localOffset = transform.TransformDirection(bulletOffset);
        GameObject ShotBullet = Instantiate(bullet, rb.transform.position + localOffset, transform.rotation);
        Destroy(ShotBullet, 4f);
    }

    IEnumerator ShootCoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1);
        Shoot();
        yield return new WaitForSeconds(enemyFireRate);
        canShoot = true;
    }


}
