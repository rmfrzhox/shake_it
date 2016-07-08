using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour {
    // 기본 게임 정보
    int iCurrentCoin = 60000000;
    int iCurrentDia = 60000000;

    // 게임 시작 정보
    public bool onShakeIt = false;

    //  데이터
    public Database database;
    public Player_Data userInfo;

    // 아이템 오브젝트
    public GameObject[] itemObj;
    public GameObject[] stageObj = new GameObject[27];
    public GameObject[] chapObj;    
    
    // 기본 프레임 스프라이트
    public Sprite lockSprite;
    public Sprite unlockSprite;

    // 공 정보 프레임
    public Text[] ballText = new Text[4];
 
    // 현재 돈 프레임
    public Text coinText;
    public Text diaText;

    // 아이템 구매 상점 프레임
    public GameObject itemStore;
    public Image itemBuyImage;
    public Text itemBuyPrice;
    public Text itemBuyScenario;

    // 스테이지 구매 상점 프레임
    public GameObject stageStore;
    public Text stageBuyPrice;
    public Text stageBuyTitle;

    // 챕터 구매 상점 프레임
    public GameObject chapStore;
    public Text chapBuyTitle;
    public Text chapBuyScenario;
    public Text chapBuyPrice;


    // 선택 상품 정보
    int itemIndex = 81;
    int stageIndex = 27;
    int chapIndex = 9;


    //  시스템
    UnlockSystem unlockSystem;
    ScreenSystem screenSystem;

    // 싱글톤 구현
    public static GameDataManager _instance = null;

    // 싱글톤 정보에 접근 할 때 사용 되는 프로퍼티
    public static GameDataManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        // 초기화하면서 인스턴스 정보 저장
        if (_instance == null)
        {
            _instance = this;

            // 씬이 바뀌어도 삭제 되지 않고 유지
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 생성되어 있는 경우에는 삭제
            Destroy(gameObject);
        }

        if (!database)
        {
            database = Resources.Load("Database") as Database;
        }
        if (!userInfo)
        {
            userInfo = Resources.Load("PlayerInfo") as Player_Data;
        }

        if (!unlockSystem)
        {
            unlockSystem = gameObject.GetComponent < UnlockSystem >();
        }
        if (!screenSystem)
        {
            screenSystem = gameObject.GetComponent<ScreenSystem>();
        }
        
    }

    // Use this for initialization
    void Start()
    {
        SetData();
    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Jump"))
        {
          //  DataInit();
           // SetData();
           // print("데이터 초기화");
        }
	
	}

    // 각 종 공 업그레이드
    public void BallUp(int index)
    {
        if (CanIbuy(userInfo.hitPoint[index]) == false) return;

        iCurrentCoin -= BallPrice(index);
        userInfo.hitPoint[index] += 1;
        BallInfoUpdate(index);
        UpdateText();
    }

    // 현재 코인으로 살 수 있는가?
    bool CanIbuy(int Coin)
    {
        if (iCurrentCoin < Coin) return false;
        return true;
    }
    // 공 단가 계산
    int BallPrice(int index)
    {
        return userInfo.hitPoint[index] * 100;
    }

    // 현재 값 텍스트 업데이트
    void BallInfoUpdate(int index)
    {
        ballText[index].text = userInfo.hitPoint[index].ToString() + " Per";
    }

    // 아이템 상품 정보 셋팅
    public void SetItemProduct(GameObject ItemProductProperty)
    {
        int currentIndex = itemObj.Length;

        // 아이템 찾기
        for (int i = 0; i < itemObj.Length; i++)
        {
            if (ItemProductProperty == itemObj[i])
            {
                currentIndex = i;
                break;
            }
            else if (i == itemObj.Length) return;
        }

        if (database.itemProperty[currentIndex].isLock == true) return;

        // 찾은 아이템 창에 셋팅
        itemStore.SetActive(true);
        itemBuyImage.sprite = database.itemProperty[currentIndex].imageItem;

        if (database.itemProperty[currentIndex].isBuy)
            itemBuyPrice.text = "구매 완료";
        else if(database.itemProperty[currentIndex].isDiaLock)
            itemBuyPrice.text = string.Format("{0:##,###,###,###}", database.itemProperty[currentIndex].priceCoin) + " C";
        else 
            itemBuyPrice.text = string.Format("{0:##,###,###,###}", database.itemProperty[currentIndex].priceCoin) + " C / " + database.itemProperty[currentIndex].priceDia.ToString() + " D";
        
        itemBuyScenario.text = database.itemProperty[currentIndex].strScenario;

        // 상품 등록
        itemIndex = currentIndex;
    }

    // 스테이지 상품 정보 셋팅
    public void SetStageProduct(GameObject stageProductProperty)
    {
        int currentIndex = stageObj.Length;

        // 스테이지 찾기
        for (int i = 0; i < stageObj.Length; i++)
        {
            if (stageProductProperty == stageObj[i])
            {
                currentIndex = i;
                break;
            }
            else if (i == stageObj.Length) return;
        }

        if (database.stageProperty[currentIndex].isLock == true) return;

        // 찾은 아이템 창에 셋팅
        stageStore.SetActive(true);

        if (database.stageProperty[currentIndex].isBuy) 
            stageBuyPrice.text = "구매 완료";
        else if (database.stageProperty[currentIndex].isDiaLock) 
            stageBuyPrice.text = database.stageProperty[currentIndex].priceCoin.ToString() + " C\n";
        else
            stageBuyPrice.text = database.stageProperty[currentIndex].priceCoin.ToString() + " C\n" + database.stageProperty[currentIndex].priceDia.ToString() + " D";

        stageBuyTitle.text = database.stageProperty[currentIndex].strName;

        // 상품 등록
        stageIndex = currentIndex;
    }

    // 챕터 상품 정보 셋팅
    public void SetChapProduct(GameObject chapProductProperty)
    {
        int currentIndex = 0;

        // 스테이지 찾기
        for (int i = 0; i < chapObj.Length; i++)
        {
             if (chapProductProperty == chapObj[i])
             {
                 currentIndex = i;
                 break;
             }
             else if (i == chapObj.Length) return;
        }

        if (database.chapProperty[currentIndex].isLock == true) return;
        
        // 찾은 아이템 창에 셋팅
        chapStore.SetActive(true);

        chapBuyTitle.text = database.chapProperty[currentIndex].strName;
        chapBuyScenario.text = database.chapProperty[currentIndex].strScenario ;

        if (database.chapProperty[currentIndex].isBuy) chapBuyPrice.text = "구매 완료";
        else chapBuyPrice.text = database.chapProperty[currentIndex].priceCoin.ToString() + " C\n" + database.chapProperty[currentIndex].priceDia.ToString() + " D\n";

        // 상품 등록
        chapIndex = currentIndex;
    }

    // 구매 시도
    public void TryBuyDia(GameObject buyLocation)
    {
        if (buyLocation == itemStore) // 아이템 구매 창인가?
        {
            // 다이아로 못 사는 것 이면 리턴
            if (database.itemProperty[itemIndex].isDiaLock) return;

            // 구매 한 것 이면 리턴
            if (database.itemProperty[itemIndex].isBuy) return;

            //현재 다이아가 부족하면 리턴
            if (iCurrentDia < database.itemProperty[itemIndex].priceDia) return;

            iCurrentDia -= database.itemProperty[itemIndex].priceDia;
            
            database.itemProperty[itemIndex].isBuy = true;
            itemBuyPrice.text = "구매 완료";

            unlockSystem.WhenBuyItem(itemIndex);
            screenSystem.WhenBuyItem(itemIndex);
        }
        else if (buyLocation == stageStore) // 스테이지 구매 창인가?
        {
            // 구매 한 것 이면 리턴
            if (database.stageProperty[stageIndex].isBuy) return;

            //현재 다이아가 부족하면 리턴
            if (iCurrentDia < database.stageProperty[stageIndex].priceDia) return;

            iCurrentDia -= database.stageProperty[stageIndex].priceDia;
            database.stageProperty[stageIndex].isBuy = true;
            stageBuyPrice.text = "구매 완료";

            unlockSystem.WhenBuyStage(stageIndex);
        }
        else if (buyLocation == chapStore) // 챕터 구매 창인가?
        {

            // 구매 한 것 이면 리턴
            if (database.chapProperty[chapIndex].isBuy) return;

            //현재 다이아가 부족하면 리턴
            if (iCurrentDia < database.chapProperty[chapIndex].priceDia) return;

            iCurrentDia -= database.chapProperty[chapIndex].priceDia;
            database.chapProperty[chapIndex].isBuy = true;
            chapBuyPrice.text = "구매 완료";

            unlockSystem.WhenBuyChapter(chapIndex);
            screenSystem.WhenBuyChapter(chapIndex);

        }
        UpdateText();
    }

    public void TryBuyCoin(GameObject buyLocation)
    {
        if (buyLocation == itemStore) // 아이템 구매 창인가?
        {
            // 구매 한 것 이면 리턴
            if (database.itemProperty[itemIndex].isBuy) return;
            
                //현재 코인이 부족하면 리턴
                if (iCurrentCoin < database.itemProperty[itemIndex].priceCoin) return;

                iCurrentCoin -= database.itemProperty[itemIndex].priceCoin;
                database.itemProperty[itemIndex].isBuy = true;
                itemBuyPrice.text = "구매 완료";

                unlockSystem.WhenBuyItem(itemIndex);
                screenSystem.WhenBuyItem(itemIndex);

        }
        else if (buyLocation == stageStore) // 스테이지 구매 창인가?
        {
            // 구매 한 것 이면 리턴
            if (database.stageProperty[stageIndex].isBuy) return;
           
                //현재 코인이 부족하면 리턴
            if (iCurrentCoin < database.stageProperty[stageIndex].priceCoin) return;

            iCurrentCoin -= database.stageProperty[stageIndex].priceCoin;
            database.stageProperty[stageIndex].isBuy = true;
            stageBuyPrice.text = "구매 완료";

            unlockSystem.WhenBuyStage(stageIndex);

        }
        else if (buyLocation == chapStore) // 챕터 구매 창인가?
        {

            // 구매 한 것 이면 리턴
            if (database.chapProperty[chapIndex].isBuy) return;
            
                //현재 코인이 부족하면 리턴
                if (iCurrentCoin < database.chapProperty[chapIndex].priceCoin) return;

                iCurrentCoin -= database.chapProperty[chapIndex].priceCoin;
                database.chapProperty[chapIndex].isBuy = true;
                chapBuyPrice.text = "구매 완료";

                unlockSystem.WhenBuyChapter(chapIndex);
                screenSystem.WhenBuyChapter(chapIndex);

        }
        UpdateText();
    }

    public void UpdateText()
    {
        coinText.text = string.Format("{0:##,###,###,###}", iCurrentCoin) + " C";
        diaText.text = string.Format("{0:##,###,###,###}", iCurrentDia) + " D";
    }
    

    // 데이터 초기화 
    void DataInit()
    {
        // 아이템 초기화
        for (int i = 0; i < 81; i++)
        {
            database.itemProperty[i].isBuy = false;
            database.itemProperty[i].isLock = true;
        }
        database.itemProperty[0].isLock = false;
        database.itemProperty[1].isLock = false;
        database.itemProperty[2].isLock = false;

        // 스테이지 초기화
        for (int i = 0; i < 27; i++)
        {
            database.stageProperty[i].isBuy = false;
            database.stageProperty[i].isLock = true;
        }

        // 챕터초기화
        for (int i = 0; i < 9; i++)
        {
            database.chapProperty[i].isBuy = false;
            database.chapProperty[i].isLock = true;
        }

        for (int i = 0; i < 9; i++)
        {
            screenSystem.screendata.chapterInfo[i].dir[0] = false;
            screenSystem.screendata.chapterInfo[i].dir[1] = false;
            screenSystem.screendata.chapterInfo[i].dir[2] = false;
            screenSystem.screendata.chapterInfo[i].dir[3] = false;
        }
        screenSystem.InitScreen();
    }

    // 데이터 셋팅
    void SetData()
    {
        // 플레이어 데이터 입력
        //   iCurrentCoin = userInfo.currentCoin;
        //  iCurrentDia = userInfo.currentDia;
        //  boxSpeed = userInfo.boxSpeed;
        //  hitPoint = userInfo.hitPoint;


        // 아이템 데이터 입력
        for (int i = 0; i < itemObj.Length; i++)
        {

            if (database.itemProperty[i].isLock == true)
            {
                itemObj[i].GetComponent<Image>().sprite = lockSprite;
                itemObj[i].transform.FindChild("Item_Image").GetComponent<Image>().enabled = false;
            }
            else
            {
                itemObj[i].GetComponent<Image>().sprite = unlockSprite;
                itemObj[i].transform.FindChild("Item_Image").GetComponent<Image>().enabled = true;
                itemObj[i].transform.FindChild("Item_Image").GetComponent<Image>().sprite = database.itemProperty[i].imageItem;

            }
        }

        // 챕터 데이터 입력
        for (int i = 0; i < chapObj.Length; i++)
        {
            if (database.chapProperty[i].isLock == true)
            {
                chapObj[i].transform.FindChild("Chapter_Btn").transform.FindChild("Chapter_Text").GetComponent<Text>().text = "? ? ?";
            }
            else
            {
                chapObj[i].transform.FindChild("Chapter_Btn").transform.FindChild("Chapter_Text").GetComponent<Text>().text = database.chapProperty[i].strName;
                chapObj[i].transform.FindChild("Chapter_Btn").GetComponent<Image>().sprite = unlockSprite;

            }
        }

        // 챕터에서 스테이지 배열 만들기
        for (int i = 0; i < chapObj.Length; i++)
        {
            stageObj[i * 3 + 0] = chapObj[i].transform.FindChild("Stage01").gameObject as GameObject;
            stageObj[i * 3 + 1] = chapObj[i].transform.FindChild("Stage02").gameObject as GameObject;
            stageObj[i * 3 + 2] = chapObj[i].transform.FindChild("Stage03").gameObject as GameObject;
        }

        // 스테이지 데이터 입력
        for (int i = 0; i < stageObj.Length; i++)
        {
            if (database.stageProperty[i].isLock == false)
            {
                stageObj[i].GetComponent<Image>().sprite = unlockSprite;
                stageObj[i].transform.FindChild("Text").GetComponent<Text>().enabled = true;
                stageObj[i].transform.FindChild("Text").GetComponent<Text>().text = database.stageProperty[i].strName;
            }
            else 
            {
                stageObj[i].GetComponent<Image>().sprite = lockSprite;
                stageObj[i].transform.FindChild("Text").GetComponent<Text>().enabled = false;
            }
        }
        UpdateText();
    }

    public void CoinSave(int _val)
    {
        GameDataManager.Instance.userInfo.currentCoin += _val;
    }
    public int CoinPerHit(int index)
    {
       return GameDataManager.Instance.userInfo.hitPoint[index];
    }
}
