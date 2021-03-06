using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HittableEnemy : MonoBehaviour
{

    public GameObject explosion;
    public Transform spriteParent;
    public bool hideWhenDead = false;
    public bool isHit;


    private SpriteRenderer sprite;
    private Animator anim;
    public Material hitMaterial;

    private Material defaultMaterial;
    protected Color defaultColor = Color.white;
    private float baseScale;
    

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
        isHit = false;
        anim = GetComponent<Animator>();

        hitMaterial = Resources.Load("WhiteFlash", typeof(Material)) as Material;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Shot") || other.gameObject.tag.Equals("SpecialShot"))
        {
            isHit = true;
            sprite.material = hitMaterial;
            StartCoroutine(ResetMaterial(0.15f));
        }
    }

    public IEnumerator ResetMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.material = defaultMaterial;
        isHit = false;
    }


}
