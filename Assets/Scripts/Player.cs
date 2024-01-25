using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum playerMotion
    {
        None,
        Idle,
        Step,
        Attack,

    }
    playerMotion curMotion = playerMotion.None;
    playerMotion beforeMotion = playerMotion.None;
    public enum playerWeapon
    {
        None,
        Sword,
    }
    playerWeapon curWeapon = playerWeapon.None;

    [Header("플레이어 스테이터스")]
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    [SerializeField] private float magicDamage;
    private Vector2 trsGaugeBarPos;

    [Header("플레이어 행동딜레이")]
    [SerializeField] private float moveDelayCheck = 100.0f;
    [SerializeField] private float moveSpeed;
    private bool boolMoveDelayCheck;
    [SerializeField] private bool isMoving;//이동중인지 체크
    [SerializeField] private float attackDelayCheck = 100.0f;
    [SerializeField] private float attackSpeed;
    private bool boolAttackDelayCheck;
    [SerializeField] private bool isAttack;


    [SerializeField] private Sprite[] idle;//move sprite
    [SerializeField] private bool footCheck;
    private float ratio = 0.0f;//이동기능 연출 비율
    Vector3 moveVec;
    Vector3 lookDir;
    Vector3 target;

    [Header("히트박스 생성")]
    [SerializeField] private List<GameObject> listCheckBox;//인스펙터에 프리팹 넣기
    private Vector2 trsCheckBoxPos;
    [SerializeField] Transform player;
    [SerializeField] GameObject objTargetBox;
    [SerializeField] GameObject castEffect;
    [SerializeField] GameObject Ai;

    [Header("스프라이트 딜레이")]
    [SerializeField] private float spriteChangeDelay = 0.0f;
    private bool checkChangeSpriteDelay;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;
    SpriteRenderer sr;
    private Color sprDefault;

    private GaugeBar gaugeBar;
    GameObject HpGaugeBar;


    private void Awake()
    {
        curHp = maxHp;
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sprDefault = sr.color;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        GameManager manager = GameManager.Instance;
        trsGaugeBarPos = transform.localPosition;
        HpGaugeBar = manager.GetGaugeBar();
        GameObject obj = Instantiate(HpGaugeBar, trsGaugeBarPos, Quaternion.identity, player);
        gaugeBar = obj.GetComponent<GaugeBar>();
        gaugeBar.SetHp(curHp, maxHp);
        layerEnemy = GameObject.Find("LayerEnemy");
    }

    private void Update()
    {
        turning();
        castMagic();
        setMagicTarget();
        //saveTargetList();
    }
    void FixedUpdate()
    {
        //setMagicTarget();
        softMoving();
        moving();
        changeSprite();
        attack();
        checkAttackDelay(attackSpeed);
        aiCheck();
    }

    GameObject temp;
    GameObject layerEnemy;
    private void aiCheck()
    {
        Ai AiSc = Ai.GetComponent<Ai>();
        if (curHp < maxHp * 0.6f)
        {
            AiSc.Heal();
        }
        else
        {
            AiSc.Check();
        }
        gaugeBar.SetHp(curHp, maxHp);
    }
    private void saveTargetList()
    {
        listTarget = new List<GameObject>();
        listTarget.Insert(0, transform.gameObject);
        listTemp2 = new List<GameObject>();
        //listTemp2.Insert(0, Ai.gameObject);
        int count = layerEnemy.transform.childCount;
        for (int i = 0; i < count; i++)//임시리스트2에 모두 저장
        {
            listTemp2.Add(layerEnemy.transform.GetChild(i).gameObject);
            //Debug.Log(listTemp[iNum].transform.position);
        }
        listTemp = new List<GameObject>();
        for (int i = 0; i < count; i++)//1번2번 범위의 오브젝트를 임시리스트1에 저장
        {
            if ((listTemp2[i].gameObject.transform.position.y >= listTarget[0].gameObject.transform.position.y &&
                    listTemp2[i].gameObject.transform.position.x > listTarget[0].gameObject.transform.position.x) ||
                    (listTemp2[i].gameObject.transform.position.y >= listTarget[0].gameObject.transform.position.y + 0.5f &&
                    listTemp2[i].gameObject.transform.position.x <= listTarget[0].gameObject.transform.position.x))
            {
                listTemp.Add(listTemp2[i].gameObject);
            }
        }
        listTemp3 = new List<GameObject>();
        for (int i = 0; i < count; i++)//3번4번 범위의 오브젝트를 임시리스트3에 저장
        {
            if ((listTemp2[i].gameObject.transform.position.y <= listTarget[0].gameObject.transform.position.y &&
                    listTemp2[i].gameObject.transform.position.x < listTarget[0].gameObject.transform.position.x) ||
                    (listTemp2[i].gameObject.transform.position.y <= listTarget[0].gameObject.transform.position.y - 0.5f &&
                    listTemp2[i].gameObject.transform.position.x >= listTarget[0].gameObject.transform.position.x))
            {
                listTemp3.Add(listTemp2[i].gameObject);
            }
        }
        for (int i = 0; i < listTemp.Count; i++)//임시리스트1의 몬스터를 가까운 거리순으로 재배치
        {
            for (int j = 0; j < listTemp.Count; j++)
            {
                if (Vector2.Distance(listTarget[0].gameObject.transform.position, listTemp[i].gameObject.transform.position) <
                    Vector2.Distance(listTarget[0].gameObject.transform.position, listTemp[j].gameObject.transform.position))
                {
                    temp = listTemp[i];
                    listTemp[i] = listTemp[j];
                    listTemp[j] = temp;
                }
            }
        }
        for (int i = 0; i < listTemp3.Count; i++)//임시리스트3의 몬스터를 먼 거리순으로 재배치
        {
            for (int j = 0; j < listTemp3.Count; j++)
            {
                if (Vector2.Distance(listTarget[0].gameObject.transform.position, listTemp3[i].gameObject.transform.position) >
                    Vector2.Distance(listTarget[0].gameObject.transform.position, listTemp3[j].gameObject.transform.position))
                {
                    temp = listTemp3[i];
                    listTemp3[i] = listTemp3[j];
                    listTemp3[j] = temp;
                }
            }
        }

        for (int i = 0; i < listTemp.Count; i++)//최종리스트에 임시리스트1의 몬스터 순서대로 넣기
        {
            listTarget.Add(listTemp[i].gameObject);
        }
        for (int i = 0; i < listTemp3.Count; i++)//최종리스트에 들어있는 몬스터의 뒤로 임시리스트3의 몬스터 순서대로 넣기
        {
            listTarget.Add(listTemp3[i].gameObject);
        }
        //for (int i = 0; i < listTarget.Count; i++)
        //{
        //    Debug.Log($"{i} = {listTarget[i].gameObject.transform.position}");
        //}
    }


    Vector3 before;
    Vector3 after;
    private bool beforeSave;

    private void softMoving()
    {
        if (boolCastMagic == true) { return; }
        if (isMoving == true && beforeSave == true)
        {
            before = transform.position;
            beforeSave = false;
        }
        //Debug.Log($"before = {before}");
        //Debug.Log($"moveVec = {moveVec}");
        //Debug.Log($"target = {target}");
        //Debug.Log($"ratio = {ratio}");
        if (isMoving == true && beforeSave == false)
        {
            ratio += Time.deltaTime * 2.0f;
            moveVec = transform.position;
            after.x = Mathf.SmoothStep(before.x, target.x, ratio);
            after.y = Mathf.SmoothStep(before.y, target.y, ratio);
            moveVec = after;
        }
        if (isMoving == true && ratio >= 1.0f)
        {
            ratio = 0.0f;
            isMoving = false;
        }
        transform.position = moveVec;
    }
    /// <summary>
    /// 플레이어가 상하좌우 1칸씩 좌표이동 후 모션변경
    /// </summary>
    private void moving()
    {
        if (boolCastMagic == true || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { return; }
        else
        {
            if (((Input.GetKey(KeyCode.LeftArrow) && boolMoveDelayCheck == false && footCheck == false))
                || (((Input.GetKey(KeyCode.LeftArrow) && boolMoveDelayCheck == false && footCheck == true))))
            {
                lookDir = Vector3.left;
                curMotion = playerMotion.Idle;
                RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
                Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
                //for (int i = 0; i < hit.Length; i++)
                //{
                //    Debug.Log(hit[i].transform.name);
                //}
                if (hit.Length != 1)
                {
                    playerMotionChange();
                    return;
                }
                else
                {
                    isMoving = true;
                    beforeSave = true;
                    target = new Vector3(transform.position.x - 0.5f, transform.position.y);
                    boolMoveDelayCheck = true;
                    checkChangeSpriteDelay = true;
                    curMotion = playerMotion.Step;
                }
            }
            if (((Input.GetKey(KeyCode.RightArrow) && boolMoveDelayCheck == false && footCheck == false))
                || ((Input.GetKey(KeyCode.RightArrow) && boolMoveDelayCheck == false && footCheck == true)))
            {
                lookDir = Vector3.right;
                curMotion = playerMotion.Idle;
                RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
                Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
                if (hit.Length != 1)
                {
                    playerMotionChange();
                    return;
                }
                else

                {
                    isMoving = true;
                    beforeSave = true;
                    target = new Vector3(transform.position.x + 0.5f, transform.position.y);
                    boolMoveDelayCheck = true;
                    checkChangeSpriteDelay = true;
                    curMotion = playerMotion.Step;
                }
            }
            if (((Input.GetKey(KeyCode.UpArrow) && boolMoveDelayCheck == false && footCheck == false))
                || ((Input.GetKey(KeyCode.UpArrow) && boolMoveDelayCheck == false && footCheck == true)))
            {
                lookDir = Vector3.up;
                curMotion = playerMotion.Idle;
                RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
                Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
                if (hit.Length != 1)
                {
                    playerMotionChange();
                    return;
                }
                else
                {
                    isMoving = true;
                    beforeSave = true;
                    target = new Vector3(transform.position.x, transform.position.y + 0.5f);
                    boolMoveDelayCheck = true;
                    checkChangeSpriteDelay = true;
                    curMotion = playerMotion.Step;
                }
            }
            if (((Input.GetKey(KeyCode.DownArrow) && boolMoveDelayCheck == false && footCheck == false))
                || ((Input.GetKey(KeyCode.DownArrow) && boolMoveDelayCheck == false && footCheck == true)))
            {
                lookDir = Vector3.down;
                curMotion = playerMotion.Idle;
                RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
                Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
                if (hit.Length != 1)
                {
                    playerMotionChange();
                    return;
                }
                else
                {
                    isMoving = true;
                    beforeSave = true;
                    target = new Vector3(transform.position.x, transform.position.y - 0.5f);
                    boolMoveDelayCheck = true;
                    checkChangeSpriteDelay = true;
                    curMotion = playerMotion.Step;
                }
            }
            //curMotion = curMotion == playerMotion.FrontDirLeftFoot ? playerMotion.FrontDirRightFoot : playerMotion.FrontDirLeftFoot;
            //Ai AiSc = Ai.GetComponent<Ai>();
            //AiSc.TrsInfomation(this.transform.position);
            checkMoveDelay(moveSpeed);//플레이어의 이동 딜레이
        }
    }
    /// <summary>
    /// 플레이어 이동방향에 따른 스프라이트 변경
    /// 왼발 오른발 번갈아가면서 출력
    /// </summary>
    private void changeSprite()
    {
        if (boolCastMagic == true || checkChangeSpriteDelay == false) { return; }
        if (checkChangeSpriteDelay == true)
        {
            if (curMotion == playerMotion.Step && footCheck == false)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    playerMotionChange();
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = playerMotion.Idle;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == playerMotion.Step && footCheck == true)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    playerMotionChange();
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = playerMotion.Idle;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            playerMotionChange();
        }
    }
    /// <summary>
    /// 제자리에서 플레이어의 방향전환 기능
    /// </summary>
    private void turning()
    {
        if (boolCastMagic == true) { return; }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            //lookDir = transform.position;
            lookDir = Vector3.left;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.RightArrow)))
        {
            //lookDir = transform.position;
            lookDir = Vector3.right;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.UpArrow)))
        {
            //lookDir = transform.position;
            lookDir = Vector3.up;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.DownArrow)))
        {
            //lookDir = transform.position;
            lookDir = Vector3.down;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
    }
    /// <summary>
    /// Player, Enemy의 모든 행동 후 다음 행동 간의 딜레이를 관리하는 기능
    /// </summary>
    /// <param name="_value"></param>
    private void checkMoveDelay(float _value)
    {
        if (boolMoveDelayCheck == false) { return; }
        if (moveDelayCheck == 100.0f && boolMoveDelayCheck == true)
        {
            saveTargetList();
            moveDelayCheck -= _value;
        }
        if (moveDelayCheck != 100.0f && boolMoveDelayCheck == true)
        {
            moveDelayCheck += Time.deltaTime;
            //Debug.Log(checkDelayCount);
        }
        if (moveDelayCheck > 100)
        {
            moveDelayCheck = 100.0f;
            boolMoveDelayCheck = false;
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
            isAttack = false;
        }
    }


    private void attack()
    {
        if (boolCastMagic == true || Input.GetKey(KeyCode.Space) && isAttack == true) { return; }
        if (Input.GetKey(KeyCode.Space) && boolAttackDelayCheck == false)
        {
            boolAttackDelayCheck = true;
            isAttack = true;
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
            Invoke("setSprite", 0.5f);
            RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, lookDir, 0.5f);
            Debug.DrawRay(boxCollider2D.bounds.center, lookDir * 0.5f, Color.blue);
            if (hit.Length == 1)
            {
                return;
            }
            if (hit.Length > 1 && hit[1].transform.gameObject.tag == "Enemy")
            {
                Transform hitEnemy;
                hitEnemy = hit[1].transform;
                Enemy hitEnemySc = hitEnemy.GetComponent<Enemy>();
                hitEnemySc.DamagefromEnemy(damage, this.gameObject);
                GameObject cast = Instantiate(castEffect, hitEnemy.position, Quaternion.identity);
                SpellEffect castSc = cast.GetComponent<SpellEffect>();
                castSc.showAttackEffect();
            }
        }
    }

    bool boolCastMagic;
    [SerializeField] List<GameObject> listTarget;
    List<GameObject> listTemp;
    List<GameObject> listTemp2;
    List<GameObject> listTemp3;
    GameObject curTarget;
    int curTargetNum = -1;
    private void castMagic()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) && boolCastMagic == false && isAttack == true) ||
           (Input.GetKeyDown(KeyCode.Keypad1) && boolCastMagic == false && isAttack == true))
        {
            //saveTargetList();
            return;
        }
        if (((Input.GetKeyDown(KeyCode.Alpha1) && boolCastMagic == false && isAttack == false && isMoving == false)) ||
                ((Input.GetKeyDown(KeyCode.Keypad1) && boolCastMagic == false && isAttack == false && isMoving == false)))
        {
            boolCastMagic = true;
            saveTargetList();
            GameObject Box = objTargetBox.gameObject;
            if (curTarget != null)
            {
                Instantiate(Box, curTarget.transform.position, Quaternion.identity, player);
                curTargetNum = 0;
                return;
            }
            Instantiate(Box, transform.position, Quaternion.identity, player);
            curTarget = listTarget[0].gameObject;
            curTargetNum = 0;
        }
    }

    private void setMagicTarget()
    {
        if (boolCastMagic == false) { return; }
        if (boolCastMagic == true)
        {
            Transform Box = player.GetChild(1);
            //TargetBox objSc = Box.GetComponent<TargetBox>();
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (curTarget.gameObject.tag == "Enemy")
                {
                    Enemy targetSc = curTarget.GetComponent<Enemy>();
                    targetSc.DamagefromEnemy(magicDamage, gameObject);
                    GameObject obj = Instantiate(castEffect, targetSc.transform.position, Quaternion.identity);
                    SpellEffect objSc = obj.GetComponent<SpellEffect>();
                    objSc.showHellfireEffect();
                }
                Destroy(Box.gameObject);
                boolAttackDelayCheck = true;
                isAttack = true;
                boolCastMagic = false;
                GameObject cast = Instantiate(castEffect, transform.position, Quaternion.identity);
                SpellEffect castSc = cast.GetComponent<SpellEffect>();
                castSc.showCastEffect();
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                Box.transform.position = new Vector2(listTarget[0].gameObject.transform.position.x, listTarget[0].gameObject.transform.position.y);
                curTarget = listTarget[0].gameObject;
                curTargetNum = 0;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(Box.gameObject);
                boolCastMagic = false;
                curTarget = listTarget[0].gameObject;
                curTargetNum = 0;
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if (curTargetNum == listTarget.Count - 1)
                {
                    curTargetNum = 0;
                }
                else
                {
                    curTargetNum++;
                }
                Box.transform.position = new Vector2(listTarget[curTargetNum].gameObject.transform.position.x, listTarget[curTargetNum].gameObject.transform.position.y);
                curTarget = listTarget[curTargetNum].gameObject;
                Debug.Log($"<color=yellow>curTargetNum = {curTargetNum}</color>");
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.DownArrow)))
            {
                if (curTargetNum == 0)
                {
                    curTargetNum = listTarget.Count - 1;
                }
                else
                {
                    curTargetNum--;
                }
                Box.transform.position = new Vector2(listTarget[curTargetNum].gameObject.transform.position.x, listTarget[curTargetNum].gameObject.transform.position.y);
                curTarget = listTarget[curTargetNum].gameObject;
                Debug.Log($"<color=yellow>curTargetNum = {curTargetNum}</color>");
            }
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
    /// <summary>
    /// 플레이어의 모션이 변경되면 스프라이트를 변경
    /// </summary>
    private void playerMotionChange()
    {
        switch (curMotion)
        {
            case playerMotion.Idle:
                {
                    if (curMotion == playerMotion.Idle && lookDir == Vector3.left)
                    {
                        sr.sprite = idle[0];
                    }
                    if (curMotion == playerMotion.Idle && lookDir == Vector3.right)
                    {
                        sr.sprite = idle[3];
                    }
                    if (curMotion == playerMotion.Idle && lookDir == Vector3.up)
                    {
                        sr.sprite = idle[6];
                    }
                    if (curMotion == playerMotion.Idle && lookDir == Vector3.down)
                    {
                        sr.sprite = idle[9];
                    }
                    break;
                }
            case playerMotion.Step:
                {
                    if (curMotion == playerMotion.Step && lookDir == Vector3.left && footCheck == false)
                    {
                        sr.sprite = idle[1];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.left && footCheck == true)
                    {
                        sr.sprite = idle[2];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.right && footCheck == false)
                    {
                        sr.sprite = idle[4];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.right && footCheck == true)
                    {
                        sr.sprite = idle[5];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.up && footCheck == false)
                    {
                        sr.sprite = idle[7];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.up && footCheck == true)
                    {
                        sr.sprite = idle[8];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.down && footCheck == false)
                    {
                        sr.sprite = idle[10];
                    }
                    if (curMotion == playerMotion.Step && lookDir == Vector3.down && footCheck == true)
                    {
                        sr.sprite = idle[11];
                    }
                    break;
                }
        }
    }
    private void setSpriteDefault()
    {
        sr.color = sprDefault;
    }
    public void DamagefromEnemy(float _damage)
    {
        curHp -= _damage;
        gaugeBar.SetHp(curHp, maxHp);
        //sprDefault = sr.color;
        sr.color = new Color(1, 0, 0, 0.5f);
        Invoke("setSpriteDefault", 0.2f);
    }
    public GameObject CurTarget()
    {
        return curTarget;
    }
    public float GetCurHp()
    {
        return curHp;
    }
    public float GetMaxHp()
    {
        return maxHp;
    }
    public void heal()
    {
        curHp += 30;
        if (curHp >= maxHp)
        {
            curHp = maxHp;
        }
    }
    public void SetHp(GaugeBar _value)
    {
        gaugeBar = _value;
        gaugeBar.SetHp(curHp, maxHp);
    }
    public Vector3 getCurLookDir()
    {
        return lookDir;
    }
    public void SetTrsInfo()
    {
        saveTargetList();
    }
}

