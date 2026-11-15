using UnityEngine;

public class CityCampaignNode : MonoBehaviour
{
    [Header("City")]
    [SerializeField] private string cityId;

    [Header("Battle Markers")]

    [Header("Cities Unlocked After Conquest")]
    [SerializeField] private CityCampaignNode unlockCity1;
    [SerializeField] private CityCampaignNode unlockCity2;

    public string CityId => cityId;
    public CityCampaignNode UnlockCity1 => unlockCity1;
    public CityCampaignNode UnlockCity2 => unlockCity2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
