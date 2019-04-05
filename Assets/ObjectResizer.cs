using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class ObjectResizer : EditorWindow
{
    private GameObject selectedObject;

    private string height;
    private string width;
    private string depth;

    private float xScale;
    private float yScale;
    private float zScale;

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
        selectedObject =  Selection.activeGameObject;
        if (selectedObject && !reset)
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
            UpdateAxis();
        }
    }

    private void getWidth()
    {
        float widthToTransform;
        float fWidth;
        float.TryParse(width, out fWidth);

        if (width == null) width = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale).ToString();

        else if (System.Math.Round(fWidth, 3) != System.Math.Round((selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale), 3))
        {
            widthToTransform = fWidth / (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale);
            if (scale) selectedObject.GetComponent<Transform>().localScale *= widthToTransform;
            else
            {
                if (widthToTransform > 1) selectedObject.GetComponent<Transform>().localScale += new Vector3((widthToTransform - 1) * xScale, 0f, 0f);
                else selectedObject.GetComponent<Transform>().localScale -= new Vector3((1 - widthToTransform) * xScale, 0f, 0f);
            }
            getScale();
            UpdateAxis();
        }
    }

    private void getDepth()
    {
        float depthToTransform;
        float fDepth;
        float.TryParse(depth, out fDepth);

        if (depth == null) depth = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale).ToString();

        else if (System.Math.Round(fDepth, 3) != System.Math.Round((selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale), 3))
        {
            depthToTransform = fDepth / (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale);
            if (scale) selectedObject.GetComponent<Transform>().localScale *= depthToTransform;
            else
            {
                if (depthToTransform > 1) selectedObject.GetComponent<Transform>().localScale += new Vector3(0f, 0f, (depthToTransform - 1) * zScale);
                else selectedObject.GetComponent<Transform>().localScale -= new Vector3(0f, 0f, (1 - depthToTransform) * zScale);
            }
            getScale();
            UpdateAxis();
        }
    }

    private void UpdateAxis()
    {
        height = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();
        width = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale).ToString();
        depth = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale).ToString();
    }

    private void getScale()
    {
        xScale = selectedObject.GetComponent<Transform>().localScale.x;
        yScale = selectedObject.GetComponent<Transform>().localScale.y;
        zScale = selectedObject.GetComponent<Transform>().localScale.z;
    }

    private void resetObject()
    {
        selectedObject.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        height = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * yScale).ToString();
        width = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * xScale).ToString();
        depth = (selectedObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * zScale).ToString();
    }
}
