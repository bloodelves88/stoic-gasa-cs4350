using UnityEngine;
using System.Collections;

public class Stage_Sequence_1_2 : Stage_Sequence_Helper {
	
	////////////////////////////////////////////////////
	//
	//		Constants
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Script variables
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		MonoBehaviour functions
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Custom functions for other scripts
	//
	////////////////////////////////////////////////////

	/*
	 * Constructor
	 * 
	 * Note: 
	 * Always need an empty constructor for 
	 * derived classes
	 * */
	public Stage_Sequence_1_2 (Stage_Sequence_Reader given_squence_reader) 	{
		
		/*
		 * Record the references
		 * */
		attached_reader = given_squence_reader;
		
		/*
		 * Change the indices
		 * */
		sequence_stage_index = 1;
		sequence_wave_index = 2;
	}
	
	/*
	 * Process an update
	 * */
	public override void process_update () 	{
		
		/*
		 * Check if the sequence has been 
		 * initialized
		 * */
		if(sequence_phase_index == 0)
		{
			/*
			 * Sequence has not been initizlied
			 * */
			
			/*
			 * Initialize 
			 * */
			spawn_list.Add(spawnBear(new Vector3( 12f,0f,279f), 1)); // target6
			spawn_list.Add(spawnBear(new Vector3(-39f,0f,290f ), 1)); // target7
			spawn_list.Add(spawnBear(new Vector3(-17f,0f,264f), 2));
			
			sequence_phase_index = 1;
		}
		else if(sequence_phase_index == 1)
		{
			/*
			 * Wave is complete if all the spawns are 
			 * destroyed
			 * */
			if(check_spawn_list_empty())
			{
				sequence_phase_index = 2;
			}
		}
		else if(sequence_phase_index == 2)
		{
			/*
			 * Move on to next sequence
			 * */
			attached_reader.attached_sequence = new Stage_Sequence_1_3(attached_reader);
		}
	}
	
	////////////////////////////////////////////////////
	//
	//		Functions available via inheritance
	//
	////////////////////////////////////////////////////
	
	////////////////////////////////////////////////////
	//
	//		Private functions for this script
	//
	////////////////////////////////////////////////////
}
