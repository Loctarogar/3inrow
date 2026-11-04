using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject boardBacground;
    [SerializeField] private GameObject crystalBoard;

    private SpriteRenderer heroRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hero != null)
        {
            heroRenderer = hero.GetComponent<SpriteRenderer>();
            if(HeroData.instance != null)
            {
                heroRenderer.sprite = HeroData.instance.heroImage;
                hero.SetActive(true);
            }
        }

        if (boardBacground != null)
            boardBacground.SetActive(true);

        if (crystalBoard != null)
            crystalBoard.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
