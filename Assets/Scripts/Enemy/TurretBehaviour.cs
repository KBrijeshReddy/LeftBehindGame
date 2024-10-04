using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private GameObject bulletFab;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float detectionRange = 20f;
    [SerializeField]
    private float fireRate = 2f;      // How often the turret fires
    [SerializeField]
    private float nextFireTime = 0f; // When the turret can fire next
    [SerializeField]
    private float bulletSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 180f;
    [SerializeField]
    private float interactRange = 20f;

    private Transform player;
    private Quaternion currentRotation;
    private Quaternion targetRotation;
    private Vector3 direction;
    private Rigidbody rb;
    private GameObject bullet;

    void Start()
    {
        player = PlayerManager.instance.gameObject.transform;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (IsPlayerInRange()) {
            LookToPlayer();

            // Check if the turret can fire (based on fireRate)
            if (Time.time >= nextFireTime)
            {
                StartShooting();
                nextFireTime = Time.time + 1f / fireRate;  // Set the next fire time
            }
        }
    }

    private bool IsPlayerInRange()
    {
        // Get all colliders within the detection range

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player")) // Ensure the collider is the player
            {
                return true; // Player is within range
            }
        }

        return false; // Player is not within range
    }

    void StartShooting() {
        // Instantiate the bullet at the fire point (turret) position
        bullet = Instantiate(bulletFab, firePoint.position, Quaternion.identity);

        // Calculate the direction from the turret (fire point) to the player
        Vector3 direction = player.position - firePoint.position;
        Debug.DrawLine(firePoint.position, player.position, Color.red, 2f);

        // Set the bullet's velocity to move towards the player
        rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
            // Debug.Log("set vel");
        }
    }

    void LookToPlayer() {
        direction = (player.position - transform.position).normalized;

        if (direction != Vector3.zero) // Prevent LookRotation with zero vector
        {
            // Calculate the target rotation
            targetRotation = Quaternion.LookRotation(direction);

            // Keep the current X and Z rotations
            currentRotation = transform.rotation;
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Set X and Z to 0
        }
    }
}
