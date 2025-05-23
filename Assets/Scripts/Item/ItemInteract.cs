using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class ItemInteract : MonoBehaviour
{
    [SerializeField] private Scriptable itemdata;

    public Scriptable Itemdata
    {
        get { return  itemdata; }
    }
    // Start is called before the first frame update
    public string GetDescription()
    {
        return itemdata.Description();
    }

    


}
