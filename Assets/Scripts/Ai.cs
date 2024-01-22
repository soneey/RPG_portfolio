using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    private enum enemyMotion
    {
        None,
        Attack,
        Idle,
        Step,
        Dead,
    }
    enemyMotion curMotion = enemyMotion.None;
    enemyMotion beforeMotion = enemyMotion.None;


    [Header("스테이터스")]
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float curMp;
    [SerializeField] private float maxMp;
    [SerializeField] private float damage;

    [Header("액션")]
    [SerializeField] private float moveDelayCheck = 100.0f;
    [SerializeReference] private float moveSpeed;
    private bool boolMoveDelayCheck;//이동 후 딜레이체크 시작,종료 체크
    [SerializeField] private bool isMoving;//이동중인지 체크
    [SerializeField] private float attackDelayCheck = 100.0f;
    [SerializeField] private float attackSpeed;
    private bool boolAttackDelayCheck;
    [SerializeField] private bool isAttack;

    [SerializeField] Transform player;
    [SerializeField] GameObject castEffect;

    [Header("스프라이트 변경")]
    [SerializeField] private Sprite[] idle;//스프라이트 등록
    [SerializeField] private bool footCheck;//왼발 오른발 순서 체크
    private float spriteChangeDelay = 0.0f;
    [SerializeField] private float ratio = 0.0f;
    private bool checkChangeSpriteDelay;
    private Vector2 trsGaugeBarPos;
    private Vector2 trsMpGaugeBarPos;
    Vector3 lookDir = Vector3.down;
    Vector3 target;
    Vector3 before;
    Vector3 after;
    Vector3 moveVec;
    Vector3 trsTarget;
    private bool beforeSave;
    private GaugeBar gaugeBar;
    private MpGaugeBar mpGaugeBar;
    [SerializeField] GameObject HpGauge;
    [SerializeField] GameObject MpGauge;
    GameObject attackTarget;
    [SerializeField] GameObject objPlayer;
    private Color sprDefault;
    SpriteRenderer sr;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sprDefault = sr.color;
        boxCollider2D = GetComponent<BoxCollider2D>();
        curHp = maxHp;
        curMp = maxMp;
    }
    void Start()
    {
        GameManager manager = GameManager.Instance;
        trsGaugeBarPos = transform.localPosition;
        GameObject obj = Instantiate(HpGauge, trsGaugeBarPos, Quaternion.identity, transform.transform);
        gaugeBar = obj.GetComponent<GaugeBar>();
        gaugeBar.SetHp(curHp, maxHp);
        trsMpGaugeBarPos.y = transform.localPosition.y - 0.05f;
        trsMpGaugeBarPos.x = transform.localPosition.x;
        GameObject obj2 = Instantiate(MpGauge, trsMpGaugeBarPos, Quaternion.identity, transform.transform);
        mpGaugeBar = obj2.GetComponent<MpGaugeBar>();
        mpGaugeBar.SetMp(curMp, maxMp);
        setTarget = true;
        setMovingTarget();
    }

    void Update()
    {
        moveTarget();
        checkMoveDelay(moveSpeed);
        changeSprite();
        checkAttackDelay(attackSpeed);
        attack();
    }

    private void FixedUpdate()
    {
        gaugeBar.SetHp(curHp, maxHp);
        mpGaugeBar.SetMp(curMp, maxMp);
    }
    public void Heal()
    {
        Player playerSc = objPlayer.GetComponent<Player>();
        playerSc.heal();
        GameObject obj = Instantiate(castEffect, playerSc.transform.position, Quaternion.identity);
        GameObject obj2 = Instantiate(castEffect, transform.position, Quaternion.identity);
        SpellEffect objSc = obj.GetComponent<SpellEffect>();
        SpellEffect objSc2 = obj2.GetComponent<SpellEffect>();
        objSc.showHealEffect();
        objSc2.showCastEffect();
        curMp -= 10;
        mpGaugeBar.SetMp(curMp, maxMp);
        setTarget = true;
        setMovingTarget();
    }



    bool setTarget;
    private void setMovingTarget()
    {
        if (setTarget == false) { return; }
        if (setTarget == true && Vector2.Distance(gameObject.transform.position, objPlayer.transform.position) != 0.5f)
        {
            trsTarget = objPlayer.transform.position;
            before = transform.position;
            if (trsTarget.y > transform.position.y)
            {
                target = new Vector2(transform.position.x, transform.position.y + 0.5f);
                lookDir = Vector3.up;
                sr.sprite = idle[6];
            }
            else if (trsTarget.y < transform.position.y)
            {
                target = new Vector2(transform.position.x, transform.position.y - 0.5f);
                lookDir = Vector3.down;
                sr.sprite = idle[9];
            }
            else if (trsTarget.x < transform.position.x)
            {
                target = new Vector2(transform.position.x - 0.5f, transform.position.y);
                lookDir = Vector3.left;
                sr.sprite = idle[0];
            }
            else if (trsTarget.x > transform.position.x)
            {
                target = new Vector2(transform.position.x + 0.5f, transform.position.y);
                lookDir = Vector3.right;
                sr.sprite = idle[3];
            }
            RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
            Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
            if (hit.Length != 1 && hit[1].transform.tag == "Enemy")
            {
                attackTarget = hit[1].transform.gameObject;
                attack();
                setTarget = false;
                isAttack = true;
                return;
            }
            if (hit.Length == 1)
            {
                setTarget = false;
                isMoving = true;
                checkChangeSpriteDelay = true;
                curMotion = enemyMotion.Step;
                moveTarget();
            }
        }
    }

    private void moveTarget()
    {
        if (isMoving == false) { return; }
        if (isMoving == true)
        {
            ratio += Time.deltaTime * 1.8f;
            after.x = Mathf.SmoothStep(transform.position.x, target.x, ratio);
            after.y = Mathf.SmoothStep(transform.position.y, target.y, ratio);
            transform.position = after;
        }
        if (target == after && isMoving == true && ratio > 1.0f)
        {
            //if (transform.position.x % 0.5 != 0 || transform.position.y % 0.5 != 0)
            //{
            //    transform.position = before;
            //}
            isMoving = false;
            ratio = 0.0f;
            curMotion = enemyMotion.Idle;
            boolMoveDelayCheck = true;
            checkChangeSpriteDelay = true;
        }
    }

    private void checkMoveDelay(float _value)
    {
        if (boolMoveDelayCheck == false) { return; }
        if (boolMoveDelayCheck == true)
        {
            if (moveDelayCheck == 100.0f)
            {
                moveDelayCheck -= _value;
            }
            if (moveDelayCheck != 100.0f)
            {
                moveDelayCheck += Time.deltaTime;
            }
            if (moveDelayCheck > 100)
            {
                boolMoveDelayCheck = false;
                moveDelayCheck = 100.0f;
                ratio = 0.0f;
                setTarget = true;
                setMovingTarget();
            }
        }
    }

    private void checkAttackDelay(float _value)
    {
        if (boolAttackDelayCheck == false) { return; }
        if (attackDelayCheck == 100.0f && boolAttackDelayCheck == true)
        {
            attackDelayCheck -= _value;
        }
        if (attackDelayCheck != 100.0f && boolAttackDelayCheck == true)
        {
            attackDelayCheck += Time.deltaTime;
        }

        if (attackDelayCheck > 100)
        {
            attackDelayCheck = 100.0f;
            boolAttackDelayCheck = false;
        }
    }


    private void attack()
    {
        if (isAttack == true && boolAttackDelayCheck == false)
        {
            if (attackTarget.gameObject != null && Vector2.Distance(attackTarget.gameObject.transform.position, transform.position) == 0.5f)
            {
                GameObject obj = attackTarget.gameObject;
                Enemy objSc = obj.gameObject.GetComponent<Enemy>();
                objSc.DamagefromEnemy(damage, lookDir, this.gameObject);
                GameObject cast = Instantiate(castEffect, obj.transform.position, Quaternion.identity);
                SpellEffect castSc = cast.GetComponent<SpellEffect>();
                castSc.showAiAttackEffect();
                boolAttackDelayCheck = true;
            }
            else
            {
                isAttack = false;
                setTarget = true;
                setMovingTarget();
            }
        }
    }

    private void changeSprite()
    {
        if (checkChangeSpriteDelay == false) { return; }
        if (checkChangeSpriteDelay == true)
        {
            if (curMotion == enemyMotion.Step && footCheck == false)
            {
                if (spriteChangeDelay == 0)
                {
                    enemyMotionChange();
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.Idle;
                    footCheck = true;
                }
            }
            if (curMotion == enemyMotion.Step && footCheck == true)
            {
                if (spriteChangeDelay == 0)
                {
                    enemyMotionChange();
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.Idle;
                    footCheck = false;
                }
            }
            enemyMotionChange();
        }
    }
    private void enemyMotionChange()
    {
        switch (curMotion)
        {
            case enemyMotion.Idle:
                {
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.left && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[0];
                    }
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.right && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[3];
                    }
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.up && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[6];
                    }
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.down && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[9];
                    }
                    break;
                }
            case enemyMotion.Step:
                {
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.left && footCheck == false && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[1];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.left && footCheck == true && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[2];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.right && footCheck == false && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[4];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.right && footCheck == true && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[5];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.up && footCheck == false && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[7];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.up && footCheck == true && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[8];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.down && footCheck == false && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[10];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.down && footCheck == true && checkChangeSpriteDelay == true)
                    {
                        sr.sprite = idle[11];
                    }
                    break;
                }

        }
        checkChangeSpriteDelay = false;
    }

    public void SetHp(GaugeBar _value)
    {
        gaugeBar = _value;
        gaugeBar.SetHp(curHp, maxHp);
    }
    public void DamagefromEnemy(float _damage)
    {
        curHp -= _damage;
        gaugeBar.SetHp(curHp, maxHp);
        sprDefault = sr.color;
        sr.color = new Color(1, 0, 0, 0.5f);
        Invoke("setSpriteDefault", 0.2f);
    }
    private void setSpriteDefault()
    {
        sr.color = sprDefault;
    }
    public void SetMp(MpGaugeBar _value)
    {
        mpGaugeBar = _value;
        mpGaugeBar.SetMp(curMp, maxMp);
    }
    public float getCurHp()
    {
        return curHp;
    }

}
