using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private Material _lineMaterial;

    [SerializeField] private float _focalLength;

    [SerializeField] private Shape[] _shapesToDraw;

    [SerializeField] private Cube _drawCube;


    [SerializeField] private bool _useWireframe = false;
    private void OnPostRender()
    {
        GL.PushMatrix();
        if(_useWireframe)
            GL.Begin(GL.LINES);
        else
            GL.Begin(GL.QUADS);

        _lineMaterial.SetPass(0);


        GL.Color(_lineMaterial.color);

        DrawShapes();
        

        GL.End();
        GL.PopMatrix();
    }

    private void DrawShapes()
    {
        foreach (Shape shape in _shapesToDraw)
        {
            var shapeZ = shape.transformPosition.z;
            var actualPerspective = _focalLength / (shapeZ + _focalLength);
            //DrawLine(shape.actualPoints, actualPerspective);
        }


        if(_drawCube == null)  return;
        DrawLine(_drawCube.frontSide);
        DrawLine(_drawCube.backSide);
        DrawLine(_drawCube.leftSide);
        DrawLine(_drawCube.rightSide);


    }

    private void DrawLine(Vector3[] vector3s)
    {

        var points = vector3s;
        for (int i = 0; i < points.Length; i++)
        {
            var nextShape = (i + 1) % points.Length;
            

            var currentPoint = points[i] * (_focalLength / (points[i].z + _focalLength));
            var point1 = new Vector3(currentPoint.x, currentPoint.y, 0);

            GL.Vertex3(point1.x, point1.y, 0);


            var nextPoint = points[nextShape] * (_focalLength / (points[nextShape].z + _focalLength));
            var point2 = new Vector3(nextPoint.x, nextPoint.y, 0);

            GL.Vertex3(point2.x, point2.y, 0);

        }
    }

    private static void DebugPoint(Vector3 point)
    {
        Debug.Log($"Drawing {point.x}, {point.y}");
    }
}
