using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBindBoxCollider : MonoBehaviour {

    void Awake()
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(GetComponent<RectTransform>().rect.width,
            GetComponent<RectTransform>().rect.height, 1);
        collider.isTrigger = true;
    }
}
