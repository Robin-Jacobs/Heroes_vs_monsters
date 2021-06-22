using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;

namespace HeroesVSMonsters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans le donjon de la forêt de Shorewood, forêt enchantée du pays de Stormwall.");
            Console.WriteLine("Votre rôle, valeureux héro est trouver la princesse, enlevée un l'Ogre, le maître du donjon.");
            Console.WriteLine("\n\nAvant de commencer votre aventure, créons votre personnage.");
            Hero Joueur = new Hero();
            Console.WriteLine("Vous voilà prêt pour l'aventure!");
            
            DeplacementDansTableau(Joueur);

            Console.WriteLine("Félicitations! Vous avez vaincu les monstres de la forêt de Shorewood!");
        }
        private static string[,] mapping()
        {
            //format des string: X10100 : X = element present à l'endroit indiqué (espace si rien)
            //                           1010 = possibilités de mouvement (haut,bas,gauche,droite), 1 = ok, 0 pas de mouvement possible dans cette direction
            //                           0 : indique que l'objet n'a pas encore ete trouve
            string[,] map = { { " 01010", " 01110", " 00110", " 01110", " 01110", " 00110", " 01100", " 01000", "P01110", " 01000", " 01010", " 00110", " 01110", " 01110", " 01100" },
                              { " 11010", " 11100", "L11110", " 11010", " 11100", "O11110", " 11000", " 11010", " 01110", " 11100", " 11000", "B11110", " 11010", " 11110", " 11100" },
                              { " 10010", " 10110", " 00110", " 10110", " 10110", " 01110", " 11100", " 10010", " 10110", " 10110", " 10110", " 00110", " 11110", " 10110", " 11100" },
                              { " 01010", " 01110", " 01110", " 01110", " 01100", " 11010", " 11110", " 01110", " 01110", " 00110", " 01110", " 01100", " 11000", "D11110", " 11100" },
                              { " 11010", "D10110", " 11010", " 11110", " 11100", " 11010", " 11110", " 11110", " 11100", "L11110", " 11010", " 11100", " 11010", " 01110", " 11100" },
                              { " 11000", "C01110", " 11000", " 11010", " 11100", " 11010", " 11110", " 10110", " 10110", " 00110", " 11110", " 11100", " 10010", " 11110", " 11100" },
                              { " 10010", " 00110", " 10100", " 11010", " 11110", " 11110", " 11110", " 01110", " 00110", " 01110", " 11110", " 11100", "D11110", " 11010", " 11100" },
                              { " 00010", " 00110", " 01110", " 10110", " 10110", " 11110", " 11110", " 11100", "L11110", " 11010", " 10110", " 10110", " 00110", " 10110", " 10100" },
                              { " 01010", " 01110", " 10110", " 01110", " 01100", " 11010", " 11110", " 11110", " 01110", " 11110", " 01110", " 01110", " 01110", " 00110", " 01100" },
                              { " 11010", " 11100", "L11110", " 11010", " 11100", " 11010", " 11110", " 11110", " 11110", " 11110", " 11110", " 11110", " 11100", "D11110", " 11000" },
                              { " 10010", " 10110", " 00110", " 10110", " 10100", " 10010", " 11110", " 11110", " 10110", " 11110", "T11110", " 11110", " 10110", " 00110", " 10100" },
                              { " 01010", " 01110", " 01110", " 01110", " 01110", " 00110", " 11110", " 11100", "O11110", " 11000", " 11010", " 11100", " 01010", " 01110", " 00100" },
                              { " 11010", " 10110", " 11110", " 11110", " 11100", "O11110", " 11000", " 11010", " 01110", " 11100", " 11010", " 11100", " 10010", " 11100", "C11100" },
                              { " 11000", "L11110", " 11010", " 11110", " 11110", " 01110", " 11100", " 11010", " 10110", " 11100", " 11010", " 11100", "L11110", " 11010", " 01100" },
                              { " 10010", " 00110", " 10110", " 10110", " 10110", " 10110", " 10100", " 10000", "S10110", " 10000", " 10010", " 10110", " 00110", " 10110", " 10100" }};
            return (map);

        }
        private static bool ProximiteMonstre(int[] Position, ref string[,] mapping, ref int[] PositionMonstre, ref char Item)
        {
            bool MonstreTrouve = false;
            if (mapping[(Position[0]+1 > mapping.GetLength(0)-1) ? Position[0] : Position[0] + 1, Position[1]][0] != ' ')
            {
                Item = mapping[(Position[0] + 1 > mapping.GetLength(0)) ? mapping.GetLength(0) : Position[0] + 1, Position[1]][0];
                MonstreTrouve = true;
                PositionMonstre[0] = (Position[0] + 1 > mapping.GetLength(0)) ? mapping.GetLength(0) : Position[0] + 1;
                PositionMonstre[1] = Position[1];
            }
            else if (mapping[(Position[0] - 1 < 0) ? 0 : Position[0] - 1, Position[1]][0] != ' ')
            {
                Item = mapping[(Position[0] - 1 < 0) ? 0 : Position[0] - 1, Position[1]][0];
                MonstreTrouve = true;
                PositionMonstre[0] = (Position[0] - 1 < 0) ? 0 : Position[0] - 1;
                PositionMonstre[1] = Position[1];
            }
            else if (mapping[Position[0], (Position[1] + 1 > mapping.GetLength(1)-1) ? Position[1] : Position[1] + 1][0] != ' ')
            {
                Item = mapping[Position[0], (Position[1] + 1 > mapping.GetLength(1)) ? mapping.GetLength(1) : Position[1] + 1][0];
                MonstreTrouve = true;
                PositionMonstre[0] = Position[0];
                PositionMonstre[1] = (Position[1] + 1 > mapping.GetLength(1)) ? mapping.GetLength(1) : Position[1] + 1;
            }
            else if (mapping[Position[0], (Position[1] - 1 < 0) ? 0 : Position[1] - 1][0] != ' ')
            {
                Item = mapping[Position[0], (Position[1] - 1 < 0) ? 0 : Position[1] - 1][0];
                MonstreTrouve = true;
                PositionMonstre[0] = Position[0];
                PositionMonstre[1] = (Position[1] - 1 < 0) ? 0 : Position[1] - 1;
            }
            else
            {
                PositionMonstre[0] = Position[0];
                PositionMonstre[1] = Position[1];
            }
            //if (Item == 'S')
            //    return (MonstreTrouve);
            ////mapping[PositionMonstre[0], PositionMonstre[1]] = mapping[PositionMonstre[0], PositionMonstre[1]].Insert(5,"1");
            ////mapping[PositionMonstre[0], PositionMonstre[1]] = mapping[PositionMonstre[0], PositionMonstre[1]].Remove(6, 1);
            return (MonstreTrouve);
        }
        private static void DeplacementDansTableau(Hero Joueur)
        {
            bool Mouvement = true;
            //char Choice = 'a';
            int TabSize = 15;//modifier egalement le mapping en cas de modification de cette variable
            char[,] tab = new char[TabSize, TabSize];
            string[,] map = mapping();
            int[] Position = new int[2];
            int[] PositionMonstre = new int[2];
            //Random random = new Random();
            //string[] ListeMonstres = { "loup", "orque", "dragonnet" };
            //char[] CurseurMonstre = { 'L', 'O', 'D' };
            char Item = ' ';
            SoundPlayer playerCoffre = new SoundPlayer();
            playerCoffre.SoundLocation = @"D:\Documents\exo\ExoAlgo\HeroesVsMonstersV3\HeroesVsMonstersV3\HeroesVsMonstersV3\Zelda.wav";
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"D:\Documents\exo\ExoAlgo\HeroesVsMonstersV3\HeroesVsMonstersV3\HeroesVsMonstersV3\Pok.wav";

            //ecriture du tableau
            for (int i = 0; i < TabSize; i++)
                for (int j = 0; j < TabSize; j++)
                    tab[i, j] = ' ';

            //definition de la position de depart
            Position[0] = 7; //i
            Position[1] = 0; //j
            tab[Position[0], Position[1]] = 'H';

            do
            {
                if (Mouvement == true)
                {
                    Item = ' ';
                    //Console.WriteLine(ProximiteMonstre(Position, ref map, ref PositionMonstre, ref Item));
                    if (ProximiteMonstre(Position, ref map, ref PositionMonstre, ref Item) && map[PositionMonstre[0],PositionMonstre[1]][5]=='0')
                    {
                        if(Item != 'S') //on indique que le monstre a deja ete visite
                        {
                            map[PositionMonstre[0], PositionMonstre[1]] = map[PositionMonstre[0], PositionMonstre[1]].Insert(5, "1");
                            map[PositionMonstre[0], PositionMonstre[1]] = map[PositionMonstre[0], PositionMonstre[1]].Remove(6, 1);
                        }
                        
                        tab[PositionMonstre[0], PositionMonstre[1]] = Item; //on indique l'item sur la carte du jeux
                        switch (Item)
                        {
                            case 'L':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                combat(Joueur, "loup");
                                Console.WriteLine("Vous prenez un repos mérité à l'auberge. (Vous êtes encouragés à imaginer la musique de soin de Pokemon dans votre tête)");
                                player.Load();
                                player.Play();
                                Joueur.RegenPV();
                                break;
                            case 'D':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                combat(Joueur, "dragonnet");
                                player.Load();
                                player.Play();
                                Console.WriteLine("Vous prenez un repos mérité à l'auberge. (Vous êtes encouragés à imaginer la musique de soin de Pokemon dans votre tête)");
                                Joueur.RegenPV();
                                break;
                            case 'O':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                combat(Joueur, "orque");
                                player.Load();
                                player.Play();
                                Console.WriteLine("Vous prenez un repos mérité à l'auberge. (Vous êtes encouragés à imaginer la musique de soin de Pokemon dans votre tête)");
                                Joueur.RegenPV();
                                break;
                            case 'C':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                Console.WriteLine("Vous trouvez un coffre contenant 5 golds et 3 cuirs!");
                                Joueur.loot(5, 3);
                                playerCoffre.Load();
                                playerCoffre.Play();
                                break;
                            case 'S':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                Console.WriteLine("Vous venez de trouvez un magasin!");
                                Shopping(Joueur);
                                break;
                            case 'P':
                                Win();
                                break;
                            case 'B':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                combat(Joueur, "ogre");
                                player.Load();
                                player.Play();
                                Console.WriteLine("Vous prenez un repos mérité à l'auberge. (Vous êtes encouragés à imaginer la musique de soin de Pokemon dans votre tête)");
                                Joueur.RegenPV();
                                break;
                            case 'T':
                                Console.Clear();
                                //Lecture du tableau
                                AfficheTab(tab,map);
                                Console.WriteLine("Vous marchez dans un piège et subissez 2 points de dégâts!");
                                Joueur.Blessure(2);
                                break;
                        }
                    }else
                    {
                        Console.Clear();
                        //Lecture du tableau
                        AfficheTab(tab,map);
                    }
                }

                Console.WriteLine("Voulez-vous monter(z), descendre(s),aller a gauche(q) ou aller a droite(d)?");
                //Choice = Console.ReadLine()[0];
                //Test = Console.ReadKey().Key;
                Mouvement = false;
                //switch (Choice)
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.Z:
                        if(map[Position[0],Position[1]][1]=='0')
                            Console.WriteLine("Tu ne peux pas aller plus haut");
                        else
                        {
                            tab[Position[0], Position[1]] = ' ';
                            Position[0] -= 1;
                            tab[Position[0], Position[1]] = 'H';
                            Mouvement = true;
                        }
                        break;
                    case ConsoleKey.S:
                        if (map[Position[0], Position[1]][2] == '0')
                            Console.WriteLine("Tu ne peux pas aller plus bas");
                        else
                        {
                            tab[Position[0], Position[1]] = ' ';
                            Position[0] += 1;
                            tab[Position[0], Position[1]] = 'H';
                            Mouvement = true;
                        }
                        break;
                    case ConsoleKey.Q:
                        if (map[Position[0], Position[1]][3] == '0')
                            Console.WriteLine("Tu ne peux pas aller plus a gauche");
                        else
                        {
                            tab[Position[0], Position[1]] = ' ';
                            Position[1] -= 1;
                            tab[Position[0], Position[1]] = 'H';
                            Mouvement = true;
                        }
                        break;
                    case ConsoleKey.D:
                        if (map[Position[0], Position[1]][4] == '0')
                            Console.WriteLine("Tu ne peux pas aller plus a droite");
                        else
                        {
                            tab[Position[0], Position[1]] = ' ';
                            Position[1] += 1;
                            tab[Position[0], Position[1]] = 'H';
                            Mouvement = true;
                        }
                        break;
                    //default:
                    //    Console.WriteLine("mauvaise commande");
                    //    break;
                }
            } while (true);
        }
        private static void Win()
        {
            Console.WriteLine("Félicitation! vous avez sauvez la princesse!");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("************************ You Won! ***********************");
            Console.WriteLine("*********************************************************");
            Environment.Exit(0);
        }
        private static void Shopping(Hero Joueur)
        {
            int choix;
            Console.WriteLine("Bienvenue aventurier! Désirez-vous acheter quelque chose? (vous possédez actuellement "+Joueur.or+" or et "+Joueur.cuir+" cuirs)");
            Console.WriteLine("1 acheter une armure de cuir (+1 end) pour 10 cuirs");
            Console.WriteLine("2 acheter une armure complète (+2 end) pour 25 cuirs");
            Console.WriteLine("3 acheter une épée longue (+1 for) pour 15 or");
            Console.WriteLine("4 acheter une épée batarde (+2 for) pour 30 or");
            Console.WriteLine("5 Quitter le magasin");
            choix = int.Parse(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    if (Joueur.cuir > 9)
                    {
                        Joueur.Achat(0, 10);
                        Joueur.Equipement("armure", 1);
                        Console.WriteLine("Merci pour votre achat!");
                    }
                    else
                        Console.WriteLine("Vous n'avez pas assez de cuir!");
                    break;
                case 2:
                    if (Joueur.cuir > 24)
                    {
                        Joueur.Achat(0, 25);
                        Joueur.Equipement("armure", 2);
                        Console.WriteLine("Merci pour votre achat!");
                    }
                    else
                        Console.WriteLine("Vous n'avez pas assez de cuir!");
                    break;
                case 3:
                    if (Joueur.or > 14)
                    {
                        Joueur.Achat(15, 0);
                        Joueur.Equipement("arme", 1);
                        Console.WriteLine("Merci pour votre achat!");
                    }
                    else
                        Console.WriteLine("Vous n'avez pas assez d'or!");
                    break;
                case 4:
                    if (Joueur.or > 29)
                    {
                        Joueur.Achat(30, 0);
                        Joueur.Equipement("arme", 2);
                        Console.WriteLine("Merci pour votre achat!");
                    }
                    else
                        Console.WriteLine("Vous n'avez pas assez d'or!");
                    break;
                default:
                    break;
            }
        }
        private static void AfficheSeparateur(int n,int i, string[,] map)
        {
            for (int j = 0; j < n; j++)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                if (i==0 || i == map.GetLength(0))
                    Console.ForegroundColor = ConsoleColor.White;
                else if (map[i,j][1]=='0' && map[i-1,j][0]==' ')
                    Console.ForegroundColor = ConsoleColor.White;
                //Console.Write("+---");
                Console.Write($"+\u2500\u2500\u2500");
            }
                
            //Console.WriteLine($"\u2511  \u2500   \u251C   "); // parametre drawing box UTF8
            Console.WriteLine("+");
            Console.ResetColor();
        }
        private static void AfficheTab(char[,] tab, string[,] map)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                AfficheSeparateur(tab.GetLength(1),i, map);
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    if(j==0)
                        Console.ForegroundColor = ConsoleColor.White;
                    else if (map[i,j][3]=='0' && map[i, j-1][0] == ' ')
                        Console.ForegroundColor = ConsoleColor.White;
                    //Console.Write("| ");
                    Console.Write($"\u2502 ");
                    if (tab[i, j] == ' ')
                        Console.ResetColor();
                    else if (tab[i,j] == 'H')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{tab[i, j]} ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");
                Console.ResetColor();
            }
            AfficheSeparateur(tab.GetLength(1),tab.GetLength(0),map);
        }
        private static void combat(Hero Joueur, string Monstre)
        {

            Console.WriteLine("Un " + Monstre + " vous attaque!");
            Monster Ennemi = new Monster(Monstre);
            int DegatsInfliges, PvRestant = 1;
            bool TourJoueur = false;

            while (PvRestant > 0)
            {
                TourJoueur = !TourJoueur;
                if (TourJoueur)
                    Console.WriteLine("\n" + Joueur.nom + " attaque " + Ennemi.nom + " ennemi!");
                else
                    Console.WriteLine("\n" + Ennemi.nom + " riposte avec colère!");
                DegatsInfliges = (TourJoueur) ? Joueur.Frappe() : Ennemi.Frappe();
                PvRestant = (TourJoueur) ? Ennemi.Blessure(DegatsInfliges) : Joueur.Blessure(DegatsInfliges);
                Console.WriteLine("Il inflige " + DegatsInfliges + " dégâts!");
                if (DegatsInfliges <= 2)
                    Console.WriteLine("Ce n'est pas très efficace...");
                else if (DegatsInfliges >= 5)
                    Console.WriteLine("C'est très efficace!");
            }
            if (!TourJoueur)
                GameOver();
            Joueur.loot(Ennemi.or, Ennemi.cuir);
            Console.WriteLine("\n*************** Victoire!!! ***************\n");
            Console.WriteLine("Vous recevez " + Ennemi.or + " pièces d'or et " + Ennemi.cuir + " morceaux de cuirs");
        }
        private static void GameOver()
        {
            Console.WriteLine("You died.....");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("************************ GAME OVER **********************");
            Console.WriteLine("*********************************************************");
            Environment.Exit(0);

        }
    }

    class Dice
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public Dice(int InMin, int InMax)
        {
            Min = InMin;
            Max = InMax;
        }
        public int Lance()
        {
            Random random = new System.Random();
            return (random.Next(Min, Max));
        }
    }
    class Personnage
    {
        public string nom { get; protected set; }
        public int Endurance { get; private set; }
        protected int BonusRacialEndurance = 0;
        public int Force { get; private set; }
        protected int BonusRacialForce = 0;
        private int PV { get; set; }

        //fonctions
        public Personnage()
        {
            Endurance = GenerationCaract();
            Force = GenerationCaract();
            RegenPV();
        }
        public int GenerationCaract()
        {
            int caract = 0;
            Dice d6 = new Dice(1, 6);
            List<int> liste = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                liste.Add(d6.Lance());
            }
            liste.Sort();
            liste.RemoveAt(0);
            for (int i = 0; i < liste.Count; i++)
                caract += liste[i];
            return (caract);
        }
        public int CalculModificateur(int Caracteristique, int Bonus)
        {
            int modificateur;
            if (Caracteristique + Bonus < 5)
                modificateur = -1;
            else if (Caracteristique + Bonus < 10)
                modificateur = 0;
            else if (Caracteristique + Bonus < 15)
                modificateur = 1;
            else
                modificateur = 2;
            return (modificateur);
        }
        public void RegenPV()
        {
            PV = Endurance + BonusRacialEndurance + CalculModificateur(Endurance, BonusRacialEndurance);
            //return (PV);
        }
        public int Frappe()
        {
            Dice d4 = new Dice(1, 4);
            int Degats = d4.Lance() + CalculModificateur(Force, BonusRacialForce);
            return (Degats);
        }
        public int Blessure(int degats)
        {
            PV -= degats;
            return (PV);
        }
    }
    class Hero : Personnage
    {
        public int or { get; private set; }
        public int cuir { get; private set; }
        public string race { get; private set; }

        //fonctions
        public Hero()
        {
            or = 0;
            cuir = 0;
            int choix = 0;
            bool verif = false;
            do
            {
                Console.WriteLine("Desirez-vous jouer un humain(1) ou un nain(2)?");
                verif = int.TryParse(Console.ReadLine(), out choix);
            } while ((choix != 1 && choix != 2) || verif == false);
            if (choix == 1)
            {
                race = "humain";
                BonusRacialEndurance = 1;
                BonusRacialForce = 1;
            }
            else
            {
                race = "nain";
                BonusRacialEndurance = 2;
            }
            RegenPV();
            Console.WriteLine("Comment vous nommez-vous?");
            nom = Console.ReadLine();
        }
        public void loot(int orLoot, int cuirLoot)
        {
            or += orLoot;
            cuir += cuirLoot;
        }
        public void Achat(int orDepense, int cuirDepense)
        {
            or -= orDepense;
            cuir -= cuirDepense;
        }
        public void Equipement(string type, int Bonus)
        {
            if (type == "arme")
                BonusRacialForce += Bonus;
            else if (type == "armure")
                BonusRacialEndurance += Bonus;
        }
    }
    class Monster : Personnage
    {
        public int or { get; private set; }
        public int cuir { get; private set; }

        //fonctions
        public Monster(string Race)
        {
            Dice d4 = new Dice(1, 4);
            Dice d6 = new Dice(1, 6);
            switch (Race)
            {
                case "loup":
                    cuir = d4.Lance();
                    or = 0;
                    break;
                case "orque":
                    or = d6.Lance();
                    cuir = 0;
                    BonusRacialForce = 1;
                    break;
                case "dragonnet":
                    or = d6.Lance();
                    cuir = d4.Lance();
                    BonusRacialEndurance = 1;
                    break;
                case "ogre":
                    or = 2 * d6.Lance();
                    BonusRacialEndurance = 2;
                    BonusRacialForce = 3;
                    break;
            }
            RegenPV();
            nom = Race;
        }

    }

}
