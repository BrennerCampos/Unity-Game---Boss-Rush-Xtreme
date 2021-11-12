using System.Collections.Generic;
using UnityEngine;

public class CommandPattern : MonoBehaviour
{
    
    public Transform instanceTransform;

    private Command growButton, shrinkButton, rotateButton;

    internal List<Command> commandQueue;


    // Start is called before the first frame update
    private void Start()
    {
        // Defining commands & buttons
        growButton = new objectGrow();
        shrinkButton = new objectShrink();
        rotateButton = new objectRotate();
    }

    // Update is called once per frame
    private void Update()
    {
        handleInput();
    }

    private void handleInput()
    {
        // Specific command triggers and actions
        if (Input.GetKeyDown(KeyCode.G))
        {
            growButton.Execute(instanceTransform);
        }
        else
        if (Input.GetKeyDown(KeyCode.S))
        {
            shrinkButton.Execute(instanceTransform);
        }
        else
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotateButton.Execute(instanceTransform);
        }
    }
}