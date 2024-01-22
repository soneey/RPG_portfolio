using UnityEngine;
using UnityEngine.UI;

public class MpGaugeBar : MonoBehaviour
{
    [SerializeField] private Image imgFrontMp;
    [SerializeField] private Image imgMidMp;
    private void Awake()
    {

    }
    void Start()
    {
        Transform target = transform.parent;
        if (target.gameObject.tag == "Ai")
        {
            Ai obj = target.GetComponent<Ai>();
            obj.SetMp(this);
        }

    }
    private void Update()
    {
        checkMp();
        //isDestroying();
    }
    private void checkMp()
    {
        float amountFront = imgFrontMp.fillAmount;
        float amountMid = imgMidMp.fillAmount;

        if (amountFront < amountMid)//mid°¡ ±ð¿©¾ß ÇÔ
        {
            imgMidMp.fillAmount -= Time.deltaTime * 0.5f;
            if (imgMidMp.fillAmount <= imgFrontMp.fillAmount)
            {
                imgMidMp.fillAmount = imgFrontMp.fillAmount;
            }
            else if (amountFront > amountMid)
            {
                imgMidMp.fillAmount = imgFrontMp.fillAmount;
            }
        }
    }
    public void SetMp(float _curMp, float _maxMp)
    {
        imgFrontMp.fillAmount = (float)_curMp / _maxMp;
    }
    
}