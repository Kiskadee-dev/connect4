
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InputManager : MonoBehaviour {
	[Header("Opções")]
	public bool Team = true;
	public int range = 3;
	public int numplaced;
	public int numMax = 16;
	public bool Player1Won;
	public bool Player2Won;
	public bool RegraComum = true;

	[Header("Player Scores")]
	public int Player1Score = 0;
	public int Player2Score = 0;

	[Header("User Interface Opções")]
	public Toggle CheckboxRegras;

	[Header("User Interface Scores")]
	public Text PScore1UI;
	public Text PScore2UI;

	[Header("User Interface Won/Lost")]
	public Text Player1NotifyWon;
	public Text Player2NotifyWon;
	public Text EmpateNotify;

	[Header("User Interface Turns")]
	public Text Player1TurnUI;
	public Text Player2TurnUI;

	[Header("Game Manager and Options")]
	public OpenOptions Options;
	public GM GameManager;

	bool SetandoVariaveisAwake;
	// Use this for initialization
	void Awake(){
		SetandoVariaveisAwake = true;
		GameManager = GameObject.Find ("GM").GetComponent<GM> ();
		if(PlayerPrefs.HasKey("RegrasComuns")){
			int ba = PlayerPrefs.GetInt("RegrasComuns");
			if(ba == 0){
				RegraComum = false;
				CheckboxRegras.interactable = false;
				CheckboxRegras.isOn = false;
				CheckboxRegras.interactable = true;

			}else{
				RegraComum = true;
				CheckboxRegras.interactable = false;
				CheckboxRegras.isOn = true;
				CheckboxRegras.interactable = true;
			}
		}else{
			PlayerPrefs.SetInt("RegrasComuns",1);
			RegraComum = true;
			CheckboxRegras.isOn = true;
		}
	}

	void Start () {
		SetandoVariaveisAwake = false;
	}
	public void TrocarRegraComum(){
		if (!SetandoVariaveisAwake) {
			RegraComum = !RegraComum;
			if (RegraComum) {
				PlayerPrefs.SetInt ("RegrasComuns", 1);
			}else {
				PlayerPrefs.SetInt ("RegrasComuns", 0);
			}
		}
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
									if (CondicaoDeVitoria (bloco, "Azul", hit, pos)) {
										vencedor ("Azul");
									}
								} else {
									if (CondicaoDeVitoria (bloco, "Vermelho", hit, pos)) {
										vencedor ("Vermelho");
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
	public bool CondicaoDeVitoria(Block bloco, string time, RaycastHit hit,WorldPos pos){
		if (RegraComum) {
			if (bloco.blocktipe == "Neutro") {
				if (time == "Azul") {
					Block colocado = VoxelTerrain.SetBlock (hit, new BlockAzul{ blockposition = new Vector3 (pos.x, pos.y, pos.z) });
					numplaced++;
					int me = 1;
					int hor = Horizontal (colocado, time);
					int vert = Vertical (colocado, time);
					int diag = Diagonal (colocado, time);
					int diag2 = Diagonal2 (colocado, time);

					if (hor + me == 4 || vert + me == 4 || diag + me == 4 || diag2 + me == 4) {
						return true;
					}
					Team = !Team;
					if (Team) {
						Debug.Log ("Player 1 turn (blue)");
					} else {
						Debug.Log ("Player 2 turn (red)");

					}
					return false;
				} else {
					Block colocado = VoxelTerrain.SetBlock (hit, new BlockVermelho{ blockposition = new Vector3 (pos.x, pos.y, pos.z) });
					numplaced++;
					int me = 1;
					int hor = Horizontal (colocado, time);
					int vert = Vertical (colocado, time);
					int diag = Diagonal (colocado, time);
					int diag2 = Diagonal2 (colocado, time);

					if (hor + me == 4 || vert + me == 4 || diag + me == 4 || diag2 + me == 4) {
						return true;
					}
					Team = !Team;
					if (Team) {
						Debug.Log ("Player 1 turn (blue)");
					} else {
						Debug.Log ("Player 2 turn (red)");

					}
					return false;
				}
			}
		} else {
			if (bloco.blocktipe == "Neutro") {
				if (time == "Azul") {
					Block colocado = VoxelTerrain.SetBlock (hit, new BlockAzul{ blockposition = new Vector3 (pos.x, pos.y, pos.z) });
					numplaced++;
					int me = 1;
					int hor = Horizontal (colocado, time);
					int vert = Vertical (colocado, time);
					int diag = Diagonal (colocado, time);
					int diag2 = Diagonal2 (colocado, time);

					if (hor + vert + diag + diag2 + me >= 4) {
						return true;
					}
					Team = !Team;
					if (Team) {
						Debug.Log ("Player 1 turn (blue)");
					} else {
						Debug.Log ("Player 2 turn (red)");

					}
					return false;
				} else {
					Block colocado = VoxelTerrain.SetBlock (hit, new BlockVermelho{ blockposition = new Vector3 (pos.x, pos.y, pos.z) });
					numplaced++;
					int me = 1;
					int hor = Horizontal (colocado, time);
					int vert = Vertical (colocado, time);
					int diag = Diagonal (colocado, time);
					int diag2 = Diagonal2 (colocado, time);

					if (hor + vert + diag + diag2 + me >= 4) {
						return true;
					}
					Team = !Team;
					if (Team) {
						Debug.Log ("Player 1 turn (blue)");
					} else {
						Debug.Log ("Player 2 turn (red)");

					}
					return false;
				}
			}
		}
		return false;
	}
	public void vencedor(string time){
		if (time == "Azul") {
			Player1Won = true;
			Player1Score++;
			PScore1UI.text = Player1Score.ToString ();
			Player1NotifyWon.gameObject.SetActive (true);
			StartCoroutine (RestartGameTimed ());
		} else {
			Player2Won = true;
			Player2Score++;
			PScore2UI.text = Player1Score.ToString ();
			Player2NotifyWon.gameObject.SetActive (true);
			StartCoroutine (RestartGameTimed ());
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
		return soma1;
	
	}
	public int Diagonal2(Block colocado,string time){
		int adjDiagLup = WinConditionDiagLeft (colocado, time) -1;
		int adjDiagLdown = WinConditionDiagDownLeft (colocado, time) - 1;

		int soma2 = adjDiagLup + adjDiagLdown;
		if (soma2 < 0) {
			soma2 = 0;
		}
		return soma2;
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
