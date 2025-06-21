//is attatched to the panel, not the prefab because I fucking hated dealing with the prefabs and
//there's no reason for this script to be on the prefab (as no matter what dome you click, they will display the same stuff)

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using TMPro;


public class DomeBuilding : MonoBehaviour
{
    public GameObject Gamemanager;
    public GameObject DomePanel;
    [HideInInspector] public Variable_Tracker tracker;
    [SerializeField] private TMP_Text totalPopulationTextObject;
    [SerializeField] private TMP_Text namesTextObject;

    [SerializeField] private TMP_Text domesTextObject;
    [SerializeField] private GameObject contentObject;

    // Start is called before the first frame update
    void Start()
    {
        tracker = Gamemanager.GetComponent<Variable_Tracker>();
    }

    void Update() 
    {
        if (DomePanel.activeInHierarchy) UpdateInfo(); //updates whenever the panel is active
    }


    public void UpdateInfo() //updates values, is only called when the panel is opened (for optimization)
    {
        UpdateNames();

        //Adds all of the names from variableTracker to the text object
        string allNames = "";
        for (int i = 0; i < tracker.crewNames.Count; i++)
        {
            //if (i == tracker.crewNames.Count - 1) allNames += "and " + tracker.crewNames[i]; //if this is the last name
            //else
            //{
                allNames += i+1 + ". " + tracker.crewNames[i] + "\n";
            //}
        }

        //Sets the new text onto the objects        
        namesTextObject.SetText(allNames);
        totalPopulationTextObject.SetText("Total Population: " + tracker.population.ToString());
        domesTextObject.SetText("Domes: " + NumDomesInScene());


        //Resizes objects accordingly
        RectTransform contentRect = contentObject.GetComponent<RectTransform>();
        RectTransform namesRect = namesTextObject.GetComponent<RectTransform>();
        contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, namesRect.rect.height);
    }


    //Updates the names in the variable tracker
    void UpdateNames()
    {
        //if there are no names generated, generate them
        if (tracker.crewNames.Count == 0)
        {
            for (int i = 0; i < tracker.population; i++) //for each crew member, generate a random name
            {
                tracker.crewNames.Add(RandomName());
            }
        }

        //if the population is greater than the list of names (population has increased since last time opening panel)
        else if (tracker.population > tracker.crewNames.Count)
        {
            for (int i = 0; i < tracker.population - tracker.crewNames.Count; i++) //add the extra names
            {
                tracker.crewNames.Add(RandomName());
            }
        }

        //if the population is less than the list of names (population has decreased since last time opening panel)
        else if (tracker.population < tracker.crewNames.Count)
        {
            int excessNames = tracker.crewNames.Count - tracker.population; //How ever many extra names there are
            tracker.crewNames.RemoveRange(tracker.crewNames.Count - excessNames /*at what index to remove from*/, excessNames /*how many to remove*/); //removes however many extra names there were
        }
    }


    int NumDomesInScene() {
        GameObject[] allDomes = GameObject.FindGameObjectsWithTag("Dome");
        return allDomes.Length / 2; //Because each dome actually makes 2 things that are tagged domes
    }

    string RandomName() {
        string randomFullName;

        string[] firstNames = new string[] //1000 Chat-GPT genererated first names
        {
        "Zaylen", "Kaelis", "Virel", "Thalyn", "Azura", "Jexar", "Oriah", "Nyrix", "Kairo", "Vaela",
        "Solan", "Riven", "Elandra", "Xavian", "Nerai", "Lyric", "Veyra", "Calix", "Zirel", "Taryn",
        "Kaedyn", "Ysera", "Drex", "Alura", "Thorne", "Zephyra", "Kovyn", "Saphen", "Renzo", "Elira",
        "Malric", "Viona", "Auren", "Zenya", "Quorin", "Syris", "Laziel", "Nahla", "Cyric", "Velora",
        "Tyven", "Aeris", "Fenric", "Orielle", "Jaxon", "Nymera", "Valen", "Thysa", "Rovik", "Evara",
        "Kaelen", "Zavia", "Tavik", "Seren", "Raelis", "Deyra", "Jorik", "Caela", "Brivan", "Mirel",
        "Kysen", "Anira", "Lioren", "Talyra", "Zarek", "Avira", "Merek", "Yalena", "Xeric", "Taisyn",
        "Zeon", "Calyra", "Halric", "Nyssia", "Torren", "Elirae", "Jorven", "Syrelle", "Kaelith", "Venra",
        "Orien", "Myraen", "Tazryn", "Velis", "Ardyn", "Luneth", "Rionel", "Thylia", "Sorik", "Elsyn",
        "Kyric", "Isari", "Rovaan", "Selira", "Thalen", "Kaerys", "Xaviel", "Aryel", "Korin", "Ilyra",
        "Vaelen", "Naeris", "Drayven", "Zephyx", "Qorvin", "Alira", "Narek", "Ysena", "Ceren", "Taliar",
        "Orion", "Vasha", "Nixan", "Saela", "Ryven", "Meris", "Torin", "Avara", "Zavian", "Tylira",
        "Lazrik", "Jynara", "Vexar", "Arlith", "Caelen", "Tyraen", "Maelik", "Sirelle", "Korvin", "Evelis",
        "Zarien", "Thalira", "Nyric", "Quisra", "Daxen", "Valira", "Zavros", "Selyne", "Thorne", "Elyra",
        "Cazric", "Jalena", "Merek", "Serith", "Kaelis", "Nyxen", "Orin", "Aelira", "Krevan", "Tessari",
        "Zephron", "Miralyn", "Darian", "Velix", "Kaelar", "Yavira", "Toriel", "Cyrin", "Zeriah", "Alonys",
        "Tharek", "Evara", "Varien", "Nyrel", "Kyros", "Aerith", "Ziron", "Lilira", "Jorric", "Avenna",
        "Xandor", "Sarina", "Caelen", "Zavria", "Quorik", "Myrella", "Tylen", "Isena", "Varek", "Liorae",
        "Zerix", "Elaryn", "Rhaen", "Vessia", "Narek", "Avenel", "Calren", "Elyss", "Koriel", "Nalya",
        "Zalen", "Thyra", "Seric", "Aelith", "Xyric", "Vaelra", "Brycyn", "Oriana", "Jarek", "Saryna",
        "Yarik", "Tahlira", "Zekar", "Irielle", "Darven", "Mireya", "Qorven", "Ysaria", "Torrik", "Selene",
        "Zevon", "Arlia", "Kaerin", "Velin", "Malric", "Tyssa", "Lorik", "Naelis", "Dareth", "Aeris",
        "Vion", "Nyvra", "Xandor", "Maelis", "Rhaenor", "Seressa", "Toric", "Jirelle", "Kalev", "Eluna",
        "Zanric", "Lyssa", "Brynnar", "Tiriel", "Kovian", "Aelra", "Yorin", "Sarya", "Kelric", "Alenya",
        "Zerik", "Nerisa", "Vorik", "Elwynn", "Joric", "Tavira", "Thalos", "Yselyn", "Xaran", "Aurelle",
        "Marek", "Nerine", "Talren", "Vessra", "Jevan", "Saelis", "Ralik", "Mirelle", "Orven", "Selyra",
        "Zaxel", "Caenya", "Torran", "Yelira", "Vorren", "Thalyn", "Nayen", "Celira", "Zorin", "Amelyn",
        "Larek", "Nirelle", "Valrik", "Aerin", "Javen", "Lunara", "Zarek", "Elaria", "Tharic", "Sireya",
        "Xenor", "Aylen", "Davor", "Velis", "Kalor", "Iselle", "Ravik", "Thalira", "Zanric", "Orielle",
        "Tavren", "Selaya", "Drayce", "Evelra", "Korrel", "Lurell", "Vaenor", "Zirelle", "Kaedros", "Yelina",
        "Jorlan", "Maeril", "Ryker", "Thalora", "Kareth", "Yssara", "Elion", "Nerella", "Ranik", "Cirelle",
        "Daxel", "Selith", "Torvik", "Alaris", "Xylen", "Virelle", "Malven", "Ysira", "Narek", "Kaelin",
        "Thior", "Lunys", "Vaelor", "Selannis", "Zorien", "Irissa", "Rylan", "Aerina", "Brycen", "Veloria",
        "Kalyx", "Orelia", "Dazen", "Telyra", "Zorvik", "Eryss", "Triven", "Nyelle", "Kelran", "Sarina",
        "Auren", "Zevra", "Thalor", "Yserin", "Cayric", "Mirel", "Vorren", "Ealyn", "Kalros", "Isera",
        "Narik", "Tyrielle", "Dravin", "Aralya", "Zarek", "Eirys", "Lorien", "Nyshara", "Vaylen", "Seris",
        "Jerek", "Celaris", "Torvik", "Vaelle", "Kovyn", "Lioris", "Zayrik", "Maelia", "Xevor", "Tirra",
        "Thandor", "Olyssia", "Riven", "Ysoria", "Darik", "Selra", "Kaelor", "Elarin", "Varek", "Myssa",
        "Zarik", "Sireth", "Kalren", "Ylara", "Orick", "Velrisa", "Tarion", "Lunessa", "Xorin", "Cyrella"
        };

        string[] lastNames = new string[] //1000 Chat-GPT genererated last names
        {
        "Virellan", "Kaelthorn", "Draxen", "Terynsol", "Orivar", "Zenthari", "Nyrellis", "Quorvan", "Sylorin", "Thalvorn",
        "Aurenix", "Velcren", "Dravion", "Sarethin", "Korvex", "Zalara", "Xenthos", "Malvaron", "Yarven", "Sileth",
        "Zorvain", "Elarros", "Myrren", "Karneth", "Torveth", "Raelynd", "Vortan", "Eryndor", "Calyros", "Zarenth",
        "Dovarel", "Therion", "Lazaren", "Kryden", "Valtren", "Xaril", "Marenth", "Nireth", "Sorvain", "Quinlar",
        "Virexen", "Drenar", "Zandrell", "Syvaran", "Eilvorn", "Thorinox", "Ralvaren", "Zethran", "Kelaros", "Nyrixor",
        "Tazarin", "Lorvyn", "Kyvaren", "Ashovar", "Zirenth", "Daelith", "Vorlyn", "Xandren", "Jovaryn", "Therel",
        "Oryndar", "Zevaran", "Norrick", "Vellaren", "Caidros", "Rovarin", "Darenth", "Maelric", "Sorun", "Kelveth",
        "Kovaris", "Zalthor", "Drelion", "Valnox", "Myrican", "Orvain", "Zarvos", "Tryndor", "Keranov", "Thavax",
        "Sirevon", "Almaric", "Darakor", "Venloris", "Rythen", "Quinox", "Zaelith", "Barynox", "Lorven", "Velrix",
        "Navaros", "Skelvar", "Tharven", "Oryssin", "Zarvek", "Xenlor", "Gorvain", "Eltheron", "Kaelov", "Drovak",
        "Sarneth", "Veraxis", "Halorin", "Tharnax", "Myrlith", "Solvex", "Kairon", "Vorlanth", "Valros", "Drynix",
        "Tyroven", "Zelros", "Erelon", "Drakess", "Quirell", "Jarethor", "Mornyx", "Kelrix", "Zepharan", "Trenvar",
        "Alvaron", "Tyrellis", "Saelric", "Xovarin", "Narveth", "Malvenor", "Craylix", "Thyrion", "Vorvex", "Zerith",
        "Virellor", "Kalveth", "Draxior", "Eironth", "Sorikhan", "Yelvaron", "Velixor", "Torvanis", "Zarnyx", "Korvyn",
        "Xirevon", "Maelros", "Drethorn", "Kelvonar", "Zorynth", "Eravorn", "Jelvax", "Sarveth", "Orminth", "Tarnyx",
        "Varikon", "Dreylor", "Yorenth", "Xalthor", "Gorvyn", "Thalvern", "Zarekian", "Morveth", "Velanik", "Zelloth",
        "Tralyx", "Aurenven", "Zirevan", "Nythros", "Vorenth", "Threxan", "Eldovax", "Kyrenth", "Selthar", "Jorvax",
        "Marithor", "Kyrellen", "Sorvix", "Valoren", "Drenthax", "Tharnen", "Zarvell", "Xalthen", "Vorothan", "Zarenth",
        "Cyrvon", "Rhelor", "Draleth", "Korvian", "Nirevan", "Syronis", "Xeronth", "Trylven", "Zevaros", "Thyrven",
        "Mavarin", "Voryth", "Kyralor", "Zorvyn", "Nytheris", "Thandros", "Qelthor", "Xeravon", "Orzellan", "Derikson",
        "Ziravar", "Lyrosen", "Thorynd", "Kalvoren", "Malvasen", "Rythelor", "Sylaren", "Quorrel", "Zinorix", "Verlith",
        "Xaranth", "Morivar", "Tarkess", "Draxalen", "Kroven", "Yzaren", "Zentaril", "Faelric", "Theronn", "Norraven",
        "Zyrosen", "Corthen", "Kelvorn", "Orelith", "Vaelthorn", "Gravion", "Vorrelan", "Drelthon", "Thasaren", "Kalthrax",
        "Jarneth", "Vaelros", "Vornar", "Yllaren", "Senvax", "Malvaren", "Torvalin", "Zanvar", "Aericson", "Narthorn",
        "Xelvaren", "Zyroven", "Kaelvin", "Thavros", "Eryson", "Mornaren", "Sorvaxen", "Vyrithon", "Zenlath", "Ralvorn",
        "Kyberon", "Zarnox", "Thalevar", "Xalenor", "Lorvax", "Myxenor", "Vandros", "Norlith", "Zarven", "Thavren",
        "Zevalen", "Kelvorn", "Trelaron", "Xenthros", "Orliven", "Zirven", "Valtoris", "Qorrin", "Thandrel", "Kavarin",
        "Yarnox", "Serivorn", "Draelith", "Zolvar", "Kelthor", "Vorven", "Myrenix", "Xerrin", "Thandron", "Jirvax",
        "Zorathin", "Rhydorn", "Tarnalis", "Velaron", "Aurenox", "Drakelle", "Yorellan", "Qylaros", "Verionis", "Thronen",
        "Zerros", "Kaelthorn", "Vondros", "Xandrel", "Lorissen", "Zythorin", "Thiravos", "Myranos", "Drelan", "Sylvenor",
        "Travenik", "Kelrosen", "Xyrenth", "Narliss", "Vandrel", "Zyralith", "Tyralon", "Kyravox", "Dovenar", "Orlenos",
        "Jandrel", "Xyronis", "Varionyx", "Thallor", "Zeravian", "Nivorak", "Yorlanth", "Calvaren", "Vorisen", "Larethor",
        "Myriqen", "Thovalen", "Xyranor", "Karnyx", "Qarven", "Zolryn", "Taravon", "Selvron", "Venthros", "Morlak",
        "Zorven", "Xirenis", "Kylthor", "Trelavon", "Narvik", "Valenor", "Korlanth", "Zavrosen", "Aelvorn", "Drysen",
        "Thalara", "Mornath", "Quelvar", "Xenros", "Jerneth", "Ravalin", "Norran", "Korravon", "Syrixor", "Valkren",
        "Tavros", "Zharven", "Kelrenis", "Myrian", "Thornax", "Xenlith", "Zorveth", "Darvonn", "Raloros", "Kairenth",
        "Orriven", "Trenith", "Zyorax", "Varethor", "Yelvin", "Quarneth", "Soryxen", "Tharnyx", "Melvros", "Vaelenis",
        "Drayven", "Zenthros", "Kelross", "Xalvorn", "Javelor", "Narynox", "Torallis", "Zyrovan", "Thirell", "Erelven",
        "Verinox", "Morveth", "Sylronis", "Qelthros", "Xanivar", "Tarnellis", "Zeraven", "Vorlith", "Kurnoth", "Yandros",
        "Drelven", "Alvrax", "Zorionis", "Telvaron", "Xelvyn", "Navoren", "Kyrvonis", "Therikson", "Jorvenis", "Syrellax",
        "Zantheron", "Ralvikar", "Elvaris", "Tarnovar", "Virelor", "Zyvenor", "Tharosel", "Korvalis", "Myrelan", "Xyvorn"
        };

        randomFullName = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)]; //takes a random first name and a random last name and adds them together
        return randomFullName;
    }
}
