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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(ShowFinalImage)
        {
            currentImageIndex = storyImages.Length - 1;
        }

        ShowImage();
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
        storyImage.sprite = storyImages[currentImageIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
