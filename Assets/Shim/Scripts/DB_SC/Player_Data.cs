using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerInfo")]
public class Player_Data : ScriptableObject
{
    public int currentCoin;
    public int currentDia;
    public float boxSpeed;
    public int[] hitPoint = new int[4];
}
