using UnityEngine;

[RequireComponent (typeof(Camera))]
public class BattleCameraFit : MonoBehaviour
{
    [SerializeField] private float baseSize = 7f;
    [SerializeField] private float referenceAspect = 9f / 16f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCemera();
    }

    void AdjustCemera()
    {
        float aspect = (float)Screen.width / Screen.height;

        cam.orthographicSize = baseSize * referenceAspect / aspect;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
