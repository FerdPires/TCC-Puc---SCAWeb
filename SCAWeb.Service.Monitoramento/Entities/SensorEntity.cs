using SCAWeb.Service.Monitoramento.Util.Enums;
using System;

namespace SCAWeb.Service.Monitoramento.Entities
{
    public class SensorEntity : Entity
    {
        public SensorEntity()
        {

        }

        public SensorEntity(string nomeSensor, Status statusSensor, Guid idSensor, DateTime dataAtualizacao, string User)
        {
            nome_sensor = nomeSensor;
            status_sensor = statusSensor;
            id_sensor = idSensor;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public string nome_sensor { get; private set; }
        public Status status_sensor { get; private set; }
        public DateTime data_atualizacao { get; private set; }
        public Guid id_sensor { get; private set; }
        public string user { get; private set; }
    }
}
