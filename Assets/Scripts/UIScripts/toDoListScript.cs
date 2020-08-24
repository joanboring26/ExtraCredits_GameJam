using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Linq;
using System;
using TMPro;
public class toDoListScript : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> listMeshes = null;
    [SerializeField] AudioClip crossOutSound;
    private AudioSource sound;
    public enum ToDoTasks
    {
        [Description("Pick up food at the supermarket.")]
        SUPERMARKET,
        [Description("Pick up new bangles from the blacksmith.")]
        BLACKSMITH,
        [Description("Receive a blessing from the holy church.")]
        CHURCH,
        [Description("Shake down a peasant who hasn't paid his taxes yet.")]
        TAXCOLLECTION,
        [Description("Attend the execution of a heretic.")]
        ATTENDEXECUTION
    }

    List<string> listOfTasks;
    List<GameObject> listItems;
    void Awake()
    {
        //listOfTasks = new List<string>();
        //foreach(ToDoTasks task in (ToDoTasks[]) ToDoTasks.GetValues(typeof(ToDoTasks)))
        //{
        //    listOfTasks.Add(GetTaskDescription(task));
        //}
        var tasks = (IEnumerable<ToDoTasks>)Enum.GetValues(typeof(ToDoTasks));
        listOfTasks = tasks.Select(task => GetTaskDescription(task)).ToList();
        for(int i = 0; i < listOfTasks.Count; i++)
            listOfTasks[i] = "-" + listOfTasks[i];
        listItems = new List<GameObject>();
        sound = GetComponent<AudioSource>();
        sound.clip = crossOutSound;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetTaskDescription(ToDoTasks task)
    {
        switch(task){
            case ToDoTasks.SUPERMARKET:
                return "Pick up food at the supermarket.";
            case ToDoTasks.BLACKSMITH:
                return "Pick up new bangles from the blacksmith.";
            case ToDoTasks.CHURCH:
                return "Receive a blessing from the holy church.";
            case ToDoTasks.TAXCOLLECTION:
                return "Shake down a peasant who hasn't paid his taxes.";
            case ToDoTasks.ATTENDEXECUTION:
                return "Attend the execution of a heretic.";
            default:
                return null;
        }
    }

    public void InitializeList()
    {
        /*for(int i = 0; i < listOfTasks.Count; i++){
            GameObject item = new GameObject();
            listItems.Add(item);
            item.transform.SetParent(GameObject.Find("UI").transform, false);
            item.AddComponent<CanvasRenderer>();
            TextMeshPro thisText = item.AddComponent<TextMeshPro>();
            thisText.SetText(listOfTasks[i]);
            thisText.fontSize = 15;
            thisText.font = listFont;
            Instantiate(item);
        }*/
        for(int i = 0; i < listMeshes.Count; i++)
        {
            listMeshes[i].SetText(listOfTasks[i]);
        }
    }

    public void CrossOut(ToDoTasks task)
    {
        int value = (int)task;
        listMeshes[value].fontStyle = FontStyles.Strikethrough;
        sound.Play();
    }

}
