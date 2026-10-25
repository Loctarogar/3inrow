using UnityEngine;
using UnityEngine.UI;

public class CountryBackground : MonoBehaviour
{
    public string currentCountry = "Mechanis";
    [SerializeField] private Sprite mechanisBackground;
    [SerializeField] private Sprite eldoriaBackground;

    void Start()
    {
        SetCountry(currentCountry);
    }

    public void SetCountry(string countryName)
    {
        GetComponent<Image>().sprite =
            countryName == "Mechanis" ? mechanisBackground : eldoriaBackground;
    }
}