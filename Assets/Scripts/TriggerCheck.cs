using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] private Player.playerMotion Attack;
    Player player;
    Vector3 moveVec;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.TriggerEnter(Attack, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.TriggerExit(Attack, collision);
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    //public void OnHitBox(Vector3 _value)
    //{
    //    if (_value.x == -1)
    //    {
    //        moveVec = transform.localPosition;
    //        moveVec += new Vector3(-1, 0, 0);
    //        transform.localPosition = moveVec;
    //    }
    //    if (_value.x == 1)
    //    {
    //        moveVec = transform.localPosition;
    //        moveVec += new Vector3(1, 0, 0);
    //        transform.localPosition = moveVec;
    //    }
    //    if (_value.y == 1)
    //    {
    //        moveVec = transform.localPosition;
    //        moveVec += new Vector3(0, 1, 0);
    //        transform.localPosition = moveVec;
    //    }
    //    if (_value.y == -1)
    //    {
    //        moveVec = transform.localPosition;
    //        moveVec += new Vector3(0, -1, 0);
    //        transform.localPosition = moveVec;
    //    }
    //}
}