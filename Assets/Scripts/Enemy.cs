using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        AttackLeft,
        AttackRight,
        AttackBack,
        AttackFront,
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

    Vector3 moveVec;
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
        moving();
        changeSprite();
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
                        moveVec = transform.position;
                        moveVec += new Vector3(-1, 0, 0);
                        transform.position = moveVec;
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        curMotion = enemyMotion.LeftDirLeftFoot;
                        break;

                    }
                case 1:
                    {
                        moveVec = transform.position;
                        moveVec += new Vector3(1, 0, 0);
                        transform.position = moveVec;
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        curMotion = enemyMotion.RightDirLeftFoot;
                        break;

                    }
                case 2:
                    {
                        moveVec = transform.position;
                        moveVec += new Vector3(0, 1, 0);
                        transform.position = moveVec;
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        curMotion = enemyMotion.BackDirLeftFoot;
                        break;

                    }
                case 3:
                    {
                        moveVec = transform.position;
                        moveVec += new Vector3(0, -1, 0);
                        transform.position = moveVec;
                        checkDelay = true;
                        checkChangeSpriteDelay = true;
                        curMotion = enemyMotion.FrontDirLeftFoot;
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
            if (curMotion == enemyMotion.LeftDirLeftFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[1];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleLeft;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == enemyMotion.LeftDirRightFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[2];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleLeft;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            if (curMotion == enemyMotion.RightDirLeftFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[4];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleRight;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == enemyMotion.RightDirRightFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[5];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleRight;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            if (curMotion == enemyMotion.BackDirLeftFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[7];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleBack;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == enemyMotion.BackDirRightFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[8];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleBack;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            if (curMotion == enemyMotion.FrontDirLeftFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[10];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleFront;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == enemyMotion.FrontDirRightFoot)
            {
                if (spriteChangeDelay == 0 && checkChangeSpriteDelay == true)
                {
                    sr.sprite = idle[11];
                    spriteChangeDelay = 0.3f;
                }
                if (spriteChangeDelay != 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay -= Time.deltaTime;
                }
                if (spriteChangeDelay < 0 && checkChangeSpriteDelay == true)
                {
                    spriteChangeDelay = 0.0f;
                    curMotion = enemyMotion.IdleFront;
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
            case enemyMotion.IdleLeft:
                {
                    sr.sprite = idle[0];
                    break;
                }
            case enemyMotion.LeftDirLeftFoot:
                {
                    sr.sprite = idle[1];
                    break;
                }
            case enemyMotion.LeftDirRightFoot:
                {
                    sr.sprite = idle[2];
                    break;
                }
            case enemyMotion.IdleRight:
                {
                    sr.sprite = idle[3];
                    break;
                }
            case enemyMotion.RightDirLeftFoot:
                {
                    sr.sprite = idle[4];
                    break;
                }
            case enemyMotion.RightDirRightFoot:
                {
                    sr.sprite = idle[5];
                    break;
                }
            case enemyMotion.IdleBack:
                {
                    sr.sprite = idle[6];
                    break;
                }
            case enemyMotion.BackDirLeftFoot:
                {
                    sr.sprite = idle[7];
                    break;
                }
            case enemyMotion.BackDirRightFoot:
                {
                    sr.sprite = idle[8];
                    break;
                }
            case enemyMotion.IdleFront:
                {
                    sr.sprite = idle[9];
                    break;
                }
            case enemyMotion.FrontDirLeftFoot:
                {
                    sr.sprite = idle[10];
                    break;
                }
            case enemyMotion.FrontDirRightFoot:
                {
                    sr.sprite = idle[11];
                    break;
                }
        }
    }
    
}
