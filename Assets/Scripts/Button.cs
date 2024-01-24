using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour
{

    [SerializeField] public float maxHealth = 5;
    [SerializeField] float healRate = 0.10f;
    [SerializeField] Player player;

    [Header("HealthBar")]
    [SerializeField] public HealthBar healthBar;
    [SerializeField] Animator healthBarAnimator;
    [SerializeField] string healthBarPulseAnim;

    [Header("VFX")]
    [SerializeField] GameObject FloatingTextPrefab;
    [SerializeField] GameObject JellySplatterPrefab;
    [SerializeField] float hitStopDuration = 0.5f;

    [Header("SFX")]
    [SerializeField] AudioSource pressSound;
    [SerializeField] AudioSource breakSound;

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
    { 
        // get inputs
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PressButton();
        }

        // health bar auto-healing
        healthBar.SetHealth(healthBar.GetHealth() + Time.deltaTime * healRate * maxHealth);
    }

    private void PressButton()
    {
        // particle effects
        if (JellySplatterPrefab != null)
        {
            SpawnJellySplatter();
        }
        if (FloatingTextPrefab != null)
        {
            SpawnFloatingText();
        }

        // sound effects
        pressSound.Play();

        // take damage
        healthBar.SetHealth(healthBar.GetHealth() - player.GetHitPoints());
        healthBarAnimator.SetTrigger("TookDamage");

        // button died--time to respawn
        if (healthBar.GetHealth() <= 0)
        {
            // hit stop
            breakSound.Play();
            FindObjectOfType<HitStop>().Stop(hitStopDuration);

            // increment round
            round++;
            Debug.Log(round);

            // button level up!
            if (round % 5 == 0)
            {
                maxHealth *= 2;
            }

            healthBar.ResetToMax(maxHealth);

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
}
