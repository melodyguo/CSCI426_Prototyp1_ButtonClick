using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

    [SerializeField] float maxHealth = 20;
    [SerializeField] float healRate = 0.10f;
    [SerializeField] HealthBar healthBar;
    [SerializeField] Player player;
    
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
        healthBar.SetHealth(healthBar.GetHealth() + Time.deltaTime * healRate * maxHealth);
    }



    private void OnMouseDown()
    {
        // for visualization -- remove later
        renderer.material.color = Color.red;


        // decrease button HP
        healthBar.SetHealth(healthBar.GetHealth() - player.GetHitPoints());

        // play button animation

        // chance of spawning powerup box
    }
    private void OnMouseUp()
    {
        // for visualization -- remove later
        renderer.material.color = Color.white;
    }
}
