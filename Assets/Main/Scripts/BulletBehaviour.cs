using System.Collections;
using System.Collections.Generic;
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
            Destroy(collision.gameObject);
            Destroy(gameObject);
            spawnManager.UpdateScore();
        }
    }
}
