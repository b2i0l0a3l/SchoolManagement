using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Interfaces
{
    public interface ITokenService
    {
        string Generate(int byteLength);
    }
}