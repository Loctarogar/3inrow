using UnityEngine;

public class CampaingManager : MonoBehaviour
{
    [Header("Campaign References")]
    [SerializeField] private MechanisCampaign mechanisCampaign;
    [SerializeField] private EldoriaCampaign eldoriaCampaign;
    public CampaignStage[] ActiveStages { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InitializeCampaign();
    }

    public void InitializeCampaign()
    {
        if (HeroData.instance == null)
        {
            Debug.LogError("HeroData.instance is null");
            return;
        }

        switch (HeroData.instance.heroCountry)
        {
            case "Mechanis":
                if (mechanisCampaign == null)
                {
                    Debug.LogError("MechanisCampaign reference is missing");
                    return;
                }

                ActiveStages = mechanisCampaign.stages;
                break;

            case "Eldoria":
                if (eldoriaCampaign == null)
                {
                    Debug.LogError("EldoriaCampaign reference is missing");
                    return;
                }

                ActiveStages = eldoriaCampaign.stages;
                break;

            default:
                Debug.LogError("Uncknown hero country");
                return;
        }

        Debug.Log("Campaign initialized: " + HeroData.instance.heroCountry + "Stages: " + ActiveStages.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
