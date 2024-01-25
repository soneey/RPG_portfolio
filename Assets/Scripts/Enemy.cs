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
    [SerializeReference] int monsterNumber;
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    [SerializeField] private float respawnTime;
    [SerializeField] float chaseSpeed;

    [Header("액션")]
    [SerializeField] private float moveDelayCheck = 100.0f;
    [SerializeReference] private float moveSpeed;
    private bool boolMoveDelayCheck;//이동 후 딜레이체크 시작,종료 체크
    [SerializeField] private bool isMoving;//이동중인지 체크
    [SerializeField] private float attackDelayCheck = 100.0f;
    [SerializeField] private float attackSpeed;
    private bool boolAttackDelayCheck;
    [SerializeField] private bool isAttack;


    [Header("스프라이트 변경")]
    [SerializeField] private Sprite[] idle;//스프라이트 등록
    [SerializeField] private bool footCheck;//왼발 오른발 순서 체크
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
    Transform layerEnemy;

    GameObject objPlayer;
    private Color sprDefault;
    SpriteRenderer sr;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (curMotion != enemyMotion.Attack && collision.gameObject.tag == "Player" && Vector2.Distance(transform.position, collision.gameObject.transform.position) < 0.5f)
        {
            Debug.Log("<color=red>EP Destroy</color>");
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && Vector2.Distance(transform.position, collision.gameObject.transform.position) < 0.5f)
        {
            Debug.Log("<color=yellow>EP before</color>");
            transform.position = before;
        }
        if (collision.gameObject.tag == "Enemy" && Vector2.Distance(transform.position, collision.gameObject.transform.position) < 0.5f)
        {
            Debug.Log("<color=yellow>EE before</color>");
            transform.position = before;
        }
        if (collision.gameObject.tag == "Enemy" && Vector2.Distance(transform.position, collision.gameObject.transform.position) == 0.0f)
        {
            Debug.Log("<color=red>EE Destroy</color>");
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ai" && Vector2.Distance(transform.position, collision.gameObject.transform.position) < 0.5f)
        {
            Debug.Log("<color=yellow>EA before</color>");
            transform.position = before;
        }
        if (collision.gameObject.tag == "Ai" && Vector2.Distance(transform.position, collision.gameObject.transform.position) == 0.0f)
        {
            Debug.Log("<color=red>EA Destroy</color>");
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sprDefault = sr.color;
        sr.color = new Color(1, 1, 1, 0f);
        boxCollider2D = GetComponent<BoxCollider2D>();
        SetStatus();
        curHp = maxHp;
        layerEnemy = transform.Find("LayerEnemy");
    }
    private GaugeBar gaugeBar;
    GameObject HpGaugeBar;

    void Start()
    {
        monsterNumber = GameManager.Instance.GetMonsterNumber();//몬스터번호0번 토끼일때
        GameManager manager = GameManager.Instance;
        trsGaugeBarPos = transform.localPosition;
        GameObject HpGaugeBar = manager.GetGaugeBar();
        GameObject obj = Instantiate(HpGaugeBar, trsGaugeBarPos, Quaternion.identity, transform.transform);
        gaugeBar = obj.GetComponent<GaugeBar>();
        gaugeBar.SetHp(curHp, maxHp);
        objPlayer = GameObject.Find("Player");
        start();
    }

    private void Update()
    {
        getRandomNumber();
        softMoving();
        checkMoveDelay(moveSpeed);
        checkChaseDelay(chaseSpeed);
        chaseTarget();
        changeSprite();
        checkAttackDelay(attackSpeed);
    }

    private void SetStatus()
    {
        if (monsterNumber == 0)
        {
            moveSpeed = UnityEngine.Random.Range(9.0f, 10.0f);
            maxHp = UnityEngine.Random.Range(20, 26);
            chaseSpeed = UnityEngine.Random.Range(2.0f, 2.7f);
        }
        if (monsterNumber == 1)
        {
            moveSpeed = UnityEngine.Random.Range(4.5f, 5.0f);
            maxHp = UnityEngine.Random.Range(90, 111);
            chaseSpeed = UnityEngine.Random.Range(1.0f, 1.5f);
        }
    }
    IEnumerator gameStart()
    {
        sr.color = new Color(1, 1, 1, 0f);
        yield return new WaitForSeconds(0.3f);
        sr.color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(0.3f); 
        sr.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.3f);
        sr.color = new Color(1, 1, 1, 1f);
        changeDice = true;
    }
    private void start()
    {
        StartCoroutine(gameStart());
    }


    bool boolAllStop;
    bool changeDice;
    private void getRandomNumber()
    {
        if (boolAllStop == true) { return; }
        if (changeDice == false) { return; }
        if (changeDice == true)
        {
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
        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.magenta);
        if (hit.Length == 1)
        {
            setTarget = true;
            setMovingTarget();
        }
        else
        {
            changeDice = true;
            return;
        }
    }

    bool setTarget;
    private void setMovingTarget()
    {
        if (boolAllStop == true) { return; }
        if (setTarget == false) { return; }
        beforeSave = true;
        before = new Vector3(transform.position.x, transform.position.y);
        switch (randomDirNumber)
        {
            case 0: { target = new Vector3(transform.position.x - 0.5f, transform.position.y); break; }
            case 1: { target = new Vector3(transform.position.x + 0.5f, transform.position.y); break; }
            case 2: { target = new Vector3(transform.position.x, transform.position.y + 0.5f); break; }
            case 3: { target = new Vector3(transform.position.x, transform.position.y - 0.5f); break; }
            case 4: break;
        }
        if (target.x % 0.5 != 0 || target.y % 0.5 != 0)
        {
            setTarget = false;
            transform.position = before;
            changeDice = true;
            return;
        }
        setTarget = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
        Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.magenta);
        if (hit.Length == 1)
        {
            isMoving = true;
            checkChangeSpriteDelay = true;
            curMotion = enemyMotion.Step;

        }
        if (hit.Length != 1)
        {
            moveDelayCheck -= 5.0f;
            Debug.Log($"{moveDelayCheck} {boolMoveDelayCheck}");
            boolMoveDelayCheck = true;
        }
    }

    private void softMoving()
    {
        if (boolAllStop == true) { return; }
        if (isMoving == false) { return; }
        if (isMoving == true && beforeSave == true && boolMoveDelayCheck == false)
        {
            ratio += Time.deltaTime * 1.3f;
            after.x = Mathf.SmoothStep(before.x, target.x, ratio);
            after.y = Mathf.SmoothStep(before.y, target.y, ratio);
            transform.position = after;
        }
        if (target == after && isMoving == true && ratio > 1.0f)
        {
            ratio = 0.0f;
            boolMoveDelayCheck = true;
            isMoving = false;
            beforeSave = false;
            curMotion = enemyMotion.Idle;
            checkChangeSpriteDelay = true;
        }
    }

    private void allStop()
    {
        boolAllStop = true;
        //transform.position = before;
        isMoving = false;
        setTarget = false;
        beforeSave = false;
        boolMoveDelayCheck = false;
        moveDelayCheck = 100.0f;
        ratio = 0.0f;
        Debug.Log($"<color=yellow>allStop Reset {moveDelayCheck}</color>");
    }
    private void checkMoveDelay(float _value)
    {
        if (boolAllStop == true) { return; }
        if (boolMoveDelayCheck == false) { return; }
        if (moveDelayCheck == 100.0f && boolMoveDelayCheck == true)
        {
            isMoving = false;
            setTarget = false;
            Player objSc = objPlayer.GetComponent<Player>();
            objSc.SetTrsInfo();
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
            ratio = 0.0f;
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

        }
        checkChangeSpriteDelay = false;
    }

    private void checkAttackDelay(float _value)
    {
        if (boolAttackDelayCheck == false) { return; }
        if (attackDelayCheck == 100.0f && boolAttackDelayCheck == true)
        {
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

    Vector3 trsTarget;//데미지를 입으면 데미지를 준 상대의 LookDir을 받아옴

    bool boolSetChaseTarget = true;
    private void setChaseTarget()
    {
        if (boolSetChaseTarget == false) { return; }
        if (boolSetChaseTarget == true)
        {
            trsTarget = attackTarget.transform.position;
            before = transform.position;
            boolMoveDelayCheck = false;
            isMoving = false;
            moveDelayCheck = 100.0f;
            ratio = 0.0f;
            if (Vector2.Distance(transform.position, trsTarget) == 0.5f)
            {
                boolSetChaseTarget = false;
                counterattack();
                return;
            }
            else if (trsTarget.x < transform.position.x)
            {
                target = new Vector2(transform.position.x - 0.5f, transform.position.y);
                lookDir = Vector3.left;
            }
            else if (trsTarget.x > transform.position.x)
            {
                target = new Vector2(transform.position.x + 0.5f, transform.position.y);
                lookDir = Vector3.right;
            }
            else if (trsTarget.y > transform.position.y)
            {
                target = new Vector2(transform.position.x, transform.position.y + 0.5f);
                lookDir = Vector3.up;
            }
            else if (trsTarget.y < transform.position.y)
            {
                target = new Vector2(transform.position.x, transform.position.y - 0.5f);
                lookDir = Vector3.down;
            }
            RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
            Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.magenta);
            if (hit.Length != 1)
            {
                return;
            }
            if (hit.Length == 1)
            {
                isChaseTarget = true;
                checkChangeSpriteDelay = true;
                curMotion = enemyMotion.Step;
                chaseTarget();
            }
        }
    }
    bool isChaseTarget;
    private void chaseTarget()
    {
        if (isChaseTarget == false) { return; }
        if (isChaseTarget == true)
        {
            ratio += Time.deltaTime * 1.8f;
            after.x = Mathf.SmoothStep(transform.position.x, target.x, ratio);
            after.y = Mathf.SmoothStep(transform.position.y, target.y, ratio);
            transform.position = after;
            //Debug.Log($"{target} check");
        }
        if (target == after && isChaseTarget == true && ratio > 1.0f)
        {
            Debug.Log("<color=red>chase</color>");
            isChaseTarget = false;
            ratio = 0.0f;
            curMotion = enemyMotion.Idle;
            boolChaseDelayCheck = true;
            boolSetChaseTarget = true;
            checkChangeSpriteDelay = true;
        }
    }
    bool boolChaseDelayCheck;
    private void checkChaseDelay(float _value)
    {
        if (boolChaseDelayCheck == false) { return; }
        if (boolChaseDelayCheck == true)
        {
            if (moveDelayCheck == 100.0f && attackTarget.tag == "Ai")
            {
                isChaseTarget = false;
                Ai TargetSc = attackTarget.GetComponent<Ai>();
                moveDelayCheck -= _value;
            }
            if (moveDelayCheck == 100.0f && attackTarget.tag == "Player")
            {
                isChaseTarget = false;
                Player TargetSc = attackTarget.GetComponent<Player>();
                TargetSc.SetTrsInfo();
                moveDelayCheck -= _value;
            }
            if (moveDelayCheck != 100.0f)
            {
                moveDelayCheck += Time.deltaTime;
            }
            if (moveDelayCheck > 100 && Vector2.Distance(transform.position, attackTarget.transform.position) != 0.5f)
            {
                boolChaseDelayCheck = false;
                moveDelayCheck = 100.0f;
                ratio = 0.0f;
                boolSetChaseTarget = true;
                setChaseTarget();
            }
            if (moveDelayCheck > 100 && Vector2.Distance(transform.position, attackTarget.transform.position) == 0.5f)
            {
                boolChaseDelayCheck = false;
                moveDelayCheck = 100.0f;
                ratio = 0.0f;
                counterattack();
            }
        }
    }
    private void counterattack()
    {
        if (curHp != maxHp && curMotion != enemyMotion.Attack && Vector3.Distance(transform.position, attackTarget.transform.position) > 0.5f)
        {//전투시작 시 플레이어가 근접해있지 않으면 추적
            allStop();
            boolSetChaseTarget = true;
            setChaseTarget();
        }
        if (curHp != maxHp && curMotion != enemyMotion.Attack && Vector3.Distance(transform.position, attackTarget.transform.position) == 0.5f)
        {//전투시작 시 플레이어가 옆에 있으면 방향전환
            allStop();
            if (transform.position.x < attackTarget.transform.position.x)
            {
                lookDir = Vector3.right;
                sr.sprite = idle[3];
            }
            if (transform.position.x > attackTarget.transform.position.x)
            {
                lookDir = Vector3.left;
                sr.sprite = idle[0];
            }
            if (transform.position.y < attackTarget.transform.position.y)
            {
                lookDir = Vector3.up;
                sr.sprite = idle[6];
            }
            if (transform.position.y > attackTarget.transform.position.y)
            {
                lookDir = Vector3.down;
                sr.sprite = idle[9];
            }
            RaycastHit2D[] hitAttackTarget = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
            Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.magenta);
            if (hitAttackTarget.Length == 1)
            {
                boolSetChaseTarget = true;
                setChaseTarget();
            }
            else if (hitAttackTarget.Length != 1 && hitAttackTarget[1].transform.gameObject == attackTarget)
            {
                Debug.Log("<color=magenta>counterattack</color>");
                curMotion = enemyMotion.Attack;
                if (attackTarget.tag == "Ai")
                {
                    Ai targetSc = attackTarget.GetComponent<Ai>();
                    targetSc.DamagefromEnemy(damage);
                    Debug.Log($"<color=green>Ai curHp = {targetSc.getCurHp()}</color>");
                }
                if (attackTarget.tag == "Player")
                {
                    Player targetSc = attackTarget.GetComponent<Player>();
                    targetSc.DamagefromEnemy(damage);
                    Debug.Log($"<color=green>Player curHp = {targetSc.GetCurHp()}</color>");
                }
                boolAttackDelayCheck = true;
                Invoke("setSprite", 0.2f);
            }
        }

        if (attackDelayCheck == 100.0f && isAttack == true && boolAttackDelayCheck == false && Vector3.Distance(transform.position, attackTarget.transform.position) == 0.5f)
        {
            Debug.Log("counterattack");
            if (transform.position.x + 0.5f == attackTarget.transform.position.x)
            {
                lookDir = Vector3.right;
                sr.sprite = idle[4];
            }
            if (transform.position.x - 0.5f == attackTarget.transform.position.x)
            {
                lookDir = Vector3.left;
                sr.sprite = idle[1];
            }
            if (transform.position.y + 0.5f == attackTarget.transform.position.y)
            {
                lookDir = Vector3.up;
                sr.sprite = idle[7];
            }
            if (transform.position.y - 0.5f == attackTarget.transform.position.y)
            {
                lookDir = Vector3.down;
                sr.sprite = idle[10];
            }
            if (attackTarget.tag == "Ai")
            {
                Ai targetSc = attackTarget.GetComponent<Ai>();
                targetSc.DamagefromEnemy(damage);
                Debug.Log($"<color=green>Ai curHp = {targetSc.getCurHp()}</color>");
            }
            if (attackTarget.tag == "Player")
            {
                Player targetSc = attackTarget.GetComponent<Player>();
                targetSc.DamagefromEnemy(damage);
                Debug.Log($"<color=green>Player curHp = {targetSc.GetCurHp()}</color>");
            }
            boolAttackDelayCheck = true;
            Invoke("setSprite", 0.2f);
        }

        if (attackDelayCheck == 100.0f && isAttack == true && boolAttackDelayCheck == false && Vector3.Distance(transform.position, attackTarget.transform.position) != 0.5f)
        {
            Debug.Log("d");
            boolSetChaseTarget = true;
            setChaseTarget();
        }
    }
    private void setSprite()
    {
        if (lookDir == Vector3.left)
        {
            sr.sprite = idle[0];
        }
        if (lookDir == Vector3.right)
        {
            sr.sprite = idle[3];
        }
        if (lookDir == Vector3.up)
        {
            sr.sprite = idle[6];
        }
        if (lookDir == Vector3.down)
        {
            sr.sprite = idle[9];
        }
    }
    GameObject attackTarget;
    public void DamagefromEnemy(float _damage, GameObject _value)
    {
        curHp -= _damage;
        if (curHp <= 0)
        {
            dead();
            return;
        }
        attackTarget = _value;
        counterattack();
        Debug.Log($"<color=red>Enemy curHp = {curHp}</color>");
        gaugeBar.SetHp(curHp, maxHp);
        sprDefault = sr.color;
        sr.color = new Color(1, 0, 0, 0.7f);
        Invoke("setSpriteDefault", 0.2f);
    }
    private void dead()
    {
        GameManager.Instance.killPlus(monsterNumber, 1);
        sr.sprite = idle[12];
        sr.color = new Color(1, 1, 1, 0.7f);
        Destroy(gameObject, 0.5f);
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
