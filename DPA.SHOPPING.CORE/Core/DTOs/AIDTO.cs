using System;
using System.Collections.Generic;
using System.Text;

namespace DPA.SHOPPING.CORE.Core.DTOs
{
    public class ChatRequestDTO
    {
        public string Message { get; set; }
    }
    public class  ChatResponseDTO
    {
        public string Answer { get; set; }
        public string Model { get; set; }
    }
}
