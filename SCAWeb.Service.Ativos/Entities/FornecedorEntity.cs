using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace SCAWeb.Service.Ativos.Entities
{
    public class FornecedorEntity : Notifiable, IValidatable
    {
        public FornecedorEntity()
        {

        }

        public FornecedorEntity(string cnpjFornecedor, string nomeFantasia, string razaoSocial, DateTime dataAtualizacao, string User)
        {
            Id = Guid.NewGuid();
            cnpj_fornecedor = cnpjFornecedor;
            nome_fantasia = nomeFantasia;
            razao_social = razaoSocial;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public Guid Id { get; set; }
        public string cnpj_fornecedor { get; set; }
        public string nome_fantasia { get; set; }
        public string razao_social { get; set; }
        public DateTime data_atualizacao { get; set; }
        public string user { get; set; }

        public void UpdateFornecedor(string nomeFantasia, string razaoSocial, DateTime dataAtualizacao, string User)
        {
            nome_fantasia = nomeFantasia;
            razao_social = razaoSocial;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(cnpj_fornecedor, "Cnpj", "Favor informar o Cnpj do fornecedor.")
                    .IsNotNullOrEmpty(nome_fantasia, "Nome Fantasia", "Favor informar o Nome Fantasia.")
                    .IsNotNullOrEmpty(razao_social, "Razão social", "Favor informar a Razão social.")
            );
        }
    }
}
