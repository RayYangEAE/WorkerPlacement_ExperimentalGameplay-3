using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalProAndWorkerTBA : MonoBehaviour {
	ProductInProcess refProProcess;
	int workerTBA;
	int finals;
	float timeLeft;
	bool fail =false;

	void Start(){
		refProProcess = GameObject.Find ("ProductInProcess").GetComponent<ProductInProcess> ();
	}
	
	// Update is called once per frame
	void Update () {
		workerTBA = refProProcess.WorkerToBeAssigned;
		finals = refProProcess.finalProduct;
		timeLeft = refProProcess.targetTime - refProProcess.gameTime;
		if (timeLeft < 0) {
			timeLeft = 0;
		}
		fail = (refProProcess.productA >= 50) || (refProProcess.productB >= 50) || (refProProcess.productC >= 50);
		if (fail) {
			gameObject.GetComponent<Text> ().text = "Game Over!";
			refProProcess.gameTime = refProProcess.targetTime;
		} else {
			gameObject.GetComponent<Text> ().text = "Time: " + timeLeft + "\n"  + "Final Products: " + finals + ".";
			//gameObject.GetComponent<Text> ().text = "Time: " + timeLeft + "\n" + "Workers To Be Assigned: " + workerTBA + "; " + "Final Products: " + finals + ".";
		}
		if (Input.GetKeyDown("r")) {
			SceneManager.LoadSceneAsync("test1");
		}
	}
}
