using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    private TurretBlueprint turretToBuild;

    public bool CanBuild {  get  {  return turretToBuild != null;}}
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
                Debug.Log("Not Enough money to build!");
                return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret=(GameObject)Instantiate(turretToBuild.prefab, node.GetBuiltPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret built ! Money left :  " + PlayerStats.Money);
    }

    public void  SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}