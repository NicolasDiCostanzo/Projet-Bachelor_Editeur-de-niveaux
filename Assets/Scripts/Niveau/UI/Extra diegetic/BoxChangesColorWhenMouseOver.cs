using UnityEngine;

public class BoxChangesColorWhenMouseOver : MonoBehaviour
{
    Color overColor;
    Color startColor;

    Renderer parentRend;

    private void Start()
    {
        parentRend = transform.parent.GetComponent<Renderer>();
        overColor = transform.parent.GetComponent<BoxesScript>().overColor;
        startColor = transform.parent.GetComponent<BoxesScript>().startColor;
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
