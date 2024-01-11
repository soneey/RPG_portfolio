using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    [SerializeField] private float CurHp;
    [SerializeField] private float MaxHp;
    [SerializeField] private float Damage;
    [SerializeField] private float AttackSpeed;

    [Header("이동관련")]
    [SerializeField, Tooltip("100이 되면 행동, 변경하지 말 것")] private float checkDelayCount = 100.0f;
    [SerializeField, Tooltip("이동간의 딜레이 시간 설정")] private float moveTime;
    [SerializeField, Tooltip("리스폰 시간")] private float respawnTime;

    [Header("스프라이트 변경")]
    [SerializeField] private Sprite[] idle;//스프라이트 등록
    [SerializeField] private bool footCheck;//왼발 오른발 순서 체크
    private float spriteChangeDelay = 0.0f;
    private float ratio = 0.0f;
    private int randomDirNumber;
    private bool isMoving;//이동중인지 체크
    private bool isAttack;
    private bool checkDelay;//이동 후 딜레이체크 시작,종료 체크
    private bool checkChangeSpriteDelay;
    private Vector2 trsGaugeBarPos;
    Vector3 moveVec;
    Vector3 lookDir;
    Vector3 target;
    Vector3 before;
    Vector3 after;
    private bool beforeSave;

    //[SerializeField] private Sprite sprHit;
    private Color sprDefault;
    SpriteRenderer sr;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;
    Transform checkBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack == true && collision.gameObject.tag == "Player")
        {
            //Enemy enemySc = collision.GetComponent<Enemy>();
            //enemySc.DamagefromEnemy(Damage);
            //Destroy(gameObject);
        }
    }
    private void Awake()
    {
        CurHp = MaxHp;
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sprDefault = sr.color;
        boxCollider2D = GetComponent<BoxCollider2D>();
        createGaugeBar();
    }
    void Start()
    {
        moveVec = transform.position;
    }

    private void Update()
    {
        rigid.velocity = Vector3.zero;
    }
    void FixedUpdate()
    {
        checkNext();
        moving();
        softMoving();
        changeSprite();
        dead();
    }

    private void checkNext()
    {
        if (checkDelay == false)
        {
            getRandomNumber();
        }
        if (isMoving == true) { return; }
        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.3f);
        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.3f, Color.red);
        for (int iNum = 1; iNum < hit.Length; iNum++)
        {
            int check = GameManager.Instance.GetMonsterNumber();//몬스터번호0번 토끼일때
            if (check == 0 && hit[1].transform != null && hit[1].transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log(hit[1].transform.name);
                run();
            }
        }
    }


    private void run()
    {
        if (lookDir.x == -1 && isMoving == false)
        {
            isMoving = true;
            beforeSave = true;
            target = new Vector3(transform.position.x + 0.5f, transform.position.y);
            checkDelay = true;
            checkChangeSpriteDelay = true;
            lookDir = Vector3.right;
            curMotion = enemyMotion.Step;
        }
        if (lookDir.x == 1 && isMoving == false)
        {
            isMoving = true;
            beforeSave = true;
            target = new Vector3(transform.position.x - 0.5f, transform.position.y);
            checkDelay = true;
            checkChangeSpriteDelay = true;
            lookDir = Vector3.left;
            curMotion = enemyMotion.Step;
        }
        if (lookDir.y == 1 && isMoving == false)
        {
            isMoving = true;
            beforeSave = true;
            target = new Vector3(transform.position.x, transform.position.y - 0.5f);
            checkDelay = true;
            checkChangeSpriteDelay = true;
            lookDir = Vector3.down;
            curMotion = enemyMotion.Step;
        }
        if (lookDir.y == -1 && isMoving == false)
        {
            isMoving = true;
            beforeSave = true;
            target = new Vector3(transform.position.x, transform.position.y + 0.5f);
            checkDelay = true;
            checkChangeSpriteDelay = true;
            lookDir = Vector3.up;
            curMotion = enemyMotion.Step;
        }

        //if체력일정이하 도망
    }
    private void softMoving()
    {
        if (isMoving == true && beforeSave == true)
        {
            before = transform.position;
            beforeSave = false;
        }
        //Debug.Log($"before = {before}");
        //Debug.Log($"target = {target}");
        //Debug.Log($"ratio = {ratio}");
        if (isMoving == true && beforeSave == false)
        {
            ratio += Time.deltaTime * 2.0f;
            //Debug.Log($"moveVec = {moveVec}");
            switch (randomDirNumber)
            {
                case 0:
                    {
                        after.x = Mathf.SmoothStep(before.x, target.x, ratio);
                        after.y = before.y;
                        break;
                    }
                case 1:
                    {
                        after.x = Mathf.SmoothStep(before.x, target.x, ratio);
                        after.y = before.y;
                        break;
                    }
                case 2:
                    {
                        after.y = Mathf.SmoothStep(before.y, target.y, ratio);
                        after.x = before.x;
                        break;
                    }
                case 3:
                    {
                        after.y = Mathf.SmoothStep(before.y, target.y, ratio);
                        after.x = before.x;
                        break;
                    }

            }
            moveVec = after;
        }
        if (isMoving == true && ratio >= 1.0f)
        {
            ratio = 0.0f;
            isMoving = false;
        }
        transform.position = moveVec;
    }
    private void moving()
    {
        if (checkDelay == false)
        {
            getRandomNumber();
            switch (randomDirNumber)
            {
                case 0:
                    {
                        lookDir = Vector3.left;
                        curMotion = enemyMotion.Idle;
                        enemyMotionChange();
                        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.3f);
                        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.3f, Color.red);
                        if (hit.Length != 1)
                        {
                            return;
                        }
                        else
                        {
                            isMoving = true;
                            beforeSave = true;
                            target = new Vector3(transform.position.x - 0.5f, transform.position.y);
                            checkDelay = true;
                            checkChangeSpriteDelay = true;
                            curMotion = enemyMotion.Step;
                        }
                        break;

                    }
                case 1:
                    {
                        lookDir = Vector3.right;
                        curMotion = enemyMotion.Idle;
                        enemyMotionChange();
                        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.3f);
                        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.3f, Color.red);
                        if (hit.Length != 1)
                        {
                            return;
                        }
                        else
                        {
                            isMoving = true;
                            beforeSave = true;
                            target = new Vector3(transform.position.x + 0.5f, transform.position.y);
                            checkDelay = true;
                            checkChangeSpriteDelay = true;
                            curMotion = enemyMotion.Step;
                        }
                        break;

                    }
                case 2:
                    {
                        lookDir = Vector3.up;
                        curMotion = enemyMotion.Idle;
                        enemyMotionChange();
                        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.3f);
                        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.3f, Color.red);
                        if (hit.Length != 1)
                        {
                            return;
                        }
                        else
                        {
                            isMoving = true;
                            beforeSave = true;
                            target = new Vector3(transform.position.x, transform.position.y + 0.5f);
                            checkDelay = true;
                            checkChangeSpriteDelay = true;
                            curMotion = enemyMotion.Step;
                        }
                        break;

                    }
                case 3:
                    {
                        lookDir = Vector3.down;
                        curMotion = enemyMotion.Idle;
                        enemyMotionChange();
                        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.3f);
                        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.3f, Color.red);
                        if (hit.Length != 1)
                        {
                            return;
                        }
                        else
                        {
                            isMoving = true;
                            beforeSave = true;
                            target = new Vector3(transform.position.x, transform.position.y - 0.5f);
                            checkDelay = true;
                            checkChangeSpriteDelay = true;
                            curMotion = enemyMotion.Step;
                        }
                        break;
                    }
                case 4:
                    checkDelay = true;
                    break;
                case 5:
                    checkDelay = true;
                    break;
            }
        }
        checkActionDelay(moveTime);
    }
    private void checkActionDelay(float _value)
    {
        if (checkDelay == false) { return; }
        if (checkDelayCount == 100.0f && checkDelay == true)
        {
            checkDelayCount -= _value;
        }
        if (checkDelayCount != 100.0f && checkDelay == true)
        {
            checkDelayCount += Time.deltaTime;
            //Debug.Log(checkDelayCount);
        }
        if (checkDelayCount > 100)
        {
            checkDelayCount = 100.0f;
            checkDelay = false;
        }
    }
    private void getRandomNumber()
    {
        randomDirNumber = UnityEngine.Random.Range(0, 6);
        //if (randomDirNumber == 0) Debug.Log("<color=aqua>next left</color>");
        //if (randomDirNumber == 1) Debug.Log("<color=aqua>next right</color>");
        //if (randomDirNumber == 2) Debug.Log("<color=aqua>next up</color>");
        //if (randomDirNumber == 3) Debug.Log("<color=aqua>next down</color>");
        //if (randomDirNumber == 4) Debug.Log("<color=aqua>next rest</color>");
        //if (randomDirNumber == 5) Debug.Log("<color=aqua>next rest</color>");
    }
    private void changeSprite()
    {
        if (checkChangeSpriteDelay == false) { return; }
        if (checkChangeSpriteDelay == true)
        {
            if (curMotion == enemyMotion.Step && footCheck == false)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    enemyMotionChange();
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.Idle;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == enemyMotion.Step && footCheck == true)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    enemyMotionChange();
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.Idle;
                    checkChangeSpriteDelay = false;
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
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.left)
                    {
                        sr.sprite = idle[0];
                    }
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.right)
                    {
                        sr.sprite = idle[3];
                    }
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.up)
                    {
                        sr.sprite = idle[6];
                    }
                    if (curMotion == enemyMotion.Idle && lookDir == Vector3.down)
                    {
                        sr.sprite = idle[9];
                    }
                    break;
                }
            case enemyMotion.Step:
                {
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.left && footCheck == false)
                    {
                        sr.sprite = idle[1];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.left && footCheck == true)
                    {
                        sr.sprite = idle[2];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.right && footCheck == false)
                    {
                        sr.sprite = idle[4];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.right && footCheck == true)
                    {
                        sr.sprite = idle[5];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.up && footCheck == false)
                    {
                        sr.sprite = idle[7];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.up && footCheck == true)
                    {
                        sr.sprite = idle[8];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.down && footCheck == false)
                    {
                        sr.sprite = idle[10];
                    }
                    if (curMotion == enemyMotion.Step && lookDir == Vector3.down && footCheck == true)
                    {
                        sr.sprite = idle[11];
                    }
                    break;
                }
        }
    }
    private void dead()
    {
        if (CurHp > 0) { return; }
        else
        {
            allDelayCheck = true;
            Destroy(gameObject);
        }
    }
    [SerializeField] GameObject HpGaugeBar;
    private void createGaugeBar()
    {
        trsGaugeBarPos = transform.localPosition;
        GameObject objHpGaugeBar = HpGaugeBar;
        GameObject obj = Instantiate(objHpGaugeBar, trsGaugeBarPos, Quaternion.identity);
    }

    float allDelay = 100.0f;
    bool allDelayCheck;
    private void checkAllDelay(float _value)
    {
        if (allDelayCheck == false) { return; }
        if (allDelay == 100.0f && allDelayCheck == true)
        {
            allDelay -= _value;
        }
        if (allDelay != 100.0f && allDelayCheck == true)
        {
            allDelay += Time.deltaTime;
        }
        if (allDelay > 100)
        {
            allDelay = 100.0f;
            allDelayCheck = false;
        }

    }
    public void DamagefromEnemy(float _damage)
    {
        Debug.Log($"Damage = {_damage}");
        CurHp -= _damage;
        Debug.Log($"CurHp = {CurHp}");
        sprDefault = sr.color;
        sr.color = new Color(1, 1, 1, 0.2f);
        Invoke("setSpriteDefault", 0.1f);
    }
    public float GetRespawnTime()
    {
        return respawnTime;
    }
    private void setSpriteDefault()
    {
        sr.color = sprDefault;
    }
}
