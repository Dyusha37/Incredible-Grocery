using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject ShoperGame;
    [SerializeField] GameObject emptySprite;
    [SerializeField] Sprite right;
    [SerializeField] Sprite notRight;
    [SerializeField] GameObject mind;
    [SerializeField] GameObject orderPrefab;
    [SerializeField] AudioSource bubbleAppered;
    [SerializeField] AudioSource bubbleDisppered;
    [SerializeField] AudioSource monyeSound;
    List<GameObject> SelectedItems;
    List<GameObject> OrderItems;
    GameObject[] sceneOrderList;
    GameObject ordered;
    GameObject sceneEmptySprite;
    ShopperScript shopper;
    Vector3 scale = new Vector3(0.7f, 0.7f, 1);
    ScoreController score;
    public void StartWork(List<GameObject> seelctedItems)
    {
        score = FindObjectOfType<ScoreController>();
        shopper = ShoperGame.GetComponent<ShopperScript>();
        OrderItems = new List<GameObject>(shopper.GetOrder());
        SelectedItems = new List<GameObject>(seelctedItems);
        SayOrder();
        StartCoroutine(Checkout());
    }

    private void SayOrder()
    {
        bubbleAppered.Play();
        ordered = Instantiate(orderPrefab, mind.transform.position, Quaternion.identity);
        sceneOrderList = new GameObject[SelectedItems.Count];
        for (int i = 0; i < SelectedItems.Count; i++)
        {
            SetOrderParent(i);
        }
        switch (SelectedItems.Count)
        {
            case 1:
                sceneOrderList[0].transform.localPosition = new Vector3(0, 0.25f, 0);
                break;
            case 2:
                sceneOrderList[0].transform.localPosition = new Vector3(-0.6f, 0.25f, 0);
                sceneOrderList[1].transform.localPosition = new Vector3(0.6f, 0.25f, 0);
                break;
            case 3:
                sceneOrderList[0].transform.localPosition = new Vector3(-0.75f, 0.25f, 0);
                sceneOrderList[1].transform.localPosition = new Vector3(0f, 0.25f, 0);
                sceneOrderList[2].transform.localPosition = new Vector3(0.75f, 0.25f, 0);
                break;
            default:
                Debug.LogError("Order Length is to long");
                break;
        }

    }
    private void SetOrderParent(int i)
    {
        sceneOrderList[i] = Instantiate(SelectedItems[i]) as GameObject;
        sceneOrderList[i].transform.parent = ordered.transform;
        sceneOrderList[i].transform.localScale = scale;
        sceneEmptySprite= Instantiate(emptySprite, sceneOrderList[i].transform) as GameObject;
        sceneEmptySprite.transform.parent = sceneOrderList[i].transform;
        sceneEmptySprite.transform.localScale = scale;
    }

    IEnumerator Checkout()
    {
        bool allRight;
        int rightCount = 0;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < SelectedItems.Count; i++)
        {
            sceneOrderList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = notRight;
            sceneOrderList[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255 / 3);
            for (int j = 0; j < OrderItems.Count; j++)
            {
                if (SelectedItems[i].GetComponent<ItemScript>().GetIndex() == OrderItems[j].GetComponent<ItemScript>().GetIndex())
                {
                    sceneOrderList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = right;
                    monyeSound.Play();
                    score.AddToScore(10);
                    rightCount++;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        allRight = OrderItems.Count == rightCount&& OrderItems.Count==SelectedItems.Count;
        bubbleDisppered.Play();
        Destroy(ordered);
        if (allRight)
        {
            monyeSound.Play();
            score.AddToScore(rightCount*10);
        }
        shopper.SetResult(allRight);
    }
}
