using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cube", menuName = "Cube")]
public class Cube : ScriptableObject
{
    #region Public Variables
    public float length;
    public float height;
    public float width;

    public Vector3 transformPosition;
    public Vector3 transformRotation;

    public Vector3[] frontSide
    {
        get
        {

            //return _frontSide;
            //z axis rotation
            var currentSide = new Vector3[_frontSide.Length];
            for (int i = 0; i < _frontSide.Length; i++)
            {
                var deductedVector = transformPosition - _frontSide[i];
                var rotatedVector = RotateBy(transformRotation.z, deductedVector.x, deductedVector.y);
                currentSide[i] = new Vector3(rotatedVector.x, rotatedVector.y, deductedVector.z) + transformPosition;
            }

            return currentSide;
        }
    }


    public Vector3[] backSide
    {
        get
        {

            //return _backSide;
            var currentSide = new Vector3[_backSide.Length];
            for (int i = 0; i < _backSide.Length; i++)
            {
                var deductedVector = transformPosition - _backSide[i];
                var rotatedVector = RotateBy(transformRotation.z, deductedVector.x, deductedVector.y);
                currentSide[i] = new Vector3(rotatedVector.x, rotatedVector.y, deductedVector.z) + transformPosition;
            }

            return currentSide;
        }
    }

    public Vector3[] leftSide
    {
        get
        {
            return new Vector3[]
            {
                frontSide[0], frontSide[1], backSide[1], backSide[0]
            };
        }
    }

    public Vector3[] rightSide
    {
        get
        {
            return new Vector3[]
            {
                frontSide[2], frontSide[3], backSide[3], backSide[2]
            };
        }
    }
    #endregion

    private Vector3[] _frontSide
    {
        get
        {
            return new Vector3[]
            {
                new Vector3(transformPosition.x +(width/2), transformPosition.y +(height/2), transformPosition.z + (length/2)),
                new Vector3(transformPosition.x +(width/2), transformPosition.y -(height/2), transformPosition.z + (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y -(height/2), transformPosition.z + (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y +(height/2), transformPosition.z + (length/2))
            };
        }
    }


    private  Vector3[] _backSide
    {
        get
        {
            return new Vector3[]
            {
                new Vector3(transformPosition.x +(width/2), transformPosition.y +(height/2), transformPosition.z - (length/2)),
                new Vector3(transformPosition.x +(width/2), transformPosition.y -(height/2), transformPosition.z - (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y -(height/2), transformPosition.z - (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y +(height/2), transformPosition.z - (length/2))
            };
        }
    }

    

    private Vector2 RotateBy(float angle, float point1, float point2)
    {
        var cos = Mathf.Cos(angle);
        var sin = Mathf.Sin(angle);

        var a = point1 * cos - point2 * sin;
        var b = point2 * cos + point1 * sin;
        return new Vector2(a, b);
    }
}
