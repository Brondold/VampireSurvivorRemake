using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private static ToolTip instance;

    [SerializeField]
    private Camera uiCamera;
    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();

        ShowTooltip("Random Tooltip Text");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }
    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideToolTip_Static()
    {
        instance.HideTooltip();
    }
}
