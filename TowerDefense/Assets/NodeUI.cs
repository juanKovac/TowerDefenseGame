using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject ui;

    public Text upgradedCost;
    public Button upgradeButton;

    public Text sellAmount;
    public Button sellButton;

    private Node target;
    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuiltPosition();

        if (!target.isUpgraded)
        {
            upgradedCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradedCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAnount();

        ui.SetActive(true);
    }
    
    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpdateTurret();
        BuildManager.instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
