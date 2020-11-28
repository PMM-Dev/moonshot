using UnityEngine;
using UnityEngine.UI;

public class BulletTimePanel : MonoBehaviour
{
    public static BulletTimePanel Instance;

    public Image Panel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Panel = GetComponent<Image>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
