using System;
using System.Linq;
using System.Collections.Generic;

namespace RockPaperScissors
{
    class Program
    {

        private static int win;
        private static int win2;
        private static int lose;
        private static int draw;

        private static readonly List<int> winList = new List<int>();

        private static readonly List<int> drawList = new List<int>();

        private static readonly List<int> avgWinList = new List<int>();

        private static readonly List<int> avgDrawList = new List<int>();

        static void Main()
        {
            Console.WriteLine("Number of external session(s) : Useful for finding the most likely event : Write 1 if not sure");
            int extlooptime = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Number of session(s)");
            int looptime = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            int extloopcount = 1;

            externalloop:

            var rng = new Random();

            for (int current = 0; current < looptime; current++)
            {
            internalloop:

                int rps1 = rng.Next(3);
                int rps2 = rng.Next(3);

                if (rps1 == rps2)
                {
                    draw++;
                    goto internalloop;
                }
                else
                {
                    drawList.Add(draw);
                    draw = 0;
                    if ((rps1 == 0 && rps2 == 2) || (rps1 == 1 && rps2 == 0) || (rps1 == 2 && rps2 == 1))
                    {
                        win++;
                        win2++;
                    }
                    else
                    {
                        winList.Add(win2);
                        win2 = 0;
                        lose++;
                    }
                }

                Console.WriteLine("W:" + win);
                Console.WriteLine("L:" + lose);
                Console.WriteLine("D:" + draw);

                if (current == looptime - 2)
                {
                    Console.Clear();
                }
            }
            Console.WriteLine("MaxConsecutiveDraw: " + drawList.Max() + " Draw(s) per 1 session");
            Console.WriteLine("MaxConsecutiveWin: " + winList.Max() + " Win(s) in a successive row of sessions");

            if (extloopcount < extlooptime)
            {
                extloopcount++;
                avgDrawList.Add(drawList.Max());
                avgWinList.Add(winList.Max());
                win = 0;
                win2 = 0;
                lose = 0;
                draw = 0;
                drawList.Clear();
                drawList.TrimExcess();
                winList.Clear();
                winList.TrimExcess();
                goto externalloop;
            }

            if (extloopcount > 1)
            {
                Console.WriteLine("avgConsecutiveDraw: " + avgDrawList.Average() + " Average draw(s) per 1 session");
                Console.WriteLine("avgConsecutiveWin: " + avgWinList.Average() + " Average win(s) in a successive row of sessions");
            }

            Console.ReadKey();
        }
    }
}
