using UnityEngine;

public class BoxChangesColorWhenMouseOver : MonoBehaviour
{
    Color overColor;
    Color startColor;

    Renderer parentRend;

    BuildManager _BM;

    private void Start()
    {
        parentRend = transform.parent.GetComponent<Renderer>();
        overColor = transform.parent.GetComponent<BoxesScript>().overColor;
        startColor = transform.parent.GetComponent<BoxesScript>().startColor;
        _BM = GameObject.Find("Game Manager").GetComponent<BuildManager>();
    }

    private void OnMouseOver()
    {
        if (parentRend && GameManager.state == State.Build) parentRend.material.color = overColor;
    }

    private void OnMouseExit()
    {
        if (parentRend && GameManager.state == State.Build) parentRend.material.color = startColor;
    }

    private void OnDestroy()
    {
        if (parentRend && GameManager.state == State.Build) parentRend.material.color = startColor;
    }
}
