using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientInfoPlugin : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfoPlugin()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body>");

      //Keep client IP[0] and port[1] p.s.RemoteEndPoint show IP:port
      String[] clientIP_Port = request.getPropertyByKey("RemoteEndPoint").Split(":"); 
      //Show client's info
      sb.Append("<b>" + "Client IP: "           + "</b>" + clientIP_Port[0] + "<br> <br>");
      sb.Append("<b>" + "Client Port: "         + "</b>" + clientIP_Port[1] + "<br> <br>");
      sb.Append("<b>" + "Browser Information: " + "</b>" + request.getPropertyByKey("user-agent")      + "<br> <br>");      
      sb.Append("<b>" + "Accept Language: "     + "</b>" + request.getPropertyByKey("accept-language") + "<br> <br>");
      sb.Append("<b>" + "Accept Encoding: "     + "</b>" + request.getPropertyByKey("accept-encoding") + "<br> <br>");

      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}