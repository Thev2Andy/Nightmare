using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIController : MonoBehaviour
{
    public Text AmmoText;

    public static AmmoUIController Instance;

    private void Awake()
    {
       if(Instance != this) Destroy(Instance);
       Instance = this;
    }

    public void DisplayAmmo(int MagBullets, int ReserveAmmo, bool Disable)
    {
        if(Disable) {
            AmmoText.gameObject.SetActive(false);
            return;
        }

        AmmoText.gameObject.SetActive(true);
        AmmoText.text = MagBullets + " / " + ReserveAmmo;
    }

    public void DisplayCustomAmmo(string CustomAmmo, bool Disable)
    {
        if(Disable) {
            AmmoText.gameObject.SetActive(false);
            return;
        }

        AmmoText.gameObject.SetActive(true);
        AmmoText.text = CustomAmmo;
    }
}
