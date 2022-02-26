//搭载在uimanger上 与pokemonmanager作用类似，储存 mousetype（第几次点击） 以及第一次点击按钮所携带的skill属性或pokemon属性以便与第二次点击的按钮进行属性交换
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlemsg;
public class PokemonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

namespace Game_Manager {

    public class Game_Scene_Manager : System.Object {
        private static Game_Scene_Manager instance;
        public int mousetype = 0;
        public int previousindex;
        public PlayerPokemon previousbuttonpokemon;
        public PlayerPokemon lastbuttonpokemon;
        public static Game_Scene_Manager GetInstance() {
            if (instance == null) {
                instance = new Game_Scene_Manager();
            }
            return instance;
        }
    }

}
