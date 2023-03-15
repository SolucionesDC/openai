using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using openai.Recursos.ChatGPT;
using RestSharp;
using System.Text.Json.Serialization;

namespace openai.Controllers
{
    public class ChatGPTController : Controller
    {
        public static string _EndPoint = "https://api.openai.com/";
        public static string _URI = "v1/chat/completions";
        public static string _APIKey = "YOU_API_KEY";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pSolicitud)
        {
            var strRespuesta = "";

            //Consumir la API
            var oCliente = new RestClient(_EndPoint);
            var oSolicitud = new RestRequest(_URI, Method.Post);
            oSolicitud.AddHeader("Content-Type", "application/json");
            oSolicitud.AddHeader("Authorization", "Bearer "+_APIKey);

            //Creamos el cuerpo de la solicitud
            var oCuerpo = new Request()
            {
                model = "gpt-3.5-turbo",
                messages= new List<Message>()
                {
                    new Message() {
                        role="user", 
                        content=pSolicitud
                    }
                }
            };

            var jsonString = JsonConvert.SerializeObject(oCuerpo);

            oSolicitud.AddJsonBody(jsonString);

            var oRespuesta = oCliente.Post<Response>(oSolicitud);

            strRespuesta = oRespuesta.choices[0].message.content;

            ViewBag.Respuesta= strRespuesta; 

            return View();
        }
    }
}
