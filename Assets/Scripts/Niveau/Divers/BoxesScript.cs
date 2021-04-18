using UnityEngine;

public class BoxesScript : MonoBehaviour
{
    public Color overColor;
    public Vector3 positionOffset;
    public Color startColor;

    private Renderer rend;
    GameManager _GM;
    BuildManager _BM;

    [SerializeField] Vector3 offset;

    [HideInInspector] public int boxIndex;

    void Start()
    {
        GameObject gameManagerGO = GameObject.Find("Game Manager");
        _GM = gameManagerGO.GetComponent<GameManager>();
        _BM = gameManagerGO.GetComponent<BuildManager>();

        //Mémorisation de l'index de la case
        boxIndex = transform.GetSiblingIndex();

        //Mémorise la couleur de la case pour pouvoir la recolorier de la bonne couleur quand la souris ne survole plus cette case
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void Update()
    {
        //Destruction d'objets
        if(GameManager.state == State.Build)
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(1)) //Quand on fait un clique droit sur une case
            {
                int layer = hit.transform.gameObject.layer;
                Transform tr = null;

                if (layer == 8)                                              //clique sur la case
                    tr = hit.transform.parent;
                else if ((layer == 9) && (hit.transform.childCount == 1))    //clique sur l'objet qui est sur la case
                    tr = hit.transform;

                if (tr != null) _BM.RemoveObjectsFromBox(tr);
            }
        }
    }

    //Changement couleur case quand la souris la survole
    private void OnMouseOver()
    {
        if(GameManager.state == State.Build) rend.material.color = overColor;
    }

    //Case reprend sa couleur originale quand la souris ne la survole plus
    private void OnMouseExit()
    {
        if (GameManager.state == State.Build) rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        //Création d'objet
        if (GameManager.state == State.Build)
        {
            if (Input.GetMouseButtonDown(0)) _BM.CreateObject(UI_Manager.selectedObject, transform);//_BM.CreateObject(transform, boxIndex, -1);
        }
    }
}

