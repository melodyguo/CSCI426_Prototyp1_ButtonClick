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
    [SerializeField] GameObject addPowerupGO;
    [SerializeField] GameObject powerPowerupGO;
    [SerializeField] GameObject multiplyPowerupGO;
    [SerializeField] TMP_Text addPowerup;
    [SerializeField] TMP_Text powerPowerup;
    [SerializeField] TMP_Text multiplyPowerup;
    [SerializeField] Animator addAnimator;
    [SerializeField] Animator powerAnimator;
    [SerializeField] Animator multiplyAnimator;

    [Header("Sound")]
    [SerializeField] AudioSource powerupSound;


    // Start is called before the first frame update
    void Start()
    {
        addPowerupGO.SetActive(false);
        powerPowerupGO.SetActive(false);
        multiplyPowerupGO.SetActive(false);
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
        if (!addPowerupGO.activeSelf)
            addPowerupGO.SetActive(true);

        add += num;
        addPowerup.text = "+" + add.ToString();
        addAnimator.SetTrigger("PowerupObtained");
        powerupSound.Play();
    }

    public void ApplyPowerPowerup(float num)
    {
        if (!powerPowerupGO.activeSelf)
            powerPowerupGO.SetActive(true);

        power += num;
        powerPowerup.text = "^" + power.ToString();
        powerAnimator.SetTrigger("PowerupObtained");
        powerupSound.Play();
    }

    public void ApplyMultiplyPowerup(float num)
    {
        if (!multiplyPowerupGO.activeSelf)
            multiplyPowerupGO.SetActive(true);

        multiply += num;
        multiplyPowerup.text = "x" + multiply.ToString();
        multiplyAnimator.SetTrigger("PowerupObtained");
        powerupSound.Play();
    }
}
