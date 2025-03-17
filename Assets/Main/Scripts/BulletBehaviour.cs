using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    Rigidbody bulletRb;
    public float bulletSpeed;
    private SpawnManager spawnManager;
    public AudioClip impactSound;
    private PlayerController playerController;
    public string TargetTag;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    void FixedUpdate()
    {
        bulletRb.AddForce(transform.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(TargetTag))
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position, 550);
            EnemyBehaviour EnemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            EnemyHealth HealthUI = collision.gameObject.GetComponentInChildren<EnemyHealth>();
            EnemyScript.Health--; //this bullet only got 1 dmg point
            HealthUI.HealthEdit(EnemyScript.Health);
            //Destroy(collision.gameObject);  i commented it bec he has health now ;-;
            Destroy(gameObject);
            spawnManager.UpdateScore();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spawnManager.enemiesSlain++;
        }
    }
}
