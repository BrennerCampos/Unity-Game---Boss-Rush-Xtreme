using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject objectToSwitch;
    private SpriteRenderer spriteRenderer;

    public Sprite downSprite;
    public bool deactivateOnSwitch;

    private bool hasSwitched;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player touches switched and it hasn't already been switched on...
        if (other.tag == "Player" && !hasSwitched)
        {
            // If deactivate on switch is turned on...
            if (deactivateOnSwitch)
            {
                // Deactivate that associated object
                objectToSwitch.SetActive(false);
            }
            else // If set to activate instead...
            {
                // Activate that associated object
                objectToSwitch.SetActive(true);
            }
            //objectToSwitch.SetActive(false);
            // Switch sprite to down position (switched)
            spriteRenderer.sprite = downSprite;
            // Change hasSwitched bool to be true
            hasSwitched = true;
        }
    }
}
