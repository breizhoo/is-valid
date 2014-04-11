using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Infrastructure
{
    public class MessagingReceiver
    {
        public Action<MMessage> ReceiveMessage { get; set; }
    }
}
