using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Настройки карты")]
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image icon;
    
    [Header("Настройки свайпа")]
    [SerializeField] private float swipeThreshold = 200f;
    [SerializeField] private float swipeDuration = 0.3f;
    [SerializeField] private float swipeOutDistance = 1000f;
        
    private Vector2 startPointerPosition;
    private Vector3 startCardPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CardChoice properties_yes;
    private CardChoice properties_no;
    
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

		SetProportionalSize();

        transform.localScale = Vector3.one * 0.1f;
        transform.DOScale(Vector3.one, swipeDuration).SetEase(Ease.OutBack);
    }

	private void SetProportionalSize()
	{
		float targetWidth = (Screen.width / 1080f) * 800f;
		float targetHeight = (Screen.height / 1920f) * 1200f;
        float posY = -(targetHeight/4);

        rectTransform.anchoredPosition = new Vector3(0, posY, 0);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
	}

    public void Initialize(CardJson cardJson)
    {
        name.text = cardJson.name;
        description.text = cardJson.description;
        properties_yes = cardJson.properties_yes;
        properties_no = cardJson.properties_no;
        icon.sprite = Resources.Load<Sprite>("Images/"+cardJson.image) ?? Resources.Load<Sprite>("Images/no_image");
        
        UIController.instance.leftDescription.GetComponent<TextMeshProUGUI>().text = cardJson.properties_no.text;
        UIController.instance.rightDescription.GetComponent<TextMeshProUGUI>().text = cardJson.properties_yes.text;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPointerPosition = eventData.position;
        startCardPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - startPointerPosition;
        rectTransform.anchoredPosition = startCardPosition + new Vector3(delta.x / canvas.scaleFactor, 0, 0);

        float rotationZ = Mathf.Clamp(delta.x / 20f, -15f, 15f);
        rectTransform.rotation = Quaternion.Euler(0, 0, rotationZ);
        if (delta.x < 0)
        {
            UIController.instance.rightBubble.SetVisibilityPercent(delta.x);
            UIController.instance.leftBubble.SetVisibilityPercent(delta.x);
            
        }
        else if (delta.x > 0)
        {
            UIController.instance.leftBubble.SetVisibilityPercent(delta.x);
            UIController.instance.rightBubble.SetVisibilityPercent(delta.x);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - startPointerPosition;
        UIController.instance.leftBubble.SetVisibilityPercent(0);
        UIController.instance.rightBubble.SetVisibilityPercent(0);
            
        if (Mathf.Abs(delta.x) > swipeThreshold)
        {
            float direction = Mathf.Sign(delta.x);
            rectTransform 
                .DOAnchorPosX(rectTransform.anchoredPosition.x + direction * swipeOutDistance, swipeDuration)
                .SetEase(Ease.InBack);

            rectTransform
                .DORotate(new Vector3(0, 0, direction * 30f), swipeDuration);
            
            DOVirtual.DelayedCall(swipeDuration, () =>
            {
                EventBusHolder.instance.eventBus.Raise(new OnDestroyCardEvent((direction > 0f) ? properties_yes : properties_no));
                Destroy(gameObject);
            });
        }
        else
        {
            rectTransform
                .DOAnchorPos(startCardPosition, swipeDuration)
                .SetEase(Ease.OutQuad);

            rectTransform
                .DORotate(Vector3.zero, swipeDuration);
        }
    }
}
