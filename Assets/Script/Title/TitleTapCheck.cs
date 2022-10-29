using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleTapCheck : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private GameObject selectpad;
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
        if (!selectpad.activeSelf)
        {
            selectpad.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
