using System;
using System.Collections.Generic;
using System.Text;

namespace DPA.SHOPPING.CORE.Core.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }

    public class CategoryListDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class CategoryCreateDTO
    {
        public string Description { get; set; }        
    }


}
