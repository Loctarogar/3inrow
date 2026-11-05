using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public static bool ShowFinalImage = false;
    [Header("Story Image")] public Sprite[] storyImages;
    [Header("Mechanis Image")] public Sprite mechanisImage;
    [Header("Eldoria Image")] public Sprite eldoriaImage;

    [Header("UI")]
    public Image storyImage;
    public Button nextButton;

    private int currentImageIndex = 0;
    public TMP_Text storyText;
    public TMP_Text storyTextBackground;
    public string[] storyTexts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ShowFinalImage)
        {
            Debug.Log(HeroData.instance.heroCountry);
            Debug.Log(HeroData.instance.heroAttack);
            Debug.Log(HeroData.instance.heroDamage);
            Debug.Log(HeroData.instance.heroDescription);
            Debug.Log(HeroData.instance.heroHealth);
            Debug.Log(HeroData.instance.heroImage);
            Debug.Log(HeroData.instance.heroMagic);
            Debug.Log(HeroData.instance.heroName);
            Debug.Log(HeroData.instance.heroProtection);

            if (HeroData.instance.heroCountry == "Mechanis")
            {
                storyImage.sprite = mechanisImage;
                storyText.text = storyTexts[5];
                storyTextBackground.text = storyTexts[5];
            } else 
            {
                storyImage.sprite = eldoriaImage;
                storyText.text = storyTexts[6];
                storyTextBackground.text = storyTexts[6];
            }
        }
        else
        {
        ShowImage();
        }

        nextButton.onClick.AddListener(NextImage);
    }

    private void NextImage()
    {
        if (ShowFinalImage)
        {
            SceneManager.LoadScene("BattleScene");
            return;
        }

        currentImageIndex++;

        if (currentImageIndex >= storyImages.Length)
        {
            SceneManager.LoadScene("HeroSelection");
            return;
        }

        ShowImage();
    }

    public void ShowImage()
    {
        Debug.Log(currentImageIndex);
        storyImage.sprite = storyImages[currentImageIndex];
        storyText.text = storyTexts[currentImageIndex];
        storyTextBackground.text = storyTexts[currentImageIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
