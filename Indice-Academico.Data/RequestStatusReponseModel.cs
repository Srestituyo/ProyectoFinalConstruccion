using System;
namespace Indice_Academico.Data
{
    public class RequestStatusReponseModel
    {
        public string ErrorMessage { get; set; }

        public int Status { get; set; }

        public enum StatusCode : int
        {
            Ok = 0,
            Error = 1
        }

    }

    
}
