using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed;
    public float jumpForce;
    [Space]
    public int startingAmmo;
    public int lifeCount;
    [Space]
    public float speedIncreasePerInterval;
    public float speedChangeIntervalTime;
    [Space]
    public GameObject CactusBullet;

    bool doubleJumpActive;

    float playerDuckScale = 0.3f;
    float playerNormalScale = 1f;

    Rigidbody PlayerRigid;

    void Awake()
    {
        GlobalData.isAlive = true;
        GlobalData.playerIsGrounded = true;
        GlobalData.playerIsDucking = false;

        GlobalData.playerLives = lifeCount;
        GlobalData.cactusAmmo = startingAmmo;
        GlobalData.playerSpeed = speed;

        GlobalData.speedChangeIntervalTime = speedChangeIntervalTime;
    }

    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveForward();
        IncreaseMoveSpeed();
        KeyboardControls();
    }

    void FixedUpdate()
    {
        if (PlayerRigid.useGravity) //THIS FIXES JUMP BEING FLOATY
        {
            PlayerRigid.AddForce(Physics.gravity * (PlayerRigid.mass * PlayerRigid.mass));
        }
    }

    void KeyboardControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Duck();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            UnDuck();
        }
    }

    void MoveForward()
    {
        PlayerRigid.velocity = new Vector3(GlobalData.playerSpeed, PlayerRigid.velocity.y, 0);
    }

    public void Jump()
    {
        if (GlobalData.playerIsGrounded)
        {
            PlayerRigid.velocity = new Vector3(PlayerRigid.velocity.x, jumpForce, 0);
            doubleJumpActive = true;
        }
        if(!GlobalData.playerIsGrounded && doubleJumpActive)
        {
            PlayerRigid.velocity = new Vector3(PlayerRigid.velocity.x, jumpForce, 0);
            doubleJumpActive = false;
        }
    }

    public void Duck()
    {
        if (GlobalData.playerIsGrounded)
        {
            GlobalData.playerIsDucking = true;
            transform.localScale = new Vector3(transform.localScale.x, playerDuckScale, transform.localScale.z);
            Debug.Log("isPlayerDucking: " + GlobalData.playerIsDucking);
        }
    }
    public void UnDuck()
    {
        GlobalData.playerIsDucking = false;
        transform.localScale = new Vector3(transform.localScale.x, playerNormalScale, transform.localScale.z);
        Debug.Log("isPlayerDucking: " + GlobalData.playerIsDucking);
    }

    public void Shoot()
    {
        if (GlobalData.cactusAmmo > 0 && !GlobalData.playerIsDucking) //PLAYER SHOOTS IF THERE IS AMMO OR IS NOT DUCKING
        {
            Instantiate(CactusBullet, transform.position, transform.rotation);
            GlobalData.cactusAmmo--;
        }
    }

    void IncreaseMoveSpeed()
    {
        GlobalData.speedChangeIntervalTime -= Time.deltaTime;
        if (GlobalData.speedChangeIntervalTime <= 0)
        {
            GlobalData.playerSpeed += speedIncreasePerInterval;
            GlobalData.bulletSpeed += speedIncreasePerInterval;

            GlobalData.speedChangeIntervalTime = speedChangeIntervalTime;
            Debug.Log("Speed increased by " + speedChangeIntervalTime + "!");
        }
    }
    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Enemy") || obj.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(obj.gameObject);

            GlobalData.playerLives--;
            Debug.Log("Player Life decreased by 1!");
            Debug.Log("Collided with Enemy/Bullet");
            if (GlobalData.playerLives <= 0) //IF NO MORE LIVES LEFT
            {
                GlobalData.isAlive = false;
                SceneManager.LoadScene("menu");
                Debug.Log("Game Over!");
            }
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            GlobalData.playerIsGrounded = true;
            Debug.Log("isGrounded: " + GlobalData.playerIsGrounded);
        }

        if (obj.gameObject.CompareTag("CactusAmmo"))
        {
            int ammo = obj.gameObject.GetComponent<CactusAmmo>().ammoGiveCount;
            GlobalData.cactusAmmo += ammo;
            Debug.Log("Pickup " + ammo + " ammo");
            Destroy(obj.gameObject);
            
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            GlobalData.playerIsGrounded = false;
            Debug.Log("isGrounded: " + GlobalData.playerIsGrounded);
        }
    }
}
