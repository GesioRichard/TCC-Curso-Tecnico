using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTCC.Classes;
using WebAppTCC.Models;

namespace WebAppTCC.Controllers
{
    public class AlunoController : Controller
    {
        modelagemtcc_Entities bd = new modelagemtcc_Entities();

        public ActionResult IndexAluno()
        {
            if (Session["loginAluno"] != null)
            {
                return View(Session["loginAluno"] );
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult CadastroAluno()
        {
            return View(bd.turma.ToList());
        }

        [HttpPost]
        public ActionResult CadastroAluno(string nome, string sobrenome, long cpf, string tipo, string email, long telefone, string status, int matriculaSesi, int matriculaSenai, int turma, string senha)
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

            aluno alu = new aluno();

            alu.Pessoa_idPessoa = p.idPessoa;
            alu.idTurma = turma;
            alu.MatSESI = matriculaSesi;
            alu.MatSENAI = matriculaSenai;

            bd.aluno.Add(alu);
            bd.SaveChanges();

            return View(bd.turma.ToList());
        }

        [HttpGet]
        public ActionResult MinhasReservas(int? id)
        {
            if (Session["loginAluno"] != null)
            {
                AlunoReserva aluRe = new AlunoReserva();

                aluRe.confirRe= new List<confirmareserva>();

                foreach (var confirma in bd.confirmareserva)
                {
                    if (confirma.Aluno_Pessoa_idPessoa == id)
                    {
                        aluRe.confirRe.Add(confirma);
                    }

                }

                aluRe.avaAluno = bd.avaliacao.ToList().Find(x => x.ConfirmaReserva_Aluno_Pessoa_idPessoa == id);
                aluRe.listaAvaAluno = bd.avaliacao.ToList();

                return View(aluRe);
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult PerfilAluno(int? id)
        {
            if (Session["loginAluno"] != null)
            {
                aluno a = bd.aluno.ToList().Find(x => x.Pessoa_idPessoa == id);

                return View(a);
            }
            return RedirectToAction("Login", "Home");
        }
      
    }
}

