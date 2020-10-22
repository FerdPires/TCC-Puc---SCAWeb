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

        public TipoAlerta tipo_aletra { get; private set; }
        public DateTime data_atualizacao { get; private set; }
        public Guid id_sensor { get; private set; }
        public string user { get; private set; }
    }
}
