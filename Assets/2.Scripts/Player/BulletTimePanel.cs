using Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTimePanel : MonoBehaviour
{
    public static BulletTimePanel Instance;

    private PlayerController _playerController;

    public Image Panel;

    private List<Sprite> _images;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Panel = GetComponent<Image>();
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

    public void ChangeImage()
    {
        if (_playerController.IsGodMode)
            Panel.sprite = _images[1];
        else
            Panel.sprite = _images[0];
    }
}
