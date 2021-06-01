using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelection : MonoBehaviour
{
    BuildManager _BM;

    public List<string> textes = new List<string>();

    private void Start()
    {
        _BM = GameObject.Find("Game Manager").GetComponent<BuildManager>();
    }

    public void SelectObject(int rankOfSelectedObject)
    {
        Debug.Log("select object");
        _BM.selectedObject = rankOfSelectedObject;
        HighlightSelectedObject(rankOfSelectedObject);
    }

    void HighlightSelectedObject(int a_rank)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == a_rank) transform.GetChild(i).GetComponent<Outline>().enabled = true;
            else transform.GetChild(i).GetComponent<Outline>().enabled = false;
        }
    }
}
