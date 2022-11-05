using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleTapCheck : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private TitleManager selectpad;
    // Start is called before the first frame update
    void Awake()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectpad.getTitleType() == TITLE_TYPE.NORMAL)
        {
            selectpad.gameObject.SetActive(true);
            selectpad.sliderYPage();
            
           // this.gameObject.SetActive(false);
        }
    }
}
