using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
   public LayerMask layerMask;

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log(other.gameObject.name + " Triggered");
      if(layerMask == (layerMask | (1 << other.gameObject.layer)))
      {
       //  Debug.Log(other.gameObject.name + " Triggered");
         
        // Debug.Log(other.gameObject.name);
         var hurtBox = other.transform.GetComponent<HurtBox>();
         if (hurtBox != null)
         {
            hurtBox.TakeHit();
         }
      }
   }

   public void OnCollisionEnter2D(Collision2D other)
   {
      Debug.Log(other.gameObject.name + " collision");
      if(layerMask == (layerMask | (1 << other.gameObject.layer)))
      {
       //  Debug.Log(other.gameObject.name + " collision");
         var hurtBox = other.transform.GetComponent<HurtBox>();
         if (hurtBox != null)
         {
            hurtBox.TakeHit();
         }
      }
   }
}
