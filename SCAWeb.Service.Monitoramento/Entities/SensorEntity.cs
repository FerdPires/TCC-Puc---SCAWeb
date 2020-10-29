using SCAWeb.Service.Monitoramento.Util.Enums;
using System;

namespace SCAWeb.Service.Monitoramento.Entities
{
    public class SensorEntity
    {
        public SensorEntity()
        {

        }

        public SensorEntity(string nomeSensor, Status statusSensor, Guid idArea, DateTime dataAtualizacao, string User)
        {
            Id = Guid.NewGuid();
            nome_sensor = nomeSensor;
            status_sensor = statusSensor;
            id_area = idArea;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public Guid Id { get; set; }
        public string nome_sensor { get; set; }
        public Status status_sensor { get; set; }
        public DateTime data_atualizacao { get; set; }
        public Guid id_area { get; set; }
        public string user { get; set; }
    }
}
