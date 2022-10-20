using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    private int[] weaponsDropRates;
        

    private void Start()
    {
        weaponsDropRates = weapons.Select(weapon => weapon.GetComponent<Weapon>().dropChanceWeight).ToArray();
        var weaponRNG = Random.Range(0f, 100f);
        var calculatedValueForChance = weaponsDropRates.Sum() / 100 * weaponRNG;
        var i = 0;
        for (; calculatedValueForChance - weaponsDropRates[i] > 0; i++)
            calculatedValueForChance -= weaponsDropRates[i];
        var weapon = weapons[i];
        gameObject.GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<Weapon>().weaponBox;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickedUp();
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        PickedUp();
    }

    private void PickedUp()
    {
        GameObject.Find("WeaponBoxInspector").GetComponent<WeaponBoxInspector>().PickedUpAt(transform.position);
        Destroy(gameObject);
    }
}
