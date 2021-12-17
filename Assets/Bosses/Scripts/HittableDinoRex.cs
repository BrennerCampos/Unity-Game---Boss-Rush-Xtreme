using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HittableDinoRex : MonoBehaviour
{
    public GameObject explosion;
    public Transform spriteParent;
    public Material hitMaterial;
    public int doubleCycloneScore;
    public int lightningWebScore;
    public int thunderDancerScore;
    public int magmaBladeFireShotScore;
    public int magmaBladeSlashScore;

    public float comboTimer, startComboTimer, comboMultiplier;
    public int comboCount;
    private int scoreForAction;
    private bool comboStartBool;

    public int doubleCycloneWindOrbDamage, lightningWebDamage, thunderDancerDamage, magmaBladeFireShotDamage, slashDamage;
    public bool isHit;

    private SpriteRenderer sprite;
    private Animator anim;

    private Material defaultMaterial;
    protected Color defaultColor = Color.white;
    private float baseScale;
    private DinoRexBoss dinoRexBoss;


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
        comboCount = 0;
        comboTimer = startComboTimer;
        comboMultiplier = 1;
        dinoRexBoss = GetComponentInParent<DinoRexBoss>();
        anim = GetComponentInParent<Animator>();

        isHit = false;
        hitMaterial = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    }

    void Update()
    {
        if (comboTimer > 0 && comboStartBool)
        {
            comboTimer -= Time.deltaTime;
        }

        if (comboTimer <= 0)
        {
            comboCount = 0;
            comboMultiplier = 1;
            comboTimer = startComboTimer;
            comboStartBool = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        var updatedScore = int.Parse(UIController.instance.dinoRexScore.text);

        if (other.gameObject.tag.Contains("Shot") || other.gameObject.tag.Equals("SpecialShot") || other.tag.Equals("PlayerAttack"))
        {
            if (comboTimer > 0)
            {
                comboCount++;
                Debug.Log("Combo Count = " + comboCount);

                comboTimer = startComboTimer;

                if (comboCount == 15)
                {
                    comboMultiplier = 1.25f;
                    Debug.Log("Combo Multiplier = " + comboMultiplier);
                }
                else if (comboCount == 35)
                {
                    comboMultiplier = 1.5f;
                    Debug.Log("Combo Multiplier = " + comboMultiplier);
                }
                else if (comboCount == 50)
                {
                    comboMultiplier = 2f;
                    Debug.Log("Combo Multiplier = " + comboMultiplier);
                }
                else if (comboCount == 100)
                {
                    comboMultiplier = 3f;
                    Debug.Log("Combo Multiplier = " + comboMultiplier);
                }
                else
                {
                    // comboMultiplier = 1;
                }
            }

            isHit = true;
            comboStartBool = true;
            sprite.material = hitMaterial;
            StartCoroutine(ResetMaterial(0.15f));

            // DOUBLE CYCLONE WIND ORB --------------------------------------------\\
            if (other.gameObject.name.Equals("Double Cyclone Wind Orb(Clone)"))
            {
                dinoRexBoss.currentHealth -= doubleCycloneWindOrbDamage;
                scoreForAction = doubleCycloneScore;
            }

            // LIGHTNING WEB ------------------------------------------------------\\
            if (other.gameObject.name.Equals("Lightning Web Shot(Clone)"))
            {
                dinoRexBoss.currentHealth -= lightningWebDamage;
                scoreForAction = lightningWebScore;
            }

            // THUNDER DANCER ------------------------------------------------------\\
            if (other.gameObject.name.Equals("Thunder Dancer(Clone)"))
            {
                dinoRexBoss.currentHealth -= thunderDancerDamage;
                scoreForAction = thunderDancerScore;
            }

            // MAGMA BLADE FIRE SHOT ------------------------------------------------------\\
            if (other.gameObject.name.Equals("Magma Blade Fire Shot(Clone)"))
            {
                dinoRexBoss.currentHealth -= magmaBladeFireShotDamage;
                scoreForAction = magmaBladeFireShotScore;
            }

            if (other.tag.Equals("PlayerAttack"))
            {
                dinoRexBoss.currentHealth -= slashDamage;
                scoreForAction = magmaBladeSlashScore;
            }
            dinoRexBoss.currentHealthSlider.value = dinoRexBoss.currentHealth;
        }

        if (other.gameObject.tag == "ShotLevel_5")
        {
            dinoRexBoss.currentHealth -= 7;
            AudioManager.instance.PlaySFXOverlap(25);
            scoreForAction = 1000;
          
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_4")
        {
            dinoRexBoss.currentHealth -= 5;
            AudioManager.instance.PlaySFXOverlap(25);
            scoreForAction = 850;
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_3")
        {
            dinoRexBoss.currentHealth -= 3;
            scoreForAction = 600;
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_2")
        {
            dinoRexBoss.currentHealth -= 2;
            scoreForAction = 400;
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_1")
        {
            dinoRexBoss.currentHealth -= 1;
            scoreForAction = 250;
           
            Destroy(other);
            //  spriteRenderer.material = materialWhite;
        }

        if (other.gameObject.tag.Contains("Shot") || other.gameObject.tag.Equals("SpecialShot") ||
            other.tag.Equals("PlayerAttack") ||
            other.gameObject.tag.Contains("ShotLevel"))
        {
            UIController.instance.dinoRexScore.text =
                Mathf.Ceil(updatedScore + (scoreForAction * comboMultiplier)).ToString();
            dinoRexBoss.currentHealthSlider.value = dinoRexBoss.currentHealth;
        }

        if (dinoRexBoss.currentHealthSlider.value <= 0)
        {
            AudioManager.instance.PlaySFX_NoPitchFlux(2);
            Destroy(other);
            dinoRexBoss.DestroyBoss();
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
