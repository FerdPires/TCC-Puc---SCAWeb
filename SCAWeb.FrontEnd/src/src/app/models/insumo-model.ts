export class InsumoModel {
    // public Parametros: any;
    public Registros: any;
    // public listaStatus: Array<any>;

    constructor() {
        //  this.Parametros = this.LimpaParametro();
        this.Registros = this.LimpaRegistros();

        //   this.listaStatus = [
        //       { Value: '', Text: ''},
        //       { Value: '0', Text: 'CONCLUÍDO'},
        //       { Value: '1', Text: 'EM ANALISE'}
        //   ];

    }

    // moment(new Date()).format("DD/MM/YYYY 00:00"),
    // moment(new Date()).format("DD/MM/YYYY 23:59"),

    // LimpaParametro() {
    //     return {
    //         descricao_insumo: '',
    //         status_insumo: null,
    //         data_aquisicao: '',
    //         data_atualizacao: '',
    //         qtd_dias_manut_prev: null,
    //         id_tipo_insumo: '',
    //         id_fornec_insumo: ''
    //     }
    // }

    LimpaRegistros() {
        return {
            Id: '',
            descricao_insumo: '',
            status_insumo: null,
            data_aquisicao: '',
            data_atualizacao: '',
            qtd_dias_manut_prev: null,
            id_tipo_insumo: '',
            id_fornec_insumo: ''
        }
    }
}
