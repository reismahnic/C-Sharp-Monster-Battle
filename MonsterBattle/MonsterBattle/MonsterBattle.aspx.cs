using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonsterBattle
{
    public partial class MonsterBattle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Create hero object
            Character hero = new Character();
            hero.Name = "Augustus";
            hero.Health = 100;
            hero.DamageMaximum = 10;
            hero.AttackBonus = false;

            //Create monster object
            Character monster = new Character();
            monster.Name = "Blarg";
            monster.Health = 9001;
            monster.DamageMaximum = 5;
            monster.AttackBonus = true;

            Dice dice = new Dice();

            //Apply attack bonus
            if(hero.AttackBonus)
            {
                monster.Defend(hero.Attack(dice));
            }
            if(monster.AttackBonus)
            {
                hero.Defend(monster.Attack(dice));
            }

            //Run attack until hero or monster dies
            while(hero.Health > 0 && monster.Health > 0)
            {
                monster.Defend(hero.Attack(dice));
                hero.Defend(monster.Attack(dice));
                print(hero);
                print(monster);
            }
            printResults(hero, monster);
        }

        private void printResults(Character hero, Character monster)
        {
            if(hero.Health <= 0 && monster.Health <= 0)
            {
                resultslbl.Text += string.Format("<p>Both the hero and the monster died");
            }
            else if(hero.Health <= 0)
            {
                resultslbl.Text += string.Format("<p>The hero has perished!");
            }
            else if(monster.Health <=0)
            {
                resultslbl.Text += string.Format("<p>That monster is super dead!");
            }
        }

        private void print(Character character)
        {
            resultslbl.Text += String.Format("<p>Name: {0} - Health: {1} - Maximum Damage: {2} - Attack Bonus: {3}</p>", character.Name, character.Health, character.DamageMaximum, character.AttackBonus.ToString());
        }
    }

    class Character
    {
        //Character attributes:
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        public int Attack(Dice dice)
        {
            dice.Sides = this.DamageMaximum;           
            return dice.Roll();
        }
        public void Defend(int damage)
        {
            Health -= damage;
        }
    }

    class Dice
    {
        public int Sides { get; set; }
        //Create the random die roll
        Random randomRoll = new Random();
        public int Roll()
        {
            return randomRoll.Next(this.Sides);
        }
    }
}