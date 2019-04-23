namespace Verizon.Connect.Sender.Loader
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            var tasks = new List<Task>();
            for (var vId = 1; vId < 100; vId++)
            {
                var startArgs = new string[] { "-v", vId.ToString(), "-interval", "100" };
                tasks.Add(Sender.Program.Main(startArgs));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}