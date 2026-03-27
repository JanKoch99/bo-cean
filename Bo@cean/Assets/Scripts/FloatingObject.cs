using UnityEngine;

public class FloatingObject : MonoBehaviour
{
   public float speedMultiplier = 1f;

   protected virtual void Update()
   {
      if (WaterManager.Instance == null)
      {
         return;
      }

      float speed = WaterManager.Instance.waterSpeed;
      Vector3 flowDirection = WaterManager.Instance.flowDirection;
      transform.position += flowDirection * speed * speedMultiplier * Time.deltaTime;
   }
}
