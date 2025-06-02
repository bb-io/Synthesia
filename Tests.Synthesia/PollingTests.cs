using Apps.Synthesia.Models;
using Apps.Synthesia.Webhooks;
using Apps.Synthesia.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Polling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Synthesia.Base;

namespace Tests.Synthesia
{
    [TestClass]
    public  class PollingTests:TestBase
    {
        [TestMethod]
        public async Task PollingVideoResponse_IssSuccess()
        {
            var actions = new PollingList(InvocationContext);
            var oldDate = new DateTime(2025, 5, 30, 0, 0, 0, DateTimeKind.Utc);
            var request = new PollingEventRequest<DateMemory>
            {
                Memory = new DateMemory
                {
                    LastInteractionDate = oldDate
                }
            };

            var response = await actions.OnVideoCompleted(request, new VideoOptionFilter { });

            var video = response.Result?.Videos;

            foreach (var canvas in video)
            {
                Console.WriteLine($"{canvas.Id} - {canvas.Title}");
            }
            Assert.IsNotNull(response);
        }

    }
}
