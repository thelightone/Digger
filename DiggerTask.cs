using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Digger;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.

    
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        { return "Terrain.png"; }
        public int GetDrawingPriority()
        { return 3; }
        public CreatureCommand Act(int x, int y)
        {
            var com = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            return com;
        }
        public bool DeadInConflict(ICreature conflictedObject)
        { return true; }
    }

    public class Monster : ICreature
    {   
        public string GetImageFileName()
        { return "Monster.png"; }
        public int GetDrawingPriority()
        { return 1; }
        public CreatureCommand Act(int x, int y)
        {
            int plX = -1;
            int plY = -1;

            var aims = new Type[3] {  typeof(Player), typeof(Gold),null };

            for (var i = 0; i < Game.MapWidth; i++)
                for (var j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] != null && Game.Map[i, j].GetType() == typeof(Player))
                    {
                        plX= i;
                        plY= j;
                    }
                }

            if (plX == -1)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

            foreach (var aim in aims)
            {
                if (plX > x)
                {
                    if (x < Game.MapWidth - 1 && Game.Map[x + 1, y]?.GetType() == aim) return new CreatureCommand() { DeltaX = 1, DeltaY = 0 };
                    else
                    {
                        if (plY > y)
                        {

                            if (y < Game.MapHeight - 1 && Game.Map[x, y + 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                        }
                        else if (plY < y)
                        {
                            if (y > 0 && Game.Map[x, y - 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
                        }
                    }
                }
                else if (plX < x)
                {
                    if (x > 0 && Game.Map[x - 1, y]?.GetType() == aim) return new CreatureCommand() { DeltaX = -1, DeltaY = 0 };
                    else
                    {
                        if (plY > y)
                        {

                            if (y < Game.MapHeight - 1 && Game.Map[x, y + 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                        }
                        else if (plY < y)
                        {
                            if (y>0&&Game.Map[x, y - 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
                        }
                    }
                }
                else
                {
                    if (plY > y)
                    {

                        if (y < Game.MapHeight - 1 && Game.Map[x, y + 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                    }
                    else if (plY < y)
                    {
                        if (y > 0 && Game.Map[x, y - 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
                    }
                }
            }

            foreach (var aim in aims)
            {
                if (x < Game.MapWidth - 1 && Game.Map[x + 1, y]?.GetType() == aim) return new CreatureCommand() { DeltaX = 1, DeltaY = 0 };
                else if (y < Game.MapHeight - 1 && Game.Map[x, y + 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                else if (x > 0 && Game.Map[x - 1, y]?.GetType() == aim) return new CreatureCommand() { DeltaX = -1, DeltaY = 0 };
                else if (y > 0 && Game.Map[x, y - 1]?.GetType() == aim) return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
            }


                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetType() == typeof(Sack)||
                (conflictedObject.GetType() == typeof(Monster))) return true;
            return false;
        }
        
    }
    
    public class Player:  ICreature
    {
       
        public string GetImageFileName()
        { return "Digger.png"; }
        public int GetDrawingPriority()
        { return 1; }
        public CreatureCommand Act(int x, int y)
        {
            
            switch (Game.KeyPressed)
            {
                case Keys.Left:
                   if (x>0 && Game.Map[x-1, y]?.GetType() != typeof(Sack)) return new CreatureCommand() { DeltaX = -1, DeltaY = 0 };
                    break;
                case Keys.Right:
                    if (x < Game.MapWidth-1 && Game.Map[x+1, y]?.GetType() != typeof(Sack)) return new CreatureCommand() { DeltaX = 1, DeltaY = 0 };
                    break;
                case Keys.Up:
                    if (y > 0 && Game.Map[x, y - 1]?.GetType() != typeof(Sack)) return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
                    break;
                case Keys.Down:
                    if (y< Game.MapHeight-1 && Game.Map[x, y+1]?.GetType() != typeof(Sack)) return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                    break;
             }
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 } ; 
            
        }
        public bool DeadInConflict(ICreature conflictedObject)
        { 
            if (conflictedObject.GetType() == typeof(Sack)||
                conflictedObject.GetType() == typeof(Monster)) return true;
            return false; 
        }

        



    }

    public class Sack : ICreature
    {
        int count = 0;
        public string GetImageFileName()
        { return "Sack.png"; }
        public int GetDrawingPriority()
        { return 2; }
        public CreatureCommand Act(int x, int y)
        {

            if (y < Game.MapHeight - 1 && (Game.Map[x, y + 1] == null || Game.Map[x, y + 1].GetType() == typeof(Player)|| Game.Map[x, y + 1]?.GetType() == typeof(Monster)))
            {
                count++;
                if (count == 1 && (Game.Map[x, y + 1]?.GetType() == typeof(Player) || Game.Map[x, y + 1]?.GetType() == typeof(Monster)))
                {
                    count = 0;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
                }

                
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1 }; 
            }
            
            if (count > 1)
            {
                count = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            count = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

        }
        public bool DeadInConflict(ICreature conflictedObject)
        { return false; }
    }
    public class Gold : ICreature
    {
        public string GetImageFileName()
        { return "Gold.png"; }
        public int GetDrawingPriority()
        { return 2; }
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetType() == typeof(Player))
            {
                Game.Scores += 10;
                return true;
            }
            else if (conflictedObject.GetType() == typeof(Monster))
                return true;
            return false;
        }
    }
}
