using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


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
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) //Icon over the node
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());

    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough money to build!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        Debug.Log("Turret built ! Money left :  " + PlayerStats.Money);
    }


    public void UpdateTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to UPGRADE it !");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //get rid of the OLD ONE 
        Destroy(turret);

        //Creat  NEW  turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        isUpgraded = true;

        Debug.Log("Turret UPGRADED ! Money left :  " + PlayerStats.Money);
    }
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAnount();

        //detroy effect later

        Destroy(turret);
        turretBlueprint = null;

        Debug.Log("Turret SELLED ! Money left :  " + PlayerStats.Money);
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
