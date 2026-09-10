using UnityEngine;
using UnityEditor;

public class TerrainTreeConverter : EditorWindow
{
    [MenuItem("Tools/Convert Terrain Trees to GameObjects")]
    public static void ShowWindow()
    {
        GetWindow<TerrainTreeConverter>("Tree Converter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Convert Painted Terrain Trees to GameObjects", EditorStyles.boldLabel);

        if (GUILayout.Button("Convert Active Terrain Trees"))
        {
            ConvertTrees();
        }
    }

    private static void ConvertTrees()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            EditorUtility.DisplayDialog("Error", "No active terrain found in the scene.", "OK");
            return;
        }

        TerrainData data = terrain.terrainData;
        TreeInstance[] instances = data.treeInstances;
        TreePrototype[] prototypes = data.treePrototypes;

        // Create a root object to keep the hierarchy clean
        GameObject root = new GameObject("Converted_Trees");
        root.transform.position = terrain.transform.position;

        Undo.RegisterCreatedObjectUndo(root, "Convert Terrain Trees");

        for (int i = 0; i < instances.Length; i++)
        {
            TreeInstance tree = instances[i];
            TreePrototype prototype = prototypes[tree.prototypeIndex];

            // Calculate world position
            Vector3 localPos = Vector3.Scale(tree.position, data.size);
            Vector3 worldPos = localPos + terrain.transform.position;

            // Instantiate the prefab
            GameObject treePrefab = prototype.prefab;
            GameObject newTree = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab);

            if (newTree != null)
            {
                newTree.transform.position = worldPos;
                newTree.transform.parent = root.transform;

                // Apply original rotation and scale from the terrain painting
                newTree.transform.localRotation = Quaternion.AngleAxis(tree.rotation * Mathf.Rad2Deg, Vector3.up);
                newTree.transform.localScale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);

                Undo.RegisterCreatedObjectUndo(newTree, "Convert Terrain Trees");
            }

            // Show a progress bar for massive maps
            if (i % 100 == 0)
            {
                EditorUtility.DisplayProgressBar("Converting Trees", $"Processing tree {i} of {instances.Length}", (float)i / instances.Length);
            }
        }

        EditorUtility.ClearProgressBar();

        // Optional: Clear the trees from the terrain so they aren't duplicated
        if (EditorUtility.DisplayDialog("Success", $"Converted {instances.Length} trees! Do you want to remove the painted trees from the terrain asset?", "Yes, Remove Them", "No, Keep Them"))
        {
            Undo.RecordObject(data, "Clear Terrain Trees");
            data.treeInstances = new TreeInstance[0];
            terrain.Flush();
        }
    }
}