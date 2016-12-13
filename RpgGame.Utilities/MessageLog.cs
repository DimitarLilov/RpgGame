using System.Collections.Generic;
using RLNET;

namespace RpgGame.Utilities.Utilities
{
    public static class MessageLog
    {
        private const int DefaultMaxLines = 9;

        private static readonly Queue<string> lines = new Queue<string>();

        public static void Add(string message)
        {
            lines.Enqueue(message);

            if (lines.Count > DefaultMaxLines)
            {
                lines.Dequeue();
            }
        }

        public static void Draw(RLConsole console)
        {
            console.Clear();

            string[] linesArray = lines.ToArray();
            for (int i = 0; i < linesArray.Length; i++)
            {
                console.Print(1, i + 1, linesArray[i], RLColor.White);
            }
        }
    }
}
