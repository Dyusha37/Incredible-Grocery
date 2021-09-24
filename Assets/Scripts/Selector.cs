using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    [SerializeField] float speed = 1;
    private Vector3 startPos;
    private Vector3 endPos;
    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.right * 1000;
        transform.position = endPos;
    }
    public void ShowOnScreen()
    {
        StartCoroutine("Enter");
    }
    IEnumerator Enter()
    {
        for (float i = 0; i < 1; i+=Time.deltaTime*speed)
        {
            transform.position = Vector3.Lerp(endPos, startPos, i);
            yield return null;
        }
        transform.position = startPos;
    }

    public void MoveFromScreen()
    {
        StartCoroutine("Exit");
    }
    IEnumerator Exit()
    {
        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        transform.position = endPos;
    }

}
