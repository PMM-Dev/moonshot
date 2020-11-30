using Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTimePanel : MonoBehaviour
{
    public enum ColorType
    { 
        BulletTime,
        GodMode,
        Die
    }

    public static BulletTimePanel Instance;

    private PlayerController _playerController;

    private Image _panel;
    public Image Panel => _panel;

    public Color BulletTimeColor;
    public Color GodModeColor;
    public Color DieColor;

    private List<Sprite> _images;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _panel = GetComponent<Image>();
    }

    private void Start()
    {
        Initialize();
        gameObject.SetActive(false);
    }

    private void Initialize()
    {
        _playerController = MainPlayerManager.Instance.Player.GetComponent<PlayerController>();

        _images = new List<Sprite>();
        _images.Add(Resources.Load<Sprite>("Player/BulletTime/focus"));
        _images.Add(Resources.Load<Sprite>("Player/BulletTime/focus_test"));
    }

    public void ChangePanelColor(ColorType colorType)
    {
        switch(colorType)
        {
            case ColorType.BulletTime:
                _panel.color = BulletTimeColor;
                break;
            case ColorType.GodMode:
                _panel.color = GodModeColor;
                break;
            case ColorType.Die:
                _panel.color = DieColor;
                break;
        }
    }
}
