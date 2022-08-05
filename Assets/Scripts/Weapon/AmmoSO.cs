using UnityEngine;

[CreateAssetMenu(menuName ="AmmoAmount",fileName = "Ammo/AmmoAmount")]
public class AmmoSO : ScriptableObject
{
    public int currentAmmo;
    public int maxAmmo;
    public int magazineSize;
}
