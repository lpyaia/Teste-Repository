using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Job
{
    public abstract class BaseJob
    {
        delegate bool DoAction(string s);

        private Timer _timer;
        private string _nome;

        /// <summary>
        /// Construtor base para os jobs da integração
        /// </summary>
        /// <param name="intervalo">Intervalo (minutos) de execução do Job</param>
        /// <param name="nome">Nome do Job criado</param>
        public BaseJob(int intervalo, string nome)
        {
            _timer = new Timer(intervalo * 60 * 1000);
            _timer.Elapsed += OnElapsed;
            _timer.Enabled = true;

            _nome = nome;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            Action();
        }

        public abstract void Action();

    }
}
