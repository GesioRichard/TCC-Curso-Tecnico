using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppTCC.Models;

namespace WebAppTCC.Classes
{
    public class FuncionarioReserva
    {
        public funcionario funcionarioDados { get; set; }

        public List<Models.reserva> reservas { get; set; }
        public List<Models.turma> turmas { get; set; }
        public List<Models.cardapio> cardapios { get; set;}
    }
}