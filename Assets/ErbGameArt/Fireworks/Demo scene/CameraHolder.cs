using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class CameraHolder : MonoBehaviour
{
    public float nameUpTime = 2;
    public string[] names;
    public GameObject name_text;
    List<GameObject> namesList = new List<GameObject>();
    List<GameObject> fireworks = new List<GameObject>();
    int name_id = 0;
    //GUI
    float windowDpi;
    private GUIStyle screenGUI = new GUIStyle();
    public GameObject[] Prefabs;
    private int Prefab;
    private GameObject Instance;
    public GameObject nameRoot;
    public Model model;
    public enum Model
    {
        add,
        _switch
    }
    void Start ()
    {
        if (Screen.dpi < 1) windowDpi = 1;
        if (Screen.dpi < 200) windowDpi = 1;
        else windowDpi = Screen.dpi / 200f;
        screenGUI.fontSize = (int)(15f * windowDpi);
        screenGUI.normal.textColor = new Color(0.5f, 0f, 0f);
		//Counter(+1);
        //for(int i = 0; i < 10; i++)
        //{
        //    names[i] = "name" + i;
        //}
    }
	
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(5 * windowDpi, 5 * windowDpi, 110 * windowDpi, 30 * windowDpi), "Previous effect"))
    //    {
    //        Counter(-1);
    //    }
    //    if (GUI.Button(new Rect(120 * windowDpi, 5 * windowDpi, 110 * windowDpi, 30 * windowDpi), "Next effect"))
    //    {
    //        Counter(+1);
    //    }
    //}

     void Update()
    {
        if (Input.GetKeyUp("right"))
        {
            ShowAName(name_id);
            Counter(name_id);
            name_id++;
        }
        if (Input.GetKeyUp("left"))
        {
            //next scene
            SceneManager.LoadScene(1);
        }
    }
    void ShowAName(int id)
    {
        if (id == names.Length)
        {
            //names show over,show firework
            print("show fireworks");
            //foreach (GameObject firework in Prefabs)
            //{
            //    GameObject obj = Instantiate(firework);

            //    print("fireworks name:" + obj.name);
            //}
            foreach(GameObject firework in fireworks)
            {
                ParticleSystem particle =
                firework.GetComponent<ParticleSystem>();
                particle.loop = true;
                particle.Play();
                
            }
            foreach (GameObject nameobj in namesList)
            {
                Destroy(nameobj);
            }
        }
        else if(id<names.Length)
        {
            GameObject newName = Instantiate(name_text);
            newName.name = "name_" + id;
            newName.GetComponent<Text>().text = names[id];
            newName.transform.parent = nameRoot.transform;
            namesList.Add(newName);
            StartCoroutine(NameAnim(newName));
        }
    }
    IEnumerator NameAnim(GameObject name)
    {
        float targetPosY = 300 - name_id * 100;
        float posY = -700;
        RectTransform rect = name.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, posY,0);
        rect.transform.localScale = new Vector3(1, 1, 1);
        for(int i = 0; i < 24*nameUpTime; i++)
        {
            yield return new WaitForSeconds(1 / 24);
            posY += (targetPosY+700) / (24 * nameUpTime);
            rect.anchoredPosition = new Vector2(0, posY);
        }
        yield return 0;
    }
    void Counter(int count)
    {
     //   Destroy(Instance, 1f);
         Prefab = count;
     
        if (Prefab > Prefabs.Length - 1)
        {
            return;
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
        if (Instance != null)
        {
            if (model == Model.add)
            {

            }
            else if (model == Model._switch)
            {
                Instance.GetComponent<ParticleSystem>().maxParticles = 0;
            }
        }
        {
            Instance = Instantiate(Prefabs[Prefab]);
            fireworks.Add(Instance);
           // Destroy(Instance,5f);
            print(Prefabs[Prefab].name);
            
        }
    }
}


