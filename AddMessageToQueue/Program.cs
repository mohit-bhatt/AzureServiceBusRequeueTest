using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace AddMessageToQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QueueItem item = new QueueItem { IsActive = true, ItemTime = DateTime.Now, Name = "TestItem" };
            BrokeredMessage msg = new BrokeredMessage(item);
            msg.ContentType = item.GetType().FullName;
            try
            {
                var qClient = QueueClient.CreateFromConnectionString("<connectionstringhere>", "punchqueue");

                qClient.Send(msg);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            Console.ReadLine();
        }
    }

    public class QueueItem
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ItemTime { get; set; }

    }
}
