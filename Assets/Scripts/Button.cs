using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour
{

    [SerializeField] float maxHealth = 5;
    [SerializeField] float healRate = 0.10f;
    [SerializeField] Player player;

    [Header("HealthBar")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] Animator healthBarAnimator;
    [SerializeField] string healthBarPulseAnim;

    [Header("VFX")]
    [SerializeField] GameObject FloatingTextPrefab;
    [SerializeField] GameObject JellySplatterPrefab;
    [SerializeField] float hitStopDuration = 0.5f;

    private float round = 1;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        healthBar.ResetToMax(maxHealth);

    }

    // Update is called once per frame
    void Update()

        // button died--load next button
    {   if (healthBar.GetHealth() <= 0)
        {
            round++;
            Debug.Log(round);

            // button level up!
            if (round%5 == 0)
            {
                maxHealth *= 2;
            }

            healthBar.ResetToMax(maxHealth);

            return;
        }

        // health bar auto-healing
        healthBar.SetHealth(healthBar.GetHealth() + Time.deltaTime * healRate * maxHealth);
    }



    private void OnMouseDown()
    {
        // for visualization -- remove later
        renderer.material.color = Color.red;


        // play splatter particle effect
        if (JellySplatterPrefab != null)
        {
            SpawnJellySplatter();
        }

        // take damage
        healthBar.SetHealth(healthBar.GetHealth() - player.GetHitPoints());

        healthBarAnimator.SetTrigger("TookDamage");

        if (healthBar.GetHealth() <= 0)
        {
            FindObjectOfType<HitStop>().Stop(hitStopDuration);
            renderer.material.color = Color.blue;
            Debug.Log("TIME TO RESPAWN");
        }

        // play damange number effect
        if (FloatingTextPrefab != null)
        {
            SpawnFloatingText();
        }
    }

    private void SpawnFloatingText()
    {
        GameObject go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TMP_Text>().SetText(player.GetHitPoints().ToString());
    }

    private void SpawnJellySplatter()
    {
        Instantiate(JellySplatterPrefab, transform.position, Quaternion.identity, transform);
    }

    private void OnMouseUp()
    {
        // for visualization -- remove later
        renderer.material.color = Color.white;
    }
}
