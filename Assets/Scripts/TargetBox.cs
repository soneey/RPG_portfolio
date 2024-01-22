using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetBox : MonoBehaviour
{
    GameObject player;
    GameObject curTarget;
    [SerializeField] Sprite[] hellFire;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }
    void Start()
    {
    }

    void Update()
    {
        Player playerSc = player.GetComponent<Player>();
        curTarget = playerSc.CurTarget();
        if (curTarget != null)
        {
            transform.position = curTarget.transform.position;
        }
        else
        {
            curTarget = player.gameObject;
        }
    }
    private void FixedUpdate()
    {
    }
    public void spell()
    {
        StartCoroutine(hellFireSprite());
    }
    IEnumerator hellFireSprite()
    {
        sr.sprite = hellFire[0];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellFire[1];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellFire[2];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellFire[3];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellFire[4];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellFire[5];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = hellFire[6];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
