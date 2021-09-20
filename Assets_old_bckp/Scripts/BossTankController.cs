using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BossTankController : MonoBehaviour
{

    // Keeping track of our states in an enum
    public enum bossStates
    {
        shooting,
        hurt,
        moving,
        ended
    }
    
    public bossStates currentState;
    public Transform theBoss;
    public Animator animator;

    // Headers allow for further organization within the Unity editor
    [Header("Movement")]
    public Transform leftPoint, rightPoint;
    public GameObject mine;
    public Transform minePoint;
    public float moveSpeed, timeBetweenMines;
    private bool movingRight;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public GameObject hitBox;
    public float hurtTime;
    private float hurtCounter;

    [Header("Health")] 
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;
    

    // Start is called before the first frame update
    void Start()
    {
        // Starts off boss state as 'shooting'
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        // FSM - Logic flow for each boss state
        switch (currentState)
        {
            // -- SHOOTING --
            case bossStates.shooting:
                // Shot counter to throttle firing frequency counting down
                shotCounter -= Time.deltaTime;
                // If that counter is done...
                if (shotCounter <= 0)
                {
                    // Reset counter
                    shotCounter = timeBetweenShots;
                    // Shoots new bullet instance (var needed to be able to alter it's localScale (direction) as it is instantiated)
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    // It's sprite 'facing direction' depends on the Boss's facing direction
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;

            // -- HURT --
            case bossStates.hurt:
                // 'Hurt counter' to throttle time it takes for boss to switch to it's moving state
                if(hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    // If that counter is done...
                    if (hurtCounter <= 0)
                    {
                        // Boss moves to opposite side
                        currentState = bossStates.moving;

                        //Immediately lays a bomb (can raise to wait a bit first)
                        mineCounter = 0;

                        if (isDefeated)
                        {
                            // Deactivate the boss
                            theBoss.gameObject.SetActive(false);
                            // SFX (explosion)
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            // Activate the new spawned platforms
                            winPlatform.SetActive(true);
                            // Stop boss-themed music
                            AudioManager.instance.StopBossMusic();
                            // Switch to exit state
                            currentState = bossStates.ended;
                        }
                    }
                }
                break;

            // -- MOVING --                                             
            case bossStates.moving:

                if (movingRight)
                {
                    // Move the boss towards the right (+) using moveSpeed (Could also use 'MoveTowards')
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    // If the boss's horizontal point passes our set 'right point'...
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        // Face boss towards the left (default)
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        // Set bool to signify moving left
                        movingRight = false;
                        // End the boss's movement
                        EndMovement();
                    }
                }
                else
                {
                    // Move the boss towards the left (-) using moveSpeed
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    // If the boss's horizontal point passes our set 'left point'...
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        // Face boss towards the right
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        // Set bool to signify moving right
                        movingRight = true;
                        // End the boss's movement
                        EndMovement();
                    }
                }

                // Count down boss's mine timer
                mineCounter -= Time.deltaTime;
                // If mine counter is finished...
                if (mineCounter <= 0)
                {
                    // Reset mine counter
                    mineCounter = timeBetweenMines;
                    // Deploy mine instance
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }

// Machine command to only run this when in our Unity Editor, excluded from any compiled build.
// Used for debugging
#if UNITY_EDITOR
        // Debugging Boss getting hit with 'H' key
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif

    }

    public void TakeHit()
    {
        // Change boss's state to 'hurt'
        currentState = bossStates.hurt;
        // Sets time boss spends hurt by specified amount
        hurtCounter = hurtTime;
        // Notify animator that boss is hit
        animator.SetTrigger("Hit");
        // Play Boss Hit sound
        AudioManager.instance.PlaySFX(0);
        // Creates an array to store all of our current mines so we can destroy them all
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        // If there are any mines currently out...
        if (mines.Length > 0)
        {
            // Go through each of them in our array and make them self-destruct.
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        // Take away boss's health
        health--;

        // If boss runs out of health, isDefeated bool true
        if (health <= 0)
        {
            isDefeated = true;
        }
        else
        {
            // Speed up how frequently boss shoots and lays mines down by dividing by our set shot & mineSpeedUp
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }

    }

    private void EndMovement()
    {
        // When done moving, switch current state back to 'shooting'
        currentState = bossStates.shooting;
        // Immediately shoot a bullet
        shotCounter = 0f;
        // Notify animator to change sprites
        animator.SetTrigger("StopMoving");
        // Activating boss's hitbox again
        hitBox.SetActive(true);
    }
}
