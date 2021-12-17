using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HittableEnemy : MonoBehaviour
{

    public bool isClone;
    public GameObject explosion;
    public Transform spriteParent;
    public Material hitMaterial;
   

    public int doubleCycloneWindOrbDamage, lightningWebDamage, thunderDancerDamage, magmaBladeFireShotDamage, slashDamage;
    public bool isHit;

    private SpriteRenderer sprite;
    private Animator anim;
    

    private Material defaultMaterial;
    protected Color defaultColor = Color.white;
    private float baseScale;
    private CyberPeacockBoss cyberPeacockBoss;
     


    protected virtual void Awake()
    {
        if (!isClone)
        {
            Transform spriteParentTransform = spriteParent != null ? spriteParent : transform;
            sprite = spriteParentTransform.GetComponentInChildren<SpriteRenderer>();

            baseScale = transform.localScale.y;
            defaultMaterial = sprite.material;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cyberPeacockBoss = GetComponentInParent<CyberPeacockBoss>();
        anim = GetComponentInParent<Animator>();


        if (!isClone)
        {
            isHit = false;
            hitMaterial = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isClone)
        {
            if (other.gameObject.tag.Contains("Shot") || other.gameObject.tag.Equals("SpecialShot") || other.tag.Equals("PlayerAttack"))
            {
                isHit = true;
                sprite.material = hitMaterial;
                StartCoroutine(ResetMaterial(0.15f));

                // DOUBLE CYCLONE WIND ORB --------------------------------------------\\
                if (other.gameObject.name.Equals("Double Cyclone Wind Orb(Clone)"))
                {
                    cyberPeacockBoss.currentHealth -= doubleCycloneWindOrbDamage;
                }

                // LIGHTNING WEB ------------------------------------------------------\\
                if (other.gameObject.name.Equals("Lightning Web Shot(Clone)"))
                {
                    cyberPeacockBoss.currentHealth -= lightningWebDamage;
                }

                // THUNDER DANCER ------------------------------------------------------\\
                if (other.gameObject.name.Equals("Thunder Dancer(Clone)"))
                {
                    cyberPeacockBoss.currentHealth -= thunderDancerDamage;
                }

                // MAGMA BLADE FIRE SHOT ------------------------------------------------------\\
                if (other.gameObject.name.Equals("Magma Blade Fire Shot(Clone)"))
                {
                    cyberPeacockBoss.currentHealth -= magmaBladeFireShotDamage;
                }

                if (other.tag.Equals("PlayerAttack"))
                {
                    cyberPeacockBoss.currentHealth -= slashDamage;
                }
            }

            if (other.gameObject.tag == "ShotLevel_5")
            {
                cyberPeacockBoss.currentHealth -= 7;
                AudioManager.instance.PlaySFXOverlap(25);
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_4")
            {
                cyberPeacockBoss.currentHealth -= 5;
                AudioManager.instance.PlaySFXOverlap(25);
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_3")
            {
                cyberPeacockBoss.currentHealth -= 3;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_2")
            {
                cyberPeacockBoss.currentHealth -= 2;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_1")
            {
                cyberPeacockBoss.currentHealth -= 1;
                Destroy(other);
                //  spriteRenderer.material = materialWhite;
            }
            
            cyberPeacockBoss.currentHealthSlider.value = cyberPeacockBoss.currentHealth;

            if (cyberPeacockBoss.currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(2);
                Destroy(other);
                cyberPeacockBoss.DestroyBoss();
            }
            else
            {
                // Invoke("ResetMaterial", 0.1f);
            }
        }
        else
        // If is a CLONE
        {
            if (other.gameObject.tag.Contains("Shot") || other.gameObject.tag.Equals("SpecialShot") || other.tag.Equals("PlayerAttack"))
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(126);
                anim.SetTrigger("cloneHit");
               // UIController.instance.cyberPeacockScore.text = (PlayerController.instance.updatedScore + 30).ToString();
            }
        }
    }


    public IEnumerator ResetMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.material = defaultMaterial;
        isHit = false;
    }


}
