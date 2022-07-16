using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;


    [Header("Optional")]
    public GameObject turret;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuiltPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //Icon over the node
        {
            return;
        }
        if (! buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("CANT  BUILD HERE");
            return;
        }
        /*
        //Build  a  turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        positionOffset.y = 0.5f;
        turret = (GameObject)Instantiate(turretToBuild, transform.position+positionOffset, transform.rotation);
         */
        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter()
    {


        if (EventSystem.current.IsPointerOverGameObject()) //Icon over the node
            return;
        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMoneyColor;
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
