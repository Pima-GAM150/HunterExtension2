using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class ObjectResizer : EditorWindow
{
    private GameObject selectedObject;

    private string height;

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
            getHeight();
        }
        else
        {
            height = null;
        }

        height = EditorGUILayout.TextField("Object Height", height);      
    }

    private void getHeight()
    {
        float heightToTransform;
        float fHeight;
        float.TryParse(height, out fHeight);

        if (height == null) height = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * selectedObject.GetComponent<Transform>().localScale.y).ToString();

        else if(System.Math.Round(fHeight,3) != System.Math.Round((selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * selectedObject.GetComponent<Transform>().localScale.y),3))
        {
            heightToTransform = fHeight / (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * selectedObject.GetComponent<Transform>().localScale.y);
            Debug.Log(heightToTransform);
            selectedObject.GetComponent<Transform>().localScale *= heightToTransform;
            height = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * selectedObject.GetComponent<Transform>().localScale.y).ToString();
        }
    }
}
