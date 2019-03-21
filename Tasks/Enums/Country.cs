using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using TaskManager.AdamResurces;


namespace TaskManager.Enums
{
    public enum Country
    {
        [Display(ResourceType = typeof(_Country), Name = "AC")]
        AC,
        [Display(ResourceType = typeof(_Country), Name = "AD")]
        AD,
        [Display(ResourceType = typeof(_Country), Name = "AE")]
        AE,
        [Display(ResourceType = typeof(_Country), Name = "AF")]
        AF,
        [Display(ResourceType = typeof(_Country), Name = "AG")]
        AG,
        [Display(ResourceType = typeof(_Country), Name = "AI")]
        AI,
        [Display(ResourceType = typeof(_Country), Name = "AL")]
        AL,
        [Display(ResourceType = typeof(_Country), Name = "AM")]
        AM,
        [Display(ResourceType = typeof(_Country), Name = "AO")]
        AO,
        [Display(ResourceType = typeof(_Country), Name = "AQ")]
        AQ,
        [Display(ResourceType = typeof(_Country), Name = "AR")]
        AR,
        [Display(ResourceType = typeof(_Country), Name = "AS")]
        AS,
        [Display(ResourceType = typeof(_Country), Name = "AT")]
        AT,
        [Display(ResourceType = typeof(_Country), Name = "AU")]
        AU,
        [Display(ResourceType = typeof(_Country), Name = "AW")]
        AW,
        [Display(ResourceType = typeof(_Country), Name = "AX")]
        AX,
        [Display(ResourceType = typeof(_Country), Name = "AZ")]
        AZ,
        [Display(ResourceType = typeof(_Country), Name = "BA")]
        BA,
        [Display(ResourceType = typeof(_Country), Name = "BB")]
        BB,
        [Display(ResourceType = typeof(_Country), Name = "BD")]
        BD,
        [Display(ResourceType = typeof(_Country), Name = "BE")]
        BE,
        [Display(ResourceType = typeof(_Country), Name = "BF")]
        BF,
        [Display(ResourceType = typeof(_Country), Name = "BG")]
        BG,
        [Display(ResourceType = typeof(_Country), Name = "BH")]
        BH,
        [Display(ResourceType = typeof(_Country), Name = "BI")]
        BI,
        [Display(ResourceType = typeof(_Country), Name = "BJ")]
        BJ,
        [Display(ResourceType = typeof(_Country), Name = "BL")]
        BL,
        [Display(ResourceType = typeof(_Country), Name = "BM")]
        BM,
        [Display(ResourceType = typeof(_Country), Name = "BN")]
        BN,
        [Display(ResourceType = typeof(_Country), Name = "BO")]
        BO,
        [Display(ResourceType = typeof(_Country), Name = "BQ")]
        BQ,
        [Display(ResourceType = typeof(_Country), Name = "BR")]
        BR,
        [Display(ResourceType = typeof(_Country), Name = "BS")]
        BS,
        [Display(ResourceType = typeof(_Country), Name = "BT")]
        BT,
        [Display(ResourceType = typeof(_Country), Name = "BW")]
        BW,
        [Display(ResourceType = typeof(_Country), Name = "BY")]
        BY,
        [Display(ResourceType = typeof(_Country), Name = "BZ")]
        BZ,
        [Display(ResourceType = typeof(_Country), Name = "CA")]
        CA,
        [Display(ResourceType = typeof(_Country), Name = "CC")]
        CC,
        [Display(ResourceType = typeof(_Country), Name = "CD")]
        CD,
        [Display(ResourceType = typeof(_Country), Name = "CF")]
        CF,
        [Display(ResourceType = typeof(_Country), Name = "CG")]
        CG,
        [Display(ResourceType = typeof(_Country), Name = "CH")]
        CH,
        [Display(ResourceType = typeof(_Country), Name = "CI")]
        CI,
        [Display(ResourceType = typeof(_Country), Name = "CK")]
        CK,
        [Display(ResourceType = typeof(_Country), Name = "CL")]
        CL,
        [Display(ResourceType = typeof(_Country), Name = "CM")]
        CM,
        [Display(ResourceType = typeof(_Country), Name = "CN")]
        CN,
        [Display(ResourceType = typeof(_Country), Name = "CO")]
        CO,
        [Display(ResourceType = typeof(_Country), Name = "CR")]
        CR,
        [Display(ResourceType = typeof(_Country), Name = "CU")]
        CU,
        [Display(ResourceType = typeof(_Country), Name = "CV")]
        CV,
        [Display(ResourceType = typeof(_Country), Name = "CW")]
        CW,
        [Display(ResourceType = typeof(_Country), Name = "CX")]
        CX,
        [Display(ResourceType = typeof(_Country), Name = "CY")]
        CY,
        [Display(ResourceType = typeof(_Country), Name = "CZ")]
        CZ,
        [Display(ResourceType = typeof(_Country), Name = "DE")]
        DE,
        [Display(ResourceType = typeof(_Country), Name = "DG")]
        DG,
        [Display(ResourceType = typeof(_Country), Name = "DJ")]
        DJ,
        [Display(ResourceType = typeof(_Country), Name = "DK")]
        DK,
        [Display(ResourceType = typeof(_Country), Name = "DM")]
        DM,
        [Display(ResourceType = typeof(_Country), Name = "DO")]
        DO,
        [Display(ResourceType = typeof(_Country), Name = "DZ")]
        DZ,
        [Display(ResourceType = typeof(_Country), Name = "EA")]
        EA,
        [Display(ResourceType = typeof(_Country), Name = "EC")]
        EC,
        [Display(ResourceType = typeof(_Country), Name = "EE")]
        EE,
        [Display(ResourceType = typeof(_Country), Name = "EG")]
        EG,
        [Display(ResourceType = typeof(_Country), Name = "EH")]
        EH,
        [Display(ResourceType = typeof(_Country), Name = "ER")]
        ER,
        [Display(ResourceType = typeof(_Country), Name = "ES")]
        ES,
        [Display(ResourceType = typeof(_Country), Name = "ET")]
        ET,
        [Display(ResourceType = typeof(_Country), Name = "FI")]
        FI,
        [Display(ResourceType = typeof(_Country), Name = "FJ")]
        FJ,
        [Display(ResourceType = typeof(_Country), Name = "FK")]
        FK,
        [Display(ResourceType = typeof(_Country), Name = "FM")]
        FM,
        [Display(ResourceType = typeof(_Country), Name = "FO")]
        FO,
        [Display(ResourceType = typeof(_Country), Name = "FR")]
        FR,
        [Display(ResourceType = typeof(_Country), Name = "GA")]
        GA,
        [Display(ResourceType = typeof(_Country), Name = "GB")]
        GB,
        [Display(ResourceType = typeof(_Country), Name = "GD")]
        GD,
        [Display(ResourceType = typeof(_Country), Name = "GE")]
        GE,
        [Display(ResourceType = typeof(_Country), Name = "GF")]
        GF,
        [Display(ResourceType = typeof(_Country), Name = "GG")]
        GG,
        [Display(ResourceType = typeof(_Country), Name = "GH")]
        GH,
        [Display(ResourceType = typeof(_Country), Name = "GI")]
        GI,
        [Display(ResourceType = typeof(_Country), Name = "GL")]
        GL,
        [Display(ResourceType = typeof(_Country), Name = "GM")]
        GM,
        [Display(ResourceType = typeof(_Country), Name = "GN")]
        GN,
        [Display(ResourceType = typeof(_Country), Name = "GP")]
        GP,
        [Display(ResourceType = typeof(_Country), Name = "GQ")]
        GQ,
        [Display(ResourceType = typeof(_Country), Name = "GR")]
        GR,
        [Display(ResourceType = typeof(_Country), Name = "GS")]
        GS,
        [Display(ResourceType = typeof(_Country), Name = "GT")]
        GT,
        [Display(ResourceType = typeof(_Country), Name = "GU")]
        GU,
        [Display(ResourceType = typeof(_Country), Name = "GW")]
        GW,
        [Display(ResourceType = typeof(_Country), Name = "GY")]
        GY,
        [Display(ResourceType = typeof(_Country), Name = "HK")]
        HK,
        [Display(ResourceType = typeof(_Country), Name = "HN")]
        HN,
        [Display(ResourceType = typeof(_Country), Name = "HR")]
        HR,
        [Display(ResourceType = typeof(_Country), Name = "HT")]
        HT,
        [Display(ResourceType = typeof(_Country), Name = "HU")]
        HU,
        [Display(ResourceType = typeof(_Country), Name = "IC")]
        IC,
        [Display(ResourceType = typeof(_Country), Name = "ID")]
        ID,
        [Display(ResourceType = typeof(_Country), Name = "IE")]
        IE,
        [Display(ResourceType = typeof(_Country), Name = "IL")]
        IL,
        [Display(ResourceType = typeof(_Country), Name = "IM")]
        IM,
        [Display(ResourceType = typeof(_Country), Name = "IN")]
        IN,
        [Display(ResourceType = typeof(_Country), Name = "IO")]
        IO,
        [Display(ResourceType = typeof(_Country), Name = "IQ")]
        IQ,
        [Display(ResourceType = typeof(_Country), Name = "IR")]
        IR,
        [Display(ResourceType = typeof(_Country), Name = "IS")]
        IS,
        [Display(ResourceType = typeof(_Country), Name = "IT")]
        IT,
        [Display(ResourceType = typeof(_Country), Name = "JE")]
        JE,
        [Display(ResourceType = typeof(_Country), Name = "JM")]
        JM,
        [Display(ResourceType = typeof(_Country), Name = "JO")]
        JO,
        [Display(ResourceType = typeof(_Country), Name = "JP")]
        JP,
        [Display(ResourceType = typeof(_Country), Name = "KE")]
        KE,
        [Display(ResourceType = typeof(_Country), Name = "KG")]
        KG,
        [Display(ResourceType = typeof(_Country), Name = "KH")]
        KH,
        [Display(ResourceType = typeof(_Country), Name = "KI")]
        KI,
        [Display(ResourceType = typeof(_Country), Name = "KM")]
        KM,
        [Display(ResourceType = typeof(_Country), Name = "KN")]
        KN,
        [Display(ResourceType = typeof(_Country), Name = "KP")]
        KP,
        [Display(ResourceType = typeof(_Country), Name = "KR")]
        KR,
        [Display(ResourceType = typeof(_Country), Name = "KW")]
        KW,
        [Display(ResourceType = typeof(_Country), Name = "KY")]
        KY,
        [Display(ResourceType = typeof(_Country), Name = "KZ")]
        KZ,
        [Display(ResourceType = typeof(_Country), Name = "LA")]
        LA,
        [Display(ResourceType = typeof(_Country), Name = "LB")]
        LB,
        [Display(ResourceType = typeof(_Country), Name = "LC")]
        LC,
        [Display(ResourceType = typeof(_Country), Name = "LI")]
        LI,
        [Display(ResourceType = typeof(_Country), Name = "LK")]
        LK,
        [Display(ResourceType = typeof(_Country), Name = "LR")]
        LR,
        [Display(ResourceType = typeof(_Country), Name = "LS")]
        LS,
        [Display(ResourceType = typeof(_Country), Name = "LT")]
        LT,
        [Display(ResourceType = typeof(_Country), Name = "LU")]
        LU,
        [Display(ResourceType = typeof(_Country), Name = "LV")]
        LV,
        [Display(ResourceType = typeof(_Country), Name = "LY")]
        LY,
        [Display(ResourceType = typeof(_Country), Name = "MA")]
        MA,
        [Display(ResourceType = typeof(_Country), Name = "MC")]
        MC,
        [Display(ResourceType = typeof(_Country), Name = "MD")]
        MD,
        [Display(ResourceType = typeof(_Country), Name = "ME")]
        ME,
        [Display(ResourceType = typeof(_Country), Name = "MF")]
        MF,
        [Display(ResourceType = typeof(_Country), Name = "MG")]
        MG,
        [Display(ResourceType = typeof(_Country), Name = "MH")]
        MH,
        [Display(ResourceType = typeof(_Country), Name = "MK")]
        MK,
        [Display(ResourceType = typeof(_Country), Name = "ML")]
        ML,
        [Display(ResourceType = typeof(_Country), Name = "MM")]
        MM,
        [Display(ResourceType = typeof(_Country), Name = "MN")]
        MN,
        [Display(ResourceType = typeof(_Country), Name = "MO")]
        MO,
        [Display(ResourceType = typeof(_Country), Name = "MP")]
        MP,
        [Display(ResourceType = typeof(_Country), Name = "MQ")]
        MQ,
        [Display(ResourceType = typeof(_Country), Name = "MR")]
        MR,
        [Display(ResourceType = typeof(_Country), Name = "MS")]
        MS,
        [Display(ResourceType = typeof(_Country), Name = "MT")]
        MT,
        [Display(ResourceType = typeof(_Country), Name = "MU")]
        MU,
        [Display(ResourceType = typeof(_Country), Name = "MV")]
        MV,
        [Display(ResourceType = typeof(_Country), Name = "MW")]
        MW,
        [Display(ResourceType = typeof(_Country), Name = "MX")]
        MX,
        [Display(ResourceType = typeof(_Country), Name = "MY")]
        MY,
        [Display(ResourceType = typeof(_Country), Name = "MZ")]
        MZ,
        [Display(ResourceType = typeof(_Country), Name = "NA")]
        NA,
        [Display(ResourceType = typeof(_Country), Name = "NC")]
        NC,
        [Display(ResourceType = typeof(_Country), Name = "NE")]
        NE,
        [Display(ResourceType = typeof(_Country), Name = "NF")]
        NF,
        [Display(ResourceType = typeof(_Country), Name = "NG")]
        NG,
        [Display(ResourceType = typeof(_Country), Name = "NI")]
        NI,
        [Display(ResourceType = typeof(_Country), Name = "NL")]
        NL,
        [Display(ResourceType = typeof(_Country), Name = "NO")]
        NO,
        [Display(ResourceType = typeof(_Country), Name = "NP")]
        NP,
        [Display(ResourceType = typeof(_Country), Name = "NR")]
        NR,
        [Display(ResourceType = typeof(_Country), Name = "NU")]
        NU,
        [Display(ResourceType = typeof(_Country), Name = "NZ")]
        NZ,
        [Display(ResourceType = typeof(_Country), Name = "OM")]
        OM,
        [Display(ResourceType = typeof(_Country), Name = "PA")]
        PA,
        [Display(ResourceType = typeof(_Country), Name = "PE")]
        PE,
        [Display(ResourceType = typeof(_Country), Name = "PF")]
        PF,
        [Display(ResourceType = typeof(_Country), Name = "PG")]
        PG,
        [Display(ResourceType = typeof(_Country), Name = "PH")]
        PH,
        [Display(ResourceType = typeof(_Country), Name = "PK")]
        PK,
        [Display(ResourceType = typeof(_Country), Name = "PL")]
        PL,
        [Display(ResourceType = typeof(_Country), Name = "PM")]
        PM,
        [Display(ResourceType = typeof(_Country), Name = "PN")]
        PN,
        [Display(ResourceType = typeof(_Country), Name = "PR")]
        PR,
        [Display(ResourceType = typeof(_Country), Name = "PS")]
        PS,
        [Display(ResourceType = typeof(_Country), Name = "PT")]
        PT,
        [Display(ResourceType = typeof(_Country), Name = "PW")]
        PW,
        [Display(ResourceType = typeof(_Country), Name = "PY")]
        PY,
        [Display(ResourceType = typeof(_Country), Name = "QA")]
        QA,
        [Display(ResourceType = typeof(_Country), Name = "RE")]
        RE,
        [Display(ResourceType = typeof(_Country), Name = "RO")]
        RO,
        [Display(ResourceType = typeof(_Country), Name = "RS")]
        RS,
        [Display(ResourceType = typeof(_Country), Name = "RU")]
        RU,
        [Display(ResourceType = typeof(_Country), Name = "RW")]
        RW,
        [Display(ResourceType = typeof(_Country), Name = "SA")]
        SA,
        [Display(ResourceType = typeof(_Country), Name = "SB")]
        SB,
        [Display(ResourceType = typeof(_Country), Name = "SC")]
        SC,
        [Display(ResourceType = typeof(_Country), Name = "SD")]
        SD,
        [Display(ResourceType = typeof(_Country), Name = "SE")]
        SE,
        [Display(ResourceType = typeof(_Country), Name = "SG")]
        SG,
        [Display(ResourceType = typeof(_Country), Name = "SH")]
        SH,
        [Display(ResourceType = typeof(_Country), Name = "SI")]
        SI,
        [Display(ResourceType = typeof(_Country), Name = "SJ")]
        SJ,
        [Display(ResourceType = typeof(_Country), Name = "SK")]
        SK,
        [Display(ResourceType = typeof(_Country), Name = "SL")]
        SL,
        [Display(ResourceType = typeof(_Country), Name = "SM")]
        SM,
        [Display(ResourceType = typeof(_Country), Name = "SN")]
        SN,
        [Display(ResourceType = typeof(_Country), Name = "SO")]
        SO,
        [Display(ResourceType = typeof(_Country), Name = "SR")]
        SR,
        [Display(ResourceType = typeof(_Country), Name = "SS")]
        SS,
        [Display(ResourceType = typeof(_Country), Name = "ST")]
        ST,
        [Display(ResourceType = typeof(_Country), Name = "SV")]
        SV,
        [Display(ResourceType = typeof(_Country), Name = "SX")]
        SX,
        [Display(ResourceType = typeof(_Country), Name = "SY")]
        SY,
        [Display(ResourceType = typeof(_Country), Name = "SZ")]
        SZ,
        [Display(ResourceType = typeof(_Country), Name = "TA")]
        TA,
        [Display(ResourceType = typeof(_Country), Name = "TC")]
        TC,
        [Display(ResourceType = typeof(_Country), Name = "TD")]
        TD,
        [Display(ResourceType = typeof(_Country), Name = "TF")]
        TF,
        [Display(ResourceType = typeof(_Country), Name = "TG")]
        TG,
        [Display(ResourceType = typeof(_Country), Name = "TH")]
        TH,
        [Display(ResourceType = typeof(_Country), Name = "TJ")]
        TJ,
        [Display(ResourceType = typeof(_Country), Name = "TK")]
        TK,
        [Display(ResourceType = typeof(_Country), Name = "TL")]
        TL,
        [Display(ResourceType = typeof(_Country), Name = "TM")]
        TM,
        [Display(ResourceType = typeof(_Country), Name = "TN")]
        TN,
        [Display(ResourceType = typeof(_Country), Name = "TO")]
        TO,
        [Display(ResourceType = typeof(_Country), Name = "TR")]
        TR,
        [Display(ResourceType = typeof(_Country), Name = "TT")]
        TT,
        [Display(ResourceType = typeof(_Country), Name = "TV")]
        TV,
        [Display(ResourceType = typeof(_Country), Name = "TW")]
        TW,
        [Display(ResourceType = typeof(_Country), Name = "TZ")]
        TZ,
        [Display(ResourceType = typeof(_Country), Name = "UA")]
        UA,
        [Display(ResourceType = typeof(_Country), Name = "UG")]
        UG,
        [Display(ResourceType = typeof(_Country), Name = "UM")]
        UM,
        [Display(ResourceType = typeof(_Country), Name = "US")]
        US,
        [Display(ResourceType = typeof(_Country), Name = "UY")]
        UY,
        [Display(ResourceType = typeof(_Country), Name = "UZ")]
        UZ,
        [Display(ResourceType = typeof(_Country), Name = "VA")]
        VA,
        [Display(ResourceType = typeof(_Country), Name = "VC")]
        VC,
        [Display(ResourceType = typeof(_Country), Name = "VE")]
        VE,
        [Display(ResourceType = typeof(_Country), Name = "VG")]
        VG,
        [Display(ResourceType = typeof(_Country), Name = "VI")]
        VI,
        [Display(ResourceType = typeof(_Country), Name = "VN")]
        VN,
        [Display(ResourceType = typeof(_Country), Name = "VU")]
        VU,
        [Display(ResourceType = typeof(_Country), Name = "WF")]
        WF,
        [Display(ResourceType = typeof(_Country), Name = "WS")]
        WS,
        [Display(ResourceType = typeof(_Country), Name = "XK")]
        XK,
        [Display(ResourceType = typeof(_Country), Name = "YE")]
        YE,
        [Display(ResourceType = typeof(_Country), Name = "YT")]
        YT,
        [Display(ResourceType = typeof(_Country), Name = "ZA")]
        ZA,
        [Display(ResourceType = typeof(_Country), Name = "ZM")]
        ZM,
        [Display(ResourceType = typeof(_Country), Name = "ZW")]
        ZW
    }
}