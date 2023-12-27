using System;
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




    [Header("플레이어 행동딜레이")]
    [SerializeField] private float checkDelayCount = 100.0f;
    [SerializeField] private float ratio = 0.0f;//이동기능 연출 비율
    private bool checkDelay;

    [SerializeField] private Sprite[] idle;//move sprite
    [SerializeField] private bool footCheck;
    [SerializeField] private bool isMoving;
    Vector3 moveVec;
    Vector3 lookDir;
    Vector3 target;

    [Header("스프라이트 딜레이")]
    [SerializeField] private float spriteChangeDelay = 0.0f;
    private bool checkChangeSpriteDelay;

    Rigidbody rigid;
    SpriteRenderer sr;

    private object manager;
    TriggerCheck triggerCheck;
    internal void TriggerEnter(playerMotion attack, Collider2D collision)
    {
        throw new NotImplementedException();
    }
    internal void TriggerExit(playerMotion attack, Collider2D collision)
    {
        throw new NotImplementedException();
    }


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        triggerCheck = GetComponentInChildren<TriggerCheck>();


    }
    void Start()
    {
        GameManager manager = GameManager.Instance;
    }

    void Update()
    {
        moving();
        changeSprite();
        turning();
        attack();
        if (isMoving == true)
        {
            ratio += Time.deltaTime * 2.0f;
            Debug.Log($"ratio = {ratio}");
            Debug.Log($"moveVec.x = {target.x}");
        }
        if (ratio >= 1.0f)
        {
            ratio = 1.0f;
            isMoving = false;
        }
        if (isMoving == false && ratio == 1.0f)
        {
            ratio = 0.0f;
        }

        toTarget();
    }

    private void toTarget()
    {
        
        moveVec = transform.position;
        target.x = Mathf.SmoothStep(moveVec.x, moveVec.x - 1.0f, ratio);
        moveVec = target;
        transform.position = new Vector3(moveVec.x, moveVec.y, 0);
    }
    /// <summary>
    /// 플레이어가 상하좌우 1칸씩 좌표이동 후 모션변경
    /// </summary>
    private void moving()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { return; }
        else
        {
            Vector3 before;
            before = moveVec;
            Debug.Log(before);
            if ((Input.GetKey(KeyCode.LeftArrow) && checkDelay == false && footCheck == false))
            {
                isMoving = true;
                lookDir = Vector3.left;
                curMotion = playerMotion.Step;
                checkDelay = true;
                checkChangeSpriteDelay = true;
            }

            if ((Input.GetKey(KeyCode.LeftArrow) && checkDelay == false && footCheck == true))
            {
                isMoving = true;
                lookDir = Vector3.left;
                curMotion = playerMotion.Step;
                checkDelay = true;
                checkChangeSpriteDelay = true;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(1, 0, 0);
                transform.position = moveVec;
                lookDir = Vector3.right;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.Step;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(1, 0, 0);
                transform.position = moveVec;
                lookDir = Vector3.right;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.Step;
            }
            if ((Input.GetKey(KeyCode.UpArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, 1, 0);
                transform.position = moveVec;
                lookDir = Vector3.up;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.Step;
            }
            if ((Input.GetKey(KeyCode.UpArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, 1, 0);
                transform.position = moveVec;
                lookDir = Vector3.up;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.Step;
            }
            if ((Input.GetKey(KeyCode.DownArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, -1, 0);
                transform.position = moveVec;
                lookDir = Vector3.down;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.Step;
            }
            if ((Input.GetKey(KeyCode.DownArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, -1, 0);
                transform.position = moveVec;
                lookDir = Vector3.down;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.Step;
            }
            //curMotion = curMotion == playerMotion.FrontDirLeftFoot ? playerMotion.FrontDirRightFoot : playerMotion.FrontDirLeftFoot;
            checkActionDelay(0.5f);//플레이어의 이동 딜레이
        }
    }

    /// <summary>
    /// Player, Enemy의 모든 행동 후 다음 행동 간의 딜레이를 관리하는 기능
    /// </summary>
    /// <param name="_value"></param>
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

    /// <summary>
    /// 플레이어 이동방향에 따른 스프라이트 변경
    /// 왼발 오른발 번갈아가면서 출력
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

            //if (curMotion == playerMotion.Step && lookDir == Vector3.left && footCheck == false)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = true;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.left && footCheck == true)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = false;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.right && footCheck == false)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = true;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.right && footCheck == true)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = false;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.up && footCheck == false)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = true;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.up && footCheck == true)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = false;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.down && footCheck == false)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = true;
            //    }
            //}
            //if (curMotion == playerMotion.Step && lookDir == Vector3.down && footCheck == true)
            //{
            //    if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
            //    {
            //        playerMotionChange();
            //        spriteChangeDelay = 0.3f;
            //    }
            //    if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay -= Time.deltaTime;
            //    }
            //    if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
            //    {
            //        spriteChangeDelay = 0.0f;
            //        curMotion = playerMotion.Idle;
            //        checkChangeSpriteDelay = false;
            //        footCheck = false;
            //    }
            //}
            playerMotionChange();
        }
    }


    /// <summary>
    /// 제자리에서 플레이어의 방향전환 기능
    /// </summary>
    private void turning()
    {
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            lookDir = transform.position;
            lookDir = Vector3.left;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.RightArrow)))
        {
            lookDir = transform.position;
            lookDir = Vector3.right;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.UpArrow)))
        {
            lookDir = transform.position;
            lookDir = Vector3.up;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.DownArrow)))
        {
            lookDir = transform.position;
            lookDir = Vector3.down;
            curMotion = playerMotion.Idle;
            playerMotionChange();
        }
    }

    private void attack()
    {
        if (Input.GetKey(KeyCode.Space) && checkDelay == false)
        {
            curMotion = playerMotion.Attack;
            //hitbox.OnHitBox(lookDir);
            checkActionDelay(0.3f);
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
                //case playerMotion.IdleLeft:
                //    {
                //        sr.sprite = idle[0];
                //        break;
                //    }
                //case playerMotion.LeftDirLeftFoot:
                //    {
                //        sr.sprite = idle[1];
                //        break;
                //    }
                //case playerMotion.LeftDirRightFoot:
                //    {
                //        sr.sprite = idle[2];
                //        break;
                //    }
                //case playerMotion.IdleRight:
                //    {
                //        sr.sprite = idle[3];
                //        break;
                //    }
                //case playerMotion.RightDirLeftFoot:
                //    {
                //        sr.sprite = idle[4];
                //        break;
                //    }
                //case playerMotion.RightDirRightFoot:
                //    {
                //        sr.sprite = idle[5];
                //        break;
                //    }
                //case playerMotion.IdleBack:
                //    {
                //        sr.sprite = idle[6];
                //        break;
                //    }
                //case playerMotion.BackDirLeftFoot:
                //    {
                //        sr.sprite = idle[7];
                //        break;
                //    }
                //case playerMotion.BackDirRightFoot:
                //    {
                //        sr.sprite = idle[8];
                //        break;
                //    }
                //case playerMotion.IdleFront:
                //    {
                //        sr.sprite = idle[9];
                //        break;
                //    }
                //case playerMotion.FrontDirLeftFoot:
                //    {
                //        sr.sprite = idle[10];
                //        break;
                //    }
                //case playerMotion.FrontDirRightFoot:
                //    {
                //        sr.sprite = idle[11];
                //        break;
                //    }
        }
    }
}

