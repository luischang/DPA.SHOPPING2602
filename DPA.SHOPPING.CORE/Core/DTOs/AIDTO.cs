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

    public class MyOrdersRequestDTO
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
    public class MyOrdersResponseDTO
    {
        public string Answer { get; set; }
        public string Model { get; set; }
        public string Context { get; set; }
    }



}
