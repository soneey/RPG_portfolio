using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//�̱���
    [SerializeField] private float checkDelayCount = 3000000.0f;
    private bool checkDelay;

    [Header("���� ����")]
    [SerializeField] private List<GameObject> listEnemys;//�ν����Ϳ� ������ �ֱ�
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// Player, Enemy�� ��� �ൿ �� �����̸� �����ϴ� ���
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
