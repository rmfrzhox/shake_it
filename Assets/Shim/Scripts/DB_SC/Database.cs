using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "Database")]
public class Database : ScriptableObject
{
int a;
    public ObjectProperty[] itemProperty = new ObjectProperty[81];
    public ObjectProperty[] stageProperty = new ObjectProperty[27];
    public ObjectProperty[] chapProperty = new ObjectProperty[9];
}

[System.Serializable]
public class ObjectProperty
{
    public string strName = "new item";
    public string strScenario = "new Scenario";
    public bool isBuy = false;
    public bool isLock = true;
    public bool isDiaLock = false;
    public int priceCoin = 100;
    public int priceDia = 100;
    public Sprite imageItem;
}
