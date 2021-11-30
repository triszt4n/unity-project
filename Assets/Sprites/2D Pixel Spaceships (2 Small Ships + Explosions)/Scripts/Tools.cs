using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallShips
{
    public class Tools {
        public static void TraceTarget(Transform workObject, Vector3 targetPos, float deltaAngle)
        {
            if (workObject == null)
                return;

            Vector3 direct = workObject.position - targetPos;

            direct = new Vector3(direct.x, direct.y, 0.0f);

            float angle = Vector3.Angle(Vector3.up, direct);
            if ((targetPos.x - workObject.TransformPoint(Vector3.zero).x) < 0)
                angle = 360.0f - angle;
            workObject.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Round(angle / (float)deltaAngle) * (float)deltaAngle);
        }
    }

}
