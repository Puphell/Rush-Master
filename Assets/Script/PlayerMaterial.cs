using UnityEngine;

public class PlayerMaterial : MonoBehaviour
{
    public static PlayerMaterial instance;
    public Material playerMaterial; // Player'ın materialini temsil eder
    public Color[] colors; // Renkleri temsil eden dizi

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Kaydedilen rengi yükle
        int colorIndex = PlayerPrefs.GetInt("UnlockedColorIndex", -1);
        if (colorIndex != -1)
        {
            ChangeColor(colorIndex);
        }
    }

    public void ChangeColor(int index)
    {
        if (index >= 0 && index < colors.Length)
        {
            playerMaterial.color = colors[index];
        }
    }

    public void OnColorButtonClicked(int index)
    {
        ChangeColor(index);
        PlayerPrefs.SetInt("UnlockedColorIndex", index);
    }
}