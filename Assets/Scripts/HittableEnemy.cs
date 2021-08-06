using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HittableEnemy : MonoBehaviour
{

    
    public GameObject explosion;
    public Transform spriteParent;
    public bool hideWhenDead = false;
    

    private SpriteRenderer sprite;
    private Animator anim;
    public Material hitMaterial;

    private Material defaultMaterial;
    protected Color defaultColor = Color.white;
    private float baseScale;
    private bool isHit;

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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        

        if (other.CompareTag("Bullet") && !isHit)
        {
            if (other.gameObject.name == "Buster Shot Bullet Level 5_0")
            {
                sprite.material = hitMaterial;
            }
            else if (other.gameObject.name == "Buster Shot Bullet Level 4_0")
            {
                sprite.material = hitMaterial;
            }
            else if (other.gameObject.name == "Buster Shot Bullet Level 3_0")
            {
                sprite.material = hitMaterial;
            }
            else if (other.gameObject.name == "Buster Shot Bullet Level 2_0")
            {
                sprite.material = hitMaterial;
            }
            else
            {
                isHit = true;
                sprite.material = hitMaterial;
                StartCoroutine(ResetMaterial(0.15f));
            }

        }
    }

    private IEnumerator ResetMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.material = defaultMaterial;
        isHit = false;
    }


}
