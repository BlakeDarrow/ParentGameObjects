#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

	public class ParentGameObjects : ScriptableWizard
	{

	[Header("Choose Parents")]
 	[SerializeField]
 	[Tooltip("Exact names of the parent folders")]
	public string[] parentObject = { // list of object names to act as the parents.

	//			Change/add these values

	"Plants", 
	"Plants",
	"Rocks",

	}; 

	[SerializeField]
	[Header("Define Search (Contains)")]
	[Tooltip("Names for the search fields. Corresponds to the parentObject element.")]
	public string[] containsSearchCriteria = { // list of words to check

	//			Change/add these values

	"tree",
	"grass",
	"rock",

	}; 

	[SerializeField]
	[Tooltip("Alphabetize newly parented gameObjects?")]
	public bool alphabetize = true;
	[Header("")]
	[TextArea(8,8)]
	public string Help =  "This wizard will parent the selected GameObjects (or all if none) based on user defined feilds. The 'Parent Folder' field requires the exact naming of the gameobjects that will act as parents. If no GameObjects exist with that name, they will be created. The 'Contains Search Criteria' field also requires exact naming. When the Alphabetize box is checked, the gameobjects will be sorted after becoming children.";
	[TextArea(4,8)]
	public string DefaultSettings = "To change default settings open the 'ParentGameObject.cs' file and change the public string values titled 'parentObject', and 'containsSearchCriteria'. Save and re-open the wizard.";
	[MenuItem("DarrowTools/Parent GameObjects")]

	static void CreateWizard()
		{
		var wizard = DisplayWizard<ParentGameObjects>("ParentGameObjects","Parent");

		}

		void OnWizardCreate()
		{
			int index = -1;
			if(Selection.transforms.Length == 0){
				Selection.objects = FindObjectsOfType<GameObject>();
			}

			foreach(string name in parentObject){
				var folderNull = GameObject.Find(name);
				if(folderNull == null){
					GameObject newObj = new GameObject(name);
				}
			}
		
			foreach(string i in parentObject){ //handles 
				index++;

				foreach(Transform selection in Selection.transforms){
					GameObject parentObj;
					if(selection.name.Contains(containsSearchCriteria[index])){
						//Debug.Log("Index at " + index + ", named " + selection.name + ", " + "Parent folder at "+ parentObject[index] +", Searched for "+ containsSearchCriteria[index]);
						parentObj = GameObject.Find(parentObject[index]);
					    selection.transform.parent = parentObj.transform;
					}
				}
			}

			if(alphabetize == true){
				foreach(string name in parentObject){
				Selection.activeGameObject = GameObject.Find(name);
					foreach (GameObject obj in Selection.gameObjects) {
						List<Transform> children = new List<Transform>();
						for (int i = obj.transform.childCount - 1; i >= 0; i--) {
							Transform child = obj.transform.GetChild(i);
							children.Add(child);
							child.parent = null;
						}
						children.Sort((Transform t1, Transform t2) => { return t1.name.CompareTo(t2.name); });
						foreach (Transform child in children) {
							child.parent = obj.transform;
						}
					}
				}
			Selection.activeGameObject = null;
		}	
	}
}
#endif

