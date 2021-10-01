using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{

    public int attackDamage;
    public float materialTimer, startMaterialTimer;
    public SpriteRenderer dinoRexSprite;
    public HittableEnemy hittableEnemy;
    public DinoRexBoss dinoRex;

    // Start is called before the first frame update
    void Start()
    {
        //enemySprite = dinoRex.GetComponent<SpriteRenderer>();
        dinoRex = FindObjectOfType<DinoRexBoss>();
        
        //dinoRexSprite = dinoRex.GetComponentInChildren<SpriteRenderer>();
        //hittableEnemy = dinoRex.GetComponentInChildren<HittableEnemy>();

       // materialTimer = startMaterialTimer;


    }

    // Update is called once per frame
    void Update()
    {
       

        /*if (dinoRex.materialTimer <= 0)
        {
            if (PlayerController.instance.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shadow_Slash_Jump"))
            {
                attackDamage = 15;
                hittableEnemy.isHit = true;
                dinoRexSprite.material = hittableEnemy.hitMaterial;
                StartCoroutine(hittableEnemy.ResetMaterial(0.15f));

            }
            else if (PlayerController.instance.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
                .IsName("Shadow_Nova_Strike"))
            {
                attackDamage = 10;
                hittableEnemy.isHit = true;
                dinoRexSprite.material = hittableEnemy.hitMaterial;
                StartCoroutine(hittableEnemy.ResetMaterial(0.15f));
            }
            else if (PlayerController.instance.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
                .IsName("Shadow_Slash_Stand"))
            {
                attackDamage = 8;
                hittableEnemy.isHit = true;
                dinoRexSprite.material = hittableEnemy.hitMaterial;
                StartCoroutine(hittableEnemy.ResetMaterial(0.15f));
            }

            dinoRex.materialTimer = dinoRex.startMaterialTimer;

        }*/
        
    }

    private void ActivateHitBox()
    {
        gameObject.SetActive(true);
    }


    private void DeactivateHitBox()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Wall")
        {
            //Instantiate(shotHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            //var shotEffect = Instantiate(shotHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            // enemySprite = other.GetComponent<SpriteRenderer>();
            //shotEffect.transform.localScale = new Vector3(11, 11, 1);

            // @TODO change this to check for specific boss tag before dealing damage?
            DinoRexBoss.instance.currentHealth -= attackDamage;
            
            
            AudioManager.instance.PlaySFX(19);

            //Destroy(other);
            //Destroy(gameObject);
        }

    }

}
