using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ApplicationManager : MonoBehaviour {

	public string scene;
	private int load;



	void Start (){
	}
	

	public void Newgame ()
	{
		Debug.Log (scene);

//		SceneManager.GetSceneByName (scene) = load;
		SceneManager.LoadScene (1);
		
	}//public GameObject.Scene scene;

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}


}
