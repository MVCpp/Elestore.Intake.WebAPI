using System;
using System.Net.Mail;
using Elestor.Intake.API.Interfaces;

namespace Elestor.Intake.API.Helpers
{
    public class CorreoConfirmacion : ICorreoConfirmacion
    {
        private readonly ICorreoConfirmacion _correoConfirmacion;
        public  CorreoConfirmacion(ICorreoConfirmacion correoConfirmacion)
        {
            _correoConfirmacion = correoConfirmacion ?? throw new ArgumentNullException(nameof(correoConfirmacion), "Cannot be null.");
        }

        public bool EnviarCorreo(string correo)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("Elestormexico@gmail.com", "n0m3l0s3"),
                EnableSsl = true
            };
            client.Send("Elestormexico@gmail.com", "miguel.cepepe@hotmail.com", "Correo de Confirmacion", "Su Cuenta ha sido creada" +
            	"exitosamente!");
            Console.WriteLine("Sent");
            Console.ReadLine();


            return true;
        }
    }
}
