using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperScript : MonoBehaviour
{
    [SerializeField] GameObject orderTransform;
    [SerializeField] GameObject orderPrefab;
    [SerializeField] GameObject goodResult;
    [SerializeField] GameObject badResult;
    [SerializeField] AudioSource bubbleAppered;
    [SerializeField] AudioSource bubbleDisppered;
    GameObject result;
    GameObject orderInstantiated;
    List<GameObject> orderList;
    GameObject[] scaneOrderArray;
    Animator animator;

    Controller controller;
    Selector selector;

    Vector3 scale = new Vector3(0.7f, 0.7f, 1);
    int orderLenght;
    IEnumerator Start()
    {
        selector = FindObjectOfType<Selector>();
        controller = FindObjectOfType<Controller>();
        animator =GetComponent<Animator>();
        yield return new WaitForSeconds(1f);
    }
    public void MakeOrder()
    {
        orderInstantiated = Instantiate(orderPrefab,orderTransform.transform.position,Quaternion.identity);
        bubbleAppered.Play();
        CreateOrder();
    }
    private void CreateOrder()
    {
        int deleteProduct;
        orderLenght = Random.Range(1,4);
        orderList = new List<GameObject>(controller.GetMenu());
        int toDelete = orderList.Count - orderLenght;
        for (int i = 0; i < toDelete; i++)
        {
            deleteProduct = Random.Range(0, orderList.Count);
            orderList.RemoveAt(deleteProduct);
        }
        SayOrder();
    }

    private void SayOrder()
    {
        scaneOrderArray = new GameObject[orderLenght];
        for (int i = 0; i < orderLenght; i++)
        {
            scaneOrderArray[i] = Instantiate(orderList[i]) as GameObject;
            SetOrderParent(scaneOrderArray[i]);
        }
        switch (orderLenght)
        {
            case 1:
                scaneOrderArray[0].transform.localPosition = new Vector3(0, 0.25f, 0);
                break;
            case 2:
                scaneOrderArray[0].transform.localPosition = new Vector3(-0.6f, 0.25f, 0);
                scaneOrderArray[1].transform.localPosition = new Vector3(0.6f, 0.25f, 0);
                break;
            case 3:
                scaneOrderArray[0].transform.localPosition = new Vector3(-0.75f, 0.25f, 0);
                scaneOrderArray[1].transform.localPosition = new Vector3(0f, 0.25f, 0);
                scaneOrderArray[2].transform.localPosition = new Vector3(0.75f, 0.25f, 0);
                break;
            default:
                Debug.LogError("Order Length is to long");
                break;
        }
        StartCoroutine("CleanOrdered");

    }
    private void SetOrderParent(GameObject item)
    {
        item.transform.parent = orderInstantiated.transform;
        item.transform.localScale = scale;
    }
    public List<GameObject> GetOrder()
    {
        return orderList;
    }
    public void SetResult(bool res)
    {
        bubbleAppered.Play();
        orderInstantiated.SetActive(true);
        if (res)
        {
            result = Instantiate(goodResult) as GameObject;
        }
        else
        {
            result = Instantiate(badResult) as GameObject;
        }
        SetOrderParent(result);
        result.transform.localPosition = new Vector3(0, 0.25f, 0);
        StartCoroutine(Exit());
    }
    IEnumerator CleanOrdered()
    {
        yield return new WaitForSeconds(5f); 
        foreach (var item in scaneOrderArray)
        {
            Destroy(item);
        }
        bubbleDisppered.Play();
        orderInstantiated.SetActive(false);
        selector.ShowOnScreen();
    }
    IEnumerator Exit()
    {
        yield return new WaitForSeconds(1f); 
        animator.SetTrigger("Exit");
    }
    public void DestroyOrdered()
    {
        bubbleDisppered.Play();
        Destroy(orderInstantiated);
    }
}
