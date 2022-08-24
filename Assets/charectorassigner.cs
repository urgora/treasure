using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charectorassigner : MonoBehaviour
{
    public GameObject[] child;
    private void Awake()
    {
        int x = Random.Range(0, child.Length);
        child[x].transform.SetAsFirstSibling();
        child[x].SetActive(true);
    }
}
