using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WorkerMovement : MonoBehaviour {

	public GameObject[] workerToInstantiate;
	public Sprite[] workerImage;
	GameObject[] AWorkerGroup;
	GameObject[] BWorkerGroup;
	GameObject[] CWorkerGroup;
	int selectedGroup;
	int selectedWorker = 1;
	public UnityEvent MinusAPlusB;
	public UnityEvent MinusBPlusC;
	public UnityEvent MinusBPlusA;
	public UnityEvent MinusCPlusB;

	void insWorkerGroup(GameObject[] WorkerGroup, int whichWorkerGroup){
		Vector3 groupPosition = workerToInstantiate [whichWorkerGroup].transform.position;
		for (int i=0; i<9; i++){
			WorkerGroup[i]=(GameObject)Instantiate(workerToInstantiate[whichWorkerGroup], groupPosition + new Vector3 (i, 0, 0) * 30f, Quaternion.identity, gameObject.transform);
			WorkerGroup [i].GetComponent<Image> ().sprite = workerImage [i];
			if ((i < whichWorkerGroup*3) || (i > (whichWorkerGroup+1)*3 - 1)) {
				WorkerGroup [i].GetComponent<Image> ().enabled = false;
			}
		}
	}

	void getGroup(){
		if (AWorkerGroup [selectedWorker-1].GetComponent<Image> ().enabled == true) {
			selectedGroup = 0;
		} else if (BWorkerGroup [selectedWorker-1].GetComponent<Image> ().enabled == true) {
			selectedGroup = 1;
		} else {
			selectedGroup = 2;
		}

	}

	// Use this for initialization
	void Start () {
		AWorkerGroup = new GameObject[9];
		BWorkerGroup = new GameObject[9];
		CWorkerGroup = new GameObject[9];

		insWorkerGroup (AWorkerGroup, 0);
		insWorkerGroup (BWorkerGroup, 1);
		insWorkerGroup (CWorkerGroup, 2);

	}

	void getKey(string keyPressed){
		if (Input.GetKeyDown (keyPressed)) {
			selectedWorker = int.Parse (keyPressed);
			getGroup ();
		}
	}

	void goLeft(){
		if (Input.GetKeyUp ("left")) {
			switch (selectedGroup){
			case 0:
				break;
			case 1:
				BWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = false;
				AWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = true;
				selectedGroup = 0;
				MinusBPlusA.Invoke();
				break;
			case 2:
				CWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = false;
				BWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = true;
				selectedGroup = 1;
				MinusCPlusB.Invoke();
				break;
			}
		}
	}

	void goRight(){
		if (Input.GetKeyUp ("right")) {
			switch (selectedGroup){
			case 0:
				AWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = false;
				BWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = true;
				selectedGroup = 1;
				MinusAPlusB.Invoke();
				break;
			case 1:
				BWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = false;
				CWorkerGroup [selectedWorker - 1].GetComponent<Image> ().enabled = true;
				selectedGroup = 2;
				MinusBPlusC.Invoke();
				break;
			case 2:
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		getKey ("1");
		getKey ("2");
		getKey ("3");
		getKey ("4");
		getKey ("5");
		getKey ("6");
		getKey ("7");
		getKey ("8");
		getKey ("9");
		goLeft ();
		goRight ();
	}
}
