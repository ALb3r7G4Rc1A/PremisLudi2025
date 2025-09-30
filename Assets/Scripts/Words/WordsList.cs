using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsList : MonoBehaviour
{
    public WordsClass[] eazy = {
        new WordsClass("català",    "cata<b><u>la</b></u>",     "català",     "catala",     "catalá"),
        new WordsClass("pera",      "<b><u>pe</b></u>ra",       "pèra",       "pera",       "péra"),
        new WordsClass("galàxia",   "ga<b><u>la</b></u>xia",    "galàxia",    "galaxia",    "galáxia"),
        new WordsClass("ànima",     "<b><u>a</b></u>nima",      "ànima",      "anima",      "ánima"),
        new WordsClass("camió",     "ca<b><u>mio</b></u>",      "camiò",      "camio",      "camió"),
        new WordsClass("aquí",      "a<b><u>qui</b></u>",       "aquì",       "aqui",       "aquí"),
        new WordsClass("sabó",      "sa<b><u>bo</b></u>",       "sabò",       "sabo",       "sabó"),
        new WordsClass("aigua",     "<b><u>ai</u></b>gua",      "aìgua",      "aigua",      "aígua"),
        new WordsClass("érem",      "<b><u>e</u></b>rem",       "èrem",       "erem",       "érem"),
        new WordsClass("telèfon",   "te<b><u>le</u></b>fon",    "telèfon",    "telefon",    "teléfon"),
        new WordsClass("calendari", "calen<b><u>da</u></b>ri",  "calendàri",  "calendari",  "calendári"),
        new WordsClass("excursió",  "excur<b><u>sio</u></b>",   "excursiò",   "excursio",   "excurció"),
        new WordsClass("màquina",   "<b><u>ma</u></b>quina",    "màquina",    "maquina",    "máquina"),
        new WordsClass("cara",      "<b><u>ca</u></b>ra",       "càra",       "cara",       "cára"),
        new WordsClass("bolígraf",  "bo<b><u>li</u></b>graf",   "bolìgraf",   "boligraf",   "bolígraf"),
        new WordsClass("cullera",   "cu<b><u>lle</u></b>ra",    "cullèra",    "cullera",    "culléra"),
        new WordsClass("dèbil",     "<b><u>de</u></b>bil",      "dèbil",      "debil",      "débil"),
        new WordsClass("estómac",   "es<b><u>to</u></b>mac",    "estòmac",    "estomac",    "estómac"),
        new WordsClass("fotògraf",  "fo<b><u>to</u></b>graf",   "fotògraf",   "fotograf",   "fotógraf"),
        new WordsClass("gairebé",   "gaire<b><u>be</u></b>",    "gairebè",    "gairabe",    "gairebé"),
        new WordsClass("rodo",      "<b><u>ro</u></b>do",       "ròdo",       "rodo",       "ródo"),
        new WordsClass("palau",     "<b><u>pa</u></b>lau",      "pàlau",      "palau",      "pálau"),
        new WordsClass("pòster",    "<b><u>pòs</u></b>ter",     "pòster",     "poster",     "póster"),
        new WordsClass("cadira",    "ca<b><u>di</u></b>ra",     "cadìra",     "cadira",     "cadíra"),
        new WordsClass("córrer",    "<b><u>cor</u></b>rer",     "còrrer",     "correr",     "córrer"),
        new WordsClass("ratolí",    "rato<b><u>li</u></b>",     "ratolì",     "ratoli",     "ratolí"),
        new WordsClass("llapis",    "<b><u>lla</u></b>pis",     "llàpis",     "llapis",     "llápis"),
        new WordsClass("congrés",   "con<b><u>gres</u></b>",    "congrès",    "congres",    "congrés"),
        new WordsClass("lògic",     "<b><u>lo</u></b>gic",      "lògic",      "logic",      "lógic"),
        new WordsClass("àngel",     "<b><u>an</u></b>gel",      "àngel",      "angel",      "ángel"),
        new WordsClass("cafè",      "ca<b><u>fe</u></b>",       "cafè",       "cafe",       "café"),
        new WordsClass("núvol",     "<b><u>nu</u></b>vol",      "nùvol",      "nuvol",      "núvol"),
        new WordsClass("nerviós",   "nervi<b><u>os</u></b>",    "nerviòs",    "nervios",    "nerviós"),
        new WordsClass("segon",     "se<b><u>gon</u></b>",      "segòn",      "segon",      "segón"),
        new WordsClass("caminaran", "camina<b><u>ran</u></b>",  "caminaràn",  "caminaran",  "caminarán"),
        new WordsClass("ningú",     "nin<b><u>gu</u></b>",      "ningù",      "ningu",      "ningú"),
        new WordsClass("passadís",  "passa<b><u>dis</u></b>",   "passadìs",   "passadis",   "passadís"),
        new WordsClass("pingüí",    "pin<b><u>güi</u></b>",     "pinguì",     "pingüi",     "pingüí"),
        new WordsClass("urbà",      "ur<b><u>bà</u></b>",       "urbà",       "urba",       "urbá"),
        new WordsClass("ratolí",    "rato<b><u>li</u></b>",     "ratolì",     "ratoli",     "ratolí"),
        new WordsClass("monòton",   "mo<b><u>no</u></b>ton",    "monòton",    "monoton",    "monóton"),
        new WordsClass("algun",     "al<b><u>gun</u></b>",      "algùn",      "algun",      "algún"),
        new WordsClass("príncep",   "<b><u>prin</u></b>cep",    "prìncep",    "princep",    "príncep"),
        new WordsClass("correspon", "cor<b><u>res</u></b>pon",  "corrèspon",  "correspon",  "corréspon"),
        new WordsClass("plàtan",    "<b><u>pla</u></b>tan",     "plàtan",     "platan",     "plátan"),
        new WordsClass("germà",     "ger<b><u>ma</u></b>",      "germà",      "germa",      "germá"),
        new WordsClass("Itàlia",    "I<b><u>tà</u></b>lia",     "Itàlia",     "Italia",     "Itália"),
        new WordsClass("permís",    "per<b><u>mis</u></b>",     "permìs",     "permis",     "permís"),
        new WordsClass("país",      "<b><u>pais</u></b>",       "paìs",       "pais",       "país"),
        new WordsClass("plàstic",   "<b><u>plas</u></b>tic",    "plàstic",    "plastic",    "plástic"),
        new WordsClass("padrí",     "pa<b><u>dri</u></b>",      "padrì",      "padri",      "padrí"),
        new WordsClass("submarí",   "subma<b><u>ri</u></b>",    "submarì",    "submari",    "submarí"),
        new WordsClass("veí",       "<b><u>vei</u></b>",        "veì",        "vei",        "veí"),
        new WordsClass("acabaran",  "aca<b><u>ba</u></b>ran",   "acabàran",   "acabaran",   "acabáran"),
        new WordsClass("remei",     "<b><u>re</u></b>mei",      "rèmei",      "remei",      "rémei"),
        new WordsClass("síndria",   "<b><u>sin</u></b>dria",    "sìndria",    "sindria",    "síndria"),
        new WordsClass("tornavís",  "torna<b><u>vis</u></b>",   "tornavìs",   "tornavis",   "tornavís"),
        new WordsClass("semàfor",   "se<b><u>ma</u></b>for",    "semàfor",    "semafor",    "semáfor"),


    };



    public WordsClass[] medium = {
        new WordsClass("créixer", "creixer", "crèixer", "creixer", "créixer"),
    };

    public WordsClass[] hard = {
        new WordsClass("renou",     "<b><u>re</u></b>nou", "rènou", "renou", "rénou"),
    };

    public void Awake()
    {
        eazy[0] = new WordsClass("Català", "Catala", "Català", "Catala", "Catalá");
    }
}
