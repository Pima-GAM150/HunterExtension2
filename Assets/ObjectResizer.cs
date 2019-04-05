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
            getHeightAsString();
            getExtentsAsStrng();
        }
        else
        {
            height = null;
            extents = null;
        }

        EditorGUILayout.TextField("Object Height",height);
        EditorGUILayout.TextField("Oject extents Y", extents);
        
    }

    private void getHeightAsString()
    {
        height = selectedObject.GetComponent<MeshFilter>().mesh.bounds.size.y.ToString();
    }

    private void getExtentsAsStrng()
    {
        extents = selectedObject.GetComponent<MeshFilter>().mesh.bounds.extents.y.ToString();
    }
}
