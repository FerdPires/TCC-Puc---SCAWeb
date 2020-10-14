using Flunt.Validations;
using System;

namespace SCAWeb.Service.Ativos.Entities
{
    public class FornecedorEntity : Entity, IValidatable
    {
        public FornecedorEntity()
        {

        }

        public FornecedorEntity(int cnpjFornecedor, string nomeFantasia, string razaoSocial, DateTime dataAtualizacao, string User)
        {
            cnpj_fornecedor = cnpjFornecedor;
            nome_fantasia = nomeFantasia;
            razao_social = razaoSocial;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public int cnpj_fornecedor { get; private set; }
        public string nome_fantasia { get; private set; }
        public string razao_social { get; private set; }
        public DateTime data_atualizacao { get; private set; }
        public string user { get; private set; }

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
                    .IsNotNullOrEmpty(nome_fantasia, "Nome Fantasia", "Favor informar o Nome Fantasia.")
                    .IsNotNullOrEmpty(razao_social, "Razão social", "Favor informar a Razão social.")
            );
        }
    }
}
