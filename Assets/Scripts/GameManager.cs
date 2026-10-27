using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurrentCountry = "Mechanis";

    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject boardBacground;
    [SerializeField] private GameObject crystalBoard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hero != null)
            hero.SetActive(true);

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
