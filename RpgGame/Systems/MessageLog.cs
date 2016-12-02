namespace RpgGame.Systems
{
    using System.Collections.Generic;
    using RLNET;

    public class MessageLog
    {
        private static int maxLines = 9;

        private readonly Queue<string> lines;

        public MessageLog()
        {
            this.lines = new Queue<string>();
        }

        public void Add(string message)
        {
            this.lines.Enqueue(message);

            if (this.lines.Count > maxLines)
            {
                this.lines.Dequeue();
            }
        }

        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] lines = this.lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 2, lines[i], RLColor.White);
            }
        }
    }
}
