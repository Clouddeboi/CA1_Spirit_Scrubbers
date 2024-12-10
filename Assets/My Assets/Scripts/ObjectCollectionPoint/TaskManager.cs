using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string taskName;
        public string requiredTag;
        public int requiredAmount = 1;
        public int currentAmount = 0;
        public bool isCompleted = false;
    }

    [SerializeField]
    private List<Task> tasks = new List<Task>();

    public void UpdateTaskProgress(string itemTag)
    {
        //Loop through all tasks in the list
        foreach (Task task in tasks)
        {
            //Check if the task is not completed and the item's tag matches the required tag for the task
            if (!task.isCompleted && task.requiredTag == itemTag)
            {
                //If you need multiple items for a task, it tracks them here
                task.currentAmount++;

                //If the collected items are >= to the required amount
                if (task.currentAmount >= task.requiredAmount)
                {
                    //Mark the task as done and debug in console
                    task.isCompleted = true;
                    Debug.Log($"Task '{task.taskName}' completed!");
                }

                //Debugs the amount needed to finish task and how many are already collected
                Debug.Log($"Task '{task.taskName}' progress: {task.currentAmount}/{task.requiredAmount}");
                return;
            }
        }
        //Indicates that there is not task for a specific item
        Debug.Log($"No task found for item tag: {itemTag}");
    }

    //Method to check if all tasks are completed
    public bool AreAllTasksComplete()
    {
        //Loop through each task in the tasks list
        foreach (Task task in tasks)
        {
            //If any task is not completed, return false
            if (!task.isCompleted)
            {
                return false;
            }
        }

        //If all tasks are completed, return true
        return true;
    }

    public List<Task> GetTasks()
    {
        return tasks;
    }
}
