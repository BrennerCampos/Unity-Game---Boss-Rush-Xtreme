
using UnityEngine;

public class BusterShotEffects : MonoBehaviour
{

    private string shotDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Play 'Bullet Shot' Sound
        //AudioManager.instance.PlaySFX(2);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            if (!PlayerController.instance.isWallSliding)
            {
                shotDirection = "Right";
                transform.localScale = new Vector3(11, 11, 1);
            }
            else
            {
                shotDirection = "Left";
                transform.localScale = new Vector3(-11, 11, 1);
            }
        }
        else
        {
            if (!PlayerController.instance.isWallSliding)
            {
                shotDirection = "Left";
                transform.localScale = new Vector3(-11, 11, 1);
            }
            else
            {
                shotDirection = "Right";
                transform.localScale = new Vector3(11, 11, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Follows the Player according to different shooting states and their firing points
        if (shotDirection == "Right")
        {
            if (PlayerController.instance.isWallShooting)
            {
                transform.position = PlayerController.instance.wallFirePointRight.position;
            }
            else if (PlayerController.instance.isJumpShooting)
            {
                transform.position = PlayerController.instance.jumpFirePointRight.position;
            } 
            else if (PlayerController.instance.isRunShooting)
            {
                transform.position = PlayerController.instance.runFirePointRight.position;
            }
            else
            {
                transform.position = PlayerController.instance.standFirePointRight.position;
            }
        }
        else if (shotDirection == "Left")
        {
            if (PlayerController.instance.isWallShooting)
            {
                transform.position = PlayerController.instance.wallFirePointLeft.position;
            }
            else if (PlayerController.instance.isJumpShooting)
            {
                transform.position = PlayerController.instance.jumpFirePointLeft.position;
            }
            else if (PlayerController.instance.isRunShooting)
            {
                transform.position = PlayerController.instance.runFirePointLeft.position;
            }
            else // if isStandShooting
            {
                transform.position = PlayerController.instance.standFirePointLeft.position;
            }
        }
    }
}
