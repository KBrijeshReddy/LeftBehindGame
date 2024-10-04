using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MagicBallShooter : MonoBehaviour
{
    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private Transform Cam;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float fireRate; // Fire rate, in seconds
    [SerializeField]
    private float throwCooldown;
    [SerializeField]
    private float ballSpeed;
    [SerializeField]
    private GameObject magicBall;


    private float timeSinceLastShot = 0f;
    private bool readyToThrow;

    void Start()
    {
        readyToThrow = true;
    }

    async void Update()
    {
        // Update the timer based on time passed

        // Check if the player presses the shoot button (P key) and can shoot
        if (Input.GetMouseButtonDown(1) && readyToThrow && MeleeAttackManager.instance.isAttacking==false)
        {
            ShootTheBall();
            // Debug.Log("shot the ball");

            // Reset the timer after shooting
            await Task.Delay(5000);
            readyToThrow = true;
        }
    }

    void ShootTheBall()
    {
        readyToThrow = false;
        GameObject bullet = Instantiate(magicBall, shootPoint.position, Cam.rotation);
        Debug.Log("Is shooting");

        Vector3 direction = Cam.transform.forward;

        // Set the bullet's velocity to move towards the player
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Cam.transform.forward * ballSpeed;
        }
    }
}
