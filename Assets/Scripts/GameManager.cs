using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TotalGold = 0;



    public void TotalAddGold(int gold)
    {
        TotalGold += gold;
    }

}
