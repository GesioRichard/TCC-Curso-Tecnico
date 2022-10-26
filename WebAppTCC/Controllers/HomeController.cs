using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTCC.Models;

namespace WebAppTCC.Controllers
{
    public class HomeController : Controller
    {
        modelagemtcc_Entities bd = new modelagemtcc_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(long cpf, string senha)
        {
            
            foreach (var item in bd.pessoa.ToList())
            {
                if ((item.CPF == cpf) && (item.Senha == senha) && (item.TipoPessoa == "A"))
                {
                    aluno a = bd.aluno.ToList().Find(x => x.pessoa.CPF == cpf);

                    Session["loginAluno"] = a;
                    return RedirectToAction("IndexAluno", "Aluno");
                }else if ((item.CPF == cpf) && (item.Senha == senha) && (item.TipoPessoa == "F"))
                {
                    funcionario f = bd.funcionario.ToList().Find(x => x.pessoa.CPF == cpf);
                    Session["loginFuncionario"] = f;
                    return RedirectToAction("IndexFuncionario", "Funcionario");
                }
            }
            
            ViewBag.ErrorLogin = "CPF ou senha invalido";
            return View();
        }
    }
}