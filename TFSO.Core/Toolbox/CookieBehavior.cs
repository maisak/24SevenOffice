using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Description;
using System.Text;

namespace TFSO.Core.Toolbox
{
    public class CookieBehavior : IEndpointBehavior
    {
        private readonly CookieContainer _cookieCont;

        public CookieBehavior(CookieContainer cookieCont)
        {
            _cookieCont = cookieCont;
        }

        public void AddBindingParameters(ServiceEndpoint serviceEndpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, System.ServiceModel.Dispatcher.ClientRuntime behavior)
        {
            behavior.ClientMessageInspectors.Add(new CookieMessageInspector(_cookieCont));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher) { }

        public void Validate(ServiceEndpoint serviceEndpoint) { }
    }
}
