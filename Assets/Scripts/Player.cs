using System;
using System.Collections.Generic;
using UnityEditor.U2D;
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

    [Header("�÷��̾� �������ͽ�")]
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    private Vector2 trsGaugeBarPos;

    [Header("�÷��̾� �ൿ������")]
    [SerializeField] private float moveDelayCheck = 100.0f;
    [SerializeField] private float moveSpeed;
    private bool boolMoveDelayCheck;
    [SerializeField] private bool isMoving;//�̵������� üũ
    [SerializeField] private float attackDelayCheck = 100.0f;
    [SerializeField] private float attackSpeed;
    private bool boolAttackDelayCheck;
    [SerializeField] private bool isAttack;


    [SerializeField] private Sprite[] idle;//move sprite
    [SerializeField] private bool footCheck;
    private float ratio = 0.0f;//�̵���� ���� ����
    Vector3 moveVec;
    Vector3 lookDir;
    Vector3 target;

    [Header("��Ʈ�ڽ� ����")]
    [SerializeField] private List<GameObject> listCheckBox;//�ν����Ϳ� ������ �ֱ�
    private Vector2 trsCheckBoxPos;
    [SerializeField] Transform player;

    [Header("��������Ʈ ������")]
    [SerializeField] private float spriteChangeDelay = 0.0f;
    private bool checkChangeSpriteDelay;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;
    SpriteRenderer sr;
    private Color sprDefault;

    private GaugeBar gaugeBar;
    GameObject HpGaugeBar;
    //private void OnValidate()
    //{
    //    if (gaugeBar != null)
    //        gaugeBar.SetHp(curHp, maxHp);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack == true && collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.tag);
            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.DamagefromEnemy(damage);
        }
    }
    
    public Vector2 curLookDir()
    {
        return lookDir;
    }
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
    }

    private void Update()
    {
        rigid.velocity = Vector3.zero;
        turning();
    }
    void FixedUpdate()
    {
        softMoving();
        moving();
        changeSprite();
        attack();

    }

    Vector3 before;
    Vector3 after;
    private bool beforeSave;

    private void softMoving()
    {
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
    /// �÷��̾ �����¿� 1ĭ�� ��ǥ�̵� �� ��Ǻ���
    /// </summary>
    private void moving()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { return; }
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
            checkMoveDelay(moveSpeed);//�÷��̾��� �̵� ������
        }
    }
    /// <summary>
    /// �÷��̾� �̵����⿡ ���� ��������Ʈ ����
    /// �޹� ������ �����ư��鼭 ���
    /// </summary>
    private void changeSprite()
    {
        if (checkChangeSpriteDelay == false) { return; }
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
    /// ���ڸ����� �÷��̾��� ������ȯ ���
    /// </summary>
    private void turning()
    {
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
    /// Player, Enemy�� ��� �ൿ �� ���� �ൿ ���� �����̸� �����ϴ� ���
    /// </summary>
    /// <param name="_value"></param>
    private void checkMoveDelay(float _value)
    {
        if (boolMoveDelayCheck == false) { return; }
        if (moveDelayCheck == 100.0f && boolMoveDelayCheck == true)
        {
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
            destroyCheckBox();
            attackDelayCheck = 100.0f;
            boolAttackDelayCheck = false;
            isAttack = false;
        }
    }
    

    private void createCheckBoxPos()
    {
        Vector2 check = transform.localPosition;
        //Debug.Log($"transform.localPosition = {check}");
        if (lookDir == Vector3.left)
        {
            sr.sprite = idle[1];
            check.x -= 0.5f;
            check.y -= 0.25f;
        }
        if (lookDir == Vector3.right)
        {
            sr.sprite = idle[4];
            check.x += 0.5f;
            check.y -= 0.25f;
        }
        if (lookDir == Vector3.up)
        {
            sr.sprite = idle[7];
            check.x -= 0;
            check.y += 0.25f;
        }
        if (lookDir == Vector3.down)
        {
            sr.sprite = idle[10];
            check.x -= 0;
            check.y -= 0.75f;
        }
        //Debug.Log($"check = {check}");
        trsCheckBoxPos = check;
        //Debug.Log($"trsCheckBoxPos = {trsCheckBoxPos}");
    }
    private void attack()
    {
        if (Input.GetKey(KeyCode.Space) && boolAttackDelayCheck == false)
        {
            boolAttackDelayCheck = true;
            isAttack = true;
            //curMotion = playerMotion.Attack;
            createCheckBoxPos();
            GameObject objCheckBox = listCheckBox[0];
            GameObject obj = Instantiate(objCheckBox, trsCheckBoxPos, Quaternion.identity, player);
        }
        checkAttackDelay(attackSpeed);
    }
    private void destroyCheckBox()
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
        Transform checkBox = player.GetChild(1);
        Destroy(checkBox.gameObject);
    }
    
    /// <summary>
    /// �÷��̾��� ����� ����Ǹ� ��������Ʈ�� ����
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
    public void DamagefromEnemy(float _damage)
    {
        Debug.Log($"Damage = {_damage}");
        curHp -= _damage;
        gaugeBar.SetHp(curHp, maxHp);
        Debug.Log($"CurHp = {curHp}");
        sprDefault = sr.color;
        sr.color = new Color(1, 1, 1, 0.4f);
        Invoke("setSpriteDefault", 0.2f);
    }
    public void SetHp(GaugeBar _value)
    {
        gaugeBar = _value;
        gaugeBar.SetHp(curHp, maxHp);
    }
    private void setSpriteDefault()
    {
        sr.color = sprDefault;
    }
}

