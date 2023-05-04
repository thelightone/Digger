using System.Windows.Forms;

namespace Digger
{
    public static class Game
    {
        private const string mapWithPlayerTerrain = @"
TTT T
TTP T
T T T
TT TT";

        private const string mapWithPlayerTerrainSackGold = @"
PTTGTT TS
TST  TSTT
TTTTTTSTT
T TSTS TT
T TTTG ST
TSTSTT TT";

        private const string mapWithPlayerTerrainSackGoldMonster = @"
TTTGTT TST TTGTT TST
TST  TTTTMTST  TTTTM
TTT TTTTTTTTT TTTTTT
T TSTS TTTT TSTS TTT
T TTTGMSTST TTTGMSTS
T TMT M TST TMT M TS
TSTSTTMTTTTSTSTTMTTT
S TTST  TGS TTST  TG
 TGST MTTT TGST MTTT
 T  TMTTTTTT  TMTTTT
TTTGTT TSTPTTGTT TST
TST  TTTTTTST  TTTTM
TTT TTTTTTTTT TTTTTT
T TSTS TTTT TSTS TTT
T TTTGMSTST TTTGMSTS
T TMT M TST TMT M TS
TSTSTTMTTTTSTSTTMTTT
S TTST  TGS TTST  TG
 TGST MTTT TGST MTTT
 T  TMTTTT T  TMTTTT";


        private const string map = @"
TTTTTTTTT
TTTTTTTTT
TTTTTTTTT
TTTTMTTTT
TTTTTTTTT
TTTTTTTTT
TTTTTTTTT";

        public static ICreature[,] Map;
        public static int Scores;
        public static bool IsOver;

        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap()
        {
            Map = CreatureMapCreator.CreateMap(mapWithPlayerTerrainSackGoldMonster);
        }
    }
}