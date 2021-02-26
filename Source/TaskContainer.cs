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

	public List<FoodItem> todoFoods;
	private List<FoodItem> queue = new List<FoodItem>();
	
	private List<Control> Tasks;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tasks = new List<Control>();
		PopulateTasks();
		todoFoods = new List<FoodItem>(Tasks.Count);
		PopulateTodoFoods();
		UpdateTaskList();
	}
	
	private void PopulateTasks(){
		
		for(int i = 0; i < GetChildren().Count; i++){
			Control TemporaryControl;
			TemporaryControl = GetChild<Control>(i);
			Tasks.Add(TemporaryControl);
			GD.Print(GetChild<Control>(i).Name);

		}
		
	}

	private void PopulateTodoFoods(){
		
		for(int i = 0; i < GetChildren().Count; i++){
			todoFoods.Add(FoodItem.NONE.GetRandomFoodItem());
		}
		
	}

	

	// Call this whenever task list is updated
	private void UpdateTaskList() {
		for (int task = 0; task < Tasks.Count; task++) {
			//GD.Print(todoFoods.Count);
			//GD.Print(task);
			//GD.Print(Tasks.Count);
			if(todoFoods[task] == FoodItem.NONE) { // If index empty
				for(int next = task + 1; next < Tasks.Count; next++) { // Check array
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
				Tasks[task].GetNode<Panel>("SampleTask").GetNode<TextureRect>("TaskTexture").Visible = true;
				Tasks[task].GetNode<Panel>("SampleTask").GetNode<TextureRect>("TaskTexture").Texture = todoFoods[task].GetTexture();
			} else {
				Tasks[task].GetNode<Panel>("SampleTask").GetNode<TextureRect>("TaskTexture").Visible = false;
			}
			Tasks[task].GetNode<Panel>("SampleTask").GetNode<Label>("TaskName").Text = "Task #" + (task + 1) + ": " + todoFoods[task].Name;  
			
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
