using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myGUI : MonoBehaviour
{

    public CompositeMap composite_fuselage;
    public CompositeMap composite_wings;
    public CompositeMap[] diffuse_Composites;
    public CompositeMap[] metallic_Composites;
    public Transform sun;

    ColorsStruct[] glossColors;
    ColorsStruct[] pearlColors;
    Dictionary<string, ColorInfo> allColors;

    // Start is called before the first frame update
    void Start()
    {
        glossColors = new ColorsStruct[]
        {
            new ColorsStruct(1, "570046", "Fire Red"),
            new ColorsStruct(2, "765295", "Mandarin Orange"),
            new ColorsStruct(3, "765454", "Red"),
            new ColorsStruct(4, "564933", "Sunfast Red"),
            new ColorsStruct(5, "764840", "Toreadore Red"),
            new ColorsStruct(6, "751332", "Ferrara Red"),
            new ColorsStruct(7, "764839", "Deep Red"),
            new ColorsStruct(8, "765422", "Ward Red"),
            new ColorsStruct(9, "765401", "Maroon"),
            new ColorsStruct(10, "765388", "New Wine"),
            new ColorsStruct(11, "565713", "Lt. Sienna"),
            new ColorsStruct(12, "569926", "Zephyr Orange"),
            new ColorsStruct(13, "751494", "Prominent Orange"),
            new ColorsStruct(14, "776517", "Calypso Orange"),
            new ColorsStruct(15, "765296", "Chrome Yellow"),
            new ColorsStruct(16, "765300", "Yellow"),
            new ColorsStruct(17, "765282", "Wildcat Yellow"),
            new ColorsStruct(18, "765427", "Shell Yellow"),
            new ColorsStruct(19, "751331", "Golden Yellow"),
            new ColorsStruct(20, "615563", "Butterscotch"),
            new ColorsStruct(21, "524711", "Spring Green"),
            new ColorsStruct(22, "494546", "Green"),
            new ColorsStruct(23, "765284", "Kelly Green"),
            new ColorsStruct(24, "765279", "Shamrock Green"),
            new ColorsStruct(25, "764846", "Jade Mist Green"),
            new ColorsStruct(26, "765229", "Teal"),
            new ColorsStruct(27, "765464", "Teal Green"),
            new ColorsStruct(28, "765234", "Dk Aqua"),
            new ColorsStruct(29, "753515", "Med. Blue"),
            new ColorsStruct(30, "765253", "Bahama Blue"),
            new ColorsStruct(31, "765254", "Sky Blue"),
            new ColorsStruct(32, "765232", "Marlin Blue"),
            new ColorsStruct(33, "765255", "Colonial Blue"),
            new ColorsStruct(34, "799806", "Blue Haze"),
            new ColorsStruct(35, "765389", "Fighter Blue"),
            new ColorsStruct(36, "785089", "Axis Blue"),
            new ColorsStruct(37, "741744", "Rich Blue"),
            new ColorsStruct(38, "738651", "Elite Blue"),
            new ColorsStruct(39, "764838", "Arista Blue"),
            new ColorsStruct(40, "564925", "Royal Blue"),
            new ColorsStruct(41, "405422", "Cadbury Purple"),
            new ColorsStruct(42, "765223", "Ameri Blue"),
            new ColorsStruct(43, "566658", "Brown"),
            new ColorsStruct(44, "765453", "Chocolate Brown"),
            new ColorsStruct(45, "765249", "Sahara Tan"),
            new ColorsStruct(46, "765393", "Khaki"),
            new ColorsStruct(47, "765238", "Sandalwood Tan"),
            new ColorsStruct(48, "765252", "Castle Tan"),
            new ColorsStruct(49, "765398", "Pewter Gray"),
            new ColorsStruct(50, "765394", "Milky Way"),
            new ColorsStruct(51, "765248", "Off White II"),
            new ColorsStruct(52, "829085", "Sykes White"),
            new ColorsStruct(53, "765385", "Matterhorn White"),
            new ColorsStruct(54, "765384", "Snow White"),
            new ColorsStruct(55, "765428", "Wisp Gray"),
            new ColorsStruct(56, "764787", "Nordic Gray"),
            new ColorsStruct(57, "765407", "Whisper Gray"),
            new ColorsStruct(58, "765400", "Gray"),
            new ColorsStruct(59, "764872", "Gamma Gray"),
            new ColorsStruct(60, "803719", "Dark Gray"),
            new ColorsStruct(61, "768551", "Charcoal"),
            new ColorsStruct(62, "765425", "Flag Blue"),
            new ColorsStruct(63, "564972", "Charcoal Gray"),
            new ColorsStruct(64, "765268", "Gloss Black")
        };
        pearlColors = new ColorsStruct[]
        {
            new ColorsStruct(1, "499019", "Electric Red"),
            new ColorsStruct(2, "815550", "Saffron"),
            new ColorsStruct(3, "814889", "Rust"),
            new ColorsStruct(4, "828168", "Red"),
            new ColorsStruct(5, "828069", "Wine"),
            new ColorsStruct(6, "523646", "Cherry Red"),
            new ColorsStruct(7, "748886", "Red"),
            new ColorsStruct(8, "777015", "Radiant Red"),
            new ColorsStruct(9, "821202", "Coral"),
            new ColorsStruct(10, "739468", "Dk. Burgundy"),
            new ColorsStruct(11, "777024", "Dk Red"),
            new ColorsStruct(12, "821203", "Dk. Saddle"),
            new ColorsStruct(13, "809462", "New Orchid"),
            new ColorsStruct(14, "856770", "Dk. Toreadore Red"),
            new ColorsStruct(15, "752804", "Dk. Bronze"),
            new ColorsStruct(16, "882362", "Bronze"),
            new ColorsStruct(17, "814863", "Brown"),
            new ColorsStruct(18, "821199", "Convoy Gold"),
            new ColorsStruct(19, "825169", "Aztec Gold"),
            new ColorsStruct(20, "810403", "Las Vegas Gold"),
            new ColorsStruct(21, "820530", "Lime"),
            new ColorsStruct(22, "828192", "Green"),
            new ColorsStruct(23, "828196", "Dk. Pine"),
            new ColorsStruct(24, "818335", "Ashen Green"),
            new ColorsStruct(25, "820529", "Topaz Green"),
            new ColorsStruct(26, "748273", "Green"),
            new ColorsStruct(27, "820532", "Mint Green"),
            new ColorsStruct(28, "824789", "April Green"),
            new ColorsStruct(29, "820826", "Turquoise"),
            new ColorsStruct(30, "818775", "Turquoise Green"),
            new ColorsStruct(31, "777019", "Artic Blue"),
            new ColorsStruct(32, "839715", "Azure Blue"),
            new ColorsStruct(33, "814874", "Blue #4"),
            new ColorsStruct(34, "818101", "Steel Blue"),
            new ColorsStruct(35, "885143", "Tropical Blue"),
            new ColorsStruct(36, "885141", "Imperial Blue"),
            new ColorsStruct(37, "494293", "Lt. Rich Blue"),
            new ColorsStruct(38, "494285", "Brt. Blue"),
            new ColorsStruct(39, "815215", "Columbia Blue"),
            new ColorsStruct(40, "814502", "Ming Blue"),
            new ColorsStruct(41, "815212", "Med. Concord Blue"),
            new ColorsStruct(42, "818093", "Arc Blue"),
            new ColorsStruct(43, "814551", "Dark Blue"),
            new ColorsStruct(44, "566631", "Purple"),
            new ColorsStruct(45, "818098", "Plum"),
            new ColorsStruct(46, "828153", "Prowler Purple"),
            new ColorsStruct(47, "821373", "Maroon"),
            new ColorsStruct(48, "821205", "Oak Brown"),
            new ColorsStruct(49, "405219", "Chameleon"),
            new ColorsStruct(50, "394903", "Lt. Autumnwood"),
            new ColorsStruct(51, "398300", "Champagne"),
            new ColorsStruct(52, "739052", "Mocha"),
            new ColorsStruct(53, "821372", "Mocha Frost"),
            new ColorsStruct(54, "821365", "Desert Tan"),
            new ColorsStruct(55, "821371", "Walnut"),
            new ColorsStruct(56, "814504", "Gold"),
            new ColorsStruct(57, "828175", "Carter Gold"),
            new ColorsStruct(58, "814560", "Silver Platinum"),
            new ColorsStruct(59, "809393", "Platinum"),
            new ColorsStruct(60, "814447", "Antique Gold"),
            new ColorsStruct(61, "822284", "Green Gold"),
            new ColorsStruct(62, "808458", "Trumpet Gold"),
            new ColorsStruct(63, "809694", "Charcoal Gray"),
            new ColorsStruct(64, "566351", "Black Red"),
            new ColorsStruct(65, "821206", "Onyx Brown"),
            new ColorsStruct(66, "828186", "Spanish Gold"),
            new ColorsStruct(67, "409520", "Smoke"),
            new ColorsStruct(68, "809692", "Antique Silver"),
            new ColorsStruct(69, "828172", "Lt. Oak Leaf"),
            new ColorsStruct(70, "820527", "Castle Silver"),
            new ColorsStruct(71, "816826", "Silver Hinde"),
            new ColorsStruct(72, "808401", "Brt. Silver Sparkle"),
            new ColorsStruct(73, "828259", "Slate Gray"),
            new ColorsStruct(74, "494247", "Silver"),
            new ColorsStruct(75, "814565", "Aztec Silver"),
            new ColorsStruct(76, "815213", "Starlight Silver"),
            new ColorsStruct(77, "499588", "Platinum"),
            new ColorsStruct(78, "814562", "Titanium"),
            new ColorsStruct(79, "816074", "Cumulus Gray"),
            new ColorsStruct(80, "748854", "Gray"),
            new ColorsStruct(81, "816076", "Med Opal"),
            new ColorsStruct(82, "816046", "Phantom Gray"),
            new ColorsStruct(83, "859682", "Sable"),
            new ColorsStruct(84, "820522", "Iceland Green"),
            new ColorsStruct(85, "784072", "Amazon Blue"),
            new ColorsStruct(86, "815826", "Black")
            };

        allColors = new Dictionary<string, ColorInfo> {
            { "765385", new ColorInfo("765385", "Matterhorn White", 0, 0, 0, 's')},
            { "765384", new ColorInfo("765384", "Snow White", 0, 0, 0, 's')},
            { "765428", new ColorInfo("765428", "Wisp Gray", 0, 0, 0, 's')},
            { "765268", new ColorInfo("765268", "Gloss Black", 0, 0, 0, 's')},
            { "765407", new ColorInfo("765407", "Whisper Gray", 0, 0, 0, 's')},
            { "765406", new ColorInfo("765406", "Soft Gray", 0, 0, 0, 's')},
            { "765400", new ColorInfo("765400", "Gray", 0, 0, 0, 's')},
            { "765418", new ColorInfo("765418", "Midway Gray", 0, 0, 0, 's')},
            { "783344", new ColorInfo("783344", "Outerspace", 0, 0, 0, 's')},
            { "764788", new ColorInfo("764788", "Viking Gray", 0, 0, 0, 's')},
            { "764787", new ColorInfo("764787", "Nordic Gray", 0, 0, 0, 's')},
            { "764872", new ColorInfo("764872", "Gamma Gray", 0, 0, 0, 's')},
            { "765272", new ColorInfo("765272", "Med. Gray", 0, 0, 0, 's')},
            { "803719", new ColorInfo("803719", "Dk. Gray", 0, 0, 0, 's')},
            { "765424", new ColorInfo("765424", "Smoke Gray", 0, 0, 0, 's')},
            { "808667", new ColorInfo("808667", "Cool Gray 11C", 0, 0, 0, 's')},
            { "768551", new ColorInfo("768551", "Charcoal", 0, 0, 0, 's')},
            { "765273", new ColorInfo("765273", "Bold Gray", 0, 0, 0, 's')},
            { "564972", new ColorInfo("564972", "Charcoal Gray", 0, 0, 0, 's')},
            { "765236", new ColorInfo("765236", "San Mateo Wheat", 0, 0, 0, 's')},
            { "615563", new ColorInfo("615563", "Butterscotch", 0, 0, 0, 's')},
            { "765342", new ColorInfo("765342", "Commercial Cream", 0, 0, 0, 's')},
            { "565713", new ColorInfo("565713", "Lt. Sienna", 0, 0, 0, 's')},
            { "765453", new ColorInfo("765453", "Chocolate Brown", 0, 0, 0, 's')},
            { "765239", new ColorInfo("765239", "Lt. Beige", 0, 0, 0, 's')},
            { "765252", new ColorInfo("765252", "Castle Tan", 0, 0, 0, 's')},
            { "765393", new ColorInfo("765393", "Khaki", 0, 0, 0, 's')},
            { "568878", new ColorInfo("568878", "Stone Gray", 0, 0, 0, 's')},
            { "765249", new ColorInfo("765249", "Sahara Tan", 0, 0, 0, 's')},
            { "765248", new ColorInfo("765248", "Off White II", 0, 0, 0, 's')},
            { "765394", new ColorInfo("765394", "Milky Way", 0, 0, 0, 's')},
            { "765238", new ColorInfo("765238", "Sandalwood Tan", 0, 0, 0, 's')},
            { "765398", new ColorInfo("765398", "Pewter Gray", 0, 0, 0, 's')},
            { "764873", new ColorInfo("764873", "Slate Gray", 0, 0, 0, 's')},
            { "829085", new ColorInfo("829085", "Sykes White", 0, 0, 0, 's')},
            { "758648", new ColorInfo("758648", "Polished Gray", 0, 0, 0, 's')},
            { "747157", new ColorInfo("747157", "Entergy Gray", 0, 0, 0, 's')},
            { "765250", new ColorInfo("765250", "Sable Brown", 0, 0, 0, 's')},
            { "566658", new ColorInfo("566658", "Brown", 0, 0, 0, 's')},
            { "751331", new ColorInfo("751331", "Golden Yellow", 0, 0, 0, 's')},
            { "765427", new ColorInfo("765427", "Shell Yellow", 0, 0, 0, 's')},
            { "765300", new ColorInfo("765300", "Yellow", 0, 0, 0, 's')},
            { "765282", new ColorInfo("765282", "Wildcat Yellow", 0, 0, 0, 's')},
            { "765296", new ColorInfo("765296", "Chrome Yellow", 0, 0, 0, 's')},
            { "776517", new ColorInfo("776517", "Calypso Orange", 0, 0, 0, 's')},
            { "751494", new ColorInfo("751494", "Prominent Orange", 0, 0, 0, 's')},
            { "569926", new ColorInfo("569926", "Zephyr Orange", 0, 0, 0, 's')},
            { "765295", new ColorInfo("765295", "Mandarin Orange", 0, 0, 0, 's')},
            { "765454", new ColorInfo("765454", "Red", 0, 0, 0, 's')},
            { "570046", new ColorInfo("570046", "Fire Red", 0, 0, 0, 's')},
            { "564933", new ColorInfo("564933", "Sunfast Red", 0, 0, 0, 's')},
            { "751332", new ColorInfo("751332", "Ferrara Red", 0, 0, 0, 's')},
            { "764840", new ColorInfo("764840", "Toreador Red", 0, 0, 0, 's')},
            { "764839", new ColorInfo("764839", "Deep Red", 0, 0, 0, 's')},
            { "765422", new ColorInfo("765422", "Ward Red", 0, 0, 0, 's')},
            { "765401", new ColorInfo("765401", "Maroon", 0, 0, 0, 's')},
            { "765251", new ColorInfo("765251", "Vendetta Red", 0, 0, 0, 's')},
            { "765388", new ColorInfo("765388", "New Wine", 0, 0, 0, 's')},
            { "765421", new ColorInfo("765421", "Commercial Maroon", 0, 0, 0, 's')},
            { "494532", new ColorInfo("494532", "Baffin Blue", 0, 0, 0, 's')},
            { "753515", new ColorInfo("753515", "Med. Blue", 0, 0, 0, 's')},
            { "321257", new ColorInfo("321257", "Beach Blue", 0, 0, 0, 's')},
            { "765254", new ColorInfo("765254", "Sky Blue", 0, 0, 0, 's')},
            { "765232", new ColorInfo("765232", "Marlin Blue", 0, 0, 0, 's')},
            { "765255", new ColorInfo("765255", "Colonial Blue", 0, 0, 0, 's')},
            { "741744", new ColorInfo("741744", "Rich Blue", 0, 0, 0, 's')},
            { "785089", new ColorInfo("785089", "Axis Blue", 0, 0, 0, 's')},
            { "738651", new ColorInfo("738651", "Elite Blue", 0, 0, 0, 's')},
            { "764838", new ColorInfo("764838", "Arista Blue", 0, 0, 0, 's')},
            { "765462", new ColorInfo("765462", "Curro Lt. Blue", 0, 0, 0, 's')},
            { "765253", new ColorInfo("765253", "Bahama Blue", 0, 0, 0, 's')},
            { "765392", new ColorInfo("765392", "Smoke Blue Haze", 0, 0, 0, 's')},
            { "765391", new ColorInfo("765391", "Squadron Blue", 0, 0, 0, 's')},
            { "765425", new ColorInfo("765425", "Flag Blue", 0, 0, 0, 's')},
            { "799806", new ColorInfo("799806", "Blue Haze", 0, 0, 0, 's')},
            { "765389", new ColorInfo("765389", "Fighter Blue", 0, 0, 0, 's')},
            { "564925", new ColorInfo("564925", "Royal Blue", 0, 0, 0, 's')},
            { "765223", new ColorInfo("765223", "Ameri Blue", 0, 0, 0, 's')},
            { "405422", new ColorInfo("405422", "Cadbury Purple", 0, 0, 0, 's')},
            { "765456", new ColorInfo("765456", "Lt. Turquoise", 0, 0, 0, 's')},
            { "765234", new ColorInfo("765234", "Dk Aqua", 0, 0, 0, 's')},
            { "499362", new ColorInfo("499362", "Blue Green", 0, 0, 0, 's')},
            { "739520", new ColorInfo("739520", "Med Teal", 0, 0, 0, 's')},
            { "765229", new ColorInfo("765229", "Teal", 0, 0, 0, 's')},
            { "819156", new ColorInfo("819156", "Cool Green", 0, 0, 0, 's')},
            { "494546", new ColorInfo("494546", "Green", 0, 0, 0, 's')},
            { "819984", new ColorInfo("819984", "Soft Teal", 0, 0, 0, 's')},
            { "765464", new ColorInfo("765464", "Teal Green", 0, 0, 0, 's')},
            { "575293", new ColorInfo("575293", "British Racing Green", 0, 0, 0, 's')},
            { "524711", new ColorInfo("524711", "Spring Green", 0, 0, 0, 's')},
            { "765396", new ColorInfo("765396", "Pilot Green", 0, 0, 0, 's')},
            { "745826", new ColorInfo("745826", "Summer Green", 0, 0, 0, 's')},
            { "765279", new ColorInfo("765279", "Shamrock Green", 0, 0, 0, 's')},
            { "765284", new ColorInfo("765284", "Kelly Green", 0, 0, 0, 's')},
            { "765395", new ColorInfo("765395", "Skyline Steel", 0, 0, 0, 's')},
            { "496141", new ColorInfo("496141", "Sage Green", 0, 0, 0, 's')},
            { "765441", new ColorInfo("765441", "Med. Green", 0, 0, 0, 's')},
            { "764846", new ColorInfo("764846", "Jade Mist Green", 0, 0, 0, 's')},
            { "799805", new ColorInfo("799805", "Broklands Green", 0, 0, 0, 's')},


            { "816826", new ColorInfo("816826", "Silver Hinde", 0, 0, 0, 'p')},
            { "814565", new ColorInfo("814565", "Aztec Silver", 0, 0, 0, 'p')},
            { "815213", new ColorInfo("815213", "Starlight Silver", 0, 0, 0, 'p')},
            { "816070", new ColorInfo("816070", "Cumulus", 0, 0, 0, 'p')},
            { "816074", new ColorInfo("816074", "Cumulus Gray", 0, 0, 0, 'p')},
            { "828259", new ColorInfo("828259", "Slate Gray", 0, 0, 0, 'p')},
            { "815826", new ColorInfo("815826", "Black", 0, 0, 0, 'p')},
            { "859682", new ColorInfo("859682", "Sable", 0, 0, 0, 'p')},
            { "814562", new ColorInfo("814562", "Titanium", 0, 0, 0, 'p')},
            { "809692", new ColorInfo("809692", "Antique Silver", 0, 0, 0, 'p')},
            { "816037", new ColorInfo("816037", "Med. Taupe", 0, 0, 0, 'p')},
            { "816046", new ColorInfo("816046", "Phantom Gray", 0, 0, 0, 'p')},
            { "808458", new ColorInfo("808458", "Trumpet Gold", 0, 0, 0, 'p')},
            { "828172", new ColorInfo("828172", "Lt. Oak Leaf", 0, 0, 0, 'p')},
            { "828186", new ColorInfo("828186", "Spanish Gold", 0, 0, 0, 'p')},
            { "809694", new ColorInfo("809694", "Charcoal Gray", 0, 0, 0, 'p')},
            { "825169", new ColorInfo("825169", "Aztec Gold", 0, 0, 0, 'p')},
            { "814447", new ColorInfo("814447", "Antique Gold", 0, 0, 0, 'p')},
            { "822284", new ColorInfo("822284", "Green Gold", 0, 0, 0, 'p')},
            { "828175", new ColorInfo("828175", "Carter Gold", 0, 0, 0, 'p')},
            { "815216", new ColorInfo("815216", "CS Platinum 2", 0, 0, 0, 'p')},
            { "809393", new ColorInfo("809393", "Platinum", 0, 0, 0, 'p')},
            { "859687", new ColorInfo("859687", "Platinum", 0, 0, 0, 'p')},
            { "821372", new ColorInfo("821372", "Mocha Frost", 0, 0, 0, 'p')},
            { "810403", new ColorInfo("810403", "Las Vegas Gold", 0, 0, 0, 'p')},
            { "814504", new ColorInfo("814504", "Gold", 0, 0, 0, 'p')},
            { "814560", new ColorInfo("814560", "Silver Platinum", 0, 0, 0, 'p')},
            { "821203", new ColorInfo("821203", "Dk. Saddle", 0, 0, 0, 'p')},
            { "821365", new ColorInfo("821365", "Desert Tan", 0, 0, 0, 'p')},
            { "821371", new ColorInfo("821371", "Walnut", 0, 0, 0, 'p')},
            { "821206", new ColorInfo("821206", "Onyx Brown", 0, 0, 0, 'p')},
            { "821205", new ColorInfo("821205", "Oak Brown", 0, 0, 0, 'p')},
            { "821199", new ColorInfo("821199", "Convoy Gold", 0, 0, 0, 'p')},
            { "821202", new ColorInfo("821202", "Coral", 0, 0, 0, 'p')},
            { "814889", new ColorInfo("814889", "Rust", 0, 0, 0, 'p')},
            { "815550", new ColorInfo("815550", "Saffron", 0, 0, 0, 'p')},
            { "828168", new ColorInfo("828168", "Red", 0, 0, 0, 'p')},
            { "777015", new ColorInfo("777015", "Radiant Red", 0, 0, 0, 'p')},
            { "499019", new ColorInfo("499019", "Electric Red", 0, 0, 0, 'p')},
            { "856770", new ColorInfo("856770", "Dk. Toreador Red", 0, 0, 0, 'p')},
            { "828150", new ColorInfo("828150", "Maroon Pearl #3", 0, 0, 0, 'p')},
            { "828069", new ColorInfo("828069", "Wine", 0, 0, 0, 'p')},
            { "777024", new ColorInfo("777024", "Dk. Red", 0, 0, 0, 'p')},
            { "821373", new ColorInfo("821373", "Maroon", 0, 0, 0, 'p')},
            { "828152", new ColorInfo("828152", "Dk. Rose Gray", 0, 0, 0, 'p')},
            { "809462", new ColorInfo("809462", "New Orchid", 0, 0, 0, 'p')},
            { "828153", new ColorInfo("828153", "Prowler Purple", 0, 0, 0, 'p')},
            { "566351", new ColorInfo("566351", "Black Red", 0, 0, 0, 'p')},
            { "814875", new ColorInfo("814875", "Sapphire Blue", 0, 0, 0, 'p')},
            { "839715", new ColorInfo("839715", "Azure Blue", 0, 0, 0, 'p')},
            { "818100", new ColorInfo("818100", "Sky Blue", 0, 0, 0, 'p')},
            { "818099", new ColorInfo("818099", "Lt. Rich Blue", 0, 0, 0, 'p')},
            { "814874", new ColorInfo("814874", "Blue #4", 0, 0, 0, 'p')},
            { "885143", new ColorInfo("885143", "Tropical Blue", 0, 0, 0, 'p')},
            { "814502", new ColorInfo("814502", "Ming Blue", 0, 0, 0, 'p')},
            { "815215", new ColorInfo("815215", "Columbia Blue", 0, 0, 0, 'p')},
            { "818101", new ColorInfo("818101", "Steel Blue", 0, 0, 0, 'p')},
            { "885141", new ColorInfo("885141", "Imperial Blue", 0, 0, 0, 'p')},
            { "818093", new ColorInfo("818093", "Arc Blue", 0, 0, 0, 'p')},
            { "814551", new ColorInfo("814551", "Dk. Blue", 0, 0, 0, 'p')},
            { "818098", new ColorInfo("818098", "Plum", 0, 0, 0, 'p')},
            { "566631", new ColorInfo("566631", "Purple", 0, 0, 0, 'p')},
            { "815212", new ColorInfo("815212", "Med. Concorde", 0, 0, 0, 'p')},
            { "784072", new ColorInfo("784072", "Amazon Blue", 0, 0, 0, 'p')},
            { "820826", new ColorInfo("820826", "Turquoise", 0, 0, 0, 'p')},
            { "818775", new ColorInfo("818775", "Turquoise Green", 0, 0, 0, 'p')},
            { "777019", new ColorInfo("777019", "Arctic Blue", 0, 0, 0, 'p')},
            { "816076", new ColorInfo("816076", "Med. Opal", 0, 0, 0, 'p')},
            { "820529", new ColorInfo("820529", "Topaz Green", 0, 0, 0, 'p')},
            { "820532", new ColorInfo("820532", "Mint Green", 0, 0, 0, 'p')},
            { "820523", new ColorInfo("820523", "Green", 0, 0, 0, 'p')},
            { "824789", new ColorInfo("824789", "April Green", 0, 0, 0, 'p')},
            { "820527", new ColorInfo("820527", "Castle Silver", 0, 0, 0, 'p')},
            { "820530", new ColorInfo("820530", "Lime", 0, 0, 0, 'p')},
            { "818327", new ColorInfo("818327", "Tropical Green", 0, 0, 0, 'p')},
            { "818335", new ColorInfo("818335", "Ashen Green", 0, 0, 0, 'p')},
            { "828192", new ColorInfo("828192", "Green", 0, 0, 0, 'p')},
            { "828196", new ColorInfo("828196", "Dk. Pine", 0, 0, 0, 'p')},
            { "820522", new ColorInfo("820522", "Iceland Green", 0, 0, 0, 'p')},
            { "499583", new ColorInfo("499583", "Jet Black", 0, 0, 0, 'p')},


            { "808401", new ColorInfo("808401", "Brt. Silver Sparkle", 0, 0, 0, 'm')},
            { "523604", new ColorInfo("523604", "Just Silver", 0, 0, 0, 'm')},
            { "494247", new ColorInfo("494247", "Silver", 0, 0, 0, 'm')},
            { "748854", new ColorInfo("748854", "Gray", 0, 0, 0, 'm')},
            { "499588", new ColorInfo("499588", "Platinum", 0, 0, 0, 'm')},
            { "494261", new ColorInfo("494261", "Pepper Gray", 0, 0, 0, 'm')},
            { "409520", new ColorInfo("409520", "Smoke", 0, 0, 0, 'm')},
            { "748864", new ColorInfo("748864", "Phantom Gray", 0, 0, 0, 'm')},
            { "739052", new ColorInfo("739052", "Mocha", 0, 0, 0, 'm')},
            { "398300", new ColorInfo("398300", "Champagne", 0, 0, 0, 'm')},
            { "394903", new ColorInfo("394903", "Lt. Autumnwood", 0, 0, 0, 'm')},
            { "405219", new ColorInfo("405219", "Chameleon", 0, 0, 0, 'm')},
            { "755046", new ColorInfo("755046", "Gold", 0, 0, 0, 'm')},
            { "499848", new ColorInfo("499848", "Gold", 0, 0, 0, 'm')},
            { "765876", new ColorInfo("765876", "Gold", 0, 0, 0, 'm')},
            { "499151", new ColorInfo("499151", "Gold", 0, 0, 0, 'm')},
            { "497504", new ColorInfo("497504", "Convoy Gold", 0, 0, 0, 'm')},
            { "882362", new ColorInfo("882362", "Bronze", 0, 0, 0, 'm')},
            { "814863", new ColorInfo("814863", "Brown", 0, 0, 0, 'm')},
            { "752804", new ColorInfo("752804", "Dk. Bronze", 0, 0, 0, 'm')},
            { "749137", new ColorInfo("749137", "Lt. Firethorn", 0, 0, 0, 'm')},
            { "739468", new ColorInfo("739468", "Dk. Burgundy", 0, 0, 0, 'm')},
            { "523646", new ColorInfo("523646", "Cherry Red", 0, 0, 0, 'm')},
            { "748886", new ColorInfo("748886", "Red", 0, 0, 0, 'm')},
            { "494285", new ColorInfo("494285", "Brt. Blue", 0, 0, 0, 'm')},
            { "494293", new ColorInfo("494293", "Lt. Rich Blue", 0, 0, 0, 'm')},
            { "736951", new ColorInfo("736951", "Sovereign Blue", 0, 0, 0, 'm')},
            { "742662", new ColorInfo("742662", "Med. Concord Blue", 0, 0, 0, 'm')},
            { "408784", new ColorInfo("408784", "Mystic Teal", 0, 0, 0, 'm')},
            { "394843", new ColorInfo("394843", "Forest Green", 0, 0, 0, 'm')},
            { "882358", new ColorInfo("882358", "Jade Green", 0, 0, 0, 'm')},
            { "748273", new ColorInfo("748273", "Green", 0, 0, 0, 'm')},
        };
    }

    // Update is called once per frame
    void Update()
    {
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 100, 20), "white"))
    //    {
    //        if (composite_fuselage)
    //        {
    //            composite_fuselage.Layers[1].Mul = new Color(1f, 1f, 1f);
    //            composite_fuselage.Render();
    //            composite_wings.Layers[1].Mul = new Color(1f, 1f, 1f);
    //            composite_wings.Render();
    //        }
    //    }
    //    if (GUI.Button(new Rect(0, 20, 100, 20), "yellow"))
    //    {
    //        if (composite_fuselage)
    //        {
    //            composite_fuselage.Layers[1].Mul = new Color(1f, 1f, 0f);
    //            composite_fuselage.Render();
    //            composite_wings.Layers[1].Mul = new Color(1f, 1f, 0f);
    //            composite_wings.Render();
    //        }
    //    }
    //}

    public void ChangeColor(string param)
    {
        string[] filt = param.Split('|');
        int layer = int.Parse(filt[0]);
        string paint_code = filt[1];
        ColorInfo colorInfo = allColors[paint_code];



    }
    public void ChangePaintScheme(string new_paintScheme)
    {

    }
    public void ResetColors()
    {
        // this should be done by javascript calling several times ChangeColor
    }

    public void ChangeColor_old(string new_color)
    {
        Color c;
        switch (new_color)
        {
            case "yellow":
                c = new Color(1f, 1f, 0f);
                break;
            case "green":
                c = new Color(0f, 1f, 0f);
                break;
            case "light-blue":
                c = new Color(0.5f, 0.5f, 1f);
                break;
            case "blue":
                c = new Color(0f, 0f, 1f);
                break;
            case "white":
                c = new Color(1f, 1f, 1f);
                break;
            case "gray":
                c = new Color(0.5f, 0.5f, 0.5f);
                break;
            case "red":
                c = new Color(1f, 0f, 0f);
                break;
            default:
                c = new Color(1f, 1f, 1f);
                break;
        }
        if (composite_fuselage)
        {
            composite_fuselage.Layers[1].Mul = c;
            composite_fuselage.Render();
        }
        if (composite_wings)
        {
            composite_wings.Layers[1].Mul = c;
            composite_wings.Render();
        } 
    }

    public void LightUpTheSun()
    {
        if (sun)
        {
            sun.gameObject.SetActive(true);
        }
    }
}

public struct ColorInfo
{
    string code, name;
    //bool isPearl;
    char color_type;
    int r, g, b;
    public ColorInfo(string _code, string _name, int _r, int _g, int _b, char _colorType)
    {
        code = _code;
        name = _name;
        color_type = _colorType;
        r = _r;
        g = _g;
        b = _b;
    }
}

public struct ColorsStruct
{
    int index;
    string code;
    string name;
    int r, g, b;

    public ColorsStruct(int _index, string _code, string _name, int _r=0, int _g=0, int _b=0) {
        index = _index;
        code = _code;
        name = _name;
        r = _r;
        g = _g;
        b = _b;
    }
}