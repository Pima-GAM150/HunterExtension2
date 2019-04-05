using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class ObjectResizer : EditorWindow
{
    private GameObject selectedObject;

    private string height;

    private float xScale;
    private float yScale;
    private float zScale;

    private bool scale;

    [MenuItem("Window/Object Resizer")]
    static void Init()
    {
        ObjectResizer window = (ObjectResizer)EditorWindow.GetWindow(typeof(ObjectResizer));
        window.Show();
    }

    void OnGUI()
    {
        selectedObject =  Selection.activeGameObject;
        if (selectedObject)
        {
            getScale();
            getHeight();
        }
        else
        {
            height = null;
        }

        height = EditorGUILayout.TextField("Object Height", height);
        scale = EditorGUILayout.Toggle("Scale", scale);
    }

    private void getHeight()
    {
        float heightToTransform;
        float fHeight;
        float.TryParse(height, out fHeight);

        if (height == null) height = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();

        else if(System.Math.Round(fHeight,3) != System.Math.Round((selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale),3))
        {
            heightToTransform = fHeight / (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale);
            if (scale) selectedObject.GetComponent<Transform>().localScale *= heightToTransform;
            else
            {
                if (heightToTransform > 1) selectedObject.GetComponent<Transform>().localScale += new Vector3(0f, (heightToTransform - 1)*yScale, 0f);
                else selectedObject.GetComponent<Transform>().localScale -= new Vector3(0f, (1 - heightToTransform) * yScale, 0f);
            }    
            getScale();
            height = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();
        }
    }

    private void getScale()
    {
        xScale = selectedObject.GetComponent<Transform>().localScale.x;
        yScale = selectedObject.GetComponent<Transform>().localScale.y;
        zScale = selectedObject.GetComponent<Transform>().localScale.z;
    }
}
