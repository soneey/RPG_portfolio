using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private enum enemyMotion
    {
        None,
        IdleLeft,
        IdleRight,
        IdleBack,
        IdleFront,
        LeftDirLeftFoot,
        LeftDirRightFoot,
        RightDirLeftFoot,
        RightDirRightFoot,
        BackDirLeftFoot,
        BackDirRightFoot,
        FrontDirLeftFoot,
        FrontDirRightFoot,
        Attack,
        Idle,
        Step,
    }
    enemyMotion curMotion = enemyMotion.None;
    enemyMotion beforeMotion = enemyMotion.None;
    [SerializeField] private Sprite[] idle;
    [SerializeField] private bool footCheck;


    [Header("적 행동딜레이")]
    [SerializeField] private float checkDelayCount = 100.0f;
    private bool checkDelay;
    [SerializeField] private float moveTime;

    [Header("스프라이트 딜레이")]
    [SerializeField] private float spriteChangeDelay = 0.0f;
    private bool checkChangeSpriteDelay;

    [SerializeField] private float ratio = 0.0f;
    [SerializeField] private bool isMoving;
    Vector3 moveVec;
    Vector3 lookDir;
    Vector3 target;
    private int randomNumber;

    SpriteRenderer sr;
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
        softMoving();
        moving();
        changeSprite();
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
        Debug.Log($"before = {before}");
        Debug.Log($"moveVec = {moveVec}");
        Debug.Log($"target = {target}");
        Debug.Log($"ratio = {ratio}");
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
    private void moving()
    {
        if (checkDelay == false)
        {
            getRandomNumber();
            switch (randomNumber)
            {
                case 0:
                    {
                        isMoving = true;
                        beforeSave = true;
                        target = new Vector3(transform.position.x - 1, transform.position.y);
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        lookDir = Vector3.left;
                        curMotion = enemyMotion.Step;
                        break;

                    }
                case 1:
                    {
                        isMoving = true;
                        beforeSave = true;
                        target = new Vector3(transform.position.x + 1, transform.position.y);
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        lookDir = Vector3.right;
                        curMotion = enemyMotion.Step;
                        break;

                    }
                case 2:
                    {
                        isMoving = true;
                        beforeSave = true;
                        target = new Vector3(transform.position.x, transform.position.y + 1);
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        lookDir = Vector3.up;
                        curMotion = enemyMotion.Step;
                        break;

                    }
                case 3:
                    {
                        isMoving = true;
                        beforeSave = true;
                        target = new Vector3(transform.position.x, transform.position.y - 1);
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        lookDir = Vector3.down;
                        curMotion = enemyMotion.Step;
                        break;
                    }
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

    /// <summary>
    /// 몬스터의 이동방향을 랜덤으로 결정하기 위해 랜덤숫자를 받음
    /// </summary>
    private void getRandomNumber()
    {
        randomNumber = Random.Range(0, 4);
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


    /// <summary>
    /// 플레이어의 모션이 변경되면 스프라이트를 변경
    /// </summary>
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
}
