using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_Estoque.Application.Interface
{
    public interface IEstoqueService
    {
        Task<string> VerificarEstoqueAsync(string mensagem);
    }
}