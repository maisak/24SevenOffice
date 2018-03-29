using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace TFSO.Core.Toolbox
{
    public class CookieMessageInspector : IClientMessageInspector
    {
        private readonly CookieContainer _cookieCont;

        public CookieMessageInspector(CookieContainer cookieCont)
        {
            _cookieCont = cookieCont;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            object obj;
            if (reply.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
            {
                var httpResponseMsg = obj as HttpResponseMessageProperty;
                if (!string.IsNullOrEmpty(httpResponseMsg?.Headers["Set-Cookie"]))
                {
                    _cookieCont.SetCookies((Uri)correlationState, httpResponseMsg.Headers["Set-Cookie"]);
                }
            }
        }

        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            object obj;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
            {
                var httpRequestMsg = obj as HttpRequestMessageProperty;
                SetRequestCookies(channel, httpRequestMsg);
            }
            else
            {
                var httpRequestMsg = new HttpRequestMessageProperty();
                SetRequestCookies(channel, httpRequestMsg);
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMsg);
            }

            return channel.RemoteAddress.Uri;
        }

        private void SetRequestCookies(System.ServiceModel.IClientChannel channel, HttpRequestMessageProperty httpRequestMessage)
        {
            httpRequestMessage.Headers["Cookie"] = _cookieCont.GetCookieHeader(channel.RemoteAddress.Uri);
        }
    }
}
