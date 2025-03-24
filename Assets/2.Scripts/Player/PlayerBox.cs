using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    [SerializeField] private PlayerBoxHpPanel playerHpPanel;
    [SerializeField] private float boxHeight;
    
    private PlayerController player;
   
    private float health;
    public float Health {
        get => health;
        set
        {
            OnHealthChanged(value);
            health = value;
        }
    }
    
    public float BoxHeight => boxHeight;

    private void OnHealthChanged(float health)
    {
        if(!playerHpPanel.gameObject.activeSelf)
            playerHpPanel.gameObject.SetActive(true);
        
        playerHpPanel.SetPanelValue(health);

        if (health <= 0)
        {
            player.onPlayerBoxDead.Invoke(this);
        }
    }

    public void SetBox(PlayerController.PlayerBoxInfo info, PlayerController player)
    {
        health = info.health;
        playerHpPanel.SetPanel(health);
        this.player = player;
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
