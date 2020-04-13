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

            if (extlooptime <= 0)
            {
                return;
            }

            Console.WriteLine("Number of session(s)");
            int looptime = Convert.ToInt32(Console.ReadLine());

            if (looptime <= 0)
            {
                return;
            }

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

            if (drawList.Count() == 0)
            {
                Console.WriteLine("MaxConsecutiveDraw: 0 Draw per 1 session");
            }
            else
            {
                Console.WriteLine("MaxConsecutiveDraw: " + drawList.Max() + " Draw(s) per 1 session");
            }

            if (winList.Count() == 0)
            {
                Console.WriteLine("MaxConsecutiveWin: 0 Win per 1 session");
            }
            else
            {
                Console.WriteLine("MaxConsecutiveWin: " + winList.Max() + " Win(s) in a successive row of sessions");
            }

            if (extloopcount < extlooptime)
            {
                extloopcount++;

                if (drawList.Count() > 0)
                {
                    avgDrawList.Add(drawList.Max());
                }

                if (winList.Count() > 0)
                {
                    avgWinList.Add(winList.Max());
                }

                win = 0;
                win2 = 0;
                lose = 0;
                draw = 0;
                drawList.Clear();
                drawList.TrimExcess();
                winList.Clear();
                winList.TrimExcess();
                Console.Clear();
                goto externalloop;
            }

            Console.WriteLine("");

            if (extloopcount > 1)
            {
                double disp1 = 0;
                double disp2 = 0;
                double disp3 = 0;
                double disp4 = 0;

                if(avgDrawList.Count() > 0)
                {
                    disp1 = avgDrawList.Average();
                    disp3 = avgDrawList.Max();
                }

                if (avgWinList.Count() > 0)
                {
                    disp2 = avgWinList.Average();
                    disp4 = avgWinList.Max();
                }

                Console.WriteLine("avgGlobalConsecutiveDraw: " + disp1 + " Average global draw(s) per 1 session");
                Console.WriteLine("avgGlobalConsecutiveWin: " + disp2 + " Average global win(s) in a successive row of sessions");
                Console.WriteLine("MaxGlobalConsecutiveDraw: " + disp3 + " Max global draw(s) per 1 session");
                Console.WriteLine("MaxGlobalConsecutiveWin: " + disp4 + " Max global win(s) in a successive row of sessions");
                Console.WriteLine("");
            }

            Console.WriteLine("Number of external session(s): " + extlooptime + " External session(s)");
            Console.WriteLine("Number of session(s): " + looptime + " Session(s)");

            Console.WriteLine("");

            Console.WriteLine("Press any key to terminate the program...");

            Console.ReadKey();
        }
    }
}
