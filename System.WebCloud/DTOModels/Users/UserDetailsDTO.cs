﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users
{
    public class UserDetailsDTO
    {
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
