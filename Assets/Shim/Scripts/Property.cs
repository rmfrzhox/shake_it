using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Property : MonoBehaviour {
    // 아이템 속성 설정
    public string strName;
    public bool isBuy;
    public bool isLock;
    public int priceCoin;
    public int priceDia;
    public Sprite imageItem;

    // 타겟 속성
    public Image targetImage;

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    // 함수
    public void SendBuyInfo()
    {
      //  GameManager.Instance.SetProduct(this);
    }
}
