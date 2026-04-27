using System;
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
   
   private bool _isSnaped;
   public float snappingForce;
   private float _snappingSpeed;

   void Start()
   {
      _isSnaped = false;
   }

    void Update()
   {
      int currentTheme =
         Mathf.RoundToInt(0 - contentPanel.localPosition.x / (sampleListThemes.rect.width + HLG.spacing));
      Debug.Log(currentTheme);

      if (scrollRect.velocity.magnitude < 100 && !_isSnaped)
      {
         scrollRect.velocity = Vector2.zero;
         _snappingSpeed += snappingForce * Time.deltaTime;
         var localPosition = contentPanel.localPosition;
         localPosition = new Vector3((Mathf.MoveTowards(localPosition.x,0 - currentTheme * (sampleListThemes.rect.width + HLG.spacing),_snappingSpeed)),
            localPosition.y,
            localPosition.z);
         contentPanel.localPosition = localPosition;

         if(Math.Abs(contentPanel.localPosition.x - (0 - currentTheme * (sampleListThemes.rect.width + HLG.spacing))) < 3) {
		    _isSnaped = true;
          if (selectorID == 1) 
             DataHandler.Instance.themeIndexX = themes[currentTheme];
          else 
             DataHandler.Instance.themeIndexO = themes[currentTheme];
         }
         
      }

      if (scrollRect.velocity.magnitude > 100)
      {
         _isSnaped = false;
         _snappingSpeed = 0;
      }
   }
}
