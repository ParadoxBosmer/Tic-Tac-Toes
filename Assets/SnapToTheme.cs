using UnityEngine;
using UnityEngine.UI;

public class SnapToTheme : MonoBehaviour
{
   [SerializeField] private int selectorID; // 0 for first selector, 1 for second
	
   [SerializeField] private ScrollRect scrollRect;
   [SerializeField] private RectTransform contentPanel;
   [SerializeField] private RectTransform sampleListThemes;

   [SerializeField] private HorizontalLayoutGroup HLG;
   [SerializeField] private Sprite[] themes;
   
   private bool isSnaped;
   public float snappingForce;
   private float snappingSpeed;

   void Start()
   {
      isSnaped = false;
   }

    void Update()
   {
      int currentTheme =
         Mathf.RoundToInt(0 - contentPanel.localPosition.x / (sampleListThemes.rect.width + HLG.spacing));
      Debug.Log(currentTheme);

      if (scrollRect.velocity.magnitude < 100 && !isSnaped)
      {
         scrollRect.velocity = Vector2.zero;
         snappingSpeed += snappingForce * Time.deltaTime;
         contentPanel.localPosition = new Vector3((Mathf.MoveTowards(contentPanel.localPosition.x,0 - currentTheme * (sampleListThemes.rect.width + HLG.spacing),snappingSpeed)),
            contentPanel.localPosition.y,
            contentPanel.localPosition.z);
         
         if(contentPanel.localPosition.x==(0 - currentTheme * (sampleListThemes.rect.width + HLG.spacing))) {
		    isSnaped = true;
          if (selectorID == 1) 
             DataHandler.Instance.themeIndexX = themes[currentTheme];
          else 
             DataHandler.Instance.themeIndexO = themes[currentTheme];
         }
         
      }

      if (scrollRect.velocity.magnitude > 100)
      {
         isSnaped = false;
         snappingSpeed = 0;
      }
   }
}
