using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization
{
    Dictionary<string, string> localeEN = new Dictionary<string, string>
    {
        {"finished_id",             "YOU HAVE COLLECTED ALL, THE DOOR IS OPEN!"},
        {"egypt_not_finished_id",   "YOU MUST COLLECT ALL THE PYRAMIDS!"},
        {"greece_not_finished_id",  "YOU MUST COLLECT ALL THE HELMETS!"},
        {"space_not_finished_id",   "YOU MUST COLLECT ALL THE ANTENNAS!"},
    };

    Dictionary<string, string> localeDE = new Dictionary<string, string>
    {
        {"finished_id",             "SIE HABEN ALLES GESAMMELT, DIE TÜR IST OFFEN!"},
        {"egypt_not_finished_id",   "SIE MÜSSEN ALLE PYRAMIDEN SAMMELN!"},
        {"greece_not_finished_id",  "SIE MÜSSEN ALLE HELME SAMMELN!"},
        {"space_not_finished_id",   "SIE MÜSSEN ALLE ANTENNEN SAMMELN!"},
    };

    Dictionary<string, string> localeES = new Dictionary<string, string>
    {
        {"finished_id",             "HAS COLECCIONADO TODO, LA PUERTA ESTA ABIERTA!"},
        {"egypt_not_finished_id",   "DEBES RECOGER TODAS LAS PIRAMIDES!"},
        {"greece_not_finished_id",  "DEBES RECOGER TODOS LOS CASCOS!"},
        {"space_not_finished_id",   "DEBES RECOGER TODAS LAS ANTENAS!"},
    };

    Dictionary<string, string> localeFR = new Dictionary<string, string>
    {
        {"finished_id",             "VOUS AVEZ TOUT COLLECTIONNÉ, LA PORTE EST OUVERTE!"},
        {"egypt_not_finished_id",   "VOUS DEVEZ COLLECTIONNER TOUTES LES PYRAMIDES!"},
        {"greece_not_finished_id",  "VOUS DEVEZ COLLECTIONNER TOUS LES CASQUES!"},
        {"space_not_finished_id",   "VOUS DEVEZ RECUEILLIR TOUTES LES ANTENNES!"},
    };

    Dictionary<string, string> localeTR = new Dictionary<string, string>
    {
        {"finished_id",             "HEPSİNİ TOPLADIN. KAPI AÇILDI!"},
        {"egypt_not_finished_id",   "TÜM PİRAMİTLERİ TOPLAMAN GEREK!"},
        {"greece_not_finished_id",  "TÜM MİĞFERLERİ TOPLAMAN GEREK!"},
        {"space_not_finished_id",   "TÜM ANTENLERİ TOPLAMAN GEREK!"},
    };

    public Dictionary<string, string> GetLocale()
    {
        SystemLanguage systemLanguage = Application.systemLanguage;

        switch (systemLanguage)
        {
            case SystemLanguage.French:
                return localeFR;
            case SystemLanguage.German:
                return localeDE;
            case SystemLanguage.Spanish:
                return localeES;
            case SystemLanguage.Turkish:
                return localeTR;
            default:
                return localeEN;
        }
    }
}
