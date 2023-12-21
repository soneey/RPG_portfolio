using UnityEngine;

public class Player : MonoBehaviour
{
    private enum playerMotion
    {
        idleFront,
        idleBack,
        idleRight,
        idleLeft,
    }

    [SerializeField] private float moveDelay = 0.5f;
    private float moveDelayTimer = 0.0f;
    private bool checkMoving;

    Rigidbody rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Sprite sprite;
    Vector3 moveVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        moving();
        doAnimation();
        //turning();
    }

    /// <summary>
    /// �÷��̾ �����¿� 1ĭ�� ��ǥ�̵� �� �����̵��� ������üũ
    /// </summary>
    private void moving()
    {
        if (Input.GetKey(KeyCode.RightArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(1, 0, 0);
            transform.position = moveVec;
            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(-1, 0, 0);
            transform.position = moveVec;
            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.UpArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, 1, 0);
            transform.position = moveVec;
            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, -1, 0);
            transform.position = moveVec;
            checkMoving = true;
        }
        checkMovingDelay();
    }
    int count = 0;
    private void doAnimation()
    {
        //if (count == 0 && Input.GetKeyDown(KeyCode.UpArrow) && checkMoving == false)
        //{
        //    //animator.SetInteger("WalkBack1", 1);
        //    animator.Play("WalkBack1");
        //    count++;
        //}
        //if (count == 1 && Input.GetKeyDown(KeyCode.UpArrow) && checkMoving == false)
        //{
        //    //animator.SetInteger("WalkBack1", 1);
        //    animator.Play("WalkBack2");
        //    count--;
        //}
    }


    //private void turning()
    //{
    //    if (Input.GetKeyUp(KeyCode.DownArrow) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.DownArrow)))
    //    {
    //        sprite = spriteRenderer.sprite;
    //    }
    //}

    /// <summary>
    /// �÷��̾� �̵� �� ���� �̵����� ������
    /// ���� ! �������¸� �����̸� �ٿ��� �̵��ӵ��� ����
    /// </summary>
    private void checkMovingDelay()
    {
        if (checkMoving == false) { return; }
        if (checkMoving == true && moveDelayTimer != 0)
        {
            moveDelayTimer -= Time.deltaTime;
        }
        if (moveDelayTimer <= 0)
        {
            moveDelayTimer = 0.0f;
            moveDelayTimer = moveDelay;
            checkMoving = false;
        }
        //Debug.Log(moveDelayTimer);
    }
}
