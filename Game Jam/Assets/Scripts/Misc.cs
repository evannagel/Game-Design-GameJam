using UnityEngine;

public class Misc {
    

    public static bool IsObjectInFrontOfOther(Transform transform, Transform objectInFront)
    {

        Vector3 dir = transform.position - objectInFront.position;
        float angle = Vector3.Angle(transform.forward, transform.position - objectInFront.position);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            return true;

        return false;
    }

    public static bool IsObjectBehindOther(Transform transform, Transform objectInFront)
    {

        Vector3 dir = transform.position - objectInFront.position;
        float angle = Vector3.Angle(transform.forward, transform.position - objectInFront.position);

        if (Mathf.Abs(angle) < 90 && Mathf.Abs(angle) > 270)
            return true;

        return false;
    }

    //Need to look at this, maybe colliders
     public static bool IsInLineOfSight(Transform transform, Transform objectToLOS, int distance)
     {
         return IsObjectInFrontOfOther(transform, objectToLOS);

         /*if(isInFront)
         {
             RaycastHit hit;
             if(Physics.Raycast(transform.position, transform.right, out hit, distance))
             {
                 Debug.DrawRay(transform.position, -objectToLOS.transform.position * hit.distance, Color.yellow);
                 if(hit.transform == objectToLOS)
                 {
                     return true;
                 }
             }
         }

         return false;*/
     }

   
}
