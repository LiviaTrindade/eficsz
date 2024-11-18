using Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;

namespace Core.Models.DTO
{
    public class UsuarioDto
    {
    
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime Nascimento { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Foto { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }

        public UsuarioDto() { }

        public UsuarioDto(string nome, string sobrenome, string cPF, string telefone, string genero, DateTime dataNascimento, string nacionalidade, string email, string senha, List<EnderecoDto> enderecos)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Telefone = telefone;
            Genero = genero;
            DataNascimento = dataNascimento;
            Nacionalidade = nacionalidade;
            Email = email;
            Senha = senha;
            Enderecos = enderecos;
        }
    }
}
