using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTCC.Models;

namespace WebAppTCC.Controllers
{
    public class CardapioController : Controller
    {
        modelagemtcc_Entities bd = new modelagemtcc_Entities();
        // GET: Cardapio
        [HttpGet]
        public ActionResult DetalhesCardapio(int? id)
        {
            if (Session["loginFuncionario"] != null)
            {
                reserva reser = bd.reserva.ToList().Find(x => x.idReserva == id);

                return View(reser);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult DetalhesCardapioAluno(int? id)
        {
            if (Session["loginAluno"] != null)
            {
                reserva reser = bd.reserva.ToList().Find(x => x.idReserva == id);

                return View(reser);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult AvaliarCardapio(int? id)
        {
            if (Session["loginAluno"] != null)
            {
                confirmareserva confir = bd.confirmareserva.ToList().Find(x => x.idConfirmaReserva == id);

                return View(confir);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AvaliarCardapio(int alunoID, int confiReservaID, string nota, string observacao)
        {
            if (Session["loginAluno"] != null)
            {
                avaliacao ava = new avaliacao();

                ava.ConfirmaReserva_Aluno_Pessoa_idPessoa = alunoID;
                ava.ConfirmaReserva_idConfirmaReserva = confiReservaID;
                ava.Nota = Int32.Parse(nota);
                ava.Observacao = observacao;

                bd.avaliacao.Add(ava);
                bd.SaveChanges();

                return RedirectToAction("IndexAluno", "Aluno");
            }
            return RedirectToAction("Login", "Home");
        }
    }
}