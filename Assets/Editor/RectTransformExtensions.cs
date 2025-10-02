using UnityEngine;
using UnityEditor;

public static class RectTransformExtensions
{
    [MenuItem("Tools/UI/Set Anchors To Rect %#c")]
    private static void SetAnchorsToRect()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            if (rectTransform == null || rectTransform.parent == null)
                continue;

            RectTransform parent = rectTransform.parent as RectTransform;
            Undo.RecordObject(rectTransform, "Set Anchors To Rect");

            Vector2 newAnchorMin = new Vector2(
                rectTransform.anchorMin.x + rectTransform.offsetMin.x / parent.rect.width,
                rectTransform.anchorMin.y + rectTransform.offsetMin.y / parent.rect.height);

            Vector2 newAnchorMax = new Vector2(
                rectTransform.anchorMax.x + rectTransform.offsetMax.x / parent.rect.width,
                rectTransform.anchorMax.y + rectTransform.offsetMax.y / parent.rect.height);

            rectTransform.anchorMin = newAnchorMin;
            rectTransform.anchorMax = newAnchorMax;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
    }

    [MenuItem("CONTEXT/RectTransform/Set Anchors To Rect")]
    private static void SetAnchorsToRectContext(MenuCommand command)
    {
        RectTransform rectTransform = (RectTransform)command.context;
        RectTransform parent = rectTransform.parent as RectTransform;

        if (parent == null)
            return;

        Undo.RecordObject(rectTransform, "Set Anchors To Rect");

        Vector2 newAnchorMin = new Vector2(
            rectTransform.anchorMin.x + rectTransform.offsetMin.x / parent.rect.width,
            rectTransform.anchorMin.y + rectTransform.offsetMin.y / parent.rect.height);

        Vector2 newAnchorMax = new Vector2(
            rectTransform.anchorMax.x + rectTransform.offsetMax.x / parent.rect.width,
            rectTransform.anchorMax.y + rectTransform.offsetMax.y / parent.rect.height);

        rectTransform.anchorMin = newAnchorMin;
        rectTransform.anchorMax = newAnchorMax;

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
