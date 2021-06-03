using System;

namespace Rathor.Models
{
    internal class MyDateAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}