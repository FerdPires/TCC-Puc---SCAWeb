using SCAWeb.Service.Monitoramento.Util.Enums;
using System;

namespace SCAWeb.Service.Monitoramento.Entities
{
    public class AlertaSensorEntity
    {
        public AlertaSensorEntity()
        {

        }

        public AlertaSensorEntity(TipoAlerta tipoAletra, string descricaoAlerta, Guid idSensor, DateTime dataAtualizacao, string User)
        {
            Id = Guid.NewGuid();
            tipo_aletra = tipoAletra;
            descricao_alerta = descricaoAlerta;
            id_sensor = idSensor;
            data_atualizacao = dataAtualizacao;
        }

        public Guid Id { get; set; }
        public TipoAlerta tipo_aletra { get; set; }
        public string descricao_alerta { get; set; }
        public DateTime data_atualizacao { get; set; }
        public Guid id_sensor { get; set; }
    }
}
