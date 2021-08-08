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

    /*private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            if (other.gameObject.name == "Buster Shot Bullet Level 5_0")
            {
                currentHealth -= 10;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.name == "Buster Shot Bullet Level 4_0")
            {
                currentHealth -= 7;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.name == "Buster Shot Bullet Level 3_0")
            {
                currentHealth -= 5;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.name == "Buster Shot Bullet Level 2_0")
            {
                currentHealth -= 3;
                // spriteRenderer.material = materialWhite;
            }
            else
            {
                currentHealth -= 1;
                //  spriteRenderer.material = materialWhite;
                currentHealthSlider.value = currentHealth;
            }


            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(2);
                // DestroyBoss();
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }

        var bossHealth = boss.GetComponent<DinoRexBoss>();
        bossHealth.currentHealth = currentHealth;
    }*/

    void ResetMaterial()
    {
        //  spriteRenderer.material = materialDefault;
    }

}
