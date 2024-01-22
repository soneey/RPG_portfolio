using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    [SerializeField] Sprite[] castEffect;
    [SerializeField] Sprite[] attackEffect;
    [SerializeField] Sprite[] aiAttackEffect;
    [SerializeField] Sprite[] healEffect;
    [SerializeField] Sprite[] hellfireEffect;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
    public void showHellfireEffect()
    { 
        StartCoroutine(hellfireEffectSprite());
    }
    public void showCastEffect()
    { 
        StartCoroutine(castEffectSprite());
    }
    public void showAiAttackEffect()
    { 
        StartCoroutine(AiAttackEffectSprite());
    }
    public void showAttackEffect()
    { 
        StartCoroutine(attackEffectSprite());
    }
    public void showHealEffect()
    {
        StartCoroutine(healEffectSprite());
    }
    IEnumerator hellfireEffectSprite()
    {
        sr.sprite = hellfireEffect[0];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellfireEffect[1];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellfireEffect[2];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellfireEffect[3];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellfireEffect[4];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellfireEffect[5];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellfireEffect[6];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    IEnumerator healEffectSprite()
    {
        sr.sprite = healEffect[0];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[1];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[2];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[3];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[4];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[5];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[6];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = healEffect[7];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    IEnumerator AiAttackEffectSprite()
    {
        sr.sprite = aiAttackEffect[0];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[1];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[2];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[3];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[4];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[5];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[6];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[7];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = aiAttackEffect[8];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    IEnumerator attackEffectSprite()
    {
        sr.sprite = attackEffect[0];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = attackEffect[1];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = attackEffect[2];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = attackEffect[3];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = attackEffect[4];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = attackEffect[5];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = attackEffect[6];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = attackEffect[7];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    IEnumerator castEffectSprite()
    {
        sr.sprite = castEffect[0];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = castEffect[1];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = castEffect[2];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = castEffect[3];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = castEffect[4];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = castEffect[5];
        yield return new WaitForSeconds(0.1f); 
        sr.sprite = castEffect[6];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    
}
