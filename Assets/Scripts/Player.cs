using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public Rigidbody rb;
    //public float playerSpeed;
    public float rotSpeed;
    public float currSpeed = 5f;
    public float sprintSpeed = 5f;
    public StaminaController staminaController;
    public Vector2 movement;
    public bool sprint = false;
    float horiz;
    float vert;

    [Header("Player Index")]
    [SerializeField]
    private int playerindex = 0;
    public Camera myCam;



    //public HUDManager hud;
    //public GameManager gm;
    [Header("Player Info")]
    public int playerHealth = 100;
    public int playerAmmo = 10;
    public int playerGold = 0;
    public bool RedKey = false;
    public int Gear = 0;
    public int PadKey;
    public  bool IsDead;


    [Header("Player Stuff")]

    public GameObject RespawnPoint;
    public GameObject GearObj;
    public GameObject MarkerObj;
    public GameObject spawnPt;
    public GameObject bulletObj;
    public Transform barrel;
    public HUDManager hud;
    private Vector2 moveInput = Vector2.zero;
    public GameManager gm;
    //If player has gears setactive sprite in inventory with child of text displaying amount
    //same for sword
    //equipscript to player and target to object to drop but set drop to invenmtory  button not keybind





    void Start()
    {
        staminaController = GetComponent<StaminaController>();
        rb = GetComponent<Rigidbody>();
       // rb.freezeRotation = true;
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        //OnMoveInput(moveInput.x, moveInput.y);

    }
    public void Drop()
    {
        if (Gear >= 1)
        {
            Instantiate(GearObj, spawnPt.transform.position, Quaternion.identity);
            UseItem();
        }
        else
        {
            Debug.Log("You do not have enough to give");
            //print error
        }

    }

    public void OnPlaceMarker()
    {
            Instantiate(MarkerObj, spawnPt.transform.position, Quaternion.identity);
    }
    public void OnYesButton()
    {
        Respawn();
    }
    public void OnNoButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (IsDead)
        {
            playerHealth = 100;
            transform.position = RespawnPoint.transform.position;
            playerGold = 0;
            IsDead = false;
            //playerHealth = 0;
            //rb.isKinematic = true;
            //GetComponent<HUDManager>().DisplayDeath();
        }

        if (sprint == true)
        {
            if (staminaController.playerStamina > 5f)
            {
                staminaController.isSprinting = true;
                staminaController.Sprinting(); ;
                staminaController.UpdateStamina(1);
                currSpeed = 10f;
            }
            else if (staminaController.playerStamina <= 5f)
            {
                sprint = false;
                staminaController.isSprinting = false;
                staminaController.UpdateStamina(0);
                currSpeed = 5f;
            }
        }
        else
        {
            staminaController.isSprinting = false;
            staminaController.UpdateStamina(0);
            currSpeed = 5f;
        }

        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
       // Vector3 moveDir = Vector3.forward * vert + Vector3.right * horiz;

        if (moveDir.magnitude > 0.1f)
        {
            rb.velocity = moveDir * currSpeed;
            //add sprite rotation

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public int GetPlayerIndex()
    {
        return playerindex;
    }

    public void OnSprint()
    {
        sprint = !sprint;
       //sprint = true;
    }
    public void OnMoveInput(float horiz, float vert)
    {
        this.vert = vert;
        this.horiz = horiz;

    }

        public void OnShoot()
    {
        //RaycastHit hit;
        if(playerAmmo != 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletObj, barrel.position, Quaternion.identity); 
            BulletController bulletController = bullet.GetComponent<BulletController>();
            //GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletController.speed;
            playerAmmo -= 1;
        }
        //code for putting decal on hit object
        /*
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = Camera.main.transform.position + Camera.main.transform.forward * 25f;
            bulletController.hit = false;
        }
        */
    }
    public void TakeDamage()
    {
        if(playerHealth <= 0)
        {
            IsDead = true;
        }
        else
        {
            playerHealth -=5;
            //GetComponent<HUDManager>().DisplayplayerHealth();
        }

    }
    public void InteractPopUp(string popUpMessage)
    {
        hud.PopUp.text = popUpMessage; ;
    }

    public void Respawn()
    {
        IsDead = false;
        transform.position = RespawnPoint.transform.position;
        playerHealth = 100;
        //rb.isKinematic = false;
        //gm.loseGold(10);
        //GetComponent<HUDManager>().DisplayDisableDeath();
    }

    public void AddGear()
    {
        Gear += 1;
    }
    public void UnlockDoor()
    {
        RedKey = true;
    }

    public void AddAmmo(int AmmoValue)
    {
        playerAmmo += AmmoValue;
    }
    public void AddGold(int goldValue)
    {
        playerGold += goldValue;
        gm.TotalAddGold(goldValue);
    }
    public void UseItem()
    {
        //update ui for inventory
        if(Gear != 0)
        {
            Gear -= 1;
            //GetComponent<HUDManager>().DisplayGears();
        }
        else
        {
            //print error message
        }
    }
}
