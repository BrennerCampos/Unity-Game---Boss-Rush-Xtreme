using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CameraShake cameraShake;
    public Transform farBackground_1, farBackground_2, middleBackground, target;
    public float minHeight, maxHeight, minWidth, maxWidth;
    public bool stopFollow, shakeIt;

    private Vector2 lastPos;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Keeping track of what our previous x and y values were so we can apply it accordingly to the camera system (Vector3 -> Vector2)
        cameraShake = instance.GetComponent<CameraShake>();
        lastPos = transform.position;
        shakeIt = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (stopFollow == false)
        {
            // Setting a 'clamp' on our screen/camera on our y axis
            transform.position = new Vector3(Mathf.Clamp(target.position.x, minWidth, maxWidth),
                Mathf.Clamp(target.position.y, minHeight, maxHeight),
                transform.position.z);

            // Setting up movement constant based on our current and last positions
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            // Adding on to the far background position with 'amountToMove' position values
            farBackground_1.position += new Vector3(amountToMove.x, amountToMove.y, 0f);

            if (farBackground_2 != null)
            {
                farBackground_2.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            }
            
            // Adding on to the middle background position with 'amountToMove' position values
            // (Half the distance of which we moved the farBackground_1 (static following) to create parallax effect
            if (middleBackground != null)
            {
                middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
            }
            

            // Lastly, update our position to use as our last position for next iteration
            lastPos = transform.position;
        }

        if (shakeIt)
        {
            StartCoroutine("Shake");
        }
        

    }

    public void ShakeCamera(float strength, float duration = 1.0f)
    {
        //Tween.Shake(transform, transform.localPosition, new Vector3(strength, strength, 0), duration, 0);
        
    }
}
