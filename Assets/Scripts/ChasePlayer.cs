using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] GameObject objPlayer;//Inspector에 objPlayer 생성
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
    /// 카메라가 플레이어를 따라다니는 기능
    /// </summary>
    private void chasePlayer()
    {
        if (objPlayer == null) { return; }// objPlayer가 null이면 작동하지 않음
        Vector3 pos = objPlayer.transform.position;// pos에 objPlayer의 위치를 Vector3로 저장
        pos.z = -10;// objPlayer보다 z축 위치를 -10으로 해야 카메라에 보임 
        transform.position = pos;// pos에 담은 Vector3값을 transform.position에 저장
    }
}
