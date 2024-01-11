using UnityEngine;

public class GaugeBar : MonoBehaviour
{
    private Transform trsPlayer;
    private Transform trsEnemy;
    private void Awake()
    {

    }
    private void Update()
    {
        checkEnemyPos();
    }
    void Start()
    {
        GameManager manager = GameManager.Instance;
        GameObject obj = manager.GetPlayerGameObject();//플레이어
        Player objSc = obj.GetComponent<Player>();
        trsPlayer = obj.transform;
    }
    private void checkEnemyPos()
    {
        if (trsPlayer == null) { return; }
        transform.position = trsPlayer.position;
    }
}