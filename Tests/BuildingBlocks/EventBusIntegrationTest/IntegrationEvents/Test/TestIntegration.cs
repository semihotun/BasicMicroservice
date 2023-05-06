using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBustUnitTest.IntegrationEvents.Test
{
    public class TestIntegration: IntegrationEvent
    {
        public TestIntegration(bool ınTest)
        {
            InTest = ınTest;
        }

        public bool InTest { get; set; }
    }
}
