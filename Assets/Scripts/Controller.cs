using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] Button sellButton;
    [SerializeField] AudioSource itemSelected;
    [SerializeField] AudioSource buttonClick;
    [SerializeField] List<GameObject> Menu;
    List<GameObject> SelectedItems = new List<GameObject>();
    List<GameObject> SelectedButtons = new List<GameObject>();
    ShopperScript shopper;
    void Start()
    {
        shopper = FindObjectOfType<ShopperScript>();
    }

    public List<GameObject> GetMenu()
    {
        return Menu;
    }
    public void AddToSelected(GameObject item, GameObject button)
    {
        itemSelected.Play();
        SelectedItems.Add(item);
        SelectedButtons.Add(button);
        if (SelectedItems.Count == shopper.GetOrder().Count)
        {
            SetActiveButton();
        }
    }
    public void RemoveFromSelected(GameObject item)
    {
        SelectedItems.Remove(item);
        SetUnactiveButton();
    }
    public void Sell()
    {
        buttonClick.Play();
        FindObjectOfType<PlayerScript>().StartWork(SelectedItems);
        FindObjectOfType<Selector>().MoveFromScreen();
        SelectedItems.Clear();
        foreach (var item in SelectedButtons)
        {
            item.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Destroy(item.transform.GetChild(0).gameObject);
        }
        SelectedButtons.Clear();
        SetUnactiveButton();

    }
    public int GetSelectedCount()
    {
        return SelectedItems.Count;
    }
    private void SetActiveButton()
    {
        sellButton.interactable = true;
    }
    private void SetUnactiveButton()
    {
        sellButton.interactable = false;
    }
}
