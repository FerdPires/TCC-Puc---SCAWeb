using SCAWeb.Service.Monitoramento.Util.Enums;
using System;

namespace SCAWeb.Service.Monitoramento.Entities
{
    public class AlertaSensorEntity : Entity
    {
        public AlertaSensorEntity()
        {

        }

        public AlertaSensorEntity(TipoAlerta tipoAletra, Guid idSensor, DateTime dataAtualizacao, string User)
        {
            tipo_aletra = tipoAletra;
            id_sensor = idSensor;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public TipoAlerta tipo_aletra { get; set; }
        public DateTime data_atualizacao { get; set; }
        public Guid id_sensor { get; set; }
        public string user { get; set; }
    }
}
