using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTCC.Models;

namespace WebAppTCC.Controllers
{
    public class PessoaController : Controller
    {
        modelagemtccEntities bd = new modelagemtccEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroPessoa()
        {
            return View(bd.turma.ToList());
        }

        //CADASTRO PESSOA
        [HttpPost]
        public ActionResult CadastroPessoa(string nome, string sobrenome, long cpf, string tipo, string email, long telefone, string status, int matriculaSesi, int matriculaSenai, int turma, cargo c, string escola, string senha)
        {
            pessoa p = new pessoa();

            p.nome = nome;
            p.sobrenome = sobrenome;
            p.cpf = cpf;
            p.tipopessoa = tipo;
            p.email = email;
            p.telefone = telefone;
            p.status = status;
            p.senha = senha;
            bd.pessoa.Add(p);
            if(p.tipopessoa.Equals("A"))
            {
                aluno a = CadastroPessoaAlu(p, matriculaSesi, matriculaSenai, turma) ;

                bd.aluno.Add(a);
                bd.SaveChanges();
            }else if (p.tipopessoa.Equals("F"))
            {
                funcionario f = CadastroPessoaFunc(p, c, escola);

                bd.funcionario.Add(f);
                bd.SaveChanges();
            }
            return View();
        }

        //CADASTRO ALUNO
        public aluno CadastroPessoaAlu(pessoa p, int matriculaSesi, int matriculaSenai, int turma)
        {
            aluno alu = new aluno();

            alu.Pessoa_idPessoa = p.idPessoa;
            alu.Turma_idTurma = turma;
            alu.matsesi = matriculaSesi;
            alu.matsenai = matriculaSenai;

            return(alu);
        }

        //CADASTRO FUNCIONÁRIO
        public funcionario CadastroPessoaFunc(pessoa p,cargo c, string escola)
        {
            funcionario fun = new funcionario();

            fun.Pessoa_idPessoa = p.idPessoa;
            fun.cargo = c;
            fun.instituicao = escola;

            return(fun);
        }
    }
}