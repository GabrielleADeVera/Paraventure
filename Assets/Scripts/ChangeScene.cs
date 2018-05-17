using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	public void ToGame()
	{
		SceneManager.LoadScene (1);
	}
}
