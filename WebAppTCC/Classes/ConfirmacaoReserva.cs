using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppTCC.Models;

namespace WebAppTCC.Classes
{
    public class ConfirmacaoReserva
    {
        public reserva reservaDados { get; set; }
        public List<Models.confirmareserva> confirmareservas { get; set; }

        public List<Models.avaliacao> avaliacaoAluno { get; set; }

        public avaliacao avaliacaoAlunoIndividual { get; set; }
    }
}