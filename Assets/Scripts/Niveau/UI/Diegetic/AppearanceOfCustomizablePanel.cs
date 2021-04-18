using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectBlinking))]
[RequireComponent(typeof(CustomizeObjectLife))]
public class AppearanceOfCustomizablePanel : MonoBehaviour
{
    GameObject customisationPanel;
    Vector2 panelAnchorPos;
    [HideInInspector] public GameObject go;


    private void Awake()
    {
        customisationPanel = GameObject.Find("CustomisationPanel");
        panelAnchorPos = GameObject.Find("CustomisationPanelAnchor").transform.position;

        //Si le customisationPanel est présent, on le cache quand on créé un nouvel objet
        HideCustomisationPanel();
    }
    private void OnMouseDown()
    {
        //On ne veut que le panel de customisation soit disponible que en mode 'Build'
        if(GameManager.state == State.Build)
        {
            if (customisationPanel != null && panelAnchorPos != null)
            {
                customisationPanel.transform.position = panelAnchorPos;
                customisationPanel.GetComponent<LinkedGameObject>().linkedGameObject = gameObject;

                GameObject.Find("Cancel_btn").GetComponent<Button>().interactable = gameObject.GetComponent<ObjectBlinking>().isActiveAndEnabled || gameObject.GetComponent<CustomizeObjectLife>().isActiveAndEnabled;
            }
        }
    }

    private void OnDestroy()
    {
        //Si le customisationPanel a été activée pour cet object, on le désactive quand cet objet est détruit
        if (customisationPanel && customisationPanel.GetComponent<LinkedGameObject>())
        {
            if (customisationPanel.GetComponent<LinkedGameObject>().linkedGameObject == this.gameObject) HideCustomisationPanel();
        }
    }

    void HideCustomisationPanel()
    {
        if (customisationPanel.GetComponent<RepositionCustomisationPanel>()) customisationPanel.GetComponent<RepositionCustomisationPanel>().RepositionPanel();
    }
}
