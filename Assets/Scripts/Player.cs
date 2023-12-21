using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveDelay = 1.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    private float moveDelayTimer = 0.0f;
    private bool checkMoving;

    Rigidbody rigid;

    Vector3 moveVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    void Update()
    {
        moving();
    }

    private void moving()
    {
        checkDelay();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec = transform.position;
            moveVec += new Vector3(1, 0, 0) * Time.deltaTime;
            transform.position = moveVec;
            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec = transform.position;
            moveVec += new Vector3(-1, 0, 0) * Time.deltaTime;
            transform.position = moveVec;
            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, 1, 0) * Time.deltaTime;
            transform.position = moveVec;
            checkMoving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVec = transform.position;
            moveVec += new Vector3(0, -1, 0) * Time.deltaTime;
            transform.position = moveVec;
            checkMoving = true;
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
