using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTCC.Models;

namespace WebAppTCC.Controllers
{
    public class FuncionarioController : Controller
    {
        modelagemtcc_Entities bd = new modelagemtcc_Entities();
        // GET: Funcionario
        public ActionResult IndexFuncionario()
        {
            if(Session["loginFuncionario"] != null)
            {
                return View(Session["loginFuncionario"]);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult CadastroFuncionario()
        {
            return View(bd.cargo.ToList());
        }

        public ActionResult CadastroFuncionario(string nome, string sobrenome, long cpf, string tipo, string email, long telefone, string status, int cargo, string instituicao, string senha)
        {
            pessoa p = new pessoa();

            p.Nome = nome;
            p.Sobrenome = sobrenome;
            p.CPF = cpf;
            p.TipoPessoa = tipo;
            p.Email = email;
            p.Telefone = telefone;
            p.Status = status;
            p.Senha = senha;

            bd.pessoa.Add(p);
            bd.SaveChanges();

            funcionario fun = new funcionario();

            fun.Pessoa_idPessoa = p.idPessoa;
            fun.idCargo = cargo;
            fun.Instituicao = instituicao;

            bd.funcionario.Add(fun);
            bd.SaveChanges();

            return View(bd.cargo.ToList());
        }
        public ActionResult PerfilFuncionario(int? id)
        {
            if (Session["loginFuncionario"] != null)
            {
                funcionario func = bd.funcionario.ToList().Find(x => x.Pessoa_idPessoa == id);

                return View(func);
            }
            return RedirectToAction("Login", "Home");
        }
    }
}