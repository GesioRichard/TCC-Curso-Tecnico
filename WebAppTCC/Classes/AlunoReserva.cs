using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppTCC.Models;

namespace WebAppTCC.Classes
{
    public class AlunoReserva
    {
        public aluno alunoDados { get; set; }
        public List<Models.reserva> reservas { get; set; }
        public confirmareserva alunoConfirma { get; set; }
        public List<Models.confirmareserva> confirRe { get; set; }
        public List<Models.avaliacao> listaAvaAluno { get; set; }
        public avaliacao avaAluno { get; set; }
    }
}