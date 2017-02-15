using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumWorkerChange : MonoBehaviour {

	private int currentWorker;
	private int inputWorker;
	private int numWorkerChanged;
	ProductInProcess refProProcess;

	public void WorkerPlus(string process){
		if (refProProcess.WorkerToBeAssigned > 0) {
			refProProcess.WorkerToBeAssigned--;
			currentWorker++;
			gameObject.GetComponent<InputField> ().text = currentWorker.ToString ();
			switch (process){
			case "A":
				refProProcess.numWorkerAChange += 1;
				break;
			case "B":
				refProProcess.numWorkerBChange += 1;
				break;
			case "C":
				refProProcess.numWorkerCChange += 1;
				break;
			}
		}
	}

	public void WorkerMinus(string process){
		if (currentWorker > 0) {
			currentWorker--;
			gameObject.GetComponent<InputField> ().text = currentWorker.ToString ();
			switch (process){
			case "A":
				refProProcess.numWorkerAChange -= 1;
				StartCoroutine(waitToMinusWorker (refProProcess.timeProcessA));
				break;
			case "B":
				refProProcess.numWorkerBChange -= 1;
				StartCoroutine(waitToMinusWorker (refProProcess.timeProcessB));
				break;
			case "C":
				refProProcess.numWorkerCChange -= 1;
				StartCoroutine(waitToMinusWorker (refProProcess.timeProcessC));
				break;
			}
		}
	}

	IEnumerator waitToMinusWorker(float ProcessTime){
		yield return new WaitForSeconds (ProcessTime);
		refProProcess.WorkerToBeAssigned++;
	}

	public void WorkerInputChange(string process){
		inputWorker = int.Parse(gameObject.GetComponent<InputField> ().text);
		if (inputWorker <= (refProProcess.WorkerToBeAssigned + currentWorker)) {
			numWorkerChanged = inputWorker - currentWorker;
			refProProcess.WorkerToBeAssigned -= numWorkerChanged;
			currentWorker = inputWorker;
		} else {
			numWorkerChanged = refProProcess.WorkerToBeAssigned;
			currentWorker += numWorkerChanged; 
			gameObject.GetComponent<InputField> ().text = currentWorker.ToString ();
			refProProcess.WorkerToBeAssigned = 0;
		}
		switch (process){
		case "A":
			refProProcess.numWorkerAChange += numWorkerChanged;
			break;
		case "B":
			refProProcess.numWorkerBChange += numWorkerChanged;
			break;
		case "C":
			refProProcess.numWorkerCChange += numWorkerChanged;
			break;
		}
	}

	void workerReadyWorking(){
		refProProcess.AIsNotWorking = currentWorker;
		refProProcess.BIsNotWorking = currentWorker;
		refProProcess.CIsNotWorking = currentWorker;
	}

	// Use this for initialization
	void Start () {
		refProProcess = GameObject.Find ("ProductInProcess").GetComponent<ProductInProcess>();
		currentWorker = 3;
		gameObject.GetComponent<InputField> ().text = currentWorker.ToString ();
		workerReadyWorking ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
