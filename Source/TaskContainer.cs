using Godot;
using System.Collections.Generic;
using System;

/*

This is the code for the Tasks on the HUD

*/
public class TaskContainer : HBoxContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    const int NUM_TASKS = 3;
    private Panel[] tasks;
    public FoodItem[] todoFoods;
    private List<FoodItem> queue = new List<FoodItem>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tasks = new Panel[NUM_TASKS];
        todoFoods = new FoodItem[NUM_TASKS];
        for(int i = 0; i < NUM_TASKS; i++) {
            tasks[i] = GetNode<Panel>("Task" + (i + 1));
            todoFoods[i] = FoodItem.NONE;
        }
        UpdateTaskList();
    }

    // Call this whenever task list is updated
    private void UpdateTaskList() {
        for (int task = 0; task < NUM_TASKS; task++) {
            if(todoFoods[task] == FoodItem.NONE) { // If index empty
                for(int next = task + 1; next < NUM_TASKS; next++) { // Check array
                    if(todoFoods[next] != FoodItem.NONE) {
                        todoFoods[task] = todoFoods[next];
                    }
                }

                if (queue.Count > 0) {
                    todoFoods[task] = queue[0]; // Grab first in queue
                    queue.RemoveAt(0);
                }
            }

            if(todoFoods[task] != FoodItem.NONE) {
                tasks[task].GetNode<TextureRect>("Task Texture").Visible = true;
                tasks[task].GetNode<TextureRect>("Task Texture").Texture = todoFoods[task].GetTexture();
            } else {
                tasks[task].GetNode<TextureRect>("Task Texture").Visible = false;
            }
            tasks[task].GetNode<Label>("Task Name").Text = "Task #" + (task + 1) + ": " + todoFoods[task].Name;  
            
        }
    }

    // Signal when a task is completed (NOT CREATED)
    public void _on_Task_Completed() {
        
    }
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
