using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogin.Model
{
    public class ApiResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
        public String Token { get;  }
        public Login Usser { get;  }

        public ApiResponse(int statusCode,Login usser,  string token = null)
        {
            StatusCode = statusCode;
            Message = GetDefaultMessageForStatusCode(statusCode);
            Token = token;
            Usser = usser;
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "OK";
                case 203:
                    return "Non-Authoritative Information";
                case 300:
                    return "ERROR DE AUTENTICACION";
                case 400:
                    return "Not Found";
                case 404:
                    return "Recurso no Encontrado";
                case 500:
                    return "Ocurrio un error en el servidor";
                default:
                    return null;
            }
        }
    }
}