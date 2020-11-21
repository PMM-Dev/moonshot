using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameoverPanel;
    [SerializeField]
    private Slider _stageProgressSlider;
    [SerializeField]
    private Text _killCountText;
    [SerializeField]
    private Text _survivedTimeText;

    public void ShowGameoverUI(string killedEnemyCount, string survivedTime)
    {
        _gameoverPanel.SetActive(true);
        _killCountText.text = killedEnemyCount;
        _survivedTimeText.text = survivedTime;
    }

    public void OnClickRestart()
    {
        //

        _gameoverPanel.SetActive(false);
    }

    public void OnClickLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
