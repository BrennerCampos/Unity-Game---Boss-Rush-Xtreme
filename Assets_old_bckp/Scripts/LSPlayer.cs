using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{

    public LSManager theManager;
    public MapPoint currentPoint;
    public float moveSpeed = 10f;

    private bool levelLoading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moves our Player icon in a steady direction towards the most recently input Map Point
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position,
            moveSpeed * Time.deltaTime);

        // If our Player icon is close to the target Point, and we haven't already selected a level to load...
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f && !levelLoading)
        {
            // GetAxisRAW has no gradual acceleration to 1, immediately goes towards direction  at constant speed
            // Moving RIGHT & LEFT
            if (Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }
            if (Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            // Moving UP & DOWN
            if (Input.GetAxisRaw("Vertical") > 0.5f)
            {

                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }
            if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            // As long as the current map position is a level & there is a name assigned to it & is not locked...
            if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                // Show the info of our current point through LSUIController
                LSUIController.instance.ShowInfo(currentPoint);

                // If the 'Space' button is pressed...
                if (Input.GetButtonDown("Jump"))
                {
                    // Load the current point's level
                    levelLoading = true;
                    theManager.LoadLevel();
                }
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        // Move our currentPoint forward
        currentPoint = nextPoint;
        // Hide the current level info bar
        LSUIController.instance.HideInfo();
        // Plays "Map Movement" SFX
        AudioManager.instance.PlaySFX(5);
    }
}
