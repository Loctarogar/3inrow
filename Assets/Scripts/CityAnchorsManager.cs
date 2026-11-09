using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CityAnchorsManager : MonoBehaviour
{
    [SerializeField] private RectTransform mapBackground;
    [SerializeField] private RectTransform markersParent;

    [ContextMenu("Normalize All City Markers")]
    public void NormalizeAllCityMarkers()
    {
        if (mapBackground == null || markersParent == null)
        {
            Debug.LogError("Assign Map Background and Markers Parent.");
            return;
        }

        foreach (RectTransform marker in markersParent.GetComponentsInChildren<RectTransform>())
        {
            if (marker == markersParent)
                continue;

            Vector3 worldPosition = marker.position;

            Vector3 localPosition =
                mapBackground.InverseTransformPoint(worldPosition);

            Rect rect = mapBackground.rect;

            float normalizedX =
                (localPosition.x - rect.xMin) / rect.width;

            float normalizedY =
                (localPosition.y - rect.yMin) / rect.height;

            marker.anchorMin = new Vector2(normalizedX, normalizedY);
            marker.anchorMax = new Vector2(normalizedX, normalizedY);
            marker.anchoredPosition = Vector2.zero;
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject.scene.IsValid()
            ? this
            : gameObject);
#endif

        Debug.Log("City markers normalized.");
    }
}