using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] GameObject objPlayer;//Inspector�� objPlayer ����
    void Start()
    {

    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        chasePlayer();
    }
    /// <summary>
    /// ī�޶� �÷��̾ ����ٴϴ� ���
    /// </summary>
    private void chasePlayer()
    {
        if (objPlayer == null) { return; }// objPlayer�� null�̸� �۵����� ����
        Vector3 pos = objPlayer.transform.position;// pos�� objPlayer�� ��ġ�� Vector3�� ����
        pos.z = -10;// objPlayer���� z�� ��ġ�� -10���� �ؾ� ī�޶� ���� 
        transform.position = pos;// pos�� ���� Vector3���� transform.position�� ����
    }
}
