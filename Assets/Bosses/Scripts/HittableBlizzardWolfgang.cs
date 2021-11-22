using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HittableBlizzardWolfgang : MonoBehaviour
{

  
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
    private BlizzardWolfgangBoss blizzardWolfgangBoss;



    protected virtual void Awake()
    {
        
            Transform spriteParentTransform = spriteParent != null ? spriteParent : transform;
            sprite = spriteParentTransform.GetComponentInChildren<SpriteRenderer>();

            baseScale = transform.localScale.y;
            defaultMaterial = sprite.material;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        blizzardWolfgangBoss = GetComponentInParent<BlizzardWolfgangBoss>();
        anim = GetComponentInParent<Animator>();


        isHit = false;
        hitMaterial = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         
            if (other.gameObject.tag.Contains("Shot") || other.gameObject.tag.Equals("SpecialShot") || other.tag.Equals("PlayerAttack"))
            {
                isHit = true;
                sprite.material = hitMaterial;
                StartCoroutine(ResetMaterial(0.15f));

                // DOUBLE CYCLONE WIND ORB --------------------------------------------\\
                if (other.gameObject.name.Equals("Double Cyclone Wind Orb(Clone)"))
                {
                    blizzardWolfgangBoss.currentHealth -= doubleCycloneWindOrbDamage;
                }

                // LIGHTNING WEB ------------------------------------------------------\\
                if (other.gameObject.name.Equals("Lightning Web Shot(Clone)"))
                {
                    blizzardWolfgangBoss.currentHealth -= lightningWebDamage;
                }

                // THUNDER DANCER ------------------------------------------------------\\
                if (other.gameObject.name.Equals("Thunder Dancer(Clone)"))
                {
                    blizzardWolfgangBoss.currentHealth -= thunderDancerDamage;
                }

                // MAGMA BLADE FIRE SHOT ------------------------------------------------------\\
                if (other.gameObject.name.Equals("Magma Blade Fire Shot(Clone)"))
                {
                    blizzardWolfgangBoss.currentHealth -= magmaBladeFireShotDamage;
                }

                if (other.tag.Equals("PlayerAttack"))
                {
                    blizzardWolfgangBoss.currentHealth -= slashDamage;
                }
            }

            if (other.gameObject.tag == "ShotLevel_5")
            {
                blizzardWolfgangBoss.currentHealth -= 7;
                AudioManager.instance.PlaySFXOverlap(25);
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_4")
            {
                blizzardWolfgangBoss.currentHealth -= 5;
                AudioManager.instance.PlaySFXOverlap(25);
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_3")
            {
                blizzardWolfgangBoss.currentHealth -= 3;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_2")
            {
                blizzardWolfgangBoss.currentHealth -= 2;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_1")
            {
                blizzardWolfgangBoss.currentHealth -= 1;
                Destroy(other);
                //  spriteRenderer.material = materialWhite;
            }

            blizzardWolfgangBoss.currentHealthSlider.value = blizzardWolfgangBoss.currentHealth;

            if (blizzardWolfgangBoss.currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(2);
                Destroy(other);
                blizzardWolfgangBoss.DestroyBoss();
            }
            else
            {
                // Invoke("ResetMaterial", 0.1f);
            }
    }

    public IEnumerator ResetMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.material = defaultMaterial;
        isHit = false;
    }
}
