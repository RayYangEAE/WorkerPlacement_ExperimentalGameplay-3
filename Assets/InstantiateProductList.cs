using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateProductList : MonoBehaviour {
	public GameObject[] productToInstantiate;
	Vector3 proAPosition;
	Vector3 proBPosition;
	Vector3 proCPosition;
	Vector3 proDPosition;
	GameObject[] productAList;
	GameObject[] productBList;
	GameObject[] productCList;
	GameObject[] productDList;
	ProductInProcess RefProProcess;
		

	// Use this for initialization
	void Start () {
		RefProProcess = GameObject.Find("ProductInProcess").GetComponent<ProductInProcess>();

		proAPosition = productToInstantiate [0].transform.position;
		proBPosition = productToInstantiate [1].transform.position;
		proCPosition = productToInstantiate [2].transform.position;
		proDPosition = productToInstantiate [3].transform.position;

		productAList = new GameObject[50];
		productBList = new GameObject[50];
		productCList = new GameObject[50];
		productDList = new GameObject[50];



		for (int i=0; i<5; i++){
			for(int j=0; j<10; j++){
				productAList[i*10+j] = (GameObject)Instantiate(productToInstantiate[0], proAPosition + new Vector3 (j, -i, 0) * 30f, Quaternion.identity, gameObject.transform);
				productAList[i*10+j].name = "A" + (i).ToString () + (j).ToString();

				productBList[i*10+j] = (GameObject)Instantiate(productToInstantiate[1], proBPosition + new Vector3 (j, -i, 0) * 30f, Quaternion.identity, gameObject.transform);
				productBList[i*10+j].name = "B" + (i).ToString () + (j).ToString();

				productCList[i*10+j] = (GameObject)Instantiate(productToInstantiate[2], proCPosition + new Vector3 (j, -i, 0) * 30f, Quaternion.identity, gameObject.transform);
				productCList[i*10+j].name = "C" + (i).ToString () + (j).ToString();

				productDList[i*10+j] = (GameObject)Instantiate(productToInstantiate[3], proDPosition + new Vector3 (j, -i, 0) * 30f, Quaternion.identity, gameObject.transform);
				productDList[i*10+j].name = "D" + (i).ToString () + (j).ToString();

				productAList[i*10+j].GetComponent<Image> ().enabled = false;
				productBList[i*10+j].GetComponent<Image> ().enabled = false;
				productCList[i*10+j].GetComponent<Image> ().enabled = false;
				productDList[i*10+j].GetComponent<Image> ().enabled = false;
			}
		}
	}

	void UpdateList(GameObject[] productList, int availableProduct, int totalProduct, Color availableProColor, Color ProcessColor){
		for (int i = 0; i < 50; i++) {
			if (i < availableProduct) {
				productList [i].GetComponent<Image> ().enabled = true;
				productList [i].GetComponent<Image> ().color = availableProColor;
			} else if (i < totalProduct) {
				productList [i].GetComponent<Image> ().enabled = true;
				productList [i].GetComponent<Image> ().color = ProcessColor;
			} else {
				productList [i].GetComponent<Image> ().enabled = false;
			}
		}
	}
	
	void UpdateDList(){
		for (int i=0; i< Mathf.Min(RefProProcess.finalProduct, 50); i++){
			productDList[i].GetComponent<Image> ().enabled = true;
			productDList[i].GetComponent<Image> ().color = Color.green;
		}

	}

	void Update(){
		UpdateList (productAList, RefProProcess.availableProductA, RefProProcess.productA, Color.grey, Color.red);
		UpdateList (productBList, RefProProcess.availableProductB, RefProProcess.productB, Color.grey, Color.blue);
		UpdateList (productCList, RefProProcess.availableProductC, RefProProcess.productC, Color.grey, Color.yellow);

		UpdateDList ();
	}

}
