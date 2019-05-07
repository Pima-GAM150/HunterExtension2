using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class ObjectResizer : EditorWindow
{
    private GameObject[] sceneSelection;
    private GameObject[] selectedObjects;

    private string height;
    private string width;
    private string depth;

    private float xScale;
    private float yScale;
    private float zScale;

    private int bigestSelection;

    private bool scale;

    private bool reset;

    [MenuItem("Window/Object Resizer")]
    static void Init()
    {
        ObjectResizer window = (ObjectResizer)EditorWindow.GetWindow(typeof(ObjectResizer));
        window.Show();
    }

    void OnGUI()
    {
        sceneSelection = Selection.gameObjects;
        GetSelectedObjects();
        GetBigestSelection();
        if (selectedObjects.Length > 0 && !reset)
        {
            getScale();
            getHeight();
            getWidth();
            getDepth();
        }
        else
        {
            height = null;
            width = null;
            depth = null;
        }

        scale = EditorGUILayout.Toggle("Scale Object", scale);
        width = EditorGUILayout.TextField("Object Width (X-axis)", width);
        height = EditorGUILayout.TextField("Object Height (Y-axis)", height);
        depth = EditorGUILayout.TextField("Object Length (Z-axis)", depth);
        reset = EditorGUILayout.Toggle("Reset scale to 1", reset);
        if (reset) resetObject();
    }

    private void getHeight()
    {
        float heightToTransform;
        float fHeight;
        float.TryParse(height, out fHeight);

        if (height == null) height = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();

        else if(System.Math.Round(fHeight,3) != System.Math.Round((selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale),3))
        {
            heightToTransform = fHeight / (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale);
            foreach (GameObject i in selectedObjects)
            {
                if (scale) i.GetComponent<Transform>().localScale *= heightToTransform;
                else
                {
                    if (heightToTransform > 1) i.GetComponent<Transform>().localScale += new Vector3(0f, (heightToTransform - 1) * yScale, 0f);
                    else i.GetComponent<Transform>().localScale -= new Vector3(0f, (1 - heightToTransform) * yScale, 0f);
                }
            }           
            getScale();
            UpdateAxis();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    private void getWidth()
    {
        float widthToTransform;
        float fWidth;
        float.TryParse(width, out fWidth);

        if (width == null) width = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale).ToString();

        else if (System.Math.Round(fWidth, 3) != System.Math.Round((selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale), 3))
        {
            widthToTransform = fWidth / (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale);
            foreach (GameObject i in selectedObjects)
            {
                if (scale) i.GetComponent<Transform>().localScale *= widthToTransform;
                else
                {
                    if (widthToTransform > 1) i.GetComponent<Transform>().localScale += new Vector3((widthToTransform - 1) * xScale, 0f, 0f);
                    else i.GetComponent<Transform>().localScale -= new Vector3((1 - widthToTransform) * xScale, 0f, 0f);
                }
            }          
            getScale();
            UpdateAxis();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    private void getDepth()
    {
        float depthToTransform;
        float fDepth;
        float.TryParse(depth, out fDepth);

        if (depth == null) depth = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale).ToString();

        else if (System.Math.Round(fDepth, 3) != System.Math.Round((selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale), 3))
        {
            depthToTransform = fDepth / (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale);
            foreach(GameObject i in selectedObjects)
            {
                if (scale) i.GetComponent<Transform>().localScale *= depthToTransform;
                else
                {
                    if (depthToTransform > 1) i.GetComponent<Transform>().localScale += new Vector3(0f, 0f, (depthToTransform - 1) * zScale);
                    else i.GetComponent<Transform>().localScale -= new Vector3(0f, 0f, (1 - depthToTransform) * zScale);
                }
            }   
            getScale();
            UpdateAxis();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    private void UpdateAxis()
    {
        height = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();
        width = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale).ToString();
        depth = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale).ToString();
    }

    private void getScale()
    {
        xScale = selectedObjects[bigestSelection].GetComponent<Transform>().localScale.x;
        yScale = selectedObjects[bigestSelection].GetComponent<Transform>().localScale.y;
        zScale = selectedObjects[bigestSelection].GetComponent<Transform>().localScale.z;
    }

    private void resetObject()
    {
        foreach (GameObject i in selectedObjects)
        {
            selectedObjects[bigestSelection].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            height = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();
            width = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale).ToString();
            depth = (selectedObjects[bigestSelection].GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale).ToString();
        }
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private void GetSelectedObjects()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach(GameObject i in sceneSelection)
        {
            MeshFilter[] tem = i.GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter j in tem)
            {
                temp.Add(j.gameObject);
            }
        }
        selectedObjects = temp.ToArray();
    }

    private void GetBigestSelection()
    {
        int counter = 0;
        float largestArea = 0;
        foreach (GameObject i in selectedObjects)
        {
            Bounds temp = i.GetComponent<MeshFilter>().sharedMesh.bounds;
            float area = temp.size.x * temp.size.y * temp.size.z;
            if (area > largestArea) { largestArea = area; bigestSelection = counter; } 
            counter++;
        }
    }
}
