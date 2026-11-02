using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [Header("Story Image")] public Sprite[] storyImages;

    [Header("UI")]
    public Image storyImage;
    public Button nextButton;

    private int currentImageIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowImage();
        nextButton.onClick.AddListener(NextImage);
    }

    private void NextImage()
    {
        currentImageIndex++;

        if (currentImageIndex >= storyImages.Length)
        {
            currentImageIndex = storyImages.Length - 1;
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
