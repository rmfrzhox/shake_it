using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "User", menuName = "DB/Player", order = 2)]

public class DataPlayer : ScriptableObject
{
    public int currentCoin;
    public int currentDia;
    public float boxSpeed;
    public int[] hitPoint = new int[4];
}
