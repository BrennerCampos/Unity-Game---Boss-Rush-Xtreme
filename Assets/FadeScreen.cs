using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public float blackScreenOpacity;
    private Image screen;

    void Start()
    {
        screen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnBlack()
    {
        screen.color = new Color(255, 255, 255, 1);
    }

    public void TurnOpaque()
    {
        screen.color = new Color(255, 255, 255, 0);
    }

}
