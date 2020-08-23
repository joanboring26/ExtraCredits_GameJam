using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Linq;
using System;
using TMPro;
public class toDoListScript : MonoBehaviour
{
    [SerializeField] TextMeshPro listText;
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

    void Awake()
    {
        //listOfTasks = new List<string>();
        //foreach(ToDoTasks task in (ToDoTasks[]) ToDoTasks.GetValues(typeof(ToDoTasks)))
        //{
        //    listOfTasks.Add(GetTaskDescription(task));
        //}
        var tasks = (IEnumerable<ToDoTasks>)Enum.GetValues(typeof(ToDoTasks));
        listOfTasks = tasks.Select(task => GetTaskDescription(task)).ToList();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
        
    }

    public void CrossOut(ToDoTasks task)
    {

    }

}
