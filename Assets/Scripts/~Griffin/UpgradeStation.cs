using UnityEngine;

public class UpgradeStation : MonoBehaviour, IInteractable
{
    [Header("Upgrade Type - 0 = Health, 1 = MoveSpeed, 2 = Damage, 3 = Reload, 4 = Shot Upgrade")]
    [SerializeField] private int upgradeType;
    [SerializeField] private int upgradeCost;

    public void Interact()
    {
        if (PlayerStats.Instance.levels[upgradeType] < 5)
        {
            if (PlayerStats.Instance.bloodVials >= upgradeCost)
            {
                PlayerStats.Instance.levels[upgradeType] += 1;
                PlayerStats.Instance.bloodVials -= upgradeCost;
                RecalculateCost();
            }
        }
    }    

    private void RecalculateCost()
    {
        upgradeCost += Mathf.Clamp(upgradeCost * PlayerStats.Instance.levels[upgradeType], 100, 1100);
        // 100, 200, 400, 700, 1100,
    }
}
