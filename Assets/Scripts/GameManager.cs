using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//싱글톤
    [SerializeField] private float checkDelayCount = 3000000.0f;
    private bool checkDelay;

    [Header("몬스터 생성")]
    [SerializeField] private List<GameObject> listEnemys;//인스펙터에 프리팹 넣기
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// Player, Enemy의 모든 행동 후 딜레이를 관리하는 기능
    /// </summary>
    /// <param name="_value"></param>
    private void checkAllDelay(float _value)
    {
        if (checkDelayCount != 3000000.0f && checkDelay == false) { return; }

        checkDelayCount = checkDelayCount - _value;
        if (checkDelay == true && _value != 0)
        {
            checkDelayCount += Time.deltaTime;
        }
        if (checkDelayCount < 3000000.0f)
        {
            checkDelayCount = 3000000.0f;
            checkDelay = false;
        }
    }

    private void enemyRespawn()
    { 
    
    }
}
