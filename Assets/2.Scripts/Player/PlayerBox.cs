using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    [SerializeField] private PlayerBoxHpPanel playerHpPanel;
   
    private float health;
    public float Health {
        get => health;
        set
        {
            OnHealthChanged(value);
            health = value;
        }
    }

    private void OnHealthChanged(float health)
    {
        if(!playerHpPanel.gameObject.activeSelf)
            playerHpPanel.gameObject.SetActive(true);
        
        playerHpPanel.SetPanelValue(health);

        if (health <= 0)
        {
            Debug.Log("dead");
        }
    }

    public void SetBox(PlayerController.PlayerBoxInfo info)
    {
        health = info.health;
        playerHpPanel.SetPanel(health);
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
