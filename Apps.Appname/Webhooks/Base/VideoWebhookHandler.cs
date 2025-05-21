using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.Synthesia.Webhooks.Base
{
    public abstract class VideoWebhookHandler(InvocationContext invocationContext) : BaseInvocable(invocationContext), IWebhookEventHandler
    {
        public Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
        {
            throw new NotImplementedException();
        }
    }
}
