using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScreenManager : MonoBehaviour
{

    private bool selected, input;
    public GameObject TxMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey)
        {
            input = true;
            TxMenu.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        if (input)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("IntroScreen");
            }
        }

    }
}
