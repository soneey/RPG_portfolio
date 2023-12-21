using UnityEngine;

public class Player : MonoBehaviour
{
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
        sprite = GetComponent<Sprite>();
    }
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        moving();
        turning();
    }

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

            animator.SetInteger("WalkBack", 1);

            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && checkMoving == false)
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, -1, 0);
            transform.position = moveVec;
            checkMoving = true;
        }
        checkDelay();
    }
    private void turning()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.RightArrow))
        {
            sprite = spriteRenderer.sprite; 
        }
    }

    private void checkDelay()
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
        Debug.Log(moveDelayTimer);
    }
}
