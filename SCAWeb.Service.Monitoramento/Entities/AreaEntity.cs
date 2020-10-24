using System;

namespace SCAWeb.Service.Monitoramento.Entities
{
    public class AreaEntity : Entity
    {
        public AreaEntity()
        {

        }

        public AreaEntity(string nomeBarragem, DateTime dataAtualizacao, string User)
        {
            nome_barragem = nomeBarragem;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public string nome_barragem { get; set; }
        public DateTime data_atualizacao { get; set; }
        public string user { get; set; }
    }
}
