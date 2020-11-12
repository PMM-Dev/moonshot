using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private Text _killedEnemyText;
    [SerializeField]
    private Text _survivedTimeText;

    public void WriteGameoverUI(string killedEnemyCount, string survivedTime)
    {
        _killedEnemyText.text = killedEnemyCount;
        _survivedTimeText.text = survivedTime;
    }
}
