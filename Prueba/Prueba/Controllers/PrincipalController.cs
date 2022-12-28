using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Prueba.Models;
using System.Data;
using System.Data.SqlClient;

namespace Prueba.Controllers
{
    public class PrincipalController : Controller
    {
        private static string conexion = "Data Source=DESKTOP-RK6FSOP\\SQLEXPRESS;Initial Catalog=pruebadb;Integrated Security=true";
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }

        private static string Encriptar(string cadena) {
            StringBuilder ConstructorS = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create()) {
                Encoding encUtf8 = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encUtf8.GetBytes(cadena));
                foreach (byte elemento in result) {
                    ConstructorS.Append(elemento.ToString("x2"));
                }
            }
            return ConstructorS.ToString();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usuarioAux)
        {
            bool se_registro;
            string message;

            if (usuarioAux.pass != null && usuarioAux.usuario != null)
            {
                usuarioAux.pass = Encriptar(usuarioAux.pass);
            }
            else {
                ViewData["Mensaje"] = "Los campos de usuario o contraseña estan vacios";
                return View();
            }
            using (SqlConnection con = new SqlConnection(conexion)) {
                SqlCommand comando = new SqlCommand("sp_RegistroUsuario", con);
                comando.Parameters.AddWithValue("usuario",usuarioAux.usuario);
                comando.Parameters.AddWithValue("pass",usuarioAux.pass);
                comando.Parameters.Add("success", SqlDbType.Bit).Direction = ParameterDirection.Output;
                comando.Parameters.Add("message", SqlDbType.VarChar,250).Direction = ParameterDirection.Output;
                comando.CommandType = CommandType.StoredProcedure;
                con.Open();
                comando.ExecuteNonQuery();
                se_registro = Convert.ToBoolean(comando.Parameters["success"].Value);
                message = comando.Parameters["message"].Value.ToString();
            }
            ViewData["Mensaje"] = message;
            if (se_registro == true)
            {
                return RedirectToAction("Login", "Principal");
            }
            else {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(Usuario usuarioAux)
        {
            //Encriptamos la clave
            usuarioAux.pass = Encriptar(usuarioAux.pass);
            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand comando = new SqlCommand("ValidacionUsuario", con);
                comando.Parameters.AddWithValue("usuario", usuarioAux.usuario);
                comando.Parameters.AddWithValue("pass", usuarioAux.pass);
                comando.CommandType = CommandType.StoredProcedure;
                con.Open();
                usuarioAux.id_usuario = Convert.ToInt32(comando.ExecuteScalar().ToString());
            }
            if (usuarioAux.id_usuario != 0)
            {
                Session["usuario"] = usuarioAux;
                return RedirectToAction("Index", "Encuestas");
            }
            else {
                ViewData["Mensaje"] = "Credenciales incorrectas o no existe el usuario";
                return View();
            }
            
        }
    }
}