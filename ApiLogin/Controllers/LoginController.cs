using ApiLogin.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Principal;

namespace ApiLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet]
        public ActionResult<OkObjectResult> okuser()
        {

            var identityW = WindowsIdentity.GetCurrent();
            //var identity = Thread.CurrentPrincipal.Identity;

            return new ActionResult<OkObjectResult>(Ok($" IPrincipal-user: {identityW.Name} - IsAuthenticated: {identityW.IsAuthenticated}"));
            //return new ActionResult<OkObjectResult>(Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}"));
        }


        [HttpPost]
        [Route("authenticate")]
        public ActionResult Authenticate(Login login)
        {

            try
            {
                if (login == null)
                    return new JsonResult(JsonConvert.SerializeObject(new ApiResponse(400, login)));

                Login isCredentialValid = CredentialValidate(login);
                if (isCredentialValid != null && !isCredentialValid.Stok)
                {
                    int controlacces = numIntentos(login.Usser, 0);
                    var token = TokenGen.GenerateTokenJwt(login.Usser);
                    var result = JsonConvert.SerializeObject(new ApiResponse(200, login, token));
                    return new JsonResult(result);
                }
                else if (isCredentialValid != null)
                {
                    return StatusCode(401, $"usuario bloquado {login.Usser}");
                }
                else
                {
                    int controlacces = numIntentos(login.Usser, 1);

                    var result = JsonConvert.SerializeObject(new ApiResponse(300, login));
                    return new JsonResult(result);
                }
            }
            catch (Exception ee)
            {

                var result = JsonConvert.SerializeObject(new ApiResponse(500, login));
                return new JsonResult(result); ;
            }
        }
        private Login CredentialValidate(Login login)
        {

            Login usuario = null;

            using (var db = new LoginContext())
            {
                var _user = db.Access.Where(w => w.Usser == login.Usser && w.Pass == login.Pass).FirstOrDefault();

                if (_user != null)
                    usuario = _user;
            }



            return usuario;
        }

        private int numIntentos(string user, int _intento)
        {
            int intento = _intento;
            using (var db = new LoginContext())
            {
                var _user = db.AccessControl.Where(w => w.USSER == user).Select(s => s).FirstOrDefault();

                if (_user != null)
                    intento = _user.COUNTACCESS + 1;
                db.AccessControl.Add(new Control
                {
                    USSER = user,
                    COUNTACCESS = intento

                });


                var count = db.SaveChanges();
            }
            return intento;
        }
        private bool insertUser()
        {
            bool isInsert = false;
            using (var db = new LoginContext())
            {
                var _idConse = db.Access.OrderByDescending(or => or.Id).Select(s => s.Id).FirstOrDefault();
                _idConse = _idConse + 1;
                db.Access.Add(new Login
                {
                    Id = _idConse,
                    Pass = 12345,
                    Usser = "jrmorales1@stefanini.com",
                    Stok = true
                });

                var count = db.SaveChanges();

            }
            return true;

        }
    }
}