using UnityEngine;

public class UISize : MonoBehaviour
{
    [SerializeField] private float screenX;
    [SerializeField] private float screenY;
    
    [SerializeField] private float X;
    [SerializeField] private float Y;
    
    private RectTransform rectTransform;
    public bool isLeft = true;
	private float hideOffset;

	[SerializeField] private float extraSize;
	[SerializeField] private float offset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SetProportionalSize();
    }
    
    private void SetProportionalSize()
    {
        float targetWidth = (Screen.width / screenX) * X;
        float targetHeight = (Screen.height / screenY) * Y;

        if (isLeft)
        {
            rectTransform.anchorMin = new Vector2(0, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(0, rectTransform.anchorMax.y);
            rectTransform.pivot = new Vector2(0, rectTransform.pivot.y);
        }
        else
        {
            rectTransform.anchorMin = new Vector2(1, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(1, rectTransform.anchorMax.y);
            rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);
        }
        
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);

		hideOffset = targetWidth;
        if (isLeft)
        {
            rectTransform.anchoredPosition = new Vector2(-hideOffset - offset, rectTransform.anchoredPosition.y);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector2(hideOffset + offset, rectTransform.anchoredPosition.y);
        }
    }
	
	public void SetVisibilityPercent(float percent)
	{
		if (rectTransform == null)
		{
			rectTransform = GetComponent<RectTransform>();
		}

		float clamped = Mathf.Clamp(Mathf.Abs(percent), 0f, 100f);
		float t = clamped / 100f;
		float targetX;
		print($"PERCENT: {percent}");
		if (percent < 0)
		{
			targetX = isLeft
				? Mathf.Lerp(-hideOffset - offset, 0f - offset, t)
				:Mathf.Lerp(hideOffset + offset, hideOffset*2, t);
		}
		else
		{
			targetX = isLeft
				? Mathf.Lerp(-hideOffset - offset, -hideOffset*2, t)
				:Mathf.Lerp(hideOffset + offset, 0f + offset, t);
		}

		rectTransform.anchoredPosition = new Vector2(targetX, rectTransform.anchoredPosition.y);
	}
	
	public void UpdatePositionWithOffset()
	{
		if (rectTransform == null)
		{
			rectTransform = GetComponent<RectTransform>();
		}

		if (isLeft)
		{
			rectTransform.anchoredPosition = new Vector2(-hideOffset - offset, rectTransform.anchoredPosition.y);
		}
		else
		{
			rectTransform.anchoredPosition = new Vector2(hideOffset + offset, rectTransform.anchoredPosition.y);
		}
	}
}
