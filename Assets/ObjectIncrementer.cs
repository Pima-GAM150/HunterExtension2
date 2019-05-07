using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class ObjectIncrementer : EditorWindow
{
    public GameObject[] selection;

    public string increment;

    public bool axisToggle;

    [MenuItem("Window/Object Incrementer")]
    static void Init()
    {
        ObjectIncrementer window = (ObjectIncrementer)EditorWindow.GetWindow(typeof(ObjectIncrementer));
        window.minSize = new Vector2(400f, 100f);
        window.Show();
    }

    private void OnGUI()
    {
        selection = Selection.gameObjects;
        increment = EditorGUILayout.TextField("Amount to Increment: ",increment);
        axisToggle = EditorGUILayout.Toggle("Toggle Z Axis",axisToggle);
        EditorGUILayout.LabelField("Use WASD keys in this window to increment the selected object(s)");
        Event e = Event.current;

        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.A)
            {
                foreach (GameObject i in selection)
                {
                    if (axisToggle) i.transform.position -= new Vector3(0f, 0f, float.Parse(increment));
                    if (!axisToggle) i.transform.position -= new Vector3(float.Parse(increment), 0f, 0f);
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
            if (e.keyCode == KeyCode.D)
            {
                foreach (GameObject i in selection)
                {
                    if (axisToggle) i.transform.position += new Vector3(0f, 0f, float.Parse(increment));
                    if (!axisToggle) i.transform.position += new Vector3(float.Parse(increment), 0f, 0f);
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
            if (e.keyCode == KeyCode.W) foreach (GameObject i in selection) i.transform.position += new Vector3(0f, float.Parse(increment), 0f);
            if (e.keyCode == KeyCode.S) foreach (GameObject i in selection) i.transform.position -= new Vector3(0f, float.Parse(increment), 0f);
        }
        Repaint();
    }
}
