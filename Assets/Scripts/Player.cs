using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Hit Points")]
    private float damage = 1;
    private float add = 0;
    private float power = 1;
    private float multiply = 1;

    [Header("Powerup UI")]
    [SerializeField] TMP_Text addPowerup;
    [SerializeField] TMP_Text powerPowerup;
    [SerializeField] TMP_Text multiplyPowerup;
    [SerializeField] Animator addAnimator;
    [SerializeField] Animator powerAnimator;
    [SerializeField] Animator multiplyAnimator;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ApplyAddPowerup(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2)) 
        {
            ApplyPowerPowerup(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ApplyMultiplyPowerup(1);
        }
    }

    public float GetHitPoints() 
    {
        return Mathf.Pow((damage + add), power) * multiply; 
    }

    public void ApplyAddPowerup(float num)
    {
        add += num;
        addPowerup.text = "+" + add.ToString();
        addAnimator.SetTrigger("PowerupObtained");
    }

    public void ApplyPowerPowerup(float num)
    {
        power += num;
        powerPowerup.text = "^" + power.ToString();
        powerAnimator.SetTrigger("PowerupObtained");
    }

    public void ApplyMultiplyPowerup(float num)
    {
        multiply += num;
        multiplyPowerup.text = "x" + multiply.ToString();
        multiplyAnimator.SetTrigger("PowerupObtained");
    }
}
