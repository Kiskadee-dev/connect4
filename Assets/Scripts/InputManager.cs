
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InputManager : MonoBehaviour {
	public bool Team = true;
	public int range = 3;
	public int numplaced;
	public int numMax = 16;
	public bool Player1Won;
	public bool Player2Won;

	public int Player1Score = 0;
	public int Player2Score = 0;
	public Text PScore1UI;
	public Text PScore2UI;

	public Text Player1NotifyWon;
	public Text Player2NotifyWon;
	public Text EmpateNotify;

	public Text Player1TurnUI;
	public Text Player2TurnUI;

	public OpenOptions Options;
	public GM GameManager;
	// Use this for initialization
	void Awake(){
		GameManager = GameObject.Find ("GM").GetComponent<GM> ();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Team) {
			Player2TurnUI.gameObject.SetActive (false);
			Player1TurnUI.gameObject.SetActive (true);
		} else {
			Player2TurnUI.gameObject.SetActive (true);
			Player1TurnUI.gameObject.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if (!Options.opened) {
				if (!EventSystem.current.IsPointerOverGameObject ()) {
					if (!Player1Won && !Player2Won) {
						RaycastHit hit;
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (ray, out hit)) {
							Block bloco = VoxelTerrain.GetBlock (hit);
							if (bloco != null) {
								WorldPos pos = VoxelTerrain.GetBP (hit);
								if (Team) {
								
									if (bloco.blocktipe == "Neutro") {
										Block colocado = VoxelTerrain.SetBlock (hit, new BlockAzul{ blockposition = new Vector3 (pos.x, pos.y, pos.z) });
										numplaced++;
										int me = 1;
										int hor = Horizontal (colocado, "Azul");
										int vert = Vertical (colocado, "Azul");
										int diag = Diagonal (colocado, "Azul");

										if (hor + me == 4 || vert + me == 4 || diag + me == 4) {
											Debug.Log ("Player 1 vence" + hor + "," + vert + "," + diag);
											Player1Won = true;
											Player1Score++;
											PScore1UI.text = Player1Score.ToString ();
											Player1NotifyWon.gameObject.SetActive (true);
											StartCoroutine (RestartGameTimed ());
										}

										int total = me + hor + vert + diag; //+ adjDiagLdown;

										Team = !Team;
										if (Team) {
											Debug.Log ("Player 1 turn (blue)");
										} else {
											Debug.Log ("Player 2 turn (red)");

										}
									}
								} else {
									if (bloco.blocktipe == "Neutro") {
										Block colocado = VoxelTerrain.SetBlock (hit, new BlockVermelho{ blockposition = new Vector3 (pos.x, pos.y, pos.z) });
										numplaced++;
										int me = 1;
										int hor = Horizontal (colocado, "Vermelho");
										int vert = Vertical (colocado, "Vermelho");
										int diag = Diagonal (colocado, "Vermelho");

										if (hor + me == 4 || vert + me == 4 || diag + me == 4) {
											Debug.Log ("Player 2 vence" + hor + "," + vert + "," + diag);
											Player2Won = true;
											Player2Score++;
											PScore2UI.text = Player2Score.ToString ();
											Player2NotifyWon.gameObject.SetActive (true);
											StartCoroutine (RestartGameTimed ());
										}
										int total = me + hor + vert + diag; //+ adjDiagLdown;						}
						
										Team = !Team;
										if (Team) {
											Debug.Log ("Player 1 turn (blue)");
										} else {
											Debug.Log ("Player 2 turn (red)");

										}

									}
								}

							}
						}
						if (numplaced == numMax) {
							if (!Player1Won && !Player2Won) {
								Debug.Log ("Empate");
								EmpateNotify.gameObject.SetActive (true);
								StartCoroutine (RestartGameTimed ());
							}
						}
					}
				}
			}
		}
	}
	public IEnumerator RestartGameTimed(){
		yield return new WaitForSeconds (4);
		RestartGame ();
	}
	public void RestartGame(){
		Player1NotifyWon.gameObject.SetActive (false);
		Player2NotifyWon.gameObject.SetActive (false);
		EmpateNotify.gameObject.SetActive (false);
		numplaced = 0;
		Player1Won = false;
		Player2Won = false;
		Team = true;
		GameManager.RecriarTabuleiro ();
	}

	public int Horizontal(Block colocado,string time){
		int adjr = WinConditionRight (colocado,time) -1;
		int adjl = WinConditionLeft (colocado, time) -1;

		int soma = adjr + adjl;
		if (soma < 0) {
			soma = 0;
		}
		return soma;
	}
	public int Vertical(Block colocado,string time){
		int adjup = WinConditionUp (colocado, time) -1;
		int adjdown = WinConditionDown (colocado, time) -1;

		int soma = adjup + adjdown;
		if (soma < 0) {
			soma = 0;
		}
		return soma;
	}

	public int Diagonal(Block colocado,string time){
		int adjDiagRup = WinConditionDiagRight (colocado, time) -1;
		int adjDiagRdown = WinConditionDiagDownRight (colocado, time) -1;

		int soma1 = adjDiagRup + adjDiagRdown;
		if (soma1 < 0) {
			soma1 = 0;
		}

		int adjDiagLup = WinConditionDiagLeft (colocado, time) -1;
		int adjDiagLdown = WinConditionDiagDownLeft (colocado, time) - 1;
	
		int soma2 = adjDiagLup + adjDiagLdown;
		if (soma2 < 0) {
			soma2 = 0;
		}


		int result = soma1 + soma2;
		return result;
	
	}





	public int WinConditionRight(Block bloco, string team){
		int AdjacentRight = 0;
		Vector3 pos = bloco.blockposition;
		//Right
		for (int i = (int)pos.x; i < (int)pos.x+range; i++) {
			Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (i, pos.y, pos.z);
			Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
			if (vizinho != null) {
				if (vizinho.blocktipe != "Neutro") {
					if (vizinho.blocktipe != "ar") {
						if (vizinho.blocktipe != team) {
							return AdjacentRight;
						}
						AdjacentRight++;

					}
				} else {
					return AdjacentRight;
				}
			}
		}
		return AdjacentRight;

	}

	public int WinConditionLeft(Block bloco, string team){
		int AdjacentLeft = 0;
		Vector3 pos = bloco.blockposition;
		//Right
		for (int i = (int)pos.x; i > (int)pos.x-range; i--) {
			Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (i, pos.y, pos.z);
			Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
			if (vizinho != null) {
				if (vizinho.blocktipe != "Neutro") {
					if (vizinho.blocktipe != "ar") {
						if (vizinho.blocktipe != team) {
							return AdjacentLeft;
						}
						AdjacentLeft++;

					}
				} else {
					return AdjacentLeft;
				}
			}
		}
		return AdjacentLeft;

	}

	public int WinConditionUp(Block bloco, string team){
		int AdjacentUp = 0;
		Vector3 pos = bloco.blockposition;
		//Right
		for (int i = (int)pos.z; i < (int)pos.z+range; i++) {
			Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (pos.x, pos.y, i);
			Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
			if (vizinho != null) {
				if (vizinho.blocktipe != "Neutro") {
					if (vizinho.blocktipe != "ar") {
						if (vizinho.blocktipe != team) {
							return AdjacentUp;
						}
						AdjacentUp++;

					}
				} else {
					return AdjacentUp;
				}
			}
		}
		return AdjacentUp;

	}

	public int WinConditionDown(Block bloco, string team){
		int AdjacentDown = 0;
		Vector3 pos = bloco.blockposition;
		//Right
		for (int i = (int)pos.z; i > (int)pos.z-range; i--) {
			Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (pos.x, pos.y, i);
			Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
			if (vizinho != null) {
				if (vizinho.blocktipe != "Neutro") {
					if (vizinho.blocktipe != "ar") {
						if (vizinho.blocktipe != team) {
							return AdjacentDown;
						}
						AdjacentDown++;

					}
				} else {
					return AdjacentDown;
				}
			}
		}
		return AdjacentDown;

	}




	public int WinConditionDiagRight(Block bloco, string team){
		int AdjacentRight = 0;
		Vector3 pos = bloco.blockposition;
		//Right
		int z = (int)pos.z -1;
		for (int i = (int)pos.x; i < (int)pos.x + range; i++) {
			z++;
				Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (i, pos.y, z);
				Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
				if (vizinho != null) {
					if (vizinho.blocktipe != "Neutro") {
						if (vizinho.blocktipe != "ar") {
							if (vizinho.blocktipe != team) {
								return AdjacentRight;
							}
							AdjacentRight++;

						}
					} else {
						return AdjacentRight;
					}
				}
			}

		return AdjacentRight;

	}


	public int WinConditionDiagDownRight(Block bloco, string team){
		int AdjacentRight = 0;
		Vector3 pos = bloco.blockposition;
		//Right
		int z = (int)pos.z +1;
		for (int i = (int)pos.x; i > (int)pos.x - range; i--) {
			z--;
			Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (i, pos.y, z);
			Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
			if (vizinho != null) {
				if (vizinho.blocktipe != "Neutro") {
					if (vizinho.blocktipe != "ar") {
						if (vizinho.blocktipe != team) {
							return AdjacentRight;
						}
						AdjacentRight++;

					}
				} else {
					return AdjacentRight;
				}
			}
		}

		return AdjacentRight;

	}










	public int WinConditionDiagLeft(Block bloco, string team){
		int AdjacentRight = 0;
		Vector3 pos = bloco.blockposition;
		//Left
		int z = (int)pos.z -1;
		for (int i = (int)pos.x; i > (int)pos.x-range; i--) {
			z++;
				Vector3 CheckNeighbours;
				CheckNeighbours = new Vector3 (i, pos.y, z);
				Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
				if (vizinho != null) {

					if (vizinho.blocktipe != "Neutro") {
						if (vizinho.blocktipe != "ar") {

							if (vizinho.blocktipe != team) {
								return AdjacentRight;
							}
							AdjacentRight++;
						}
					} else {
						return AdjacentRight;
					}
				}
			}


		return AdjacentRight;

	}


	public int WinConditionDiagDownLeft(Block bloco, string team){
		int AdjacentRight = 0;
		Vector3 pos = bloco.blockposition;
		//Left
		int z = (int)pos.z +1;

		for (int i = (int)pos.x; i < (int)pos.x + range; i++) {
			z--;
			Vector3 CheckNeighbours;
			CheckNeighbours = new Vector3 (i, pos.y, z);
			Block vizinho = GameManager.world.GetBlock ((int)CheckNeighbours.x, (int)CheckNeighbours.y, (int)CheckNeighbours.z);
			if (vizinho != null) {
				if (vizinho.blocktipe != "Neutro") {
					if (vizinho.blocktipe != "ar") {
						if (vizinho.blocktipe != team) {
							return AdjacentRight;
						}
						AdjacentRight++;

					}
				} else {
					return AdjacentRight;
				}
			}
		}

		return AdjacentRight;

	}
}
