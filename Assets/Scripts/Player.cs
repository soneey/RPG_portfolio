using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum playerMotion
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
    playerMotion curMotion = playerMotion.None;
    playerMotion beforeMotion = playerMotion.None;
    [SerializeField] private Sprite[] idle;
    [SerializeField] private bool footCheck;


    [Header("플레이어 행동딜레이")]
    [SerializeField] private float checkDelayCount = 100.0f;
    private bool checkDelay;

    [Header("스프라이트 딜레이")]
    [SerializeField] private float spriteChangeDelay = 0.0f;
    private bool checkChangeSpriteDelay;

    Rigidbody rigid;
    SpriteRenderer sr;
    Vector3 moveVec;
    Vector3 lookDir;

    private object manager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sr = gameObject.GetComponent<SpriteRenderer>();

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
    }

    /// <summary>
    /// 플레이어가 상하좌우 1칸씩 좌표이동 후 모션변경
    /// </summary>
    private void moving()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { return; }
        else
        {
            if ((Input.GetKey(KeyCode.LeftArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(-1, 0, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.LeftDirLeftFoot;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(-1, 0, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.LeftDirRightFoot;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(1, 0, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.RightDirLeftFoot;
            }
            if ((Input.GetKey(KeyCode.RightArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(1, 0, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.RightDirRightFoot;
            }
            if ((Input.GetKey(KeyCode.UpArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, 1, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.BackDirLeftFoot;
            }
            if ((Input.GetKey(KeyCode.UpArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, 1, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.BackDirRightFoot;
            }
            if ((Input.GetKey(KeyCode.DownArrow) && checkDelay == false && footCheck == false))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, -1, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.FrontDirLeftFoot;
            }
            if ((Input.GetKey(KeyCode.DownArrow) && checkDelay == false && footCheck == true))
            {
                moveVec = transform.position;
                moveVec += new Vector3(0, -1, 0);
                transform.position = moveVec;
                checkDelay = true;
                checkChangeSpriteDelay = true;
                curMotion = playerMotion.FrontDirRightFoot;
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
            if (curMotion == playerMotion.LeftDirLeftFoot)
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
                    curMotion = playerMotion.IdleLeft;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == playerMotion.LeftDirRightFoot)
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
                    curMotion = playerMotion.IdleLeft;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            if (curMotion == playerMotion.RightDirLeftFoot)
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
                    curMotion = playerMotion.IdleRight;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == playerMotion.RightDirRightFoot)
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
                    curMotion = playerMotion.IdleRight;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            if (curMotion == playerMotion.BackDirLeftFoot)
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
                    curMotion = playerMotion.IdleBack;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == playerMotion.BackDirRightFoot)
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
                    curMotion = playerMotion.IdleBack;
                    checkChangeSpriteDelay = false;
                    footCheck = false;
                }
            }
            if (curMotion == playerMotion.FrontDirLeftFoot)
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
                    curMotion = playerMotion.IdleFront;
                    checkChangeSpriteDelay = false;
                    footCheck = true;
                }
            }
            if (curMotion == playerMotion.FrontDirRightFoot)
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
                    curMotion = playerMotion.IdleFront;
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
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            lookDir = transform.position;
            lookDir = new Vector3(-1, 0);
            curMotion = playerMotion.IdleLeft;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.RightArrow)))
        {
            lookDir = transform.position;
            lookDir = new Vector3(1, 0);
            curMotion = playerMotion.IdleRight;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.UpArrow)))
        {
            lookDir = transform.position;
            lookDir += new Vector3(0, -1, 0);
            Debug.Log(lookDir);
            lookDir += new Vector3(0, 1, 0);
            Debug.Log(lookDir);
            transform.position = lookDir;
            curMotion = playerMotion.IdleBack;
            playerMotionChange();
        }
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.DownArrow)))
        {
            lookDir = transform.position;
            lookDir += new Vector3(0, 1, 0);
            Debug.Log(lookDir);
            lookDir += new Vector3(0, -1, 0);
            Debug.Log(lookDir);
            transform.position = lookDir;
            curMotion = playerMotion.IdleFront;
            playerMotionChange();
        }
    }

    private void attack()
    {
        if (Input.GetKey(KeyCode.Space))
        { 
        
        }
    }

    /// <summary>
    /// 플레이어의 모션이 변경되면 스프라이트를 변경
    /// </summary>
    private void playerMotionChange()
    {
        switch (curMotion)
        {
            case playerMotion.IdleLeft:
                {
                    sr.sprite = idle[0];
                    break;
                }
            case playerMotion.LeftDirLeftFoot:
                {
                    sr.sprite = idle[1];
                    break;
                }
            case playerMotion.LeftDirRightFoot:
                {
                    sr.sprite = idle[2];
                    break;
                }
            case playerMotion.IdleRight:
                {
                    sr.sprite = idle[3];
                    break;
                }
            case playerMotion.RightDirLeftFoot:
                {
                    sr.sprite = idle[4];
                    break;
                }
            case playerMotion.RightDirRightFoot:
                {
                    sr.sprite = idle[5];
                    break;
                }
            case playerMotion.IdleBack:
                {
                    sr.sprite = idle[6];
                    break;
                }
            case playerMotion.BackDirLeftFoot:
                {
                    sr.sprite = idle[7];
                    break;
                }
            case playerMotion.BackDirRightFoot:
                {
                    sr.sprite = idle[8];
                    break;
                }
            case playerMotion.IdleFront:
                {
                    sr.sprite = idle[9];
                    break;
                }
            case playerMotion.FrontDirLeftFoot:
                {
                    sr.sprite = idle[10];
                    break;
                }
            case playerMotion.FrontDirRightFoot:
                {
                    sr.sprite = idle[11];
                    break;
                }
        }
    }
}

