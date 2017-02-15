using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInProcess : MonoBehaviour {

	public float targetTime;
	public float gameTime = 0;
	public float timePreProcess;
	public float timeProcessA;
	public float timeProcessB;
	public float timeProcessC;
	public int productA = 0;
	public int productB = 0;
	public int productC = 0;
	public int availableProductA = 0;
	public int availableProductB = 0;
	public int availableProductC = 0;
	public int numWorkerAChange =0;
	public int numWorkerBChange =0;
	public int numWorkerCChange =0;
	public int finalProduct = 0;
	public int AIsNotWorking;
	public int BIsNotWorking;
	public int CIsNotWorking;
	bool preProcessNotCD = true;
	public int WorkerToBeAssigned;
	float timeA;
	float timeB;
	float timeC;
	float timePre;

	IEnumerator doPreProcess(){
		if ((gameTime < targetTime) && (preProcessNotCD)) {
			preProcessNotCD = false;
			yield return new WaitForSeconds (timePreProcess);
			productA++;
			availableProductA++;
			preProcessNotCD = true;
		}
	}

	IEnumerator doAProcess(){
		if (gameTime < targetTime) {
			if (availableProductA > 0) {
				AIsNotWorking += numWorkerAChange;
				numWorkerAChange = 0;
				if (AIsNotWorking > 0) {
					int currentProcessA = Mathf.Min (availableProductA, AIsNotWorking);
					AIsNotWorking -= currentProcessA;
					availableProductA -= currentProcessA;
					yield return new WaitForSeconds (timeProcessA);
					productA -= currentProcessA;
					//Debug.Log (productA-availableProductA);
					productB += currentProcessA;
					availableProductB += currentProcessA;
					AIsNotWorking += currentProcessA;
				}
			}
		}
	}

	IEnumerator doBProcess(){
		if (gameTime < targetTime) {
			if (availableProductB > 0) {
				BIsNotWorking += numWorkerBChange;
				numWorkerBChange = 0;
				if (BIsNotWorking > 0) {
					int currentProcessB = Mathf.Min (availableProductB, BIsNotWorking);
					BIsNotWorking -= currentProcessB;
					availableProductB -= currentProcessB;
					//Debug.Log ("availableProductB: "+ availableProductB);
					yield return new WaitForSeconds (timeProcessB);
					productB -= currentProcessB;
					//Debug.Log (currentProcessB);
					//Debug.Log ("productB:" + productB);
					productC += currentProcessB;
					availableProductC += currentProcessB;
					BIsNotWorking += currentProcessB;
				}
			}
		}
	}

	IEnumerator doCProcess(){
		if (gameTime < targetTime) {
			if (availableProductC > 0) {
				CIsNotWorking += numWorkerCChange;
				numWorkerCChange = 0;
				if (CIsNotWorking > 0) {
					int currentProcessC = Mathf.Min (availableProductC, CIsNotWorking);
					CIsNotWorking -= currentProcessC;
					availableProductC -= currentProcessC;
					yield return new WaitForSeconds (timeProcessC);
					productC -= currentProcessC;
					finalProduct += currentProcessC;
					CIsNotWorking += currentProcessC;
				}
			}
		}
	}

	void Start(){
		timeA = timeProcessA;
		timeB = timeProcessB;
		timeC = timeProcessC;
		timePre = timePreProcess;
	}

	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
		StartCoroutine (doPreProcess());
		StartCoroutine (doAProcess());
		StartCoroutine (doBProcess());
		StartCoroutine (doCProcess());

		timeProcessA = Random.Range(timeA,timeA*2);
		timeProcessB = Random.Range(timeB,timeB*2);
		timeProcessC = Random.Range(timeC,timeC*2);
		timePreProcess = Random.Range (timePre, timePre*2);

//		Debug.Log ("productA: "+ productA);
//		Debug.Log ("productB: "+ productB);
//		Debug.Log ("productC: "+ productC);
//		Debug.Log ("finalProduct: "+ finalProduct);
//		Debug.Log ("gameTime: "+ gameTime);
	}
}
