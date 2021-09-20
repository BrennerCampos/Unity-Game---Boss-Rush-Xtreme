using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHazardDamageHandler : MonoBehaviour
{

    public GameObject boss;
    public Slider currentHealthSlider;

    private DinoRexBoss bossScript;
    private int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Enemy");
        bossScript = boss.GetComponent<DinoRexBoss>();
        this.currentHealth = bossScript.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetMaterial()
    {
        //  spriteRenderer.material = materialDefault;
    }

}
