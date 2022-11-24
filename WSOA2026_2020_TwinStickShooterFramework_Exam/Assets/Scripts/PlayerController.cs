
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ////////// PUBLIC FIELDS //////////
    public float moveFactor = 10f;
    public float bulletDelay = 0.2f;
    public float bulletSpeed = 0.2f;
    public float bulletDuration = 3;
    public int playerHealth = 10;
    public Transform bulletSpawnPnt;

    public GameObject muzzleFlash;
    public GameObject bulletPfb;
    public string sceneToLoad;
    //public GameObject reticule; // Hypothetical sprite to show the player where they are aiming.

    ////////// PRIVATE FIELDS //////////
    Camera viewCamera;
    Vector3 direction;
    Quaternion rotation;
    Rigidbody rb;
    bool canShoot = true;
    Animator bLAMBOAnim; // Hypothetical Animator component for bLAMBO (it doesn't actually exist in the scene yet!).

    void Start()
    {
        ////////// INITIALIZATIONS //////////
        viewCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        bLAMBOAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //// MOUSE CONTROLS ////
        /*  These are taken directly from Sebastian Lague's 'Unity Create a Game Series E01'
            URL: https://www.youtube.com/watch?v=jdv8erC7ML8&list=PLFt_AvWsXl0ctd4dgE1F8g3uec4zKNRV0 */

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            LookAt(point);
        }
        
        //// GENERIC CONTROLS ////
        if (Input.GetButton("Fire1") && canShoot == true)
        {
            Shooting();
                        
        }

        if (playerHealth <= 0)
        {
            PlayerDeath();
        }
    }

    void FixedUpdate()
    {
        //// MOVEMENT CONTROLS ////
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal")*moveFactor, 0, Input.GetAxisRaw("Vertical")*moveFactor);
        rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z);

        ///// ANIMATION STATES ////
        /* This is an example of how you might go about calling the various animation clips on the character
        (Please Note: these states and animators don't actually exist in the scene yet, you'll need to create them!)
        */
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
        {
            bLAMBOAnim.SetInteger("AnimState", 0); //This is the walking state.
        }
        else
        {
            bLAMBOAnim.SetInteger("AnimState", 1); // This is the idle state.
        }
    }

    // Spawns a bullet and controls the rate at which bullets can be fired.
    void Shooting()
    {
        Instantiate(bulletPfb, bulletSpawnPnt.position, transform.rotation);
        Instantiate(muzzleFlash, bulletSpawnPnt.position, transform.rotation);
        StartCoroutine(BulletDelay());
    }

    // Kills the player and sends you back to the title screen.
    void PlayerDeath()
    {
        Destroy(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene (sceneToLoad);
    }

    // Points the player at the mouse.
    public void LookAt(Vector3 lookPoint)
    {
        transform.LookAt(lookPoint);
    }

    // Controls whether the player can spawn bullets or not.
    IEnumerator BulletDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletDelay);
        canShoot = true;
    }

    // Draws a line in the scene view to show you where the player is pointing.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}

