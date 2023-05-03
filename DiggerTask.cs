using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        { return ""; }
        public int GetDrawingPriority()
        { return 0; }
        public CreatureCommand Act(int x, int y)
        { return null; }
        public bool DeadInConflict(ICreature conflictedObject)
        { return false; }
    }
   
    public class Player:  ICreature
    {
        public string GetImageFileName()
        { return ""; }
        public int GetDrawingPriority()
        { return 0; }
        public CreatureCommand Act(int x, int y)
        { return null; }
        public bool DeadInConflict(ICreature conflictedObject)
        { return false; }
    }

}
