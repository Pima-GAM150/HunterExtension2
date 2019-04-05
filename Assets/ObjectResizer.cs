using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class ObjectResizer : EditorWindow
{
    private GameObject selectedObject;

    private string height;
    private string extents;

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
            getExtents();
        }
        else
        {
            height = null;
            extents = null;
        }

        height = EditorGUILayout.TextField("Object Height", height);
        extents = EditorGUILayout.TextField("Oject extents Y", extents);
        
    }

    private void getHeight()
    {
        float heightToTransform;
        float tHeight;

        if (height == null) height = selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y.ToString();

        if (height != selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y.ToString())
        {
            float.TryParse(height, out tHeight);
            heightToTransform = tHeight / selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y;
            Debug.Log(heightToTransform);
            selectedObject.GetComponent<Transform>().localScale *= heightToTransform;
            height = selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y.ToString();
        }
    }

    private void getExtents()
    {
        extents = selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y.ToString();
    }
}
