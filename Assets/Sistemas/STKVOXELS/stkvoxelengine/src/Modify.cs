using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Modify : MonoBehaviour
{
	public List<string> blocosatuais = new List<string> ();
	public GameObject previewcube;
	public GameObject previewmestadecubo;


	private GameObject instp;
	public Vector3 actpos = new Vector3 (0, 0, 0);
	public Vector3 lastvalidposition;
	public bool spawned;

	public int chosencolor = 0;

	Vector2 rot;

	public World workingworld; //colocado por quem vai fazer o spawn disso.
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.C)){
			chosencolor++;
			if(chosencolor > 8){
				chosencolor = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			if (instp) {
				Destroy (instp);
			}
			instp = (GameObject)Instantiate (previewcube, this.transform.position, Quaternion.identity);
				
		}
		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			if (instp) {
				Destroy (instp);
			}
			instp = (GameObject)Instantiate (previewmestadecubo, this.transform.position, Quaternion.identity);

		}
		if (spawned == false) {
			spawned = true;
			instp = (GameObject)Instantiate (previewcube, this.transform.position, Quaternion.identity);
		}
		if (spawned) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, 100)) {
				instp.SetActive (true);


				if (actpos != VoxelTerrain.GetPrevPosBlock (hit, true)) {
					
						if (VoxelTerrain.GetValidPositiontoPlaceBlock (hit)) {

							instp.gameObject.GetComponent<Renderer> ().material.color = Color.green;

							instp.transform.position = VoxelTerrain.GetPrevPosBlock (hit, true);
							actpos = VoxelTerrain.GetPrevPosBlock (hit, true);
							lastvalidposition = actpos;
							Quaternion quat = Quaternion.FromToRotation (Vector3.up, hit.normal);
							instp.transform.rotation = new Quaternion (quat.x, quat.y, quat.z, quat.w);

						} else {
							instp.gameObject.GetComponent<Renderer> ().material.color = Color.red;
							//instp.transform.position = VoxelTerrain.GetPrevPosBlock (hit, true);
							instp.transform.position = lastvalidposition;
							instp.gameObject.GetComponent<Renderer> ().material.color = Color.green;


							actpos = VoxelTerrain.GetPrevPosBlock (hit, true);

							Quaternion quat = Quaternion.FromToRotation (Vector3.up, hit.normal);
							instp.transform.rotation = new Quaternion (quat.x, quat.y, quat.z, quat.w);
						}
				
				}
			} else {
				instp.SetActive (false);
			}
			if(Input.GetKeyDown(KeyCode.E)){
				instp.transform.Rotate(new Vector3(instp.transform.rotation.x,instp.transform.rotation.y + 90,instp.transform.rotation.z));
			}
		}
		if(Input.GetKeyDown(KeyCode.P)){
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, 100)) {
				if (VoxelTerrain.GetValidPositiontoPlaceBlock (hit)) {
					VoxelTerrain.paintblock (hit, chosencolor.ToString());
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward,out hit, 100 ))
			{
				if (VoxelTerrain.GetValidPositiontoPlaceBlock (hit)) {
					//VoxelTerrain.SetBlock (hit, new Blockmetade {quatcasorodarbloco = instp.transform.rotation,ischanging = true}, true);
					WorldPos pos = VoxelTerrain.GetBlockPos(hit,true);
					Vector3 newpos = new Vector3 (pos.x, pos.y, pos.z);

					Chunk chunkverify = voxelload.verifychunk (workingworld,(int)instp.transform.position.x, (int)instp.transform.position.y, (int)instp.transform.position.z);
					if (chunkverify != null) {
						Block bloco = VoxelTerrain.SetBlock (hit, new BuildingBlock {
							blockposition = newpos,
							blockrotation = instp.transform.rotation, color = chosencolor.ToString()
						}, true, instp.transform.position);
						blocosatuais.Add (bloco.blocktipe);
						if (bloco != null) {
							Debug.Log ("Esta função está funcionando: " + bloco.blocktipe);
							//Instantiate (previewmestadecubo, hit.point, instp.transform.rotation);
						}
					} else {
						Debug.Log ("Chunk inválido");

					}
				} else {
					Debug.Log ("cant place here");

				}
			}
		}

		rot= new Vector2(
			rot.x + Input.GetAxis("Mouse X") * 3,
			rot.y + Input.GetAxis("Mouse Y") * 3);

		transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

		transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
		transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
	
	
	
	
	
	
	
	
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward,out hit, 100 ))
			{
				Block block = VoxelTerrain.GetBlock(hit, false);

				if (block.blocktipe == "grass") {
					Debug.Log ("ITS GRASS!");
				} else {
					Debug.Log (block.blocktipe);
					Debug.Log ("called");
				}
			}
		}

		rot= new Vector2(
			rot.x + Input.GetAxis("Mouse X") * 3,
			rot.y + Input.GetAxis("Mouse Y") * 3);

		transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

		transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
		transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
	}
	
	
	
	}
