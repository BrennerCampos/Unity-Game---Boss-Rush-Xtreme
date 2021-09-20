using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace sr
{

  public class SRLoc
  {
    protected static SRLoc instance { get; private set; }

    private Dictionary<string, string> loc = new Dictionary<string, string>();

    static SRLoc()
    {
      instance = new SRLoc();
      instance.Add("property", "Property Search");
      instance.Add("property.tooltip", "Searches based on a property of an object. For example you may want to search the position of a transform, the shader of a material, or a variable on a monobehaviour you made.");
      
      instance.Add("search", "Search");
      instance.Add("search.tooltip", "Search based on the type of object you are looking for. This will find all references of the given type.");

      instance.Add("instance", "Prefab Instance Search");
      instance.Add("instance.tooltip", "Search for instances of prefabs. You can search for prefabs in scenes, or prefabs within prefabs.");

      instance.Add("search.popup", "Search:");
      instance.Add("search.popup.tooltip", "There are three types of search:\nSearch - Searches by the type of object.\nProperty Search - Search by property of an object\nInstance Search - Search for instances of prefabs\n\nFor more information on the different types, first select an option and then roll over it.");

      instance.Add("replace.toggle", "Replace?");
      instance.Add("replace.toggle.tooltip", "This toggle chooses whether you want to show or hide the UI for replacing.");

      instance.Add("help.window.title", "Search & Replace Help");
      instance.Add("help.overview.title", "Overview");
      instance.Add("help.overview.contents", "<size=20><b>Overview</b></size>\n\nProject Search & Replace (PSR) is a plugin for Unity's Editor which provides the ability to search and replace text, numerical data, images, materials, and other content on a project-wide basis.\n\n<b><color=red>Caution: Use version control or have a backup.</color></b>\nWith great search and replace comes great responsibility. The intent of this program is to allow you freedom in searching and replacing content on a project-wide basis. This means you have the freedom to completely break your project in unique and unexpected ways.\n\n<size=20><b>The Search Window</b></size>\n\nThe Search Window provides you with the ability to search and optionally replace content. There are three ways to search: <b>Property</b>, <b>Type</b>, and <b>Prefab Instance</b>.\n\n  • <b>Type Search</b>\nThis is the default search, simply called <b>Search</b> in the interface. This allows you to search by type. For example you can search for text, search for usage of a shader, or usage of a MonoBehaviour.\n\n  • <b>Property Search</b>\n Property Search allows you to search inside one property of an Object. For example, you can search for the number 42 on the position of a Transform. Type searches search over everything. For example, you can search for the number 42 in every Unity Object. \n\n  • <b>Prefab Instance Search</b>\nPrefab Instance Search can search and replace instances of prefabs. You can search for instances of a prefab in a scene and replace them with other instances. If you'd like to search for normal game objects and replace them with prefabs that is possible as well using the <i>Swap With Prefab</i> replace action.");

      instance.Add("help.intro.title", "Getting Started");
      instance.Add("help.intro.contents", "<size=20><b>Your First Search</b></size>\n\nLet's start off by doing a simple search for the text <i>'Hello World'</i>.\n\n<size=14><b>Step 1: Setup</b></size>\nCreate a game object in your scene and rename it to <b>Hello World</b>. Save your scene.\n\n<size=14><b>Step 2: Search...</b></size>\nConfigure your search with the following settings:\n\n  • <b>Search</b>: 'Search'.\n  • <b>Replace?</b> Unchecked.\n  • <b>Scope</b>: Entire Project.\n  • <b>Dependencies</b>: Not applicable.\n  • <b>Type</b>: Text\n  • <b>Value</b>: 'Hello'\n\nPress the <b>Search</b> button. You should see the game object you just created as a search result. You can click the 'cube' button on the right of the result to go to the scene or object that contains the search result.\n\n<size=14><b>Step 3: ...and Replace!</b></size>\nLastly we'll use PSR to change the name from Hello World to something else.\n\nCheck the Replace checkbox to display the UI for replacing.\nEnsure 'Replace With:' is the current replace action.\nIn the 'Replace' field type 'Goodbye'\n\nPress the Search And Replace button. Your game object called 'Hello World' should now be called 'Goodbye World'\n\nThose are the basics of searching and replacing!");

      instance.Add("help.search.text.title", "Searching Text");
      instance.Add("help.search.text.contents", "<size=20><b>Searching For Text</b></size>\n\nWhen searching for text, you are searching any field that can contain text: the names of objects, strings in monobehaviours, and other textual information. \n\nIf you want to search a specific property, for example just the name of an object, check out the section <b>Searching Properties.</b>\n\n  • <b>Ignore Case</b> - When checked the search will ignore differences in capitalization.\n\n  • <b>Contains</b> - Whether this should match the exact text here, or if the searched text contains this value. For example if this is on then 'hi' would match 'hi', 'how hip' and 'ship'. If the search is not case sensitive then it would also match 'HI', 'how HIP' and 'ShIp'. When Regex is checked this checkbox does nothing.\n\n  • <b>Regex</b> - Advanced! Whether or not the given string is a regular expression. Uses C# regular expressions. Groupings are supported. For example, you can search for <i>Hello (World)</i> and then replace with <i>Goodbye $1</i> to get <i>Goodbye World</i>.");

      instance.Add("help.property.search.title", "Searching Properties");
      instance.Add("help.property.search.contents", "<size=20><b>Searching Properties of Objects</b></size>\n\nSometimes you want to search <i>every</i> field of a game object, but sometimes it is more helpful to search just one area, such as the name of an object, the position of a Transform, or the font size for a text object. \n\nYou can search for these <i>properties</i> by dragging the type of object you want to search into the interface. There are a number of different ways to do this:\n\n  • <b>From The Inspector</b>: Drag a MonoBehaviour or Component from the inspector by dragging its name into the search window.\n\n  • <b>From the Project Tab</b>: Drag a Unity Object into the search window to search Textures, Materials, Prefabs and More. Tip: When you drag a Game Object into the search area you can select a property from any Component attached to it.\n\nThe Search field will use the game object you dragged in as the default value to search for. For example, if you drag a Transform into the field with a position of 1,2,3, then it will use those values automatically. ");
      instance.Add("help.instance.search.title", "Searching For Prefabs");
      instance.Add("help.instance.search.contents", "<size=20><b>Searching For Prefab Instances</b></size>\n\nPSR can search both scenes and prefabs for instances of prefabs. In the <i>Search</i> dropdown select <b>Prefab Instance Search</b> for this feature.\n\nIn the <b>Prefab</b> field you can drag a prefab from the project to search for the given prefab. Alternately you can drag an instance from the scene view, and PSR will determine its prefab and use that.\n\nClick the <i>Search</i> button and PSR will list the instances found.\n\n<size=14><b>Replacing Instances</b></size>\n\nWhen replacing, you simply drag a prefab you'd like to be a replacement and press the <i>Search And Replace</i> button. PSR will create new instances of this prefab and delete the old instance. Replacing has two options:\n\n  • <b>Keep Name</b> - Whether or not to keep the original name of the object. When the name is replaced, there is no attempt to add a number to the end or otherwise modify the name of the instance.\n\n  • <b>Keep Transform Values</b> - When the new instance is added, the old transform values are copied over.\n\n<b>Replacing References</b> - In addition to replacing the object itself, any references to the top-most object in the prefab will be replaced as well. For example, if you are replacing an instance of MyPrefab in MyScene, and there is a reference to MyPrefab's MyMonoBehaviour in the scene's MyGameController object, this will also be updated to point to the new prefab's MyMonoBehaviour.\n\n<size=14><b>Replacing Vanilla Objects with Prefabs</b></size>\nA common search is to search for a vanilla (aka a non-prefab game object) and replace it with a prefab instance. The best way to handle this is to usually search for a known value such as the name, and use it to find the game objects you'd like to replace. This can be done with the <b>Property Search</b> instead of a Prefab Instance Search. In order to replace the found object, you can use the <b>Swap With Prefab</b> replace action to replace the prefab. For more information on using the Property Search check out the <b>Searching Properties</b> help section.\n\n<size=14><b>What Can and Cannot be Replaced</b></size>\nFor the most part searching and replacing prefabs works as expected. But certain combinations of prefabs inside other prefabs cannot be replaced easily. When an error occurs attempting to replace an instance it cannot, PSR will give an explanation in the search result.\n\n  • 'Vanilla' game objects within prefabs can be replaced.\n  • Nested prefab instances within prefabs can be replaced.\n  • You can only replace the 'outermost' prefab instance game object.\n  • Game Objects of any type within variants can only be replaced if added as part of the variant, but must instead be replaced by replacing the parent prefab's instances. An example: <b>MyPrefab</b> contains the game object <b>MyPrefabChild</b>. <b>MyPrefabVariant</b> is a variant of <b>MyPrefab</b>. You cannot attempt to replace the <b>MyPrefabChild</b> object inside <b>MyPrefabVariant</b>, because it really exists inside <b>MyPrefab</b>.\n");
      instance.Add("help.location.options.title", "Search Options");
      instance.Add("help.location.options.contents", "<size=20><b>Choosing What To Search</b></size>\n\nSometimes you may want to search the entire project, the current scene, or just prefabs. These options are controlled by the <b>Scope</b> and <b>Assets</b> dropdowns.\n\n<size=14><b>Scope Options</b></size>\n  • <b>Entire Project</b> - Searches every folder within the project.\n  • <b>Specific Location</b> - A folder or object can be dragged into this field so you can search this specific asset or folder. When searching folders, subfolders are also searched.\n  • <b> Scene View</b> - Searches the current 'stage', aka the scene or the currently open prefab.\n  • <b> Current Selection </b> Searches the current selection. The selection can be objects within the scene, currently open prefab, or the <i>Project</i> tab. You can select as many objects as you like.\n\n<size=14><b>Asset Scope</b></size>\nIf <i>Entire Project</i> is selected you can choose to only search certain items. You can select or deselect the options to search only these types.\n  • <b>Prefabs</b> - .prefab\n  • <b>Scenes</b> - .unity\n  • <b>Scriptable Objects</b> - .asset\n  • <b>Materials</b> - .mat\n  • <b>Animations</b> - .anim\n  • <b>Animators</b> - .controller\n  • <b>Textures</b> - .png, psd, .tiff, .tif, .tga, .gif, .bmp, .jpg, .jpeg, .iff, .pict\n  • <b>Audio Clips</b> - .wav, .mp3\n\n<size=14><b>Searching Dependencies</b></size>\nA <i>dependency</i> is an object that another object relies on. For example a Game Object may need a mesh, and that mesh may need specific textures. The mesh and textures are all dependencies of the Game Object. \n\nWhen the <b>Dependencies</b> checkbox is checked then all dependences of an object are searched as well. It is important to note that searching dependencies ignores the current asset scope: If you are only searching prefabs, all its textures, etc. will be searched.\n\n<size=14><b>The Load Prefabs Checkbox</b></size>\nThis defaults to on, but exists in case you have a code that modifies objects upon load (such as NGUI). In this case you may want to turn this off. By default PSR loads the prefab into a scene before searching. This is required for certain replace operations to perform correctly. If you are having trouble with exceptions being thrown because loaded prefabs are being modified or moved, uncheck this box.\n\n<size=14><b>Search Performance Tips</b></size>\n  • Scenes drastically reduce search performance, so if possible avoid searching them unless you have to.\n  • Search is directly related to the number of items searched. Searching a specific directory or the current selection can significantly improve performance.\n  • Certain assets such as audio clips need to be fully loaded into memory in order to be searched. This can cause slowdowns. Try to limit the asset type to only those you are interested in.\n  • Great care is taken to only open and search objects that need to be searched. If you are searching a folder with prefabs and other assets, only the prefabs will be searched, and all other assets will be ignored.\n\nNot all file types are searched. Only the files with the given extensions above are searched. Search ignores searching all packages and package manager assets.");

      instance.Add("help.object.search.title", "Searching For Objects");
      instance.Add("help.object.search.contents", "<size=20><b>Searching Objects</b></size>\n\nAn 'object' can be a number of different things: A Game Object, a MonoBehaviour, a Texture or a Shader. These are all Unity <i>Objects</i>. Unity Objects frequently contain more 'sub'-objects and references to other objects.\n\nYou can search for an object by choosing the <b>Search</b> option from the dropdown, then selecting <b>Object</b> as the type of search to perform. Then simply drag an object that you'd like to search for into the field and press <i>Search</i>.\n\nThis will create a list of Search Results that display where that object is used.\n\n<size=14><b>Search For Components</b></size>\nThe Object search has a <i>Search for Components</i> checkbox that allows you to search not only for the object but it also searches for all references to Components and Monobehaviours of that object. \n\nExample: The BaddiePrefab spawns a BulletPrefab via the GunBehaviour. The behaviour references the bullet's Transform property. When the <i>Search For Components</i> checkbox is checked, you can search for the BulletPrefab's Game Object, and it will also search for the Transform, yielding a result on the BaddiePrefab's GunBehaviour.\n\n<size=14><b>Incompatible Searches</b></size>\nCertain types of searches are incompatible:\n  • It is not possible to search for a scene object within a project, since that instance only exists within the scene.\n  • Your search and replace options need to be compatible. You cannot search for a Mesh then replace it with a Material. It is possible to override this but it will most likely not do what you want!\n  • Searching for objects while the application is playing may yield no results since unity copies the items at runtime.\n");

      instance.Add("help.replace.actions.title", "Replacing Results");
      instance.Add("help.replace.actions.contents", "<size=20><b>Replacing Results</b></size>	\n\nWhen replacing within PSR, you have a number of 'Replace Actions' at your disposal. Each action works in its own way.\n\n<size=14><b>Replace With</b></size>\n\nThe <i>Replace With</i> option is the default search option, and will directly replace what you are searching with the value provided. \n\n<size=14><b>Replace Another Property</b></size>\n\nThis allows you to search for one property, and replace the value of another property. \n\nFor example, you may want to search for a game object named <b>Tree</b> and replace the Mesh of the MeshFilter with a new value. The property that is replaced is defined by dragging an object into the UI. In the above example you can drag a MeshFilter into the interface in order to replace the mesh.\n\n<size=14><b>Replace With GetComponent()</b></size>\n\nThis replace action is only available when searching for Objects via <b>Property Search</b>. If a GameObject is the search result, it will call GetComponent() on it and set the field with the value found. This can be helpful if, for some reason, things get unwired, or you've renamed a variable and a reference was lost.\n\n<size=14><b>Swap With Prefab</b></size>\n\nThis will replace the search result with a copy of the given Prefab. For example you may search for Game Objects containing the word \"Building\" with a copy of the prefab \"Building5_final\". This will replace existing prefabs and 'vanilla' game objects. For more information on replacing prefabs you may view the <i>Searching For Prefabs</i> section.\n\n<size=14><b>Run Script...</b></size>\n\nThis allows you to arbitrarily execute code when a search result is found. This action has its own help section called 'Running Scripts'.");

      instance.Add("help.running.scripts.title", "Running Scripts");
      instance.Add("help.running.scripts.contents", "<size=20><b>Running Scripts on Search Results</b></size>\n\nFor specialized cases it can be useful to run your own code. It is possible to execute a method for each search result. The <b>Run Script</b> replace action can be chosen for both standard <b>Search</b> and <b>Property Search</b> modes.\n\n<color=red><size=14><b>This is an experimental feature.</b></size></color>\n\nThis method needs to be:\n\n  • A public static method on a static class.\n  • Take a <b>SearchParameters</b> object as its sole parameter.\n  • Exist within the editor folder as an editor class, or in the assembly specified.\n\nThe Search Parameters object exists in the 'sr' namespace and has two properties:\n  • prop - The SerializedProperty that returned true for the given search.\n  • assetBeingSearched - The SearchAssetData object that encapsulates the asset currently being searched.\n\nWhen modifying the asset of a search result you should use the SerializedProperty and SerializedObject classes. While you may be able to modify the object directly, it is likely the change may not save to disk. \n\nIt is possible that the prop and assetBeingSearched properties may be null.\n\n\nAfter your function has executed, you may call <b>ActionTaken(yourMessage)</b>on the <b>SearchParameters </b>object. <i>yourMessage</i> will display in the search results.\n\nA simple example script is below. The <i>doThing</i> function will increment the rotation by 10 degrees. To use the example create a search that yields Transforms as results. \n\nusing UnityEngine;\nusing UnityEditor;\nusing System;\nusing sr;\n/**\n  * To use this example, copy and paste it in the editor folder. \n  */\npublic static class TestRunningScript\n{\n  public static void doThing(SearchParameters searchParams)\n  {\n    SerializedProperty rotationProp = searchParams.prop.serializedObject.FindProperty(\"m_LocalRotation\");\n    Quaternion currentRotation = rotationProp.quaternionValue;\n    float angle = 10.0f;\n    currentRotation *= Quaternion.AngleAxis(angle, Vector3.up);\n    rotationProp.quaternionValue = currentRotation;\n    searchParams.ActionTaken(\"Setting rotation to: \" + currentRotation.eulerAngles);\n  }\n}\n");

      instance.Add("help.further.support.title", "Further Support");
      instance.Add("help.further.support.contents", "<size=20><b>Further Support</b></size>\n\nQuestions? Comments? I'd love to hear from you!\nFor support you can email me directly at:\n\nchris@enemyhideout.com. \n\nYou may also join the PSR Discord at:\nhttps://discord.gg/25bWxkH");


      instance.Add("help.numeric.data.title", "Numeric Data");
      instance.Add("help.numeric.data.contents", "<size=20><b>Searching Numeric Data</b></size>\n\nPSR allows for search of a number of types of numbers:\n\n  • <b>Float, Double </b>\n  • <b>Integer </b>\n  • <b>Boolean</b>\n  • <b>Vector2</b>\n  • <b>Vector3</b>\n  • <b>Vector4</b>\n  • <b>Quaternion</b>\n  • <b>Rect</b>\n\nThese numeric structures all follow a similar format, and can be searched for using the standard <b>Search</b> and <b>Property Search</b> modes.\n\n<size=14><b>Using Approximate</b></size>\n\nThe <i>Approximate</i> checkbox is found on most numeric fields.\nIt defines whether numbers match exactly or use Unity's built-in Mathf.Approximately() functionality.\n\n<size=14><b>Granular search.</b></size>\n\nThe checkboxes next to each field defines the fields we are interested in searching for. For example, if the <b>x</b> field is checked, but the <b>y</b> is unchecked, the search will match only the x, and ignore the y.\n\nSimilarly the replace options can be granularly replaced. You may only want to replace the <b>y</b> and leave the <b>x</b> untouched.\n");
      
      instance.Add("help.script.search.title", "Script Search");
      instance.Add("help.script.search.contents", "<size=20><b>Searching For Scripts</b></size>\n\nIt is possible to search for usages of specific scripts. For example, you might be interested to know whether a specific MonoBehaviour is used at all, so that you can safely delete it.\n\nScripts are:\n\n  • MonoBehaviours\n  • ScriptableObjects\n  • Components\n\n<size=14><b>Searching For Script Usage</b></size>\n\n  • In the <b>Search</b> dropdown choose the standard <i>Search</i> mode.\n  • In the <b>Type</b> dropdown select Scripts.\n  • Select the script you are interested in by:\n     • Dragging the Monobehaviour class into the field from the Project Tab.\n     • For built-in MonoBehaviours such as the UI classes you can drag from a prefab or scene object.\n     • Dragging a Component from the Inspector into the field.\n     • You may also drag a folder of MonoBehaviours into the field and search for them in bulk.\n  • Click <b>Search</b>.\n\nThis will show all usages of the script within the given scope, including prefab instances.\n\n<size=14><b>Replacing Scripts</b></size>\n\nIt is also possible to swap out the script that a MonoBehaviour uses. For example you might want to search for MyBehaviour and replace the usage with MyBehaviourExtended.\n\nReplacing scripts comes with a few caveats. MonoBehaviours contain references to objects called MonoScripts. In order to search and replace a MonoBehaviour to use a different class, you can switch the underlying MonoScript and it is now using the new script. All the saved property data for the behaviour will attempt to be re-mapped to use the new script as best as possible.\n\n<size=14><b>Searching For Unused Scripts</b></size>\n\nOne of the most gratifying feelings as a developer is deleting unused code. While you can determine if a script is unused by searching individually, it is more efficient to do this in bulk.\n\nPSR helps facilitate this via the <b>Show Unused Scripts</b> option. This only appears when you are searching for a folder of scripts. When this is checked the search results will be all scripts that are not used within the project.\n\n<size=14><b>Assets Missing Scripts</b></size>\nSometimes there may be a loose contract that a game object should have a specific script. This is where the 'Match assets where script is missing' option comes in.\n\nFor example, you may have a folder of flying enemies, and they all should have the MyFlyingScript attached. But maybe a few of them didn't have the script added...but which ones? That is where this feature comes in.\n\nWhen checked, any assets that do <i>not</i> have at least one instance of the given script will return as a result. This information can then be used to determine whether the script should be added."); 


      instance.Add("help.usage.vs.instance.title", "Usage Vs. Instance");
      instance.Add("help.usage.vs.instance.contents", "<size=20><b>Usage vs. Instance</b></size>\nIts important to note the distinction between <i>usage</i> and <i>instance</i>. This can help in choosing the right search for the job.\n\nA usage is a reference to another object. This is where an object 'points' to another object.\n\nAn instance is an occurrence of an object template. It is a copy of the object, with additional modifications. For example a Prefab used in a scene is an instance of that Prefab.\n\n<size=14><b>Prefab Usage vs Prefab Instances</b></size>\nWhen using the standard <b>Search</b> and <b>Property Search </b> modes, PSR is looking for where that specific object is being <i>used</i>. This is helpful for searching for references to prefabs, or for relationships between objects. If you need to find where a prefab is used within a scene, then the <b>Prefab Instance Search </b> is more appropriate.\n\n<size=14><b>Script Usages</b></size>\nIt can be helpful to search for usage of a type of script. For example if a MonoBehaviour script is used in the project or not. This is gone over in more detail in the <i>Script Search</i> help section.\n\n"); 

      instance.Add("help.animation.clips.title", "Animation Clips");
      instance.Add("help.animation.clips.contents", "<size=20><b>Searching Animations</b></size>\n\nCurrently most of the contents of an AnimationClip is not easily modifiable. The only currently searchable data is the animation 'path'. The 'path' of an animation is a string that describes the hierarchy of objects from the animation 'root' to the object being animated. Some examples:\n\n  • Parent/Child\n  • Parent/Child/Grandparent\n  • MyObject\n\nWhen a GameObject is moved in Unity, you can press 'Enter' to rename the animation paths by hand, but PSR provides the way to do this across many animation clips.\n\n<size=14><b>Fixing Broken Animation Paths</b></size>\n\nIt is possible to fix broken paths using Search & Replace.\n\n  • Choose the <b>Property Search</b> mode.\n  • Drag an AnimationClip into the Object field.\n  • Enter the value of the broken path into the Search field.\n  • Enter the correct value into the Replace field, making sure to add slashes between items.\n  • Verify the search scope correctly includes the AnimationClips we want to fix.\n  • Press Search to test your search and verify the values are correct.\n  • Press Search and Replace to modify all AnimationClips.\n"); 

      instance.Add("help.search.results.title", "The Results Pane");
      instance.Add("help.search.results.contents", "<size=20><b>Working With Results</b></size>\n\n\nWhen you complete a search the Results Pane will display a list of results. Results are shown in the format:\n\n/Path/to/object.extension/RootObject:SubObject->MyMonoBehaviour.MyProperty\n\n<b>Note:</b> If the results do not show as above, the window is in Compact Mode. Make the window wider to see the full path.\n\nThese results are selectable and can be copied and pasted.\n\n<b><size=16>Selecting Results</size></b>\n\nThe button next to the result can be used to select the given result. This button has the following modes:\n  • Left Click - For prefabs and scenes, this will open the scene and highlight the object inside.\n  • Ctrl-Click - This will select the object in the Project tab. Ctrl-clicking multiple objects adds or subtracts from the selection. This can be used to compare multiple objects within the Inspector. On Mac use the Command button instead of Control.\n  • Shift-Click -This can be used to select a range of items within the Project tab, similar to Ctrl-Click.\n\n<b>Note:</b> When selecting objects inside prefabs, they will show in the inspector but if they are not the root object, this may not correctly select in the Project tab. This is a limitation within Unity.\n\n<b>Copy To Clipboard</b> - Creates a textual representation of the Results Pane output and copies it to the clipboard. This can be further used in your text editor of choice.\n\n<b>Select Objects</b> - Selects all objects (internal game objects, animators, materials, etc.) for all results and if they are not scene objects, it will attempt to open the items in the scene. Shift-Select will select all objects in the project, displayable in the inspector."); 

    }

    public void Add(string key, string val)
    {
      loc.Add(key, val);
    }

    public string Lookup(string key)
    {
      string retVal;
      if(instance.loc.TryGetValue(key, out retVal))
      {
        return retVal;
      }
      return key;
    }

    public static string L(string key)
    {
      return instance.Lookup(key);
    }

    public static GUIContent GUI(string label)
    {
      return new GUIContent(instance.Lookup(label), instance.Lookup(label + ".tooltip"));
    }
  }

}
