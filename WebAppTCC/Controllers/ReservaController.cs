using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTCC.Classes;
using WebAppTCC.Models;

namespace WebAppTCC.Controllers
{
    public class ReservaController : Controller
    {
        modelagemtcc_Entities bd = new modelagemtcc_Entities();
        // GET: Reserva
        [HttpGet]
        public ActionResult CadastroReserva()
        {
            if(Session["loginFuncionario"] != null)
            {
                FuncionarioReserva funcRe = new FuncionarioReserva();

                funcRe.funcionarioDados = (funcionario)Session["loginFuncionario"];
                funcRe.turmas = bd.turma.ToList();
                funcRe.cardapios = bd.cardapio.ToList();

                return View(funcRe);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult CadastroReserva(int funcionario, int cardapio, int turma, string status, string descricaoReserva)
        {
            string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            reserva reser = new reserva();

            reser.Funcionario_Pessoa_idPessoa = funcionario;
            reser.idcardapio = cardapio;
            reser.idTurma = turma;
            reser.DTCadReserva = data;
            reser.DescricaoReserva = descricaoReserva;
            reser.StatusReserva = status;

            bd.reserva.Add(reser);
            bd.SaveChanges();

            return RedirectToAction("IndexFuncionario", "Funcionario");
        }

        [HttpGet]
        public ActionResult ListarReserva()
        {
            if (Session["loginFuncionario"] != null)
            {
                FuncionarioReserva funcRe = new FuncionarioReserva();

                funcRe.funcionarioDados = (funcionario)Session["loginFuncionario"];
                funcRe.reservas = bd.reserva.ToList();

                return View(funcRe);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult ListarReservaAluno()
        {
            if (Session["loginAluno"] != null)
            {
                AlunoReserva aluRe = new AlunoReserva();

                aluRe.alunoDados = (aluno)Session["loginAluno"];
                aluRe.reservas = bd.reserva.ToList();
                //aluRe.alunoConfirma = bd.confirmareserva.ToList().Find(x => x.Aluno_Pessoa_idPessoa == aluRe.alunoDados.Pessoa_idPessoa);
                aluRe.confirRe = bd.confirmareserva.ToList();

                return View(aluRe);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult DetalhesReserva(int? id)
        {
            if (Session["loginFuncionario"] != null)
            {
                ConfirmacaoReserva confirma = new ConfirmacaoReserva();

                confirma.reservaDados = bd.reserva.ToList().Find(x => x.idReserva == id);
                confirma.confirmareservas = bd.confirmareserva.ToList();

                return View(confirma);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult DetalhesReservaAluno(int? id)
        {
            if (Session["loginAluno"] != null)
            {
                reserva r = bd.reserva.ToList().Find(x => x.idReserva == id);

                return View(r);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult ConfirmaReserva(int id)
        {
            if (Session["loginAluno"] != null)
            {
                string dt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                aluno a = (aluno)Session["loginAluno"];

                confirmareserva confirRe = new confirmareserva();

                confirRe.Aluno_Pessoa_idPessoa = a.Pessoa_idPessoa;
                confirRe.idReserva = id;
                confirRe.DTConfReserva = dt;
                confirRe.ConfPresente = "N";

                bd.confirmareserva.Add(confirRe);
                bd.SaveChanges();

                AlunoReserva aluRe = new AlunoReserva();

                aluRe.alunoDados = a;
                aluRe.reservas = bd.reserva.ToList();
                aluRe.confirRe = bd.confirmareserva.ToList();

                return View("ListarReservaAluno", aluRe);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult ConfirmarAluno(int? id)
        {
            if (Session["loginFuncionario"] != null)
            {                
                confirmareserva confirAluno = bd.confirmareserva.ToList().Find(x => x.idConfirmaReserva == id);

                confirAluno.ConfPresente = "S";

                bd.SaveChanges();

                ConfirmacaoReserva confirma = new ConfirmacaoReserva();

                confirma.reservaDados = bd.reserva.ToList().Find(x => x.idReserva == confirAluno.idReserva);
                confirma.confirmareservas = bd.confirmareserva.ToList();

                return View("DetalhesReserva", confirma);
            }

            return RedirectToAction("Login", "Home");
        }

        

        [HttpGet]
        public ActionResult HistoricoReserva()
        {
            if (Session["loginFuncionario"] != null)
            {
                return View(bd.reserva.ToList());
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult AlunosReserva(int? id)
        {
            if (Session["loginFuncionario"] != null)
            {
                ConfirmacaoReserva confirma = new ConfirmacaoReserva();

                confirma.reservaDados = bd.reserva.ToList().Find(x => x.idReserva == id);
                confirma.confirmareservas = bd.confirmareserva.ToList();
                confirma.avaliacaoAluno = bd.avaliacao.ToList();
                

                return View(confirma);
            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult AvaliacaoAlunoReserva(int? id)
        {
            if (Session["loginFuncionario"] != null)
            {
                avaliacao ava = bd.avaliacao.ToList().Find(x => x.idAvaliacao == id);

                return View(ava);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult FecharReserva(int? id)
        {
            if(Session["loginFuncionario"] != null)
            {
                reserva reser = bd.reserva.ToList().Find(x => x.idReserva == id);

                reser.StatusReserva = "I";

                bd.SaveChanges();

                FuncionarioReserva funcRe = new FuncionarioReserva();

                funcRe.funcionarioDados = (funcionario)Session["loginFuncionario"];
                funcRe.reservas = bd.reserva.ToList();

                return View("ListarReserva", funcRe);
            }

            return RedirectToAction("Login", "Home");
        }
    }
}