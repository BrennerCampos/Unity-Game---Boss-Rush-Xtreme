using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

public class PNGsToUnityScene : EditorWindow
{
    private Transform containerGO = null;
    private string directoryPath = "";

    [MenuItem("Tools/PNGs to Unity Scene")]
    public static void ShowWindow()
    {
        GetWindow<PNGsToUnityScene>("PNGs to Unity Scene");
    }

    private void OnGUI()
    {
        GUILayout.Label("Generate Scene of Sprites from Folder of PNGs", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();

        containerGO = (Transform)EditorGUILayout.ObjectField("Container Object:", containerGO, typeof(Transform), true);

        EditorGUILayout.LabelField("Assets Folder:", GetRelativeFilePath(directoryPath));

        if (GUILayout.Button("Select Folder with PNG Assets"))
        {
            directoryPath = EditorUtility.OpenFolderPanel("Select Assets Folder with PNGs...", Application.dataPath, "");
            if(!directoryPath.StartsWith(Application.dataPath)) { directoryPath = ""; }
        }

        EditorGUI.BeginDisabledGroup(directoryPath == string.Empty || (containerGO != null && EditorUtility.IsPersistent(containerGO)));
        
        if (GUILayout.Button("Start"))
        {
            SpawnImagesInScene();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        if(directoryPath == string.Empty)
        {
            EditorGUILayout.HelpBox("Please select the folder containing the images you wish to bring into the scene", MessageType.Info);
        }
        if(containerGO == null)
        {
            EditorGUILayout.HelpBox("No container object set. All created objects will spawn in root of scene", MessageType.Info);
        }
        if(containerGO != null && EditorUtility.IsPersistent(containerGO))
        {
            EditorGUILayout.HelpBox("Parent object must be an object in the current scene.", MessageType.Warning);
        }
    }

    void SpawnImagesInScene()
    {
        string[] filesInDirectory = Directory.GetFiles(directoryPath);

        int index = 0;

        foreach (string curFilePath in filesInDirectory)
        {
            float curProgress = (float)index / filesInDirectory.Length;
            EditorUtility.DisplayProgressBar("Generating Scene From PNGs...", (curProgress * 100).ToString("F0") + "% Complete", curProgress);

            if (!curFilePath.EndsWith(".png")) { index++; continue; }

            string relativeFilePath = GetRelativeFilePath(curFilePath);

            TextureImporter curTextureAsset = (TextureImporter)TextureImporter.GetAtPath(relativeFilePath);

            curTextureAsset.textureType = TextureImporterType.Sprite;
            int longestSide = GetLongestSide(curTextureAsset);
            curTextureAsset.maxTextureSize = GetMaxTextureSize(longestSide);
            curTextureAsset.SaveAndReimport();
            
            Sprite curSprite = AssetDatabase.LoadAssetAtPath<Sprite>(relativeFilePath);
            string[] splitFileName = curSprite.name.Split(new[] { '_' }, 2);
            if (!int.TryParse(splitFileName[0], out int orderInLayer))
            {
                Debug.LogError("Error: Cannot convert first part of file name to integer for file: " +
                               curSprite.name + ". Stopping operation.\n" +
                               "Ensure that the file name of all images begins with the layer index " +
                               "followed by an underscore. i.e. 01_ImageName.png");
                break;
            }

            GameObject newGO = new GameObject(splitFileName[1]);
            SpriteRenderer spRenderer = newGO.AddComponent<SpriteRenderer>();
            spRenderer.sprite = curSprite;
            spRenderer.sortingOrder = orderInLayer;

            newGO.transform.parent = containerGO;
            newGO.transform.position = Vector3.zero;
            newGO.transform.rotation = Quaternion.identity;
            newGO.transform.localScale = Vector3.one;

            index++;
        }
        EditorUtility.ClearProgressBar();
    }

    // Utilities

    int GetLongestSide(TextureImporter curAsset)
    {
        int width, height;
        if (curAsset != null)
        {
            object[] args = new object[2] { 0, 0 };
            MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(curAsset, args);

            width = (int)args[0];
            height = (int)args[1];

            return width > height ? width : height;
        }
        return 0;
    }

    string GetRelativeFilePath(string fullPath)
    {
        if(fullPath == string.Empty) { return ""; }
        string relativeFilePath = "Assets" + fullPath.Substring(Application.dataPath.Length);
        return relativeFilePath;
    }

    int GetMaxTextureSize(int longestSide)
    {
        if (longestSide <= 32f)
        {
            return 32;
        }
        else if (longestSide <= 64f)
        {
            return 64;
        }
        else if (longestSide <= 128f)
        {
            return 128;
        }
        else if (longestSide <= 256f)
        {
            return 256;
        }
        else if (longestSide <= 512f)
        {
            return 512;
        }
        else if (longestSide <= 1024f)
        {
            return 1024;
        }
        else if (longestSide <= 2048f)
        {
            return 2048;
        }
        else if (longestSide <= 4096f)
        {
            return 4096;
        }
        else if (longestSide <= 8192f)
        {
            return 8192;
        }
        else
        {
            Debug.LogWarning("Warning: Image is larger than max size of 8192 pixels in one or more directions. " +
                             "Setting current TextureImporter's max size to max value of 8192.");
            return 8192;
        }
    }
}
