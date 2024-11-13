using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public bool inCombat;
    public bool isAttacking;


    [Header("-----------------Preset-----------------")]
    [SerializeField]
    float mouseSens;
    [SerializeField]
    float speed;
    [SerializeField]
    float sprintSpeed;
    [SerializeField]
    float gravity;
    [SerializeField]
    float jumpforce;
    [SerializeField]
    private Transform cameraTransform;
    public GameObject meleeAttackManagers;


    private CharacterController controller;
    private Rigidbody rb;
    private Transform player;

    private bool onGround;
    private Vector3 moveVel;
    private Vector3 turnVel;
    private float x;
    private float z;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;
    private bool isOn = true;
    private Vector3 move;

    void Awake()
    {
        instance = this;

        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Transform>();
        
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.volume = SettingsManager.volumes["master"];
        mouseSens = SettingsManager.mouseSens;
    }

    void FixedUpdate()
    {
        if (!InventoryUI.isActive)
        {
            // movement
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift)) {

                controller.Move(move * sprintSpeed * Time.deltaTime);
            }
            
            moveVel.y += gravity * Time.deltaTime;
            controller.Move(moveVel * Time.deltaTime);
            
            transform.Rotate(turnVel * Time.deltaTime);


            // mouse look
            mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            SettingsManager.instance.ReturnMusic("walking").Play();
        }
        else {
            SettingsManager.instance.ReturnMusic("walking").Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
}
