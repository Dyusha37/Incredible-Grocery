using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
    [SerializeField] GameObject selected;
    GameObject selectedShow; 
    ShopperScript shopper;
    Controller controller;
    bool notSelected = true;
    private void Start()
    {
        shopper = FindObjectOfType<ShopperScript>();
        controller = FindObjectOfType<Controller>();
    }
    public void Add(GameObject item)
    {
        if (notSelected)
        {
            if (controller.GetSelectedCount() < shopper.GetOrder().Count)
            {
                controller.AddToSelected(item, gameObject);
                GetComponent<Image>().color = new Color32(255, 255, 255, 255 / 3);
                selectedShow = Instantiate(selected, gameObject.transform) as GameObject;
                selectedShow.transform.parent = gameObject.transform;
                notSelected = false;
            }
        }
        else
        {
            controller.RemoveFromSelected(item);
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Destroy(selectedShow);
            notSelected = true;
        }
    }
}
