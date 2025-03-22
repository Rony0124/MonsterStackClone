using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
   public LayerMask layerMask;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if(layerMask == (layerMask | (1 << other.gameObject.layer)))
      {
         var hurtBox = other.transform.GetComponent<HurtBox>();
         if (hurtBox != null)
         {
            hurtBox.TakeHit();
         }
      }
   }
}
