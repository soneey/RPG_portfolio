using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

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


    [Header("�������ͽ�")]
    [SerializeReference] int monsterNumber;
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    [SerializeField] private float respawnTime;

    [Header("�׼�")]
    [SerializeField] private float moveDelayCheck = 100.0f;
    [SerializeReference] private float moveSpeed;
    private bool boolMoveDelayCheck;//�̵� �� ������üũ ����,���� üũ
    [SerializeField] private bool isMoving;//�̵������� üũ
    [SerializeField] private float attackDelayCheck = 100.0f;
    [SerializeField] private float attackSpeed;
    private bool boolAttackDelayCheck;
    [SerializeField] private bool isAttack;


    [Header("��������Ʈ ����")]
    [SerializeField] private Sprite[] idle;//��������Ʈ ���
    [SerializeField] private bool footCheck;//�޹� ������ ���� üũ
    private float spriteChangeDelay = 0.0f;
    [SerializeField] private float ratio = 0.0f;
    private int randomDirNumber;
    private bool checkChangeSpriteDelay;
    private Vector2 trsGaugeBarPos;
    Vector3 counterattackDir;
    Vector3 lookDir = Vector3.down;
    Vector3 target;
    Vector3 before;
    Vector3 after;
    Vector3 moveVec;
    private bool beforeSave;

    //[SerializeField] private Sprite sprHit;
    private Color sprDefault;
    SpriteRenderer sr;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;

    //private void OnValidate()
    //{
    //    if (gaugeBar != null)
    //        gaugeBar.SetHp(curHp, maxHp);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitBox")
        {
            Vector2 targerDir;
            Transform obj = collision.gameObject.transform.parent;
            targetPlayer = obj.gameObject;
            Player objSc = obj.GetComponent<Player>();
            targerDir = objSc.curLookDir();
            Debug.Log(targerDir);

            if (targerDir.x == -1)
            {
                lookDir = Vector3.right;
                sr.sprite = idle[3];
            }
            if (targerDir.x == 1)
            {
                lookDir = Vector3.left;
                sr.sprite = idle[0];
            }
            if (targerDir.y == -1)
            {
                lookDir = Vector3.up;
                sr.sprite = idle[6];
            }
            if (targerDir.y == 1)
            {
                lookDir = Vector3.down;
                sr.sprite = idle[9];
            }
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sprDefault = sr.color;
        boxCollider2D = GetComponent<BoxCollider2D>();
        SetStatus();
        curHp = maxHp;
    }
    private GaugeBar gaugeBar;
    GameObject HpGaugeBar;
    void Start()
    {
        monsterNumber = GameManager.Instance.GetMonsterNumber();//���͹�ȣ0�� �䳢�϶�
        moveVec = transform.position;
        GameManager manager = GameManager.Instance;
        trsGaugeBarPos = transform.localPosition;
        GameObject HpGaugeBar = manager.GetGaugeBar();
        GameObject obj = Instantiate(HpGaugeBar, trsGaugeBarPos, Quaternion.identity, transform.transform);
        gaugeBar = obj.GetComponent<GaugeBar>();
        gaugeBar.SetHp(curHp, maxHp);
    }
    private void Update()
    {

    }
    void FixedUpdate()
    {
        getRandomNumber();
        //setMovingTarget();
        checkMoveDelay(moveSpeed);
        softMoving();
        checkNext();
        changeSprite();
        checkAttackDelay(attackSpeed);
        dead();
    }

    private void SetStatus()
    {
        if (monsterNumber == 0)
        {
            moveSpeed = UnityEngine.Random.Range(7.5f, 10.0f);
            maxHp = UnityEngine.Random.Range(19, 22);
        }
        if (monsterNumber == 1)
        {
            moveSpeed = UnityEngine.Random.Range(3.5f, 4.0f);
            maxHp = UnityEngine.Random.Range(90, 111);
        }
    }

    bool boolAllStop;
    bool changeDice = true;
    private void getRandomNumber()
    {
        if (boolAllStop == true) { return; }
        if (changeDice == false) { return; }
        if (changeDice == true)
        {
            //Debug.Log("getRandomNumber");
            randomDirNumber = UnityEngine.Random.Range(0, 5);
            switch (randomDirNumber)
            {
                case 0:
                    lookDir = Vector2.left;
                    sr.sprite = idle[0];
                    break;
                case 1:
                    lookDir = Vector2.right;
                    sr.sprite = idle[3];
                    break;
                case 2:
                    lookDir = Vector2.up;
                    sr.sprite = idle[6];
                    break;
                case 3:
                    lookDir = Vector2.down;
                    sr.sprite = idle[9];
                    break;
                case 4:
                    break;
            }
            changeDice = false;
        }
        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
        setTarget = true;
        setMovingTarget();
    }

    bool setTarget;
    private void setMovingTarget()
    {
        if (boolAllStop == true) { return; }
        if (setTarget == false) { return; }
        //Debug.Log("setMovingTarget");
        switch (randomDirNumber)
        {
            case 0: { target = new Vector3(transform.position.x - 0.5f, transform.position.y); break; }
            case 1: { target = new Vector3(transform.position.x + 0.5f, transform.position.y); break; }
            case 2: { target = new Vector3(transform.position.x, transform.position.y + 0.5f); break; }
            case 3: { target = new Vector3(transform.position.x, transform.position.y - 0.5f); break; }
            case 4: break;
        }
        before = new Vector3(transform.position.x, transform.position.y);
        beforeSave = true;
        isMoving = true;
        setTarget = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
        if (hit.Length != 1)
        {
            Destroy(gameObject);
        }
        if (hit.Length == 1)
        {
            softMoving();
        }
        else
        {
            setTarget = false;
            changeDice = true;
        }
    }

    private void softMoving()
    {
        if (boolAllStop == true) { return; }
        if (isMoving == false) { return; }
        if (isMoving == true && beforeSave == true && boolMoveDelayCheck == false)
        {
            //Debug.Log("softMoving");
            ratio += Time.deltaTime * 1.8f;
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
            transform.position = after;
        }
        if (isMoving == true && ratio >= 1.0f)
        {
            ratio = 0.0f;
            isMoving = false;
            beforeSave = false;
            curMotion = enemyMotion.Idle;
            checkChangeSpriteDelay = true;
            boolMoveDelayCheck = true;
            changeDice = true;
            nextAction = true;
            checkNext();
        }
    }

    bool nextAction;
    private void checkNext()
    {
        if (boolAllStop == true) { return; }
        if (nextAction == false) { return; }
        //Debug.Log("checkNext");
        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
        if (hit.Length != 1 && hit[1].transform.gameObject.tag == "Player")
        {
            run();
        }
    }

    private void run()
    {
        allStop();
        before = new Vector3(transform.position.x, transform.position.y);
        if (lookDir == Vector3.left)
        {
            lookDir = Vector3.right;
            target = new Vector3(transform.position.x + 1.0f, transform.position.y);
            curMotion = enemyMotion.Step;
            checkChangeSpriteDelay = true;
        }
        if (lookDir == Vector3.right)
        {
            lookDir = Vector3.left;
            target = new Vector3(transform.position.x - 1.0f, transform.position.y);
            curMotion = enemyMotion.Step;
            checkChangeSpriteDelay = true;
        }
        if (lookDir == Vector3.up)
        {
            lookDir = Vector3.down;
            target = new Vector3(transform.position.x, transform.position.y - 1.0f);
            curMotion = enemyMotion.Step;
            checkChangeSpriteDelay = true;
        }
        if (lookDir == Vector3.down)
        {
            lookDir = Vector3.up;
            target = new Vector3(transform.position.x, transform.position.y + 1.0f);
            curMotion = enemyMotion.Step;
            checkChangeSpriteDelay = true;
        }
        isMoving = true;
        beforeSave = true;
        boolMoveDelayCheck = false;
        Debug.Log("run");
        curMotion = enemyMotion.Idle;
        boolAllStop = false;
        softMoving();
        changeDice = true;
        //ifü���������� ����
    }
    private void allStop()
    {
        boolAllStop = true;
        transform.position = before;
        changeDice = false;
        isMoving = false;
        setTarget = false;
        nextAction = false;
        beforeSave = false;
        boolMoveDelayCheck = false;
        moveDelayCheck = 100.0f;
        Debug.Log($"<color=yellow>Reset {moveDelayCheck}</color>");
    }
    private void checkMoveDelay(float _value)
    {
        if (boolAllStop == true) { return; }
        if (boolMoveDelayCheck == false) { return; }
        if (moveDelayCheck == 100.0f && boolMoveDelayCheck == true)
        {
            //Debug.Log("checkMoveDelay");
            isMoving = false;
            setTarget = false;
            moveDelayCheck -= _value;
        }
        if (moveDelayCheck != 100.0f && boolMoveDelayCheck == true)
        {
            moveDelayCheck += Time.deltaTime;
        }
        if (moveDelayCheck > 100 && boolMoveDelayCheck == true)
        {
            boolMoveDelayCheck = false;
            moveDelayCheck = 100.0f;
            changeDice = true;
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
            case enemyMotion.Attack:
                {
                    if (boolAttackDelayCheck == false)
                    {
                        if (lookDir == Vector3.left)
                        {
                            sr.sprite = idle[1];
                        }
                        if (lookDir == Vector3.right)
                        {
                            sr.sprite = idle[4];
                        }
                        if (lookDir == Vector3.up)
                        {
                            sr.sprite = idle[7];
                        }
                        if (lookDir == Vector3.down)
                        {
                            sr.sprite = idle[10];
                        }
                    }
                    curMotion = enemyMotion.Idle;
                }
                break;
        }
        checkChangeSpriteDelay = false;
    }
    private void dead()
    {
        if (curHp > 0) { return; }
        else
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }


    private void checkAttackDelay(float _value)
    {
        if (boolAttackDelayCheck == false) { return; }
        if (attackDelayCheck == 100.0f && boolAttackDelayCheck == true)
        {
            curMotion = enemyMotion.Attack;
            isAttack = true;
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
            counterattack();
        }
    }
    RaycastHit2D attackTarget;
    private void RayVec()
    {
        if (lookDir == Vector3.left)
        {
            attackTarget = Physics2D.Raycast(new Vector2(transform.position.x - 0.3f, transform.position.y), lookDir, 0.2f);
            Debug.DrawRay(new Vector2(transform.position.x - 0.3f, transform.position.y), lookDir * 0.2f, Color.red);
        }
        if (lookDir == Vector3.right)
        {
            attackTarget = Physics2D.Raycast(new Vector2(transform.position.x + 0.3f, transform.position.y), lookDir, 0.2f);
            Debug.DrawRay(new Vector2(transform.position.x + 0.3f, transform.position.y), lookDir * 0.2f, Color.red);
        }
        if (lookDir == Vector3.up)
        {
            attackTarget = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.3f), lookDir, 0.2f);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.3f), lookDir * 0.2f, Color.red);
        }
        if (lookDir == Vector3.down)
        {
            attackTarget = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.3f), lookDir, 0.2f);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.3f), lookDir * 0.2f, Color.red);
        }

    }
    GameObject targetPlayer;

    private void counterattack()
    {
        Debug.Log("counterattack");
        RayVec();
        GameObject target = attackTarget.transform.gameObject;
        targetPlayer = target.gameObject;
        Player targetPlayerSc = targetPlayer.GetComponent<Player>();
        Debug.Log(targetPlayerSc);
        if (targetPlayer == null)
        {
            curMotion = enemyMotion.Idle;
        }
        if (curMotion != enemyMotion.Attack)
        {
            allStop();
            curMotion = enemyMotion.Attack;
            boolAttackDelayCheck = true;
            targetPlayerSc.DamagefromEnemy(damage);
        }
        if (curMotion == enemyMotion.Attack)
        {
            boolAttackDelayCheck = true;
            targetPlayerSc.DamagefromEnemy(damage);
        }
    }
    public void DamagefromEnemy(float _damage)
    {
        curHp -= _damage;
        gaugeBar.SetHp(curHp, maxHp);
        sprDefault = sr.color;
        sr.color = new Color(1, 1, 1, 0.4f);
        Invoke("setSpriteDefault", 0.2f);
        if (curMotion != enemyMotion.Attack)
            counterattack();
    }
    public void DamageToEnemy(float _damage)
    {

    }
    public float GetRespawnTime()
    {
        return respawnTime;
    }
    private void setSpriteDefault()
    {
        sr.color = sprDefault;
    }

    public void SetHp(GaugeBar _value)
    {
        gaugeBar = _value;
        gaugeBar.SetHp(curHp, maxHp);
    }
}
