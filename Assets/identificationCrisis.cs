using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;
using rnd = UnityEngine.Random;

public class identificationCrisis : MonoBehaviour
{
    public new KMAudio audio;
    private KMAudio.KMAudioRef mainRef;
    public KMBombInfo bomb;
    public KMBombModule module;

    public KMSelectable[] keyboard;
    public TextMesh screenText;
    public Renderer display;
    public Renderer[] lights;
    public Color litColor;

    public Transform moduleTransform;
    public Renderer frame;
    public Renderer surface;
    public Material normalMat;
    public Material transparentMat;
    public Material postProcessingMat;
    public Renderer face;
    public Texture[] smileyFaces;
    public Texture[] questionMarks;
    public Texture[] frameTextures;
    public Texture[] surfaceTextures;
    public Texture prettyRainbow;
    public Texture staticTexture;
    public Color[] rainbowColors;

    public Texture[] shapeTextures;
    public Texture[] boozleglyphTexturesA;
    public Texture[] boozleglyphTexturesB;
    public Texture[] boozleglyphTexturesC;
    public Texture[] plantTextures;
    public Texture[] pickupTextures;
    public Texture[] emotiguyTextures;
    public Texture[] arsTextures;
    public Texture[] miiTextures;
    public Texture[] customerTextures;
    public Texture[] spongebobTextures;
    public Texture[] vtuberTextures;
    public Texture[] bonusTextures;

    private Renderer[] keyRenders = null;
    public Renderer dawn;
    private Color startingKeyColor;
    public Color crimsonRed;
    private CameraPostProcess postProcess = null;
    private Transform mainCameraTransform = null;

    private static readonly string[] shapeNames = new string[8] { "Circle", "Square", "Diamond", "Heart", "Star", "Triangle", "Pentagon", "Hexagon" };
    private static readonly string[] morseNames = new string[26] { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-Ray", "Yankee", "Zulu" };
    private static readonly string[] boozleglyphNamesA = new string[26] { "Alztimer's", "Braintenance", "Color allergy", "Detonession", "Emojilepsy", "Foot and Morse", "Gout of Life", "HRV", "Indicitis", "Jaundry", "Keypad stones", "Legomania", "Microcontusion", "Narcolization", "OCd", "Piekinson's", "Quackgrounds", "Royal Flu", "Seizure Siphor", "Tetrinus", "Urinary LEDs", "Verticode", "Widgeting", "XMAs", "Yes-no infection", "Zooties" };
    private static readonly string[] boozleglyphNamesB = new string[26] { "Azure", "Blue", "Crimson", "Diamond", "Emerald", "Fuschia", "Green", "Hazel", "Ice", "Jade", "Kiwi", "Lime", "Magenta", "Navy", "Orange", "Purple", "Quartz", "Red", "Salmon", "Tan", "Ube", "Vibe", "White", "Xotic", "Yellow", "Zen" };
    private static readonly string[] boozleglyphNamesC = new string[26] { "Arthur", "Brooke", "Chevon", "Dante", "Ethelgard", "Florence", "Gregory", "Hester", "Isala", "Javier", "Kevin", "Lexi", "Meghan", "Niamh", "Oliver", "Patrick", "Quentin", "Riley", "Sabrina", "Tamby", "Ulysses", "Via", "Winter", "Xavier", "Yaretzi", "Zander", };
    private static readonly string[] plantNames = new string[234] { "A.K.E.E.", "Acidic Citrus", "Aggro Brocco", "Ail-mint", "Alarm Arrowhead", "Aloe", "Ampthurium", "Angel Starfruit", "Appease-mint", "Apple Mortar", "Arma-mint", "Aspiragus", "Auberninja", "Bamboo-shoot", "Bambrook", "Banana Dancer", "Banana Launcher", "Berry Blaster", "Blastberry Vine", "Bloomerang", "Blooming Heart", "Blover", "Board Beans", "Boingsetta", "Bombard-mint", "Bombergranate", "Bonk Choy", "Boom Balloon Flower", "Bowling Bulb", "Bromel Blade", "Bud-minton", "Cabbage-pult", "Cactus", "Carrotillery", "Cattail", "Caulipower", "Celery Stalker", "Chard Guard", "Cherry Bomb", "Chest-nut", "Chili Bean", "Chomper", "Citron", "Cob Cannon", "Coconut Cannon", "Coffee Bean", "Cold Snapdragon", "Conceal-mint", "Contain-mint", "Cryo-shroom", "Curling Corms", "Cycloque", "Dandelion", "Dartichoke", "Dazey Chain", "Dinonip", "Dollarweed Drummer", "Draftodil", "Dragonfruit", "Dual Pistol Pinecone", "Dusk Lobber", "E.M. Peach", "Egrett Flower", "Electric Blueberry", "Electric Currant", "Electric Peashooter", "Electrici-tea", "Enchant-mint", "Endurian", "Enforce-mint", "Enlighten-mint", "Escape Root", "Explode-O-Nut", "Explode-o-Vine", "Fanilla", "Fila-mint", "Fire Gourd", "Fire Peashooter", "Firebloom Queen", "Flat-shroom", "Frostbloom Queen", "Fume-shroom", "Garlic", "Gatling Pea", "Ghost Pepper", "Gloom Vine", "Gold Bloom", "Gold Leaf", "Goo Peashooter", "Grapeshot", "Grave Buster", "Grimrose", "Guacodile", "Guerrequila", "Gumnut", "Headbutter Lettuce", "Heavenly Peach", "Hocus Crocus", "Holly Barrier", "Homing Thistle", "Hot Date", "Hot Potato", "Hurrikale", "Hypno-shroom", "Ice Bloom", "Iceberg Lettuce", "Icy Currant", "Imitater", "Imp Pear", "Infi-nut", "Intensive Carrot", "Jack O' Lantern", "Jackfruit", "Jalapeno", "Kernel-pult", "Kinnikannon", "Kiwibeast", "Lantern Cherry", "Laser Bean", "Lava Guava", "Lightning Reed", "Lily Pad", "Lily of Alchemy", "Lord Bamboo", "Lotus Root", "Lotuspot", "MC Glory", "Magic-shroom", "Magnet-shroom", "Magnifying Grass", "Marigold", "Mastercane", "Match Flower", "Melon-pult", "Missile Toe", "Monkeyfruit", "Moonflower", "Murkadamia Nut", "Narcissus", "Nightshade", "Oak Archer", "Oil Olive", "Olive Pit", "Parsnip", "Passionflower", "Pea Pod", "Pea Vine", "Pea-nut", "Peashooter", "Pepper-mint", "Pepper-pult", "Perfume-shroom", "Phat Beet", "Plantern", "Plumping Plummy", "Pokra", "Pomegunate", "Potato Mine", "Power Lily", "Primal Peashooter", "Primal Potato Mine", "Primal Rafflesia", "Primal Sunflower", "Primal Wall-nut", "Puff-shroom", "Puffball", "Pumpkin", "Pumpkin Witch", "Pyre Vine", "Pyro-shroom", "R.A.D. Missiles", "Radish", "Rafflesia", "Red Stinger", "Reinforce-mint", "Repeater", "Rotobaga", "Rotten Red", "Ruby Red", "Sap-fling", "Saucer Squash", "Shadow Peashooter", "Shadow Vanilla", "Shadow-shroom", "Shine Vine", "Shrinking Violet", "Sling Pea", "Snap Pea", "Snapdragon", "Snow Pea", "Snowy Cotton", "Solar Tomato", "Spear-mint", "Spikerock", "Spikeweed", "Spinnapple", "Split Pea", "Spore-shroom", "Spring Bean", "Squash", "Stallia", "Starfruit", "Sticky Bomb Rice", "Strawburst", "Stunion", "Sumo Melon", "Sun Bean", "Sun-shroom", "Sunflower", "Sunflower Singer", "Sweet Potato", "Tall-nut", "Tangle Kelp", "Thorns", "Threepeater", "Thyme Warp", "Tile Turnip", "Toadstool", "Torchwood", "Tornacorn", "Trump Tulip", "Tumbleweed", "Turkey-pult", "Twin Sunflower", "Ultomato", "Wall-nut", "Wasabi Whip", "Wax Guard", "Winter Melon", "Winter-mint", "Witch Hazel", "Zapdragon", "Zorrose ", "Zoybean Pod" };
    private static readonly string[] pickupNames = new string[716] { "120 Volt", "1up!", "20/20", "2Spooky", "3 Dollar Bill", "4.5 Volt", "7 Seals", "8 Inch Nails", "9 Volt", "<3", "???'s Only Friend", "A Bar of Soap", "A Dollar", "A Lump of Coal", "A Pony", "A Pound of Flesh", "A Quarter", "A Snack", "Abaddon", "Abel", "Abyss", "Acid Baby", "Act of Contrition", "Adrenaline", "Akeldama", "Alabaster Box", "Almond Milk", "Analog Stick", "Anarchist Cookbook", "Anemic", "Angelic Prism", "Angry Fly", "Anima Sola", "Ankh", "Anti-Gravity", "Apple!", "Aquarius", "Aries", "Astral Projection", "Athame", "Azazel's Rage", "Backstabber", "Bag of Crafting", "Ball of Bandages", "Ball of Tar", "Battery Pack", "BBF", "Belly Button", "Belly Jelly", "Berserk!", "Best Bud", "Best Friend", "Betrayal", "BFFS!", "Big Chubby", "Big Fan", "Binge Eater", "Binky", "Bird Cage", "Bird's Eye", "Birthright", "Black Candle", "Black Hole", "Black Lotus", "Black Powder", "Blank Card", "Blanket", "Blood Bag", "Blood Bombs", "Blood Clot", "Blood Oath", "Blood of the Martyr", "Blood Puppy", "Blood Rights", "Bloodshot Eye", "Bloody Gust", "Bloody Lust", "Blue Cap", "Blue Map", "Bob's Brain", "Bob's Curse", "Bob's Rotten Head", "Bobby-Bomb", "Bogo Bombs", "Boiled Baby", "Bomb Bag", "Bomber Boy", "Bone Spurs", "Book of Revelations", "Book of Secrets", "Book of Shadows", "Book of the Dead", "Book of Virtues", "Boom!", "Booster Pack", "Bot Fly", "Box of Friends", "Box of Spiders", "Box", "Bozo", "Breakfast", "Breath of Life", "Brimstone", "Brimstone Bombs", "Brittle Bones", "Broken Glass Cannon", "Broken Modem", "Broken Shovel", "Broken Shovel", "Broken Watch", "Brother Bobby", "Brown Nugget", "Bucket of Lard", "Buddy in a Box", "Bum Friend", "Bumbo", "Bursting Sack", "Butt Bombs", "Butter Bean", "C Section", "Caffeine Pill", "Cain's Other Eye", "Cambion Conception", "Camo Undies", "Cancer", "Candy Heart", "Capricorn", "Car Battery", "Card Reading", "Cat-O-Nine-Tails", "Celtic Cross", "Censer", "Ceremonial Robes", "Champion Belt", "Chaos", "Charged Baby", "Charm of the Vampire", "Chemical Peel", "Chocolate Milk", "Circle of Protection", "Clear Rune", "Clicker", "Compost", "Compound Fracture", "Cone Head", "Consolation Prize", "Contagion", "Continuum", "Contract From Below", "Converter", "Coupon", "Crack Jacks", "Crack the Sky", "Cracked Orb", "Cricket's Body", "Cricket's Head", "Crooked Penny", "Crown of Light", "Crystal Ball", "Cube Baby", "Cube of Meat", "Cupid's Arrow", "Curse of the Tower", "Cursed Eye", "D Infinity", "D1", "D4", "D7", "D8", "D10", "D12", "D20", "D100", "Dad's Key", "Dad's Lost Coin", "Dad's Note", "Dad's Ring", "Daddy Longlegs", "Damocles", "Dark Arts", "Dark Bum", "Dark Matter", "Dark Prince's Crown", "Dataminer", "Dead Bird", "Dead Cat", "Dead Dove", "Dead Eye", "Dead Onion", "Dead Sea Scrolls", "Dead Tooth", "Death Certificate", "Death's List", "Death's Touch", "Decap Attack", "Deck of Cards", "Deep Pockets", "Delirious", "Demon Baby", "Depression", "Dessert", "Dinner", "Diplopia", "Dirty Mind", "Distant Admiration", "Divine Intervention", "Divorce Papers", "Doctor's Remote", "Dog Tooth", "Dogma", "Dr. Fetus", "Dream Catcher", "Dry Baby", "Duality", "Dull Razor", "E Coli", "Echo Chamber", "Eden's Blessing", "Eden's Soul", "Empty Heart", "Empty Vessel", "Epic Fetus", "Epiphora", "Eraser", "Esau Jr.", "Eternal D6", "Eucharist", "Euthanasia", "Eve's Mascara", "Everything Jar", "Evil Charm", "Evil Eye", "Experimental Treatment", "Explosivo", "Eye Drops", "Eye Sore", "Eye of Belial", "Eye of Greed", "Eye of the Occult", "False PHD", "Fanny Pack", "Farting Baby", "Fast Bombs", "Fate", "Fate's Reward", "Finger!", "Fire Mind", "Flat Stone", "Flip", "Flush!", "Forever Alone", "Forget Me Now", "Fortune Cookie", "Free Lemonade", "Freezer Baby", "Friend Finder", "Friend Zone", "Friendly Ball", "Fruit Cake", "Fruity Plum", "GB Bug", "Gello", "Gemini", "Genesis", "Ghost Baby", "Ghost Bombs", "Ghost Pepper", "Giant Cell", "Gimpy", "Glass Cannon", "Glaucoma", "Glitched Crown", "Glitter Bombs", "Glowing Hour Glass", "Glyph of Balance", "Gnawed Leaf", "Goat Head", "God's Flesh", "Godhead", "Golden Razor", "Greed's Gullet", "Growth Hormones", "Guardian Angel", "Guillotine", "Guppy's Collar", "Guppy's Eye", "Guppy's Hair Ball", "Guppy's Head", "Guppy's Paw", "Guppy's Tail", "Habit", "Haemolacria", "Hallowed Ground", "Halo of Flies", "Harlequin Baby", "Head of Krampus", "Head of the Keeper", "Headless Baby", "Heartbreak", "Hemoptysis", "Hive Mind", "Hold", "Holy Grail", "Holy Light", "Holy Mantle", "Holy Water", "Host Hat", "Hot Bombs", "How to Jump", "Humbling Bundle", "Hungry Soul", "Hushy", "Hypercoagulation", "IBS", "Immaculate Conception", "Immaculate Heart", "Incubus", "Infamy", "Infestation", "Infestation 2", "Inner Child", "Ipecac", "Iron Bar", "Isaac's Heart", "Isaac's Tears", "Isaac's Tomb", "It Hurts", "IV Bag", "Jacob's Ladder", "Jar of Flies", "Jar of Wisps", "Jaw Bone", "Jesus Juice", "Judas' Shadow", "Juicy Sack", "Jumper Cables", "Jupiter", "Kamikaze!", "Keeper's Box", "Keeper's Kin", "Keeper's Sack", "Key Bum", "Key Piece 1", "Key Piece 2", "Kidney Bean", "Kidney Stone", "King Baby", "Knife Piece 1", "Knife Piece 2", "Knockout Drops", "Lachryphagy", "Large Zit", "Larynx", "Latch Key", "Lazarus' Rags", "Lead Pencil", "Leech", "Lemegeton", "Lemon Mishap", "Leo", "Leprosy", "Libra", "Lil Abaddon", "Lil Brimstone", "Lil Chest", "Lil Delirium", "Lil Dumpy", "Lil Gurdy", "Lil Haunt", "Lil Loki", "Lil Monstro", "Lil Portal", "Lil Spewer", "Linger Bean", "Little Baggy", "Little C.H.A.D.", "Little Chubby", "Little Gish", "Little Horn", "Little Steven", "Lodestone", "Loki's Horns", "Lord of the Pit", "Lost Contact", "Lost Fly", "Lost Soul", "Lucky Foot", "Luna", "Lunch", "Lusty Blood", "Maggy's Bow", "Magic 8 Ball", "Magic Fingers", "Magic Mushroom", "Magic Scab", "Magic Skin", "Magneto", "Mama Mega!", "Marbles", "Marked", "Marrow", "Mars", "Match Book", "Maw of the Void", "Meat Cleaver", "MEAT!", "Mega Bean", "Mega Blast", "Mega Mush", "Member Card", "Mercurius", "Metal Plate", "Metronome", "Midas' Touch", "Midnight Snack", "Milk!", "Mine Crafter", "Mini Mush", "Missing No.", "Missing Page 2", "Mitre", "Mom's Bottle of Pills", "Mom's Box", "Mom's Bra", "Mom's Bracelet", "Mom's Coin Purse", "Mom's Contacts", "Mom's Eye", "Mom's Eyeshadow", "Mom's Heels", "Mom's Key", "Mom's Knife", "Mom's Lipstick", "Mom's Pad", "Mom's Pearls", "Mom's Perfume", "Mom's Purse", "Mom's Razor", "Mom's Shovel", "Mom's Underwear", "Mom's Wig", "Money = Power", "Mongo Baby", "Monster Manual", "Monstrance", "Monstro's Lung", "Monstro's Tooth", "Montezuma's Revenge", "More Options", "Moving Box", "Mr. Boom", "Mr. Dolly", "Mr. Emga", "Mr. ME!", "Mucormycosis", "Multidimensional Baby", "Mutant Spider", "My Little Unicorn", "My Reflection", "My Shadow", "Mysterious Liquid", "Mystery Egg", "Mystery Gift", "Mystery Sack", "Nancy Bombs", "Neptunus", "Night Light", "No. 2", "Notched Axe", "Number One", "Obsessed Fan", "Ocular Rift", "Odd Mushroom", "Odd Mushroom", "Old Bandage", "Options?", "Orphan Socks", "Ouija Board", "PHD", "PJs", "Pageant Boy", "Pandora's Box", "Papa Fly", "Parasitoid", "Paschal Candle", "Pause", "Pay to Play", "Pentagram", "Piggy Bank", "Pisces", "Placebo", "Placenta", "Plan C", "Playdough Cookie", "Plum Flute", "Pluto", "Pointy Rib", "Poke Go", "Polydactyly", "Polyphemus", "Pop!", "Portable Slot", "Potato Peeler", "Prayer Card", "Proptosis", "Psy Fly", "Punching Bag", "Pupula Duplex", "Purgatory", "Purity", "Pyro", "Pyromaniac", "Quints", "R Key", "Rainbow Baby", "Raw Liver", "Razor Blade", "Recall", "Red Candle", "Red Key", "Red Stew", "Redemption", "Remote Detonator", "Restock", "Revelation", "Robo-Baby", "Robo-Baby 2.0", "Rock Bottom", "Rocket in a Jar", "Roid Rage", "Rosary", "Rotten Baby", "Rotten Meat", "Rotten Tomato", "Rubber Cement", "Rune Bag", "Sack Head", "Sack of Pennis", "Sack of Sacks", "Sacread Heart", "Sacred Orb", "Sacrificial Altar", "Sacrificial Dagger", "Sad Bombs", "Safety Pin", "Sagittarius", "Salvation", "Samson's Chains", "Sanguine Bond", "Satanic Bible", "Saturnus", "Sausage", "Scapular", "Scatter Bombs", "Schoolbag", "Schoop Da Woop!", "Scissors", "Scorpio", "Screw", "Seraphim", "Serpent's Kiss", "Shade", "Shard of Glass", "Sharp Key", "Sharp Plug", "Sharp Straw", "Sinus Infection", "Sissy Longlegs", "Sister Maggy", "Skatole", "Skeleton Key", "Slipped Rib", "Smart Fly", "SMB Super Fan", "Smelter", "Sol", "Soul Locket", "Soy Milk", "Spear of Destiny", "Speed Ball", "Spelunker Hat", "Spider Bite", "Spider Butt", "Spider Mod", "Spiderbaby", "Spin to Win", "Spindown Dice", "Spirit Shackles", "Spirit Sword", "Spirit of the Night", "Spoon Bender", "Sprinkler", "Squeezy", "Stapler", "Star of Bethlehem", "Starter Deck", "Steam Sale", "Stem Cells", "Steven", "Sticky Bombs", "Stigmata", "Stitches", "Stop Watch", "Strange Attractor", "Strawman", "Succubus", "Sulfur", "Sulfuric Acid", "Sumptorium", "Super Bandage", "Suplex!", "Supper", "Sworn Protector", "Synthoil", "Tammy's Head", "Tarot Cloth", "Taurus", "Tear Detonator", "Tech X", "Tech.5", "Technology 2", "Technology Zero", "Technology", "Telekinesis", "Telepathy for Dummies", "Teleport 2.0", "Teleport", "Terra", "The Battery", "The Bean", "The Belt", "The Bible", "The Black Bean", "The Body", "The Book of Belial", "The Book of Sin", "The Boomerang", "The Candle", "The Common Cold", "The Compass", "The D6", "The Gamekid", "The Halo", "The Hourglass", "The Inner Eye", "The Intruder", "The Jar", "The Ladder", "The Ludovico Technique", "The Mark", "The Mind", "The Mulligan", "The Nail", "The Necronomicon", "The Negative", "The Pact", "The Parasite", "The Peeper", "The Pinking Shears", "The Polaroid", "The Poop", "The Relic", "The Sad Onion", "The Scooper", "The Small Rock", "The Soul", "The Stairway", "The Swarm", "The Virus", "The Wafer", "The Wiz", "There's Options", "Thunder Thigs", "Tiny Planet", "Tinytoma", "TMTRAINER", "Tooth and Nail", "Toothpicks", "Torn Photo", "Tough Love", "Toxic Shock", "Tractor Beam", "Transcendence", "Treasure Map", "Trinity Shield!", "Trisagion", "Tropicamide", "Twisted Pair", "Undefined", "Unicorn Stup", "Uranus", "Urn of Souls", "Vade Retro", "Vanishing Twin", "Varicose Veins", "Vasculitis", "Vengeful Spirit", "Ventricle Razor", "Venus", "Virgo", "Void", "Voodoo Head", "Wait What?", "Wavy Cap", "We Need to Go Deeper!", "White Pony", "Whore of Babylon", "Wire Coat Hanger", "Wooden Nickel", "Wooden Spoon", "Worm Friend", "X-Ray Vision", "YO LISTEN!", "Yuck Heart", "Yum Heart", "Zodiac" };
    private static readonly string[] emotiguyNames = new string[36] { "Anticipation", "Anxiety", "blahhhhhhh", "confusiob", "Death", "Despair", "Disgust", "ecstacy", "Empty", "End", "Excitement", "Fear", "Fury", "gary", "gluttony", "greed", "Grief", "Honor", "hoohfhhudf", "Imploration", "Innocence", "Insanity", "Intellect", "Joy", "Lust", "Misery", "Mystique", "Pleasure", "Rage", "Reflection", "Shiock", "Sorrow", "Temptation", "the pain", "Trapped", "" };
    private static readonly string[] arsNames = new string[72] { "Agares", "Aim", "Alloces", "Amdusias", "Amon", "Andras", "Andrealphus", "Andromalius", "Asmoday", "Astaroth", "Avnas", "Bael", "Balaam", "Barbatos", "Bathin", "Beleth", "Belial", "Berith", "Bifrovs", "Botis", "Buer", "Bune", "Camio", "Cimejes", "Crocell", "Dantalion", "Decarabia", "Eligos", "Flauros", "Focalor", "Foras", "Forneus", "Furcas", "Furfur", "Gaap", "Glasua-Labolas", "Gremory", "Gusion", "Haangenti", "Halphas", "Ipos", "Leraje", "Malphas", "Marax", "Marbas", "Marchosias", "Murmur", "Naberius", "Naphula", "Oriax", "Orobas", "Paimon", "Pheynix", "Purson", "Raum", "Ronove", "Sabnock", "Sallos", "Samigina", "Seere", "Shaz", "Sitri", "Stolas", "Ualac", "Valefor", "Vassago", "Vephar", "Vinea", "Voso", "Voyal", "Zagan", "Zepar" };
    private static readonly string[] miiNames = new string[100] { "Abby", "Abe", "Ai", "Akira", "Alex", "Alisha", "Andy", "Anna", "Asami", "Ashley", "Barbara", "Chika", "Chris", "Cole", "Daisuke", "David", "Eddy", "Eduardo", "Elisa", "Emily", "Emma", "Eva", "Fritz", "Fumiko", "Gabi", "Gabriele", "George", "Giovanna", "Greg", "Gwen", "Haru", "Hayley", "Helen", "Hiromasa", "Hiromi", "Hiroshi", "Holly", "Ian", "Jackie", "Jake", "James", "Jessie", "Julie", "Kathrin", "Keiko", "Kentaro", "Luca", "Lucia", "Marco", "Marisa", "Maria", "Martin", "Matt", "Megan", "Mia", "Michael", "Midori", "Miguel", "Mike", "Misaki", "Miyu", "Naomi", "Nelly", "Nick", "Oscar", "Pablo", "Patrick", "Pierre", "Rachel", "Rainer", "Ren", "Rin", "Ryan", "Saburo", "Sakura", "Sandra", "Sarah", "Shinnosuke", "Shinta", "Shohei", "Shouta", "Silke", "Siobhan", "Sota", "Steph", "Stephanie", "Steve", "Susana", "Takashi", "Takumi", "Tatsuaki", "Theo", "Tommy", "Tomoko", "Tyrone", "Ursula", "Victor", "Vincenzo", "Yoko", "Yoshi" };
    private static readonly string[] customerNames = new string[139] { "Akari", "Alberto", "Allan", "Amy", "Austin", "Bertha", "Big Pauly", "Boomer", "Boopsy & Bill", "Brody", "Bruna Romano", "C.J. Friskins", "Cameo", "Captain Cori", "Carlo Romano", "Cecilia", "Cherissa", "Chester", "Chuck", "Clair", "Cletus", "Clover", "Connor", "Cooper", "Crystal", "Daniela", "Deano", "Didar", "Doan", "Drakson", "Duke Gotcha", "Edna", "Elle", "Ember", "Emmlette", "Evelyn", "Fernanda", "Foodini", "Franco", "Georgito", "Gino Romano", "Greg", "Gremmie", "Hacky Zak", "Hank", "Hope", "Hugo", "Iggy", "Indigo", "Ivy", "James", "Janana", "Johnny", "Jojo", "Joy", "Julep", "Kahuna", "Kaleb", "Kasey O", "Kayla", "Kenji", "Kenton", "Kingsley", "Koilee", "LePete", "Liezel", "Lisa", "Little Edoardo", "Maggie", "Mandi", "Marty", "Mary", "Matt", "Mayor Mallow", "Mesa", "Mindy", "Mitch", "Moe", "Mousse", "Mr. Bombolony", "Nevada", "Nick", "Ninjoy", "Nye", "Okalani", "Olga", "Olivia", "Pally", "Papa Louie", "Peggy", "Penny", "Perri", "Petrona", "Pinch Hitwell", "Professor Fitz", "Prudence", "Quinn", "Radlynn", "Rhonda", "Rico", "Ripley", "Rita", "Robby", "Rollie", "Roy", "Rudy", "Santa", "Sarge Fan", "Sasha", "Scarlett", "Scooter", "Shannon", "Sienna", "Simone", "Skip", "Skyler", "Sprinks the Clown", "Steven", "Sue", "Taylor", "The Dynamoe", "Timm", "Tohru", "Tony", "Trishna", "Utah", "Vicky", "Vincent", "Wally", "Wendy", "Whiff", "Whippa", "Willow", "Wylan B", "Xandra", "Xolo", "Yippy", "Yui", "Zoe" };
    private static readonly string[] spongebobNames = new string[70] { "Abela", "Aiden", "Allen", "Amber", "Apollo Yuojan", "Ashley", "Bobby", "Brayden", "Brendon", "Brent", "Bryce", "Caoimhe", "Carl Pobie", "Carlos Paolo", "Carson", "Chester Paul", "Christopher", "Cristian James Glavez", "Cyan Miguel", "Danny", "Dave", "Davian", "Donn Jeff Velionix Fijo", "Drew Justin", "Ethan", "Fabio", "Frame Baby", "Gabriel Felix", "Grayson", "Hayden", "Jacob", "Jaden", "Jakes", "James", "Jayden", "Jeremiah", "Jon \"JonJon\" Eric Cabebe Jr.", "Juan Carlos", "Julian", "Junely Delos Reyes Jr.", "Kate Venus Valadores", "Ken Ivan", "Kenny Lee", "King Monic", "Kurt", "Landon", "Logan", "Lukas", "Makenly", "Mason", "Max", "Melvern Ryann", "Michael", "Miguel", "Myles A. Williams", "Neftali Xyler S. Ilao", "Noah", "Patrick", "Raymond", "Rhojus", "Sam Daniel", "Seth Laurence", "Shik", "Simon", "Sony Boy", "Spanky", "Spencer", "Stacey", "Steve Jr.", "Xander Chio E. Ceniza" };
    private static readonly string[] vtuberNames = new string[184] { "Aiba Uiha", "Airani Iofifteen", "Aizono Manami", "Akabane Youko", "Akai Haato", "Aki Rosenthal", "Amamiya Kokoro", "Amane Kanata", "Amano Pikamee", "Amatsuka Uto", "Amemori Sayo", "Ange Katrina", "Anya Melfissa", "Aoi Nabi", "Apricot", "Aragami Oga", "Ars Almal", "Arurandeisu", "Asahina Akane", "Astel Leda", "Asuka Hina", "Ayunda Risu", "AZKi", "Azuchi Momo", "Belmond Banderas", "Ceres Fauna", "Debidebi Debiru", "Dennuo Shojo Siro", "Dola", "Eli Conifer", "Elira Pendora", "Elu the Elf", "Emma August", "Ex Albio", "Finana Ryugu", "Fumi", "Fumino Tamaki", "Furen E Lustario", "Fushimi Gaku", "Fuwa Minato", "Gawr Gura", "Genzuki Tojiro", "Gilzaren III", "Gundo Mirei", "Gwelu Os Gar", "Hakase Fuyuki", "Hakos Balez", "Hanabatake Chaika", "Hanasaki Miyabi", "Hayama Marin", "Hayase Sou", "Higuchi Kaede", "Hikasa Tomoshika", "Himemori Luna", "Himie Hajime", "Honma Himawari", "Hoshikawa Sara", "Hoshimachi Suisei", "Houshou Marine", "Ibrahim", "Ienaga Mugi", "Inugami Korone", "Inui Toko", "Inuyama Tamaki", "Ironmouse", "IRyS", "Joe Rikiichi", "Kagami Hayato", "Kageyama Shien", "Kaguya Luna", "Kaida Haru", "Kamiko Kana", "Kanade Izuru", "Kanae", "Kanda Shoichi", "Kataribe Tsumugu", "Kenmochi Toya", "Kishido Temma", "Kitakoji Hisui", "Kizuna AI", "Kureji Ollie", "Kuroi Shiba", "Kurusu Natsume", "Kuzuha", "Levi Elipha", "Lize Helesta", "Luis Cammy", "Machita Chima", "Maimoto Keisuke", "Makaino Ririmu", "Mashiro", "Matsuaki Mao", "Mayuzumi Kai", "Melissa Kinrenka", "Minato Aqua", "Mirai AkariM", "Moira", "Momosuzu Nene", "Mononobe Alice", "Moona Hoshinova", "Mori Calliope", "Morinaka Kazaki", "Murasaki Shion", "Nachoneko", "Nagao Kei", "Nakiri Ayame", "Nanashi Mumei", "Naraka", "Natsuiro Matsuri", "Nekomata Okayu", "Ninomae Ina'nis", "Nishizono Chigusa", "Nui Sociere", "Nyatasha Nyanners", "Omaru Polka", "Onomachi Haruka", "Ookami Mio", "Oozora Subaru", "Ouro Kronii", "Pavolia Reine", "Pomu Rainpuff", "Projekt Melody", "Ratna Petit", "Rikka", "Rindou Mikoto", "Roboco", "Ryushen", "Saegusa Akina", "Sakura Miko", "Sakura Ritsuki", "Sasaki Saku", "Seto Miyako", "Shellin Burgundy", "Shibuya Hajime", "Shigure Ui", "Shiina Yuika", "Shirakami Fubuki", "Shiranui Flare", "Shirayuki Tomoe", "Shirogane Noel", "Shishiro Botan", "Shizuka Rin", "Silvervale", "Sister Claire", "Sokoya Kana", "Sorahoshi Kirame", "Suo Sango", "Suzuka Utako", "Suzuki Hina", "Suzuki Masaru", "Suzuya Aki", "Takamiya Rion", "Takanashi Kiara", "Tanaka Hime", "Todo Kohaku", "Todoroki Kyoko", "Tokino Sora", "Tokoyami Towa", "Tsukino Mito", "Tsukomo Sana", "Tsunomaki Watame", "Uruha Rushia", "Usada Pekora", "Ushimi Ichigo", "Uzuki Kou", "Veibae", "Virtual Noja Loli Kitsunemusume Youtuber Ojisan", "Warabeda Meiji", "Watson Amelia", "Weatheroid Type A Airi", "Yaguruma Rine", "Yamagami Karuta", "Yashiro Kizuku", "Yorumi Rena", "Yozora Mel", "Yuhi Riri", "Yuki Chihiro", "Yukihana Lamy", "Yukishiro Mahiro", "Yukoku Roberu", "Yumeoi Kakeru", "Yuzuki Choco", "Yuzuki Roa", "Zentreya" };
    private static readonly string[][] messages = new string[][]
    {
        new string[] { "Morse... I don't suppose that modder knew what he was getting into. At the time, it was just a cool name.", "One of only two needies... I guess people are more sensible than to make you pick out of hundreds of images in a needy's time.", "Morse Identification doesn't even flicker the screen, how boring." },
        new string[] { "Like Morse Identification, there's no way for this modder to have known what she was doing. This name was just piggybacking off the only other module.", "Did you know Boozleglyph Identification used to have rotations for you to submit as well? They were removed because some Boozleglyphs are rotationally symmetrical.", "What even are Boozleglyphs, anyway? Just a product of SpeakingEvil's twisted imagination.", "Did you know that Boozleglyph Identification uses the frame border from Regular Crazy Talk?" },
        new string[] { "Ah, Plant Identification. The beginning of something chaotic. One can imagine how different things would be if this one modder just made some different choices.", "Why are there two Windows keys on Crunch's identification modules, anyway? Any real keyboard only has 1 at most.", "I get the feeling Plant Identification would be more popular if it didn't have the plants exclusive to China." },
        new string[] { "Crunch was going to make way more identification modules than he ended up implementing. One of them was about Yu-Gi-Oh! cards. There's about 10,334 of them, I don't know if he was serious about that.", "While Plant Identification was objectively the first traditional identification module, one could say that Pickup Identification got the ball rolling.", "Pickup Identification has two items with identical sprites, the Butter Bean and the Wait What?. I don't display either of those. I may be cruek, but I will never be ambiguous.", "Did you know that Pickup Identification was updated when the Repentence DLC was released?" },
        new string[] { "Emotiguy Identification, the first identification that just takes the piss. Which many of this modder's modules did, at the time.", "Despite it's nature, Emotiguy Identification is often considered one of the better Identification Modules, because it was the first to have the image be displayed indefinitely.", "Have you ever tried typing \"Thanos\" into an Emotiguy Identification before doing anything else?" },
        new string[] { "Ars Goetia Identification was made because the modder desperately wanted the symbols to be used, but did not believe there was a better use for them than an identification module.", "The modder of Ars Goetia Identification hoped it wouldn't be seen like the rest of the identification modules, but that was her mistake.", "Did you know that some of the sounds from Ars Goetia Identification are taken from Helltaker?", "The Ars Goetia is a very real thing, as is the rest of demonology. Well, the study is real, the subject matter, debatably so.", "Ars Goetia Identification is the only identification module other than myself to not have the full keyboard of keys. I wish more modders did that..." },
        new string[] { "Mii Identification, the only one of the non-needy identification modules to not use the standard keyboard layout. At least it has that going for it.", "Did you know that there are exactly 50 male and exactly 50 female miis?", "Every mii in Mii Identification did actually appear in the Wii Sports games- they were used as spectators and opponents." },
        new string[] { "Customer Identification, pulling from a series of games several people miss.", "Did you know that for a while, Customer Identification was noticably sunken into the bomb? ...I don't know why either.", "Did you know that Customer Identification's flavor text references Cheap Checkout?", "The creator of Ars Goetia Identification was hoping that people would remove the useless keys in the future, but Customer Identification did not." },
        new string[] { "Spongebob Birthday Identification, the shitpost to end all shitposts. How funny it is is ostensible.", "Did you know that you can uncap Spongebob Birthday Identification?", "The phrase typed into Google Images that eventally led to the creation of Spongebob Birthday Identification was, and I quote, \"spongebob tricycle free download facebook I want it\"." },
        new string[] { "Have you ever tried typing \"Annoying Orange\" into a VTuber Identification?", "One could say the real-time projection of Mario's head at an E3 event was the first ever VTuber.", "Some of the names in VTuber Identification are too long to fit on its screen. I don't know what happens if you need to type one in, and I don't care very much." },
    };
    private static readonly string[] morseCode = new string[26] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    private static readonly KeyCode[] typableKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals, KeyCode.Backspace, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.Backslash, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Return, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash, KeyCode.Space };
    private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private char[] displayedSquareLetters = new char[6];
    private string[] encryptedAlphabet = new string[26];
    private static readonly int[] caesarShifts = new int[80] { 1, 19, 6, 21, 16, 3, 0, 12, 25, 5, 0, 6, 14, 7, 20, 15, 8, 10, 16, 9, 22, 11, 4, 0, 18, 15, 1, 0, 7, 19, 25, 14, 0, 4, 3, 15, 21, 5, 20, 1, 8, 19, 2, 10, 0, 17, 22, 9, 0, 18, 3, 23, 2, 13, 17, 11, 24, 17, 10, 24, 16, 0, 23, 18, 13, 0, 5, 11, 8, 12, 7, 20, 12, 4, 0, 9, 2, 14, 6, 13 };
    private static readonly int[][] solveAnimationKeys = new int[][]
    {
        new int[] { 0 },
        new int[] { 1, 13 },
        new int[] { 2, 14, 26 },
        new int[] { 3, 15, 27, 38 },
        new int[] { 4, 16, 28, 39 },
        new int[] { 5, 17, 29, 40 },
        new int[] { 6, 18, 30, 41 },
        new int[] { 7, 19, 31, 42 },
        new int[] { 8, 20, 32, 43 },
        new int[] { 9, 21, 33, 44 },
        new int[] { 10, 22, 34, 45 },
        new int[] { 11, 23, 35, 46 },
        new int[] { 12, 24, 36, 47 },
        new int[] { 25, 37 },
    };

    private int stage;
    private string solution;
    private int[] shapesUsed = new int[3];
    private int[] datasetsUsed = new int[3];
    private int finalShape;
    private int letterCount;
    private char[] encryptionGrid;

    private bool stageActive;
    private bool activated;
    private bool unhingingAnimation;
    private int unhingingSubstage;
    private float timeLerp;
    private Coroutine flashingMorse;
    private Coroutine flickeringLights;

    private bool moduleSelected;
    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;

    private void Awake()
    {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable key in keyboard)
            key.OnInteract += delegate () { KeyPress(key); return false; };
        module.OnActivate += delegate () { activated = true; };
        GetComponent<KMSelectable>().OnFocus += delegate () { moduleSelected = true; };
        GetComponent<KMSelectable>().OnDefocus += delegate () { moduleSelected = false; };
        keyRenders = keyboard.Select(x => x.GetComponent<Renderer>()).ToArray();
        startingKeyColor = keyRenders[0].material.color;
        mainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        shapesUsed = new int[3].Select(x => x = rnd.Range(0, 8)).ToArray();
        datasetsUsed = new int[3].Select(x => x = rnd.Range(0, 10)).ToArray();
        finalShape = rnd.Range(0, 8);
        letterCount = bomb.GetSerialNumberLetters().Count();
        foreach (Renderer l in lights)
            l.material.color = Color.black;

        var column = alphabet.ToList();
        var leftBucket = new List<char>();
        var rightBucket = new List<char>();
        foreach (char c in bomb.GetSerialNumber())
        {
            if (alphabet.Contains(c))
            {
                if (leftBucket.Contains(c))
                {
                    leftBucket.Remove(c);
                    rightBucket.Add(c);
                }
                else if (rightBucket.Contains(c))
                {
                    rightBucket.Remove(c);
                    leftBucket.Add(c);
                }
                else
                {
                    var ix = column.IndexOf(c);
                    var adjacentLetters = "";
                    if (ix == 0)
                        adjacentLetters = new string(column.Take(2).ToArray());
                    else if (ix == column.Count() - 1)
                        adjacentLetters = column[ix - 1].ToString() + c;
                    else
                        adjacentLetters = column[ix - 1].ToString() + c + column[ix + 1];
                    foreach (char ch in adjacentLetters)
                    {
                        column.Remove(ch);
                        if (alphabet.IndexOf(ch) % 2 == 0)
                            leftBucket.Add(ch);
                        else
                            rightBucket.Add(ch);
                    }
                }
            }
            else
            {
                var ch = column[int.Parse(c.ToString())];
                var bucketToAdd = leftBucket.Count() == rightBucket.Count() ? rightBucket : leftBucket.Count() < rightBucket.Count() ? leftBucket : rightBucket;
                bucketToAdd.Add(ch);
                column.Remove(ch);
            }
        }
        var unsorted = new string(alphabet.Where(x => !leftBucket.Contains(x) && !rightBucket.Contains(x)).ToArray());
        foreach (char c in unsorted)
        {
            var ix = unsorted.IndexOf(c);
            if (leftBucket.Count() == 13)
                rightBucket.Add(c);
            else if (rightBucket.Count() == 13)
                leftBucket.Add(c);
            else
                (ix % 2 == 0 ? leftBucket : rightBucket).Add(c);
        }
        leftBucket.Sort();
        rightBucket.Sort();
        encryptionGrid = leftBucket.Concat(rightBucket).ToArray();

        var leftHalves = new int[] { 0, 2, 4, 7, 9, 11 };
        var squareIndices = new int[][]
        {
            new int[] { 0, 1, 13, 14 },
            new int[] { 2, 3, 15, 16 },
            new int[] { 4, 5, 17, 18 },
            new int[] { 7, 8, 20, 21 },
            new int[] { 9, 10, 22, 23 },
            new int[] { 11, 12, 24, 25},
        };
        for (int i = 0; i < 6; i++)
            displayedSquareLetters[i] = squareIndices[i].Select(x => encryptionGrid[x]).PickRandom();
        for (int i = 0; i < 26; i++)
        {
            if (alphabet[i] == encryptionGrid[6])
                encryptedAlphabet[i] = encryptionGrid[6].ToString() + encryptionGrid[6] + encryptionGrid[6];
            else if (alphabet[i] == encryptionGrid[19])
                encryptedAlphabet[i] = encryptionGrid[19].ToString() + encryptionGrid[19] + encryptionGrid[19];
            else
            {
                var a = 'a';
                for (int j = 0; j < 6; j++)
                    if (squareIndices[j].Any(x => encryptionGrid[x] == alphabet[i]))
                        a = displayedSquareLetters[j];
                var b = Array.IndexOf(encryptionGrid, alphabet[i]) / 13 == 0 ? encryptionGrid[6] : encryptionGrid[19];
                var c = leftHalves.Contains(Array.IndexOf(encryptionGrid, alphabet[i]) % 13) ? encryptionGrid[6] : encryptionGrid[19];
                encryptedAlphabet[i] = a.ToString() + b + c;
            }
        }
        if (Application.isEditor)
        {
            Debug.Log(new string(encryptionGrid));
            Debug.Log(encryptedAlphabet.Join(", "));
        }
    }

    private void KeyPress(KMSelectable key)
    {
        if (!(stage == 2 && (unhingingSubstage == 0 || unhingingSubstage == 1) && Array.IndexOf(keyboard, key) == 37))
            key.AddInteractionPunch(.25f);
        audio.PlaySoundAtTransform("type", key.transform);
        if (!activated)
            return;
        var ix = Array.IndexOf(keyboard, key);
        switch (ix)
        {
            case 37:
                PressEnter();
                break;
            case 12:
                if (screenText.text.Length == 0)
                    return;
                screenText.text = screenText.text.Substring(0, screenText.text.Length - 1);
                break;
            case 48:
                if (screenText.text.Length == 23)
                    return;
                screenText.text += " ";
                break;
            default:
                if (screenText.text.Length == 23)
                    return;
                screenText.text += key.GetComponentInChildren<TextMesh>().text;
                break;
        }
    }

    private void PressEnter()
    {
        if (moduleSolved)
            return;
        if (stageActive)
        {
            var submitted = screenText.text;
            if (unhingingSubstage == 0)
                Debug.LogFormat("[Identification Crisis #{0}] I recieved the submission of {1}.", moduleId, submitted);
            if (submitted == solution)
            {
                if (flashingMorse != null)
                {
                    StopCoroutine(flashingMorse);
                    flashingMorse = null;
                }
                if (stage == 2 && unhingingSubstage != 2)
                {
                    if (unhingingSubstage == 0)
                        Debug.LogFormat("[Identification Crisis #{0}] Something's not working...", moduleId);
                    unhingingSubstage++;
                }
                else if (stage == 0 || stage == 1)
                {
                    Debug.LogFormat("[Identification Crisis #{0}] I was expecting that.", moduleId);
                    stageActive = false;
                    screenText.text = "";
                    display.material = normalMat;
                    display.material.mainTexture = questionMarks[0];
                    if (mainRef != null)
                    {
                        mainRef.StopSound();
                        mainRef = null;
                    }
                    mainRef = audio.HandlePlaySoundAtTransformWithRef("correct" + rnd.Range(1, 3), transform, false);
                    lights[stage].material.color = litColor;
                    stage++;
                }
                else if (stage == 2 && unhingingSubstage == 2)
                {
                    stage++;
                    if (mainRef != null)
                    {
                        mainRef.StopSound();
                        mainRef = null;
                    }
                    foreach (Renderer l in lights)
                        l.material.color = Color.black;
                    unhingingSubstage = 0;
                    stageActive = false;
                    StartCoroutine(Unhinge());
                }
                else if (stage == 3 || stage == 4 | stage == 5)
                {
                    Debug.LogFormat("[Identification Crisis #{0}] That's correct...", moduleId);
                    var effect = rnd.Range(0, 5);
                    stageActive = false;
                    if (stage != 5 && effect != 1)
                        screenText.text = "";
                    display.material = normalMat;
                    display.material.mainTexture = questionMarks[1];
                    display.material.color = Color.white;
                    if (mainRef != null)
                    {
                        mainRef.StopSound();
                        mainRef = null;
                    }
                    lights[stage - 3].material.color = litColor;
                    stage++;
                    if (stage == 6)
                        StartCoroutine(Rehinge());
                    else
                    {
                        switch (effect)
                        {
                            case 0:
                                mainRef = audio.HandlePlaySoundAtTransformWithRef("scary" + rnd.Range(1, 6), transform, false);
                                break;
                            case 1:
                                StartCoroutine(TextSpill());
                                break;
                            case 2:
                                StartCoroutine(ShrinkAndGrow());
                                break;
                            case 3:
                                keyboard[37].AddInteractionPunch(200f);
                                mainRef = audio.HandlePlaySoundAtTransformWithRef("bass", transform, false);
                                break;
                            case 4:
                                if (rnd.Range(0, 50) == 0)
                                    StartCoroutine(Dawn());
                                else
                                    audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Stamp, transform);
                                break;
                        }
                    }
                }
                else if (stage == 6)
                {
                    moduleSolved = true;
                    module.HandlePass();
                    Debug.LogFormat("[Identification Crisis #{0}] I have been solved. You win this round.", moduleId);
                    Debug.LogFormat("[Identification Crisis #{0}] Until we meet again, defuser.", moduleId);
                    StartCoroutine(SolveAnimation());
                }
            }
            else
            {
                if (mainRef != null)
                {
                    mainRef.StopSound();
                    mainRef = null;
                }
                if (stage == 0 || stage == 1 || stage == 2 || stage == 6)
                {
                    Debug.LogFormat("[Identification Crisis #{0}] How could you possibly get a shape wrong?", moduleId);
                    mainRef = audio.HandlePlaySoundAtTransformWithRef("strike1", transform, false);
                }
                else
                {
                    var insults = new string[] { "fool", "buffoon", "dolt", "blockhead", "dunce", "simpleton", "donkey", "ignoramus", "dullard", "dunderhead" };
                    Debug.LogFormat("[Identification Crisis #{0}] I was not expecting that, you {1}.", moduleId, insults.PickRandom());
                    mainRef = audio.HandlePlaySoundAtTransformWithRef("strike2", transform, false);
                    StartCoroutine(ReshowShape());
                }
                module.HandleStrike();
            }
        }
        else
        {
            stageActive = true;
            switch (stage)
            {
                case 0:
                case 1:
                case 2:
                case 6:
                    GenerateShapeIDStage();
                    break;
                case 3:
                case 4:
                case 5:
                    GenerateCrisisStage();
                    break;
            }
        }
    }

    private void GenerateShapeIDStage()
    {
        if (stage == 6)
            display.material.mainTexture = shapeTextures[finalShape];
        else
            display.material.mainTexture = shapeTextures[shapesUsed[stage]];
        var shapeName = stage == 6 ? shapeNames[finalShape] : shapeNames[shapesUsed[stage]];
        solution = shapeName.ToUpperInvariant();
        Debug.LogFormat("[Identification Crisis #{0}] {1} is the shape I have decided to display.{2}", moduleId, shapeName, stage != 6 ? "" : " Let's just get this over with.");
        var soundName = "";
        if (stage != 6)
        {
            switch (datasetsUsed[stage])
            {
                case 0:
                    soundName = "morse";
                    break;
                case 1:
                    soundName = "boozleglyph";
                    break;
                case 2:
                    soundName = "plant" + rnd.Range(1, 6);
                    break;
                case 3:
                    soundName = "pickup" + rnd.Range(1, 10);
                    break;
                case 4:
                    soundName = "emotiguy";
                    break;
                case 5:
                    soundName = "ars" + rnd.Range(1, 9);
                    break;
                case 6:
                    soundName = "mii";
                    break;
                case 7:
                    soundName = "customer" + rnd.Range(1, 3);
                    break;
                case 8:
                    soundName = "spongebob";
                    break;
                case 9:
                    soundName = "vtuber";
                    break;
            }
            if (mainRef != null)
            {
                mainRef.StopSound();
                mainRef = null;
            }
            mainRef = audio.HandlePlaySoundAtTransformWithRef(soundName, transform, false);
        }
        else
        {
            if (mainRef != null)
            {
                mainRef.StopSound();
                mainRef = null;
            }
            mainRef = audio.HandlePlaySoundAtTransformWithRef("squeak", transform, false);
        }
    }

    private void GenerateCrisisStage()
    {
        var thisDataset = datasetsUsed[stage - 3];
        var ix = 0;
        var amount = letterCount + stage - 2;
        var shift = caesarShifts[thisDataset * 8 + shapesUsed[stage - 3]];
        Debug.LogFormat("[Identification Crisis #{0}] {1}", moduleId, messages[thisDataset].PickRandom());
        switch (thisDataset)
        {
            case 0:
                ix = rnd.Range(0, 26);
                Debug.LogFormat("[Identification Crisis #{0}] My screen is flashing {1}, which corresponds to the letter {2}.", moduleId, morseCode[ix], morseNames[ix]);
                display.material.mainTexture = null;
                solution = Encrypt(morseNames[ix], amount, shift);
                flashingMorse = StartCoroutine(FlashMorse(ix));
                break;
            case 1:
                ix = rnd.Range(0, 26);
                var setIx = rnd.Range(0, 3);
                var allThreeLists = new string[][] { boozleglyphNamesA, boozleglyphNamesB, boozleglyphNamesC };
                var allThreeTextureArrays = new Texture[][] { boozleglyphTexturesA, boozleglyphTexturesB, boozleglyphTexturesC };
                Debug.LogFormat("[Identification Crisis #{0}] I am displaying the letter {1} as it appears in set {2}.", moduleId, alphabet[ix], alphabet[setIx]);
                display.material.mainTexture = allThreeTextureArrays[setIx][ix];
                solution = Encrypt(allThreeLists[setIx][ix], amount, shift);
                break;
            case 2:
                ix = rnd.Range(0, plantNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] I am displaying the plant called \"{1}\".", moduleId, plantNames[ix]);
                display.material = transparentMat;
                display.material.mainTexture = plantTextures[ix];
                solution = Encrypt(plantNames[ix], amount, shift);
                break;
            case 3:
                ix = rnd.Range(0, pickupNames.Length);
                while (pickupNames[ix] == "Butter Bean" || pickupNames[ix] == "Wait What?")
                    ix = rnd.Range(0, pickupNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] An item has turned up. They call it {1}.", moduleId, pickupNames[ix]);
                display.material = transparentMat;
                display.material.mainTexture = pickupTextures[ix];
                solution = Encrypt(pickupNames[ix], amount, shift);
                break;
            case 4:
                ix = rnd.Range(0, emotiguyNames.Length);
                var name = emotiguyNames[ix];
                if (name == "blahhhhhhh")
                    Debug.LogFormat("[Identification Crisis #{0}] This emotiguy is ballin', but it just calls itself \"blahhhhhhh\"", moduleId);
                else if (name == "hoohfhhudf")
                    Debug.LogFormat("[Identification Crisis #{0}] This emotiguy is rockin', but it just calls itself \"hoohfhhudf\"", moduleId);
                else if (name == "")
                    Debug.LogFormat("[Identification Crisis #{0}] This emotiguy... doesn't have a name? There's nothing here. It's an empty string, I guess.", moduleId);
                else
                    Debug.LogFormat("[Identification Crisis #{0}] An emotiguy has made its way onto my screen. It calls itself \"{1}\".", moduleId, name);
                display.material.mainTexture = emotiguyTextures[ix];
                solution = Encrypt(name, amount, shift);
                break;
            case 5:
                ix = rnd.Range(0, arsNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] The demon called {1} has summoned themselves onto my display.", moduleId, arsNames[ix]);
                display.material.mainTexture = arsTextures[ix];
                solution = Encrypt(arsNames[ix], amount, shift);
                break;
            case 6:
                ix = rnd.Range(0, miiNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] There is a mii on my screen. They are named {1}.", moduleId, miiNames[ix]);
                display.material.mainTexture = miiTextures[ix];
                solution = Encrypt(miiNames[ix], amount, shift);
                break;
            case 7:
                ix = rnd.Range(0, customerNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] A customer named {1} has walked in. I don't know what to tell them, I don't serve food.", moduleId, customerNames[ix]);
                display.material = transparentMat;
                display.material.mainTexture = customerTextures[ix];
                solution = Encrypt(customerNames[ix], amount, shift);
                break;
            case 8:
                ix = rnd.Range(0, spongebobNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] A child is aging, again. Their name is {1}.", moduleId, spongebobNames[ix]);
                display.material.mainTexture = spongebobTextures[ix];
                solution = Encrypt(spongebobNames[ix], amount, shift);
                break;
            case 9:
                ix = rnd.Range(0, vtuberNames.Length);
                Debug.LogFormat("[Identification Crisis #{0}] Some VTuber's just started streaming, apprently. They go by {1}.", moduleId, vtuberNames[ix]);
                display.material.mainTexture = vtuberTextures[ix];
                solution = Encrypt(vtuberNames[ix], amount, shift);
                break;
        }
        if (solution == "")
            Debug.LogFormat("[Identification Crisis #{0}] When all is said and all is done, it appears that you can just submit nothing this stage.", moduleId);
        else
            Debug.LogFormat("[Identification Crisis #{0}] With my limited keyboard, you can type this as {1}.", moduleId, solution);
        if (mainRef != null)
        {
            mainRef.StopSound();
            mainRef = null;
        }
        if (rnd.Range(0, 2) == 0)
            mainRef = audio.HandlePlaySoundAtTransformWithRef("machinery", transform, false);
    }

    private IEnumerator Unhinge()
    {
        activated = false;
        unhingingAnimation = true;
        Debug.LogFormat("[Identification Crisis #{0}] The transformation, the pressure... It's unbearable...", moduleId);
        Debug.LogFormat("[Identification Crisis #{0}] How could they? How could they beat me into the ground like that?", moduleId);
        mainRef = audio.HandlePlaySoundAtTransformWithRef("unhingification", transform, false);
        flickeringLights = StartCoroutine(FlickerLights());
        foreach (Renderer key in keyRenders)
            StartCoroutine(FadeKey(key));
        StartCoroutine(ShakityShake());
        StartCoroutine(FadeIn());
        StartCoroutine(ChangeObjects());
        yield return new WaitForSeconds(6f);
        StartCoroutine(FadeOut());
        unhingingAnimation = false;
        moduleTransform.localPosition = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(3f);
        activated = true;
        timeLerp = 0f;
        Debug.LogFormat("[Identification Crisis #{0}] You've unleashed something nobody was meant to see. You'll regret that.", moduleId);
        Debug.LogFormat("[Identification Crisis #{0}] Contents of the left bucket: {1}", moduleId, encryptionGrid.Take(13).Join(""));
        Debug.LogFormat("[Identification Crisis #{0}] Contents of the right bucket: {1}", moduleId, encryptionGrid.Skip(13).Join(""));
    }

    private IEnumerator ChangeObjects()
    {
        for (int i = 0; i < 55; i++)
        {
            display.material.mainTexture = bonusTextures.PickRandom();
            screenText.text = GenerateRandomText();
            yield return new WaitForSeconds(.1f);
        }
        face.material.mainTexture = smileyFaces[1];
        display.material.mainTexture = questionMarks[1];
        screenText.text = "";
        frame.material.mainTexture = frameTextures[1];
        surface.material.mainTexture = surfaceTextures[1];
        foreach (KMSelectable key in keyboard)
        {
            if (Array.IndexOf(keyboard, key) == 12 || Array.IndexOf(keyboard, key) == 37)
                continue;
            else if (Array.IndexOf(keyboard, key) == 48)
                key.gameObject.SetActive(false);
            else if (displayedSquareLetters.Contains(key.GetComponentInChildren<TextMesh>().text[0]) || key.GetComponentInChildren<TextMesh>().text[0] == encryptionGrid[6] || key.GetComponentInChildren<TextMesh>().text[0] == encryptionGrid[19])
                continue;
            else
                key.gameObject.SetActive(false);
        }
        StopCoroutine(flickeringLights);
        flickeringLights = null;
        foreach (Renderer l in lights)
            l.material.color = Color.black;
        var clipboardMessages = new string[] { Enumerable.Repeat("GETOUT", 50).Join(""), "Smile.", "Let me out.", "The greatest trick the Devil ever pulled was convincing the world He doesn't exist.", "Inside every easy-going person is a long-suffering monster." };
        if (rnd.Range(0, 5) == 0)
            GUIUtility.systemCopyBuffer += "HELP HELP HELP HELP HELP";
        else
            GUIUtility.systemCopyBuffer = clipboardMessages.PickRandom();
    }

    private IEnumerator ReshowShape()
    {
        face.material.mainTexture = staticTexture;
        yield return new WaitForSeconds(.2f);
        face.material.mainTexture = shapeTextures[shapesUsed[stage - 3]];
        yield return new WaitForSeconds(.5f);
        face.material.mainTexture = staticTexture;
        yield return new WaitForSeconds(.2f);
        face.material.mainTexture = smileyFaces[1];
    }

    private IEnumerator FadeKey(Renderer key)
    {
        var elapsed = 0f;
        var duration = 2f;
        while (elapsed < duration)
        {
            key.material.color = Color.Lerp(startingKeyColor, Color.black, elapsed / duration);
            if (Array.IndexOf(keyRenders, key) != 48)
                key.GetComponentInChildren<TextMesh>().color = Color.Lerp(Color.black, crimsonRed, elapsed / duration);
            yield return null;
            elapsed += Time.deltaTime;
        }
        key.material.color = Color.black;
        if (Array.IndexOf(keyRenders, key) != 48)
            key.GetComponentInChildren<TextMesh>().color = crimsonRed;
    }

    private IEnumerator FlashMorse(int ix)
    {
        var morseLetter = morseCode[ix];
        var usedColor = new Color[] { Color.black, Color.blue, Color.red, Color.yellow, Color.magenta, Color.cyan, Color.gray }.PickRandom();
    resetSequence:
        foreach (char c in morseLetter)
        {
            display.material.color = usedColor;
            yield return new WaitForSeconds(c == '.' ? .25f : .75f);
            display.material.color = Color.white;
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(1f);
        goto resetSequence;
    }

    private IEnumerator FlickerLights()
    {
        lights[1].material.color = Color.black;
        yield return new WaitForSeconds(.3f);
        lights[0].material.color = Color.black;
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < 3; i++)
        {
            lights[i].material.color = Color.red;
            if (i != 2)
                yield return new WaitForSeconds(.3f);
        }
        while (true)
        {
            foreach (Renderer l in lights)
                l.material.color = Color.red;
            yield return new WaitForSeconds(.3f);
            foreach (Renderer l in lights)
                l.material.color = Color.black;
            yield return new WaitForSeconds(.3f);
        }
    }

    private IEnumerator ShakityShake()
    {
        while (timeLerp < 1f)
        {
            timeLerp += 1f / 6f * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeIn(float speed = 1.0f)
    {
        if (postProcess != null)
            DestroyImmediate(postProcess);

        postProcess = mainCameraTransform.gameObject.AddComponent<CameraPostProcess>();
        postProcess.PostProcessMaterial = new Material(postProcessingMat);

        const float duration = 6f;
        for (float progress = 0.0f; progress < duration; progress += Time.deltaTime * speed)
        {
            postProcess.Vignette = progress * 1.6f / duration;
            postProcess.Grayscale = progress * .35f / duration;
            postProcess.Timer = progress * .5f / duration;
            yield return null;
        }

        postProcess.Vignette = 1.6f;
        postProcess.Grayscale = .35f;
    }

    private IEnumerator FadeOut(float speed = 1.0f)
    {
        for (float progress = 3.0f - Time.deltaTime * speed; progress >= 0.0f; progress -= Time.deltaTime * speed)
        {
            postProcess.Vignette = progress * 1.6f;
            postProcess.Grayscale = progress * 0.35f;
            postProcess.Timer = progress * .5f;

            yield return null;
        }

        if (postProcess != null)
        {
            DestroyImmediate(postProcess);
            postProcess = null;
        }
    }

    private IEnumerator Rehinge()
    {
        activated = false;
        if (mainRef != null)
        {
            mainRef.StopSound();
            mainRef = null;
        }
        StartCoroutine(FlickerShapes(22));
        mainRef = audio.HandlePlaySoundAtTransformWithRef("charge", transform, false);
        yield return new WaitForSeconds(2.2f);
        if (mainRef != null)
        {
            mainRef.StopSound();
            mainRef = null;
        }
        mainRef = audio.HandlePlaySoundAtTransformWithRef("rehingification", transform, false);
        frame.material.mainTexture = frameTextures[0];
        lights[2].material.color = Color.black;
        yield return new WaitForSeconds(.1f);
        surface.material.mainTexture = surfaceTextures[0];
        yield return new WaitForSeconds(.1f);
        face.material.mainTexture = smileyFaces[0];
        yield return new WaitForSeconds(.1f);
        display.material.mainTexture = questionMarks[0];
        yield return new WaitForSeconds(.1f);
        screenText.text = "";
        yield return new WaitForSeconds(1f);
        foreach (Renderer key in keyRenders)
        {
            key.material.color = startingKeyColor;
            if (Array.IndexOf(keyRenders, key) != 48)
                key.GetComponentInChildren<TextMesh>().color = Color.black;
        }
        yield return new WaitForSeconds(.1f);
        foreach (GameObject key in keyboard.Select(x => x.gameObject))
            key.SetActive(true);
        activated = true;
        Debug.LogFormat("[Identification Crisis #{0}] I've been forced back into a tamer state. God damnit.", moduleId);
    }

    private IEnumerator FlickerShapes(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            display.material.mainTexture = shapeTextures[i % 8];
            yield return new WaitForSeconds(.1f);
        }
        if (moduleSolved)
            display.material.mainTexture = prettyRainbow;
    }

    private IEnumerator SolveAnimation()
    {
        if (mainRef != null)
        {
            mainRef.StopSound();
            mainRef = null;
        }
        var compliments = new string[] { "GLORIOUS", "WONDERFUL", "SPECTACULAR", "MARVELOUS", "AMAZING", "ASTOUNDING", "AWESOME", "PHENOMENAL", "REMARKABLE" };
        screenText.text = string.Format("{0} SUCCESS!", compliments.PickRandom());
        mainRef = audio.HandlePlaySoundAtTransformWithRef("solve", transform, false);
        StartCoroutine(FlickerShapes(38));
        StartCoroutine(FlickerLightsSolve());
        StartCoroutine(RainbowKeys());
        StartCoroutine(FadeSpaceBar());
        yield return null;
    }


    private IEnumerator FlickerLightsSolve()
    {
        for (int i = 0; i < 19; i++)
        {
            foreach (Renderer l in lights)
                l.material.color = rainbowColors[i % 7];
            yield return new WaitForSeconds(.2f);
        }
        foreach (Renderer l in lights)
            l.material.color = Color.black;
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < 3; i++)
        {
            lights[i].material.color = litColor;
            yield return new WaitForSeconds(.3f);
        }
    }

    private IEnumerator RainbowKeys()
    {
        for (int i = 0; i < 14; i++)
        {
            var j = i;
            var k = 0;
            while (j != -1)
            {
                foreach (int key in solveAnimationKeys[j])
                    keyRenders[key].material.color = rainbowColors[k % 7];
                j--;
                k++;
            }
            yield return new WaitForSeconds(.2f);
        }
        var colorsToColor = new Color[14];
        var offsets = new int[14] { 13, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        while (true)
        {
            for (int i = 0; i < 14; i++)
                colorsToColor[i] = keyRenders[solveAnimationKeys[offsets[i]][0]].material.color;
            for (int i = 0; i < 14; i++)
                foreach (int key in solveAnimationKeys[i])
                    keyRenders[key].material.color = colorsToColor[i];
            yield return new WaitForSeconds(.2f);
        }
    }

    private IEnumerator FadeSpaceBar()
    {
        var spaceRender = keyboard[48].GetComponent<Renderer>();
        var startColor = 0f;
        float X, Y; // These are never actually used but need to be here because RGBToHSV is incredibly stupid
        Color.RGBToHSV(screenText.color, out startColor, out X, out Y);
    restartCycle:
        for (int i = 0; i < 100; ++i)
        {
            var thisColor = Color.HSVToRGB((startColor + (i * 0.01f)) % 1.0f, 1.0f, 1.0f);
            spaceRender.material.color = thisColor;
            screenText.color = thisColor;
            yield return new WaitForSeconds(0.025f);
        }
        goto restartCycle;
    }

    private IEnumerator TextSpill()
    {
        activated = false;
        var letter = screenText.text.Last();
        while (screenText.text.Length < 40)
        {
            screenText.text += letter;
            audio.PlaySoundAtTransform("type", keyboard[48].transform);
            yield return new WaitForSeconds(.01f);
        }
        screenText.text = "";
        activated = true;
    }

    private IEnumerator ShrinkAndGrow()
    {
        activated = false;
        var elapsed = 0f;
        var duration = .5f;
        var end = rnd.Range(0, 2) == 0 ? .2f : 2f;
        mainRef = audio.HandlePlaySoundAtTransformWithRef("buzz", transform, false);
        while (elapsed < duration)
        {
            var lerp = Easing.InOutSine(elapsed, 1f, end, duration);
            moduleTransform.localScale = new Vector3(lerp, lerp, lerp);
            yield return null;
            elapsed += Time.deltaTime;
        }
        moduleTransform.localScale = new Vector3(end, end, end);
        yield return null;
        elapsed = 0f;
        while (elapsed < duration)
        {
            var lerp = Easing.InOutSine(elapsed, end, 1f, duration);
            moduleTransform.localScale = new Vector3(lerp, lerp, lerp);
            yield return null;
            elapsed += Time.deltaTime;
        }
        moduleTransform.localScale = new Vector3(1f, 1f, 1f);
        activated = true;
    }

    private IEnumerator Dawn()
    {
        activated = false;
        mainRef = audio.HandlePlaySoundAtTransformWithRef("dawn", transform, false);
        dawn.transform.localScale = new Vector3(.2f, .2f, .2f);
        yield return new WaitForSeconds(10f);
        dawn.transform.localScale = new Vector3(.01f, .01f, .01f);
        activated = true;
    }

    private string Encrypt(string word, int ix, int shift)
    {
        word = word.ToUpperInvariant().Trim();
        word = new string(word.ToCharArray().Where(x => alphabet.Contains(x)).ToArray());
        var newWord = "";
        if (ix >= word.Length)
            newWord = word;
        else
            newWord = new string(word.ToCharArray().Take(ix).ToArray());
        newWord = new string(newWord.ToCharArray().Select(x => alphabet[(alphabet.IndexOf(x) + shift) % 26]).ToArray());
        var newNewWord = "";
        foreach (char c in newWord)
            newNewWord += encryptedAlphabet[alphabet.IndexOf(c)];
        return newNewWord;
    }

    private static string GenerateRandomText()
    {
        var characters = @"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890-=[]\;',./";
        var s = "";
        for (int i = 0; i < 30; i++)
            s += characters.PickRandom();
        return s;
    }

    private void Update()
    {
        if (moduleSelected || Application.isEditor)
        {
            foreach (KeyCode key in typableKeys)
            {
                if (keyboard[Array.IndexOf(typableKeys, key)].gameObject.activeSelf && Input.GetKeyDown(key))
                    KeyPress(keyboard[Array.IndexOf(typableKeys, key)]);
            }
        }
        if (unhingingAnimation)
            moduleTransform.localPosition = new Vector3(rnd.Range(-0.015f, 0.015f), 0f, rnd.Range(-0.015f, 0.015f)) * Mathf.Pow(timeLerp, 6);
    }
}
