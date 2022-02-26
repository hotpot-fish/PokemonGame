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
//����Battle�ϣ�ս���߼�
//��Ҳ���->receiveChoose(sendMsgAsync->receiveMsgAsync->finishRound)->��Ҳ���
public class BattleController : MonoBehaviour
{
    //public static GameObject web = GameObject.FindWithTag("websocket");
    //public static websocket ws = web.GetComponent<websocket>();
    // Start is called before the first frame update
    int pokemonbutton_trigger = 0;//Ϊ0ʱText��ʾ���Ǿ��飬Ϊ1ʱ��ʾ���Ǽ���
    public Method method;
    public bool hasSwitch=false;
    public static int teamIndex = 0;
    public static Battlemsg.Team team;
    public static Battlemsg.PokemonInBattle opponent;
    public static int round = 0;//������ӻغ�����ʾҪ��,��ʱ����
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
        //��������û����ݶ���team�л�ȡ
        if (battle.teams[0].player.id == websocket.id)
        {
            teamIndex = 0;
        }
        else
        {
            teamIndex = 1;
        }
        team = battle.teams[teamIndex];
        //opponent��з���ǰ�����ζ�����Ϣ
        opponent = battle.teams[teamIndex ^ 1].pokemon[battle.teams[teamIndex ^ 1].currentPokemonIndex];
        //�غ���
        round = 0;

        pokemon = GameObject.Find("Main Camera");
        //��ս�����γ�ʼ��(loadPokemon�������Ѿ���ӱ��ű���mypokemon��enemypokemon������,����Ҫ�ڵ�����loadPokemon��Find���ұ�������)
        pokemon.GetComponent<LoadPokemon>().loadMypokemon(team.pokemon[team.currentPokemonIndex]);
        pokemon.GetComponent<LoadPokemon>().loadEnemypokemon(opponent);
        //Ѫ����ʼ��
        hp1.GetComponent<User1Bar>().hpchange(team.pokemon[team.currentPokemonIndex]);
        hp2.GetComponent<User2Bar>().hpchange(opponent);
        //�����ʼ��
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
        //ץ����Ϣ��ʼ��
        //
        catchmessage.GetComponent<Text>().text = "";
        timemessage.GetComponent<Countdown>().startcount = true;
    }
    public void BallButton()
    {//�����򱳰���ť
        ballimage.SetActive(true);
        skillimage.SetActive(false);
        potionimage.SetActive(false);
        pokemonimage.SetActive(false);
        pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "����";
        pokemonbutton_trigger = 1;
    }
    public void PokemonButton()
    {//���鱳����ť�������
        if (pokemonbutton_trigger == 0)
        {
            pokemonimage.SetActive(true);
            skillimage.SetActive(false);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "����";
            pokemonbutton_trigger = 1;
            return;
        }
        if (pokemonbutton_trigger == 1)
        {
            pokemonimage.SetActive(false);
            skillimage.SetActive(true);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "����";
            pokemonbutton_trigger = 0;
            return;
        }
    }

    public void PotionButton()
    {//ҩˮ���߱�����ť�������
        pokemonimage.SetActive(false);
        skillimage.SetActive(false);
        potionimage.SetActive(true);
        ballimage.SetActive(false);
        pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "����";
        pokemonbutton_trigger = 1;
    }

    async public void Receivechoose(String a) {
        //���պ����Ϣ����
        timemessage.GetComponent<Countdown>().startcount = false;
        //��ʱ�����й���Ҳ����İ�ť��Ϊ���ɵ��
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
        String answer = await ws.receiveMsgAsync();//���պ����Ϣ
        String[] message = answer.Split('\n');
        StartCoroutine(finishRound(message));
    }
    public void escapButton()
    {
        Receivechoose("battle\n1 0");
    }
    IEnumerator finishRound(String[] message)
    {//�غϽ�������,�ڽ���������Ϣ�����
        Battle[] allBattleMsg = LitJson.JsonMapper.ToObject<Battle[]>(message[1]);
        if (allBattleMsg.Length == 0)
        {
            battleEnd();
            yield break;
        }
        Battle finalBattleMsg = allBattleMsg[allBattleMsg.Length-1];
        team = finalBattleMsg.teams[teamIndex];
        //�������
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

        //�з���Ϣ����
        opponent = finalBattleMsg.teams[teamIndex ^ 1].pokemon[finalBattleMsg.teams[teamIndex ^ 1].currentPokemonIndex];
        //���Ŷ�����Ѫ���仯
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

        //ս�������ж�
        if (message[0] == "battle_end")
        {
            int winnerIndex = Int32.Parse(message[2]);
            //���ž�����������
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
        {//���ŵз���������������
            enemypokemon.GetComponent<Behaviour>().setbehaviour(5);
            yield return new WaitForSeconds(2);
        }
        if (team.pokemon[team.currentPokemonIndex].curAttribution.HP == 0)
        {//���ż�����������������,ͬʱ��ʾ���鱳���е����о���
            //���ż�����������������
            mypokemon.GetComponent<Behaviour>().setbehaviour(5);
            yield return new WaitForSeconds(2);
            //��ʾ���鱳���е����о���
            skillimage.SetActive(false);
            pokemonimage.SetActive(true);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            bagbutton.GetComponent<Button>().interactable=false;
            pokemonbutton.GetComponent<Button>().interactable = false;
            pokemonimage.GetComponent<Pokemonupdate>().setNodie(false);
            escapeButton.GetComponent<Button>().interactable = true;
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "����";
            pokemonbutton_trigger = 1;
            ifDie = true;
        }
        else
        {//��������û������,���¼�����Ϣ
            escapeButton.GetComponent<Button>().interactable = true;
            //skillimage.GetComponent<Skillupdate>().Setflag(true);
            //pokemonimage.GetComponent<Pokemonupdate>().Setflag(true);
            skillimage.SetActive(true);
            pokemonimage.SetActive(false);
            potionimage.SetActive(false);
            ballimage.SetActive(false);
            pokemonbutton.transform.GetChild(0).GetComponent<Text>().text = "����";
            pokemonbutton_trigger = 0;
        }
        //
        //ץ����Ϣ��ʼ��
        //
        catchmessage.GetComponent<Text>().text = "";
        timemessage.GetComponent<Countdown>().startcount = true;
        //
    }
    async public void battleEnd()
    {
        //�ָ���ͼ�����е����
        String answer = await ws.receiveMsgAsync();
        String[] messages = answer.Split('\n');
        if(messages[0] == "PVP_calc")
        {
            int score = Int32.Parse(messages[1]);
            websocket ws = web.GetComponent<websocket>();
            if (score > websocket.rank)
            {
                message.GetComponent<Text>().text = "ս��ʤ������ǰ������" + score;
            }
            else
            {
                message.GetComponent<Text>().text = "ս��ʧ�ܣ���ǰ������" + score;
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
                message.GetComponent<Text>().text = "ս��ʧ�ܣ�";
            }
            else
            {
                String content = "";
                if (things.Length > 0)
                {
                    content += "��ϲ������";
                    for (int i = 0; i < things.Length; i++)
                    {
                        content += things[i] + " ";
                    }
                    content += "��\n";
                }
                content += "�����б����εľ���������" + exp + "�㣡";
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
    {//�����ҽ�������ǰ�������������л�������
        hasSwitch = true;
        team.currentPokemonIndex = choose;
        Destroy(mypokemon);
        user1changeeffect.GetComponent<ParticleSystem>().Play();
        pokemon.GetComponent<LoadPokemon>().loadMypokemon(team.pokemon[team.currentPokemonIndex]);
        hp1.GetComponent<User1Bar>().hpchange(team.pokemon[team.currentPokemonIndex]);
        //�������
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
    {//����Ѫ���仯
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
    {//���ű����ζ���
        Team myTeam = battleMsg.teams[teamIndex];
        Team opponent = battleMsg.teams[teamIndex ^ 1];
        String []detailAnimation;
        if(myTeam.animation != null)
        {
            detailAnimation = myTeam.animation.Split(' ');
            switch (detailAnimation[0])
            {
                //���ż�����������
                case "4":
                    catcheffect.GetComponent<ParticleSystem>().Play();
                    if (detailAnimation[2] == "true")
                    {
                        catchmessage.GetComponent<Text>().text = "ץ���ɹ�";
                        Debug.Log("ʹ����" + detailAnimation[1] + "\nץ���ɹ�");
                    }
                    else
                    {
                        catchmessage.GetComponent<Text>().text = "ץ��ʧ��";
                        Debug.Log("ʹ����" + detailAnimation[1] + "\nץ��ʧ��");
                    }
                    break;

                case "5":
                    //int skillIndex = Int32.Parse(detailAnimation[1]);
                    if (detailAnimation[2] == "����") {
                        mypokemon.GetComponent<Behaviour>().setbehaviour(1);
                    }
                    else{
                        mypokemon.GetComponent<Behaviour>().setbehaviour(4);
                    }
                    yield return new WaitForSeconds(1);
                    break;
                case "2":
                    if (hasSwitch)
                    {//�����л���ǰ���Ѿ��л������˴������ظ��л����������
                        hasSwitch = false;
                    }
                    else
                    {//�����л������ڴ˴��л�
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
                //���ŵз���������
                case "5":
                    //int skillIndex = Int32.Parse(detailAnimation[1]);
                    enemypokemon.GetComponent<Behaviour>().setbehaviour(1);
                    yield return new WaitForSeconds(1);
                    break;
                //�з�����л�������
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
    //Buff��� , 1 ���� 2 ���� 3 �ж� 4 ��ѣ 5 ƣ�� 6 ʧ�� 7 ǿ������ 8 ǿ������ 9 ǿ���ٶ� 10 �������� 11 �������� 12 �����ٶ�
    void printAllBuff(Battle battleMsg)
    {//��ӡBuff
        PokemonInBattle myPokemon = battleMsg.teams[teamIndex].pokemon[battleMsg.teams[teamIndex].currentPokemonIndex];
        PokemonInBattle opponent = battleMsg.teams[teamIndex ^ 1].pokemon[battleMsg.teams[teamIndex ^ 1].currentPokemonIndex];
        buff.GetComponent<Bufupdate>().Buffupdate(getBuffDic(myPokemon));
        buff.GetComponent<Bufupdate>().BuffupdateEnemy(getBuffDic(opponent));
    }
    Dictionary<int ,int> getBuffDic(PokemonInBattle pokemon)
    {//��ȡBuff��Ϣ
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
                    case "����": if (buff.data > 0) effect = 7; else effect = 10; break;
                    case "����": if (buff.data > 0) effect = 8; else effect = 11; break;
                    case "�ٶ�": if (buff.data > 0) effect = 9; else effect = 12; break;
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
                    case "��ѣ": effect = 4; break;
                    case "ƣ��": effect = 5; break;
                    case "ʧ��": effect = 6; break;
                    case "���": effect = 13;break;
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
                    case "����": effect = 1; break;
                    case "����": effect = 2; break;
                    case "�ж�": effect = 3; break;
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