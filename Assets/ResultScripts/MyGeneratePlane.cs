using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class MyGeneratePlane : MonoBehaviour {
    
    public GameObject planePrefab;
    
    Dictionary<string, ARPlaneAnchorGameObject> planes = 
		new Dictionary<string,ARPlaneAnchorGameObject> ();
	bool planeVisible = true;

	// Use this for initialization
	void Start () {
		UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
        UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
        UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;
	}
    
    void OnDestroy() {
		foreach (KeyValuePair<string, ARPlaneAnchorGameObject> plane in planes) {
			GameObject.Destroy (plane.Value.gameObject);
		}
		planes.Clear ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddAnchor(ARPlaneAnchor arPlaneAnchor){
        GameObject go = CreatePlane(arPlaneAnchor);
        go.AddComponent<DontDestroyOnLoad>();
        go.SetActive(planeVisible);
        
        ARPlaneAnchorGameObject arpag = new ARPlaneAnchorGameObject();
        arpag.planeAnchor = arPlaneAnchor;
        arpag.gameObject = go;
        
        planes.Add(arPlaneAnchor.identifier, arpag);
    }
    
    GameObject CreatePlane(ARPlaneAnchor arPlaneAnchor){
        GameObject plane = GameObject.Instantiate(planePrefab);
        plane.name = arPlaneAnchor.identifier;
        return UpdatePlane(plane, arPlaneAnchor);
    }
    
    GameObject UpdatePlane(GameObject plane, ARPlaneAnchor arPlaneAnchor){
        plane.transform.position = UnityARMatrixOps.GetPosition(arPlaneAnchor.transform);
        plane.transform.rotation = UnityARMatrixOps.GetRotation(arPlaneAnchor.transform);
        
        MeshFilter mf = plane.GetComponentInChildren<MeshFilter>();
        if(mf != null){
            mf.gameObject.transform.localScale = new Vector3(
            arPlaneAnchor.extent.x * 0.1f,
            arPlaneAnchor.extent.y * 0.1f,
            arPlaneAnchor.extent.z * 0.1f);
            
            mf.gameObject.transform.localPosition = new Vector3(
            arPlaneAnchor.center.x,
            arPlaneAnchor.center.y,
            -arPlaneAnchor.center.z);
        }
        return plane;
    }
    
    public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor){
        if(planes.ContainsKey(arPlaneAnchor.identifier)){
            ARPlaneAnchorGameObject arpag = planes[arPlaneAnchor.identifier];
            UpdatePlane(arpag.gameObject, arPlaneAnchor);
            arpag.planeAnchor = arPlaneAnchor;
            planes[arPlaneAnchor.identifier] = arpag;
        }
    }
    
    public void RemoveAnchor(ARPlaneAnchor arPlaneAnchor){
        if(planes.ContainsKey(arPlaneAnchor.identifier)){
            ARPlaneAnchorGameObject arpag = planes[arPlaneAnchor.identifier];
            GameObject.Destroy(arpag.gameObject);
            planes.Remove(arPlaneAnchor.identifier);
        }
    }
    
    public void SetPlaneVisible(bool planeVisible) {
		this.planeVisible = planeVisible;
		foreach (KeyValuePair<string, ARPlaneAnchorGameObject> plane in planes) {
			plane.Value.gameObject.SetActive (planeVisible);
		}
	}
    public int GetPlanesCount() {
		return planes.Count;
	}
}
