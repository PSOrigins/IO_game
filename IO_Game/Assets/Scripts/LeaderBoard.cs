using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    private Player[] players { get; set; }
    private List<string> names;
    public GameObject Prefab;
    private const int PREFAB_HEIGHT = 50;

    void Start() {
        init();
        LoadBoard();
        Sort();
        LoadBoard();
        InitBoard();
    }

    void InitBoard(){
        for(int i = 0; i < players.Length; i++){
            var newEl = Instantiate(Prefab);
            newEl.SetActive(true);
            newEl.transform.parent = Prefab.transform.parent;

            newEl.GetComponent<TextMeshPro>().text = players[i].Name;
            newEl.transform.GetChild(0).GetComponent<TextMeshPro>().text = players[i].score.ToString();

            var pos = Prefab.transform.position;
            pos.y -= PREFAB_HEIGHT * i; 
            newEl.transform.position = pos;
        }
    }

    void LoadBoard(){
        string p1 = string.Empty;

        for(int i = 0; i < players.Length; i++){
            p1 += i+1.ToString() + ". " + players[i].ToString() + "\n";
        }
        Debug.Log(p1);
    }

    void init(){
        
        
        
        names = new List<string>() {
            "Motiejus", 
            "Dovydas", 
            "Gvidas", 
            "Gytis", 
            "Jonas", 
            "Pijus", 
            "Matas", 
            "Patrik", 
            "Viktor"
        };
        players = new Player[names.Count];
        for(int i = 0; i < names.Count; i++){
            players[i].Name = names[i];
            players[i].score = Random.Range(0, 35500);
        }
        
    }

    

    void Sort(){
        for(int i = 0; i < players.Length-1; i++){
            for(int j = i + 1; j < players.Length; j++){
                if(players[j].score < players[i].score){
                    var temp = new Player();

                    temp.Name = players[j].Name;
                    temp.score = players[j].score;

                    players[j].Name = players[i].Name;
                    players[j].score = players[i].score;


                    players[i].Name = temp.Name;
                    players[i].score = temp.score;
                }
            }

        }
    }

    private struct Player
    {
        public string Name;
        public int score;

        public override string ToString(){
            return Name + "\t" + score.ToString(); 
        }
    }
}
