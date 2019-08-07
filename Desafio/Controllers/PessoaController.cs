using Desafio.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System;

namespace DesafioIATec.Controllers
{
    public class PessoaController : Controller
    {
        /// <summary>
        /// Códigos de erro 
        /// 
        /// 1: Sucesso
        /// 2: Erro
        /// 3: Outros
        /// 
        /// Mensagens tratadas nas Views
        /// 
        /// </summary>

        DESAFIO_IATECEntities1 bd = new DESAFIO_IATECEntities1();

        string Mensagem = "";

        [HttpPost]
        // Para aceitar o código html vindo do campo Descrição
        [ValidateInput(false)]
        public ActionResult AcaoPessoa(Pessoa pessoa, string acao, string retorno)
        {
            if (acao == "Adicionar")
            {
                Mensagem = CriarPessoa(pessoa);
                if (Mensagem == "erro")
                    return RedirectToAction("ListarPessoa", "Pessoa", new { Mensagem }); ;
            }
            else if (acao == "Editar")
                Mensagem = EditarPessoa(pessoa);
            else
                Mensagem = RemoverPessoa(pessoa);
            return RedirectToAction("DetalhePessoa", "Pessoa", new { cpf = pessoa.CPF, Mensagem }); 
        }

        [ValidateInput(false)]
        public ActionResult ListarPessoa(string Mensagem)
        {
            ViewBag.Mensagem = Mensagem;
            ViewBag.Lista = bd.Pessoa.ToList();
            return View();
        }

        public ActionResult DetalhePessoa(string cpf, string Mensagem)
        {
            ViewBag.Mensagem = Mensagem;
            var pessoa = bd.Pessoa.FirstOrDefault(x => x.CPF == cpf);
            ViewBag.Descricao = pessoa.Descricao;
            return View(pessoa);
        }
        //public ActionResult CriarPessoa(int? erro)
        //{
        //    ViewBag.erro = erro;            
        //    return View();
        //}

        [HttpPost]
        // Para aceitar o código html vindo do campo Descrição
        [ValidateInput(false)]
        public string CriarPessoa(Pessoa pessoa)
        {
            if (bd.Pessoa.FirstOrDefault(x => x.CPF == pessoa.CPF || x.Email == pessoa.Email) != null) return "Erro";
            pessoa.AcessoId = 2;
            bd.Pessoa.Add(pessoa);
            bd.SaveChanges();
            return "Pessoa";
        }

        [HttpPost]
        [ValidateInput(false)]
        public string EditarPessoa(Pessoa pessoa)
        {
            var p = bd.Pessoa.FirstOrDefault(x => x.CPF == pessoa.CPF);
            p.Descricao = pessoa.Descricao;
            p.Email = pessoa.Email;
            p.Nome = pessoa.Nome;
            bd.Entry(p).State = System.Data.Entity.EntityState.Modified;
            bd.SaveChanges();
            return "Edição";
        }

        [HttpPost]
        public string RemoverPessoa(Pessoa pessoa)
        {
            var p = bd.Pessoa.FirstOrDefault(x => x.CPF == pessoa.CPF);
            bd.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            bd.SaveChanges();
            return "Remoção";
        }

        [HttpPost]
        public ActionResult AcaoNumero(Telefone telefone, string retorno, string acao)
        {
            if (acao == "Adicionar")
                Mensagem = AdicionarNumero(telefone);
            else if (acao == "Editar")
                Mensagem = EditarNumero(telefone);
            else
                Mensagem = RemoverNumero(telefone);
            if (retorno == "Lista")
            {
                return RedirectToAction("ListarPessoa", "Pessoa", new { Mensagem });
            }
            return RedirectToAction("DetalhePessoa", "Pessoa", new { cpf = telefone.CPF, Mensagem });
        }

        [HttpPost]
        public string AdicionarNumero(Telefone telefone)
        {
            if (bd.Telefone.FirstOrDefault(x => x.Numero == telefone.Numero && x.CPF == telefone.CPF) != null) return "erro";
            bd.Telefone.Add(telefone);
            bd.SaveChanges();
            return "Adição";
        }

        public string EditarNumero(Telefone telefone)
        {
            var tel = bd.Telefone.FirstOrDefault(x => x.TelefoneId == telefone.TelefoneId);
            tel.Descricao = telefone.Descricao;
            tel.Numero = telefone.Numero;
            bd.Entry(tel).State = System.Data.Entity.EntityState.Modified;
            bd.SaveChanges();
            return "Edição";
        }

        [HttpPost]
        public string RemoverNumero(Telefone telefone)
        {
            bd.Entry(bd.Telefone.FirstOrDefault(x => x.TelefoneId == telefone.TelefoneId)).State = System.Data.Entity.EntityState.Deleted;
            bd.SaveChanges();
            return "Remoção";
        }

        public ActionResult AcaoEndereco(Endereco endereco, string retorno, string acao)
        {
            if (acao == "Adicionar")
                Mensagem = AdicionarEndereco(endereco);
            else if (acao == "Editar")
                Mensagem = EditarEndereco(endereco);
            else
                Mensagem = RemoverEndereco(endereco);
            if (retorno == "Lista")
            {
                return RedirectToAction("ListarPessoa", "Pessoa", new { Mensagem });
            }
            return RedirectToAction("DetalhePessoa", "Pessoa", new { cpf = endereco.CPF, Mensagem });
        }

        [HttpPost]
        public string AdicionarEndereco(Endereco endereco)
        {
            bd.Endereco.Add(endereco);
            bd.SaveChanges();
            return "Adição";
        }

        public string EditarEndereco(Endereco endereco)
        {
            var end = bd.Endereco.FirstOrDefault(x => x.EnderecoId == endereco.EnderecoId);
            end.Pais = endereco.Pais;
            end.Estado = endereco.Estado;
            end.CEP = endereco.CEP;
            end.Cidade = endereco.Cidade;
            end.Bairro = endereco.Bairro;
            end.Rua = endereco.Rua;
            end.Numero = endereco.Numero;
            end.Descricao = endereco.Descricao;
            end.Complemento = endereco.Complemento;
            bd.Entry(end).State = System.Data.Entity.EntityState.Modified;
            bd.SaveChanges();
            return "Edição";
        }
        public string RemoverEndereco(Endereco endereco)
        {
            var end = bd.Endereco.FirstOrDefault(x => x.EnderecoId == endereco.EnderecoId);
            var cpf = end.CPF;
            bd.Entry(end).State = System.Data.Entity.EntityState.Deleted;
            bd.SaveChanges();
            return "Remoção";
        }

    }
}