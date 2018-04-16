using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public QRCodeDecodeController QRDecode = null;
	public GameObject cameraGo = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Loads the index of the scene.
	/// </summary>
	/// <param name="sceneIndex">Scene index.</param>
	public void LoadSceneIndex(int sceneIndex){
		
		if(QRDecode != null) {
			QRDecode.enabled = false;
		}

		if (cameraGo != null) {
			cameraGo.SetActive (false);
		}


		SceneManager.LoadSceneAsync (sceneIndex);
	}
}
