﻿namespace System.WebCloud.DTOModels.Users
{
    public class CreateUserDTO
    {
        public int idrol { get; set; }
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }

        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string password{ get; set; }

    }
}
