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
        Run,
    }

    enemyMotion curMotion = enemyMotion.None;
    enemyMotion beforeMotion = enemyMotion.None;

    [Header("스프라이트 변경")]
    [SerializeField] private Sprite[] idle;//스프라이트 등록
    [SerializeField] private bool footCheck;//왼발 오른발 순서 체크


    [Header("이동관련")]
    [SerializeField, Tooltip("100이 되면 행동, 변경하지 말 것")] private float checkDelayCount = 100.0f;
    [SerializeField, Tooltip("이동간의 딜레이 시간 설정")] private float moveTime;
    [SerializeField, Tooltip("리스폰 시간")] private float respawnTime;
   
    private float spriteChangeDelay = 0.0f;
    private float ratio = 0.0f;
    private int randomDirNumber;
    private bool isMoving;//이동중인지 체크
    private bool checkDelay;//이동 후 딜레이체크 시작,종료 체크
    private bool checkChangeSpriteDelay;
    Vector3 moveVec;
    Vector3 lookDir;
    Vector3 target;
    Vector3 before;
    Vector3 after;
    private bool beforeSave;


    SpriteRenderer sr;
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        moveVec = transform.position;
    }
    public float GetRespawnTime()
    {
        return respawnTime;
    }
    
    void Update()
    {
        softMoving();
        moving();
        changeSprite();
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
    private void getRandomNumber()
    {
        randomDirNumber = Random.Range(0, 4);
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
}
