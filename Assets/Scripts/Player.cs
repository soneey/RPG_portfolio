using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum playerMotion
    {
        None,
        IdleFront,
        IdleBack,
        IdleRight,
        IdleLeft,
    }
    playerMotion curMotion = playerMotion.None;
    playerMotion beforeMotion = playerMotion.None;

    [SerializeField] private float moveDelay = 0.5f;
    private float moveDelayTimer = 0.0f;
    private bool checkMoving;

    Rigidbody rigid;
    Animator animator;
    SpriteRenderer sr;
    Vector3 moveVec;

    private enum Step
    {
        None,
        LeftDirLeftFoot,
        LeftDirRightFoot,
        RightDirLeftFoot,
        RightDirRightFoot,
        BackDirLeftFoot,
        BackDirRightFoot,
        FrontDirLeftFoot,
        FrontDirRightFoot,
    }

    Step curStep = Step.None;
    Step beforeStep = Step.None;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        moving();
        doAnimation();
        turning();
    }

    /// <summary>
    /// 플레이어가 상하좌우 1칸씩 좌표이동 후 다음이동간 딜레이체크
    /// </summary>
    private void moving()
    {
        if (Input.GetKey(KeyCode.RightArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(1, 0, 0);
            transform.position = moveVec;
            checkMoving = true;
            moveDelayTimer = moveDelay;
            curStep = curStep == Step.RightDirLeftFoot ? Step.RightDirRightFoot : Step.RightDirLeftFoot;
            //if (curStep == Step.RightDirLeftFoot)
            //{
            //    curStep = Step.RightDirRightFoot;
            //}
            //else
            //{
            //    curStep = Step.RightDirLeftFoot;
            //}
        }
        if (Input.GetKey(KeyCode.LeftArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(-1, 0, 0);
            transform.position = moveVec;
            checkMoving = true;
            moveDelayTimer = moveDelay;
            curStep = curStep == Step.LeftDirLeftFoot ? Step.LeftDirRightFoot : Step.LeftDirLeftFoot;
        }
        if (Input.GetKey(KeyCode.UpArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, 1, 0);
            transform.position = moveVec;
            checkMoving = true;
            moveDelayTimer = moveDelay;
            curStep = curStep == Step.BackDirLeftFoot ? Step.BackDirRightFoot : Step.BackDirLeftFoot;
        }
        if (Input.GetKey(KeyCode.DownArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, -1, 0);
            transform.position = moveVec;
            checkMoving = true;
            moveDelayTimer = moveDelay;
            curStep = curStep == Step.FrontDirLeftFoot ? Step.FrontDirRightFoot : Step.FrontDirLeftFoot;
        }

        checkMovingDelay();
    }

    /// <summary>
    /// 플레이어 이동방향에 따른 애니메이션 출력
    /// 왼발 오른발 번갈아가면서 출력
    /// </summary>
    private void doAnimation()
    {
        if (checkMoving == true && curStep != beforeStep)
        {
            beforeStep = curStep;
            switch (curStep)
            {
                case Step.LeftDirLeftFoot:
                    {
                        animator.Play("WalkLeft1");
                        curMotion = playerMotion.IdleLeft;
                        break;
                    }
                case Step.LeftDirRightFoot:
                    {
                        animator.Play("WalkLeft2");
                        curMotion = playerMotion.IdleLeft;
                        break;
                    }
                case Step.RightDirLeftFoot:
                    {
                        animator.Play("WalkRight1");
                        curMotion = playerMotion.IdleRight;
                        break;
                    }
                case Step.RightDirRightFoot:
                    {
                        animator.Play("WalkRight2");
                        curMotion = playerMotion.IdleRight;
                        break;
                    }
                case Step.BackDirLeftFoot:
                    {
                        animator.Play("WalkBack1");
                        curMotion = playerMotion.IdleBack;
                        break;
                    }
                case Step.BackDirRightFoot:
                    {
                        animator.Play("WalkBack2");
                        curMotion = playerMotion.IdleBack;
                        break;
                    }
                case Step.FrontDirLeftFoot:
                    {
                        animator.Play("WalkFront1");
                        curMotion = playerMotion.IdleFront;
                        break;
                    }
                case Step.FrontDirRightFoot:
                    {
                        animator.Play("WalkFront2");
                        curMotion = playerMotion.IdleFront;
                        break;
                    }
            }
        }
    }

    private void turning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            curMotion = playerMotion.IdleFront;
        }
    }

    /// <summary>
    /// 플레이어 이동 후 다음 이동간에 딜레이
    /// 예정 ! 버프상태면 딜레이를 줄여서 이동속도를 증가
    /// </summary>
    private void checkMovingDelay()
    {
        if (checkMoving == false) { return; }
        if (checkMoving == true && moveDelayTimer != 0)
        {
            moveDelayTimer -= Time.deltaTime;
        }
        if (moveDelayTimer < 0)
        {
            moveDelayTimer = 0.0f;
            checkMoving = false;
        }
        //Debug.Log(moveDelayTimer);
    }

    public void PlayerMotion()
    {
        if (curMotion == playerMotion.IdleFront)
        {
            //curMotion = sr.sprite.GetSpriteID;
        }
    }
}
