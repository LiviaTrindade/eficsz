using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IEnderecoService
    {
        Task<List<EnderecoModels>> RegistrarEnderecos(List<EnderecoModels> novoEnderecos);
    }
}
