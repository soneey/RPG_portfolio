using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    private Transform trs;
    [SerializeField] private Image imgFrontHp;
    [SerializeField] private Image imgMidHp;
    private void Awake()
    {

    }
    void Start()
    {

    }
    private void Update()
    {
        checkHp();
        SetHp();
        //isDestroying();
    }
    private void checkHp()
    {
        float amountFront = imgFrontHp.fillAmount;
        float amountMid = imgMidHp.fillAmount;

        if (amountFront < amountMid)//mid°¡ ±ð¿©¾ß ÇÔ
        {
            imgMidHp.fillAmount -= Time.deltaTime * 0.5f;
            if (imgMidHp.fillAmount <= imgFrontHp.fillAmount)
            {
                imgMidHp.fillAmount = imgFrontHp.fillAmount;
            }
            else if (amountFront > amountMid)
            {
                imgMidHp.fillAmount = imgFrontHp.fillAmount;
            }
        }
    }
    private void isDestroying()
    {
        if (imgMidHp.fillAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetHp(float _curHp, float _maxHp)
    {
        imgFrontHp.fillAmount = (float)_curHp / _maxHp;
    }
    //public void SetHp(float _curHp, float _maxHp)
    //{
    //    imgFrontHp.fillAmount = (float)_curHp / _maxHp;
    //}
}