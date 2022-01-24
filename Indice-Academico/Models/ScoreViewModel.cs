 
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Indice_Academico.Models
{
    public class ScoreViewModel
    {
        public List<SelectListItem> Students { get; set; }

        public List<SelectListItem> Teachers { get; set; }

        public List<SelectListItem> Subjects { get; set; }
    }
}
