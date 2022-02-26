using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Battlemsg;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//挂在Battle上，战斗逻辑
//玩家操作->receiveChoose(sendMsgAsync->receiveMsgAsync->finishRound)->玩家操作
public class BattleController : MonoBehaviour
{
    //public static GameObject web = GameObject.FindWithTag("websocket");
    //public static websocket ws = web.GetComponent<websocket>();
    // Start is called before the first frame update
    int pokemonbutton_trigger = 0;//为0时Text显示的是精灵，为1时显示的是技能
    public Method method;
    public bool hasSwitch=false;
    public static int teamIndex = 0;
    public static Battlemsg.Team team;
    public static Battlemsg.PokemonInBattle opponent;
    public static int round = 0;//后面添加回合数显示要用,暂时不用
    public static GameObject potionimage;
    public static GameObject skillimage;
    public static GameObject pokemonimage;
    public static GameObject bagimage;
    public static GameObject ballimage;
    //
    //
    //
    GameObject user1damagevalue;
    GameObject user2damagevalue;
    //
    //
    //
    GameObject catchmessage;
    GameObject user1changeeffect;
    GameObject user2changeeffect;
    GameObject catcheffect;
    GameObject timemessage;
    //
    //
    //
    GameObject messageBox;
    GameObject message;
    GameObject escapeButton;
    GameObject pokemonbutton;
    GameObject pokemon;
    GameObject web;
    public GameObject mypokemon;
    public GameObject enemypokemon;
    GameObject buff;

    GameObject bagbutton;
    GameObject propbutton;

    public GameObject hp1;
    public GameObject hp2;
    websocket ws;
    bool ifDie = false;
    void Start()
    {
        //
        //
        //
        user1damagevalue = GameObject.Find("user1damagevalue");
        user2damagevalue = GameObject.Find("user2damagevalue");
        //
        //
        //
        user1changeeffect = GameObject.Find("BlueBuff1");
        user2changeeffect = GameObject.Find("BlueBuff2");
        catcheffect = GameObject.Find("BluePortal");
        catchmessage = GameObject.Find("catchMessage");
        timemessage = GameObject.Find("timeMessage");
        //
        //
        //
        messageBox = GameObject.Find("MessageBox");
        message = GameObject.Find("Message");
        messageBox.SetActive(false);
        method = GameObject.Find("Method").GetComponent<Method>();
        bagbutton = GameObject.Find("bagButton");
        pokemonbutton = GameObject.Find("pokemonButton");
        escapeButton = GameObject.Find("escapeButton");
        potionimage = GameObject.Find("potionImage");
        skillimage = GameObject.Find("skillImage");
        pokemonimage = GameObject.Find("pokemonImage");
        ballimage = GameObject.Find("ballImage");
        web = GameObject.Find("websocket");
        ws = web.GetComponent<websocket>();
        buff = GameObject.Find("userState");
        String battlemsg = GameObject.Find("websocket").GetComponent<websocket>().getBattleMsg();
        Battlemsg.Battle battle = JsonMapper.ToObject<Battlemsg.Battle>(battlemsg);
        //后面调用用户数据都从team中获取
        if (battle.teams[0].player.id == websocket.id)
        {
            teamIndex = 0;
        }
        else
        {
            teamIndex = 1;
        }
        team = battle.teams[teamIndex];
        //opponent存敌方当前宝可梦对象信息
        opponent = battle.teams[teamIndex ^ 1].pokemon[battle.teams[teamIndex ^ 1].currentPokemonIndex];
        //回合数
        round = 0;

        pokemon = GameObject.Find("Main Camera");
        //出战宝可梦初始化(loadPokemon函数中已经添加本脚本中mypokemon和enemypokemon的设置,不需要在调用完loadPokemon再Find敌我宝可梦了)
        pokemon.GetComponent<LoadPokemon>().loadMypokemon(team.pokemon[team.currentPokemonIndex]);
        pokemon.GetComponent<LoadPokemon>().loadEnemypokemon(opponent);
        //血条初始化
        hp1.GetComponent<User1Bar>().hpchange(team.pokemon[team.currentPokemonIndex]);
        hp2.GetComponent<User2Bar>().hpchange(opponent);
        //组件初始化
        pokemonimage.GetComponent<Pokemonupdate>().Pokemonchange(team.pokemon);
        pokemonimage.GetComponent<Pokemonupdate>().Show(team.pokemon, team.currentPokemonIndex);
        skillimage.GetComponent<Skillupdate>().Skillchange(team.pokemon[team.currentPokemonIndex]);
        skillimage.GetComponent<Skillupdate>().Show(team.pokemon[team.currentPokemonIndex]);
        potionimage.GetComponent<Potionupdate>().Potionchange(team.player.potionList);
        potionimage.GetComponent<Potionupdate>().Show(team.player.potionList);
        ballimage.GetComponent<Ballupdate>().Ballchange(team.player.ballList);
        ballimage.GetComponent<Ballupdate>().Show(team.player.ballList);
        skillimage.SetActive(true);
        pokemonimage.SetActive(false);
        potionimage.SetActive(false);
        ballimage.SetActive(false);
        user1changeeffect.GetComponent<ParticleSystem>().Stop();
        user2changeeffect.GetComponent<ParticleSystem>().Stop();
        catcheffect.GetComponent<ParticleSystem>().Stop();
        //
        //抓捕信息初始化
        //
        catchmessage.GetComponent<Text>().text = "";
        timemessage.GetComponent<Countdown>().startcount = true;
    }
    public void BallButton()
    {//精灵球背包按钮
        ballimage.SetActive(true);
        skillimage.SetActive(false);
        potionimage.SetActive(false);
        pokemonimage.SetActive(false);
        pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "技能";
        pokemonbutton_trigger = 1;
    }
    public void PokemonButton()
    {//精灵背包按钮点击函数
        if (pokemonbutton_trigger == 0)
        {
            pokemonimage.SetActive(true);
            skillimage.SetActive(false);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "技能";
            pokemonbutton_trigger = 1;
            return;
        }
        if (pokemonbutton_trigger == 1)
        {
            pokemonimage.SetActive(false);
            skillimage.SetActive(true);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "精灵";
            pokemonbutton_trigger = 0;
            return;
        }
    }

    public void PotionButton()
    {//药水道具背包按钮点击函数
        pokemonimage.SetActive(false);
        skillimage.SetActive(false);
        potionimage.SetActive(true);
        ballimage.SetActive(false);
        pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "技能";
        pokemonbutton_trigger = 1;
    }

    async public void Receivechoose(String a) {
        //接收后端信息函数
        timemessage.GetComponent<Countdown>().startcount = false;
        //此时所有有关玩家操作的按钮都为不可点击
        skillimage.GetComponent<Skillupdate>().Setflag(false);
        pokemonimage.GetComponent<Pokemonupdate>().Setflag(false);
        ballimage.GetComponent<Ballupdate>().Setflag(false);
        potionimage.GetComponent<Potionupdate>().Setflag(false);
        escapeButton.GetComponent<Button>().interactable = false;
        if (ifDie)
        {
            ifDie = false;
            await ws.sendMsgAsync("battle\n0 " + team.currentPokemonIndex + " " + a.Split('\n')[1]);
        }
        else {
            await ws.sendMsgAsync(a);
        }
        String answer = await ws.receiveMsgAsync();//接收后端信息
        String[] message = answer.Split('\n');
        StartCoroutine(finishRound(message));
    }
    public void escapButton()
    {
        Receivechoose("battle\n1 0");
    }
    IEnumerator finishRound(String[] message)
    {//回合结束结算,在接收完后端信息后调用
        Battle[] allBattleMsg = LitJson.JsonMapper.ToObject<Battle[]>(message[1]);
        if (allBattleMsg.Length == 0)
        {
            battleEnd();
            yield break;
        }
        Battle finalBattleMsg = allBattleMsg[allBattleMsg.Length-1];
        team = finalBattleMsg.teams[teamIndex];
        //组件更新
        skillimage.GetComponent<Skillupdate>().Skillchange(team.pokemon[team.currentPokemonIndex]);
        pokemonimage.GetComponent<Pokemonupdate>().Pokemonchange(team.pokemon);
        potionimage.GetComponent<Potionupdate>().Potionchange(team.player.potionList);
        ballimage.GetComponent<Ballupdate>().Ballchange(team.player.ballList);
        skillimage.GetComponent<Skillupdate>().Setflag(false);
        pokemonimage.GetComponent<Pokemonupdate>().Setflag(false);
        potionimage.GetComponent<Potionupdate>().Setflag(false);
        ballimage.GetComponent<Ballupdate>().Setflag(false);
        skillimage.SetActive(true);
        pokemonimage.SetActive(false);
        potionimage.SetActive(false);
        ballimage.SetActive(false);

        //敌方信息更新
        opponent = finalBattleMsg.teams[teamIndex ^ 1].pokemon[finalBattleMsg.teams[teamIndex ^ 1].currentPokemonIndex];
        //播放动画和血条变化
        foreach (Battle battleMsg in allBattleMsg)
        {
            yield return printAnimation(battleMsg);
            printAllBuff(battleMsg);
            yield return printDeltaHP(battleMsg);
        }
        skillimage.GetComponent<Skillupdate>().Show(team.pokemon[team.currentPokemonIndex]);
        pokemonimage.GetComponent<Pokemonupdate>().Show(team.pokemon, team.currentPokemonIndex);
        potionimage.GetComponent<Potionupdate>().Show(team.player.potionList);
        ballimage.GetComponent<Ballupdate>().Show(team.player.ballList);

        //战斗结束判断
        if (message[0] == "battle_end")
        {
            int winnerIndex = Int32.Parse(message[2]);
            //播放精灵死亡动画
            if (winnerIndex == teamIndex)
            {
                //Debug.Log("You win!");
                enemypokemon.GetComponent<Behaviour>().setbehaviour(5);
                yield return new WaitForSeconds(2);
            }
            else
            {
                //Debug.Log("You lose!");
                mypokemon.GetComponent<Behaviour>().setbehaviour(5);
                yield return new WaitForSeconds(2);
            }
            battleEnd();
            yield break;
        }
        if (opponent.curAttribution.HP == 0)
        {//播放敌方宝可梦死亡动画
            enemypokemon.GetComponent<Behaviour>().setbehaviour(5);
            yield return new WaitForSeconds(2);
        }
        if (team.pokemon[team.currentPokemonIndex].curAttribution.HP == 0)
        {//播放己方宝可梦死亡动画,同时显示精灵背包中的所有精灵
            //播放己方宝可梦死亡动画
            mypokemon.GetComponent<Behaviour>().setbehaviour(5);
            yield return new WaitForSeconds(2);
            //显示精灵背包中的所有精灵
            skillimage.SetActive(false);
            pokemonimage.SetActive(true);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            bagbutton.GetComponent<Button>().interactable=false;
            pokemonbutton.GetComponent<Button>().interactable = false;
            pokemonimage.GetComponent<Pokemonupdate>().setNodie(false);
            escapeButton.GetComponent<Button>().interactable = true;
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "技能";
            pokemonbutton_trigger = 1;
            ifDie = true;
        }
        else
        {//己方精灵没有死亡,更新技能信息
            escapeButton.GetComponent<Button>().interactable = true;
            //skillimage.GetComponent<Skillupdate>().Setflag(true);
            //pokemonimage.GetComponent<Pokemonupdate>().Setflag(true);
            skillimage.SetActive(true);
            pokemonimage.SetActive(false);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "精灵";
            pokemonbutton_trigger = 0;
        }
        //
        //抓捕信息初始化
        //
        catchmessage.GetComponent<Text>().text = "";
        timemessage.GetComponent<Countdown>().startcount = true;
        //
    }
    async public void battleEnd()
    {
        //恢复地图场景中的相机
        String answer = await ws.receiveMsgAsync();
        String[] messages = answer.Split('\n');
        if(messages[0] == "PVP_calc")
        {
            int score = Int32.Parse(messages[1]);
            websocket ws = web.GetComponent<websocket>();
            if (score > websocket.rank)
            {
                message.GetComponent<Text>().text = "战斗胜利！当前分数：" + score;
            }
            else
            {
                message.GetComponent<Text>().text = "战斗失败！当前分数：" + score;
            }
            websocket.rank = score;
        }
        else
        {
            if (answer == "") {
                Debug.Log(answer);
            }
            String[] things = LitJson.JsonMapper.ToObject<String[]>(messages[1]);
            int exp = Int32.Parse(messages[2]);
            if (things.Length == 0 && exp == 0)
            {
                message.GetComponent<Text>().text = "战斗失败！";
            }
            else
            {
                String content = "";
                if (things.Length > 0)
                {
                    content += "恭喜你获得了";
                    for (int i = 0; i < things.Length; i++)
                    {
                        content += things[i] + " ";
                    }
                    content += "！\n";
                }
                content += "背包中宝可梦的经验增加了" + exp + "点！";
                message.GetComponent<Text>().text = content;
            }
        }
        messageBox.SetActive(true);
    }
    public void switchScene()
    {
        method.SetFmAndVm(true);
        method.SetPlayer(true);
        SceneManager.LoadScene(method.mapPath);
    }
    public void chooseNewPokemon(int choose)
    {//（当且仅当）当前宝可梦死亡，切换宝可梦
        hasSwitch = true;
        team.currentPokemonIndex = choose;
        Destroy(mypokemon);
        user1changeeffect.GetComponent<ParticleSystem>().Play();
        pokemon.GetComponent<LoadPokemon>().loadMypokemon(team.pokemon[team.currentPokemonIndex]);
        hp1.GetComponent<User1Bar>().hpchange(team.pokemon[team.currentPokemonIndex]);
        //组件更新
        skillimage.GetComponent<Skillupdate>().Skillchange(team.pokemon[team.currentPokemonIndex]);
        skillimage.GetComponent<Skillupdate>().Show(team.pokemon[team.currentPokemonIndex]);
        pokemonimage.GetComponent<Pokemonupdate>().Pokemonchange(team.pokemon);
        pokemonimage.GetComponent<Pokemonupdate>().Show(team.pokemon, team.currentPokemonIndex);
        potionimage.GetComponent<Potionupdate>().Potionchange(team.player.potionList);
        potionimage.GetComponent<Potionupdate>().Show(team.player.potionList);
        ballimage.GetComponent<Ballupdate>().Ballchange(team.player.ballList);
        ballimage.GetComponent<Ballupdate>().Show(team.player.ballList);
        pokemonimage.GetComponent<Pokemonupdate>().setNodie(true);
        skillimage.SetActive(true);
        pokemonimage.SetActive(false);
        ballimage.SetActive(false);
        potionimage.SetActive(false);
        bagbutton.GetComponent<Button>().interactable = true;
        pokemonbutton.GetComponent<Button>().interactable = true;
        escapeButton.GetComponent<Button>().interactable = true;
    }
    IEnumerator printDeltaHP(Battle battleMsg)
    {//播放血条变化
        Team myTeam = battleMsg.teams[teamIndex];
        Team opponent = battleMsg.teams[teamIndex ^ 1];
        if(myTeam.deltaHP != 0)
        {   //
            //
            //
            user1damagevalue.GetComponent<DamageValue>().Valueupdate(myTeam.deltaHP);
            //
            //
            //
            hp1.GetComponent<User1Bar>().hpchange(myTeam.pokemon[myTeam.currentPokemonIndex]);
            yield return new WaitForSeconds(1);
            //
            //
            //
            user1damagevalue.GetComponent<DamageValue>().Valueupdate(0);
            //
            //
            //
        }
        if (opponent.deltaHP != 0)
        {
            //
            //
            //
            user2damagevalue.GetComponent<DamageValue>().Valueupdate(opponent.deltaHP);
            //
            //
            //
            hp2.GetComponent<User2Bar>().hpchange(opponent.pokemon[opponent.currentPokemonIndex]);
            yield return new WaitForSeconds(1);
            //
            //
            //
            user2damagevalue.GetComponent<DamageValue>().Valueupdate(0);
            //
            //
            //
        }
    }
    IEnumerator printAnimation(Battle battleMsg)
    {//播放宝可梦动画
        Team myTeam = battleMsg.teams[teamIndex];
        Team opponent = battleMsg.teams[teamIndex ^ 1];
        String []detailAnimation;
        if(myTeam.animation != null)
        {
            detailAnimation = myTeam.animation.Split(' ');
            switch (detailAnimation[0])
            {
                //播放己方攻击动画
                case "4":
                    catcheffect.GetComponent<ParticleSystem>().Play();
                    if (detailAnimation[2] == "true")
                    {
                        catchmessage.GetComponent<Text>().text = "抓捕成功";
                        Debug.Log("使用了" + detailAnimation[1] + "\n抓捕成功");
                    }
                    else
                    {
                        catchmessage.GetComponent<Text>().text = "抓捕失败";
                        Debug.Log("使用了" + detailAnimation[1] + "\n抓捕失败");
                    }
                    break;

                case "5":
                    //int skillIndex = Int32.Parse(detailAnimation[1]);
                    if (detailAnimation[2] == "攻击") {
                        mypokemon.GetComponent<Behaviour>().setbehaviour(1);
                    }
                    else{
                        mypokemon.GetComponent<Behaviour>().setbehaviour(4);
                    }
                    yield return new WaitForSeconds(1);
                    break;
                case "2":
                    if (hasSwitch)
                    {//死亡切换，前面已经切换过，此处不用重复切换，提高性能
                        hasSwitch = false;
                    }
                    else
                    {//主动切换，需在此处切换
                        Destroy(mypokemon);
                        user1changeeffect.GetComponent<ParticleSystem>().Play();
                        pokemon.GetComponent<LoadPokemon>().loadMypokemon(myTeam.pokemon[myTeam.currentPokemonIndex]);
                    }
                    hp1.GetComponent<User1Bar>().hpchange(myTeam.pokemon[myTeam.currentPokemonIndex]);
                    yield return new WaitForSeconds(1);
                    break;
                default:
                    break;
            }
        }
        else if(opponent.animation != null)
        {
            detailAnimation = opponent.animation.Split(' ');
            switch (detailAnimation[0])
            {
                //播放敌方攻击动画
                case "5":
                    //int skillIndex = Int32.Parse(detailAnimation[1]);
                    enemypokemon.GetComponent<Behaviour>().setbehaviour(1);
                    yield return new WaitForSeconds(1);
                    break;
                //敌方玩家切换宝可梦
                case "2":
                    Destroy(enemypokemon);
                    pokemon.GetComponent<LoadPokemon>().loadEnemypokemon(opponent.pokemon[opponent.currentPokemonIndex]);
                    hp2.GetComponent<User2Bar>().hpchange(opponent.pokemon[opponent.currentPokemonIndex]);

                    yield return new WaitForSeconds(1);
                    break;
                default: break;
            }
        }
    }
    //Buff编号 , 1 烧伤 2 冻伤 3 中毒 4 晕眩 5 疲惫 6 失明 7 强化攻击 8 强化防御 9 强化速度 10 弱化攻击 11 弱化防御 12 弱化速度
    void printAllBuff(Battle battleMsg)
    {//打印Buff
        PokemonInBattle myPokemon = battleMsg.teams[teamIndex].pokemon[battleMsg.teams[teamIndex].currentPokemonIndex];
        PokemonInBattle opponent = battleMsg.teams[teamIndex ^ 1].pokemon[battleMsg.teams[teamIndex ^ 1].currentPokemonIndex];
        buff.GetComponent<Bufupdate>().Buffupdate(getBuffDic(myPokemon));
        buff.GetComponent<Bufupdate>().BuffupdateEnemy(getBuffDic(opponent));
    }
    Dictionary<int ,int> getBuffDic(PokemonInBattle pokemon)
    {//获取Buff信息
        Dictionary<int, int> allBuffs = new Dictionary<int, int>();
        int n = pokemon.abilityBuffs.Length;
        for (int i = 0; i < n; i++)
        {
            AbilityBuff buff = pokemon.abilityBuffs[i];
            int effect, data;
            if (buff != null)
            {
                switch (buff.effect)
                {
                    case "攻击": if (buff.data > 0) effect = 7; else effect = 10; break;
                    case "防御": if (buff.data > 0) effect = 8; else effect = 11; break;
                    case "速度": if (buff.data > 0) effect = 9; else effect = 12; break;
                    default: effect = 0; break;
                }
                data = Math.Abs(buff.data);
                if (effect != 0)
                    allBuffs.Add(effect, data);
            }
        }
        n = pokemon.controlBuffs.Length;
        for (int i = 0; i < n; i++)
        {
            ControlBuff buff = pokemon.controlBuffs[i];
            int effect, data;
            if (buff != null)
            {
                switch (buff.effect)
                {
                    case "晕眩": effect = 4; break;
                    case "疲惫": effect = 5; break;
                    case "失明": effect = 6; break;
                    case "免控": effect = 13;break;
                    default: effect = 0; break;
                }
                data = Math.Abs(buff.lasting);
                if (effect != 0)
                    allBuffs.Add(effect, data);
            }
        }
        n = pokemon.HPBuffs.Length;
        for (int i = 0; i < n; i++)
        {
            HPBuff buff = pokemon.HPBuffs[i];
            int effect, data;
            if (buff != null)
            {
                switch (buff.effect)
                {
                    case "烧伤": effect = 1; break;
                    case "冻伤": effect = 2; break;
                    case "中毒": effect = 3; break;
                    default: effect = 0; break;
                }
                data = Math.Abs(buff.lasting);
                if (effect != 0)
                    allBuffs.Add(effect, data);
            }
        }
        return allBuffs;
    }
    // Update is called once per frame
    void Update()
    {

    }
}