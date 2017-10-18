using UnityEngine;
using System.Collections;

public class ShapeManager : MonoBehaviour {

	public static ShapeManager instance;

	public GameObject[] shapes;
	public GameObject puffs;
	public GameObject particles;
	public GameObject pieces;

	public AudioSource wrongShape;
	public AudioSource correctShape;
	public AudioSource combo;
	public AudioSource breakShape;
	public AudioSource speedUp;
	public AudioSource newRecord;
	public AudioSource noPoints;
	public AudioSource particleSound;
	public AudioSource bubbleSound;
	public AudioSource drumRoll;
	public AudioSource cymbalCrash;

	public GameObject shapesContainer;
	public GameObject particlesContainer;

	private GameObject instantiatedParticle;
	private GameObject instantiatedPuff;
	private GameObject instantiatedPieces;
	private GameObject shape;
	private GameObject bubble;

	public float shapeSpeed;
	public float shapeSpawnRate;
	public float shapeHoldTimer = 0;

	void Awake () {

		instance = this;

		//Set audio clips
		correctShape = GetComponents<AudioSource> () [0];
		wrongShape = GetComponents<AudioSource> () [1];
		breakShape = GetComponents<AudioSource> () [2];
		combo = GetComponents<AudioSource> () [3];
		newRecord = GetComponents<AudioSource> () [4];
		noPoints = GetComponents<AudioSource> () [5];
		particleSound = GetComponents<AudioSource> () [6];
		bubbleSound = GetComponents<AudioSource> () [7];
		drumRoll = GetComponents<AudioSource> () [8];
		cymbalCrash = GetComponents<AudioSource> () [9];

		//Set shape speed
		shapeSpeed = 2f;
		shapeSpawnRate = 1.2f;

	}
		
	//-----------------------------------------------------
	//CREATE SHAPES
	//-----------------------------------------------------

	public void InstantiateShape () {
	
		shape = (GameObject) Instantiate (
			shapes[Random.Range(0,29)], 
			new Vector3(0,0,0), 
			Quaternion.identity
		);
		shape.transform.parent = shapesContainer.transform;

		shape.GetComponent<Animation> ().Play ("shape_popin");
		if (!shape.GetComponents<AudioSource>()[0].isPlaying) {
			shape.GetComponents<AudioSource> () [0].Play ();
		}
	}

	public void ExplodeShape(GameObject shape) {
		
		Vector3 position = shape.transform.position;

		breakShape.Play ();

		//Instantiate particle
		instantiatedParticle = (GameObject) Instantiate (particles, position, Quaternion.identity);
		instantiatedParticle.transform.parent = particlesContainer.transform;

		//Instantiate puff
		instantiatedPuff = (GameObject) Instantiate (puffs, position, Quaternion.identity);
		instantiatedPuff.transform.parent = particlesContainer.transform;

		particleSound.Play ();

		Destroy (instantiatedParticle, 1f);
		Destroy (instantiatedPuff, 1f);

		Destroy (shape);
		shapeHoldTimer = 0;

	}


}